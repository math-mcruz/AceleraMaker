using Projeto1E2.Contas;
using Projeto1E2.Exceptions;
using Projeto1E2.Repository;
using Projeto1E2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



namespace Projeto1E2.Controller;

public class ContaController: IContaRepository
{
    //instancia das lista de contas, pilar do projeto
    private List<Conta> contas = new List<Conta>();

    public ContaController() //precisa para ler o JSON
    {
        //sempre que programa começar ele ja vai ter os dados
        LerContas();
    }
    public void ProcurarPorNumero(int numero)
    {
        //Procura por cada conta se ela existe e mostra se achar
        foreach (var conta in contas)
        {
            if (conta.Numero == numero)
            {
                Console.Clear();
                Console.WriteLine($"Conta {conta.Numero} encontrada, dados: ");
                Console.WriteLine($"\nTitular da conta: {conta.Titular}");
                Console.WriteLine($"Agência: {conta.Agencia}");
                string tipoString = conta.Tipo == 1 ? "Corrente" : "Poupança";
                Console.WriteLine($"Tipo: {tipoString}");
                return;
            }
            
        }
        Cores.ExibirErro("Conta não encontrada");
    }
    public void ListarTodas()
    {
        //se tiver mais de uma conta ele lista todas
        if(contas.Count != 0)
        {
            foreach(var conta in contas)
            {
                conta.Visualizar();
            }
        }
        else
        {
            Cores.ExibirErro("Não existem contas cadastradas.");
        }
            
    }
    
    public void Cadastrar(Conta conta)
    {
        //se tiver conta com número igual ele não pode criar
        if (BuscarNaCollection(conta.Numero) != null)
        {
            Cores.ExibirErro("Conta já cadastrada.");
        }
        else
        {
            //cria a conta
            contas.Add(conta);
            //grava no arquivo
            GravarContas();
            Cores.ExibirSucesso($"Conta cadastrada com sucesso, numero da conta é: {conta.Numero}");
        }

    }
    public void Atualizar(Conta conta)
    {
        //verifica se existe a conta
        var contaAntiga = BuscarNaCollection(conta.Numero);
        if (contaAntiga == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {conta.Numero} não existe no sistema.");
        }

        Conta contaAtualizada = null;
        bool saidaAtualizar = false;
        //loop infinito para escolher o que quer mudar e depois encerra
        while (!saidaAtualizar)
        {
            //Exibe menu de operações
            Cores.CorMenu();
            byte opcaoOperacao = ExibirMenu.Atualizar();
            Cores.CorOriginal();
            switch (opcaoOperacao)
            {
                case 1:// atualizar titular, Validações(ValidacaoHelper) para garantir os dados certos

                    //precisa ser um nome(string) e sem números ou outros char
                    string? atualizarTitular = ValidacaoHelper.TextoMenu("\nDigite o nome novo do titular:\n");

                    //buscar qual tipo de conta 
                    if (contaAntiga is ContaCorrente cc1)
                    {
                        contaAtualizada = new ContaCorrente(numero: contaAntiga.Numero, agencia: contaAntiga.Agencia, tipo: 1, titular: atualizarTitular, limite: cc1.Limite);
                    }
                    else if (contaAntiga is ContaPoupanca cp1)
                    {
                        contaAtualizada = new ContaPoupanca(numero: contaAntiga.Numero, agencia: contaAntiga.Agencia, tipo: 1, titular: atualizarTitular, aniversario: cp1.Aniversario);

                    }

                    Console.Clear();
                    //quebra o looping
                    saidaAtualizar = true;
                    break;

                case 2: //atualizar agencia, Validações(ValidacaoHelper) para garantir os dados certos

                    //precisa ser um valor positivo
                    int atualizarAgencia = ValidacaoHelper.ValorPositivo("\nDigite o número da nova agência:\n");

                    //buscar qual tipo de conta
                    if (contaAntiga is ContaCorrente cc2)
                    {
                        contaAtualizada = new ContaCorrente(numero: contaAntiga.Numero, agencia: atualizarAgencia, tipo: 1, titular: contaAntiga.Titular, limite: cc2.Limite);

                    }
                    else if (contaAntiga is ContaPoupanca cp2)
                    {
                        contaAtualizada = new ContaPoupanca(numero: contaAntiga.Numero, agencia: atualizarAgencia, tipo: 1, titular: contaAntiga.Titular, aniversario: cp2.Aniversario);

                    }

                    Console.Clear();
                    saidaAtualizar = true;
                    //quebra o looping
                    break;

                case 3: // atualizar tipo, Validações(ValidacaoHelper) para garantir os dados certos

                    //precisa ser entre 1 e 2
                    byte atualizarTipo = ValidacaoHelper.OpcaoRestricao("\n[1] - Corrente\t[2] - Poupança\n\nDigite qual o novo da tipo da Conta:", 1, 2);
                    if (atualizarTipo == 1)
                    {
                        //precisa ser positivo
                        float atualizarLimite = ValidacaoHelper.ValorPositivoFloat("Digite o novo limite:\n");

                        contaAtualizada = new ContaCorrente(numero: contaAntiga.Numero, agencia: contaAntiga.Agencia, tipo: 1, titular: contaAntiga.Titular, limite: atualizarLimite);
                    }
                    else
                    {
                        //pega o dia para ser o aniversário de rendimento
                        int atualizarAniversario = DateTime.Now.Day;
                        contaAtualizada = new ContaPoupanca(numero: contaAntiga.Numero, agencia: contaAntiga.Agencia, tipo: 2, titular: contaAntiga.Titular, aniversario: atualizarAniversario);

                    }

                    Console.Clear();
                    saidaAtualizar = true;
                    break;

                case 4:             //nenhuma das opções (cancelar) 

                    Console.WriteLine("Voltando para o menu principal.");
                    Console.Clear();
                    //quebra o looping
                    saidaAtualizar = true;
                    break;

                default:                    //numero errado

                    Cores.ExibirErro("Opção inválida, digite outra.");
                    break;
            }
        }
        //remove a antiga
        contas.Remove(contaAntiga);
        //cria uma nova com os mesmos dados, pórem com a mudança escolhida
        contas.Add(contaAtualizada);
        //salva no JSON
        GravarContas();
        Cores.ExibirSucesso($"Seu novo número de conta é: {contaAtualizada.Numero}");
        
    }
    public void Deletar(int numero) 
    {
        //a conta precisa existir
        var contaBuscada = BuscarNaCollection(numero);
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numero} não existe no sistema.");
        }
        //se existe então deleta
        contas.Remove(contaBuscada);
        //salva a mudança
        GravarContas();
    }
    public void Sacar(int numero, float valor) 
    {
        //verifica se existe a conta
        Conta contaBuscada = BuscarNaCollection(numero);
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numero} não existe no sistema.");
        }
        else if (contaBuscada.Sacar(valor))
        {
            Cores.ExibirSucesso($"Saque no valor: {valor}, realizado com sucesso.\nSaldo atual: {BuscarNaCollection(numero).Saldo}");
            //faz o saque e salva
            GravarContas();
        }

    }
    public void Depositar(int numero, float valor) 
    {
        //verifica se existe a conta
        Conta contaBuscada = BuscarNaCollection(numero);
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numero} não existe no sistema.");
        }
        contaBuscada.Depositar(valor);
        //faz o depósito e salva
        GravarContas();
        Cores.ExibirSucesso($"Depósito realizado com sucesso, saldo atual: {BuscarNaCollection(numero).Saldo}");
    }
       
    public void Transferir(int numeroOrigem, int numeroDestino, float valor) 
    {
        //verifica se existe as contas
        Conta contaOrigem = BuscarNaCollection(numeroOrigem);
        Conta contaDestino = BuscarNaCollection(numeroDestino);
        if (contaOrigem == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numeroOrigem} não existe no sistema.");
        }

        if (contaDestino == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numeroDestino} não existe no sistema.");
        }
        //pirmeiro tenta sacar da conta origem, se der certo ai deposita na conta destino, caso não a transação é cancelada
        if (contaOrigem.Sacar(valor))
        {
           contaDestino.Depositar(valor);
           //salva a mudança
           GravarContas();
           Cores.ExibirSucesso($"Transferência realizada com sucesso, saldo atual da conta de origem: {BuscarNaCollection(numeroOrigem).Saldo}, saldo atual da conta de destino: {BuscarNaCollection(numeroDestino).Saldo}");

        }
        else
        {
           throw new SaldoInsuficienteException($"Saldo insuficiente para realizar a transferência de R${valor} da conta {numeroOrigem} para a conta {numeroDestino}.");
        }
        
    }
    public int GerarNumero()
    {
        //gera um número aleatorio até 1000 para ser o id da conta
        Random random = new Random();
        return random.Next(1, 1000);
    }


    public Conta BuscarNaCollection(int numero)
    {
        //caso não ache o numero da conta ele retorna o valor padrão que vai ser null nesse caso
        return contas.FirstOrDefault(c => c.Numero == numero);
    }

    private void GravarContas()
    {
        //não precisa usar o using para liberar dados, File.WriteAllText faz isso

        //formatação do JSON
        var opcoes = new JsonSerializerOptions { WriteIndented = true };

        //converte as contas para string para serem gravados no arquivo
        string jsonString = JsonSerializer.Serialize(contas, opcoes);

        //salva as contas no arquivo
        File.WriteAllText("contas.json", jsonString);
    }

    private void LerContas()
    {
        //verifica se tem o arquivo, ou seja, primeira vez usando o sistema
        if (!File.Exists("contas.json")) return;

        //le ps dados do arquivo
        string jsonString = File.ReadAllText("contas.json");

        //retorna as contas certas usando a formatação
        contas = JsonSerializer.Deserialize<List<Conta>>(jsonString);
    }

}
