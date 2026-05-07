using Projeto1E2.Contas;
using Projeto1E2.Exceptions;
using Projeto1E2.Repository;
using Projeto1E2.Utils;
using System;
using System.Collections.Generic;
using System.Text;


namespace Projeto1E2.Controller;

public class ContaController: IContaRepository
{
    private List<Conta> contas = new List<Conta>();
    public void ProcurarPorNumero(int numero)
    {
        foreach (var conta in contas)
        {
            if (conta.GetNumero() == numero)
            {
                Console.WriteLine($"\nTitular da conta: {conta.GetTitular()}");
                Console.WriteLine($"Número da conta: {conta.GetNumero()}");
                Console.WriteLine($"Agência: {conta.GetAgencia()}");
                string tipoString = conta.GetTipo() == 1 ? "Corrente" : "Poupança";
                Console.WriteLine($"Tipo: {tipoString}");
                return;
            }
            //chamar o visualizar da conta para mostrar as informações da conta encontrada
        }

    }
    public void ListarTodas()
    {
        if(contas.Count != 0)
        {
            foreach(var conta in contas)
            {
                conta.Visualizar();
            }
        }
        else
        {
            Console.WriteLine("Não existem contas cadastradas.");
        }
            
    }
    
    public void Cadastrar(Conta conta)
    {
        //analisar aqui se precisa tratar
        if (BuscarNaCollection(conta.GetNumero()) != null)
        {
            Console.WriteLine("Conta já cadastrada.");
        }
        else
        {
            contas.Add(conta);
            Console.WriteLine($"Conta cadastrada com sucesso, numero da conta é: {conta.GetNumero()}");
        }

    }
    public void Atualizar(Conta conta)
    {
        var contaAntiga = BuscarNaCollection(conta.GetNumero());
        if (contaAntiga == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {conta.GetNumero()} não existe no sistema.");
        }

        Conta contaAtualizada = null;
        bool saidaAtualizar = false;
        while (!saidaAtualizar)
        {
            //Exibe menu de operações
            byte opcaoOperacao = ExibirMenu.Atualizar();
            switch (opcaoOperacao)
            {
                case 1:// atualizar titular --------------------------------------------------------------------> OK
                    string? atualizarTitular = ValidacaoHelper.TextoMenu("\nDigite o nome novo do titular:\n");

                    //buscar qual tipo de conta 
                    if (contaAntiga is ContaCorrente cc1)
                    {
                        contaAtualizada = new ContaCorrente(numero: contaAntiga.GetNumero(), agencia: contaAntiga.GetAgencia(), tipo: 1, titular: atualizarTitular, limite: cc1.GetLimite());
                    }
                    else if (contaAntiga is ContaPoupanca cp1)
                    {
                        contaAtualizada = new ContaPoupanca(numero: contaAntiga.GetNumero(), agencia: contaAntiga.GetAgencia(), tipo: 1, titular: atualizarTitular, aniversario: cp1.GetAniversario());

                    }

                    Console.Clear();
                    saidaAtualizar = true;
                    break;

                case 2: //atualizar agencia ----------------------------------------------------------------------------> OK
                    int atualizarAgencia = ValidacaoHelper.ValorPositivo("\nDigite o número da nova agência:\n");

                    //buscar qual tipo de conta
                    if (contaAntiga is ContaCorrente cc2)
                    {
                        contaAtualizada = new ContaCorrente(numero: contaAntiga.GetNumero(), agencia: atualizarAgencia, tipo: 1, titular: contaAntiga.GetTitular(), limite: cc2.GetLimite());

                    }
                    else if (contaAntiga is ContaPoupanca cp2)
                    {
                        contaAtualizada = new ContaPoupanca(numero: contaAntiga.GetNumero(), agencia: atualizarAgencia, tipo: 1, titular: contaAntiga.GetTitular(), aniversario: cp2.GetAniversario());

                    }

                    Console.Clear();
                    saidaAtualizar = true;
                    break;

                case 3: // atualizar tipo  --------------------------------------------------------> OK
                    byte atualizarTipo = ValidacaoHelper.OpcaoRestricao("\n[1] - Corrente\t[2] - Poupança\n\nDigite qual o novo da tipo da Conta:", 1, 2);
                    if (atualizarTipo == 1)
                    {
                        Console.WriteLine("Digite o novo limite:\n");
                        float atualizarLimite = ValidacaoHelper.ValorPositivoFloat("Digite o número da nova agência:\n");

                        contaAtualizada = new ContaCorrente(numero: contaAntiga.GetNumero(), agencia: contaAntiga.GetAgencia(), tipo: 1, titular: contaAntiga.GetTitular(), limite: atualizarLimite);
                    }
                    else
                    {
                        int atualizarAniversario = DateTime.Now.Day;
                        contaAtualizada = new ContaPoupanca(numero: contaAntiga.GetNumero(), agencia: contaAntiga.GetAgencia(), tipo: 2, titular: contaAntiga.GetTitular(), aniversario: atualizarAniversario);

                    }

                    Console.Clear();
                    saidaAtualizar = true;
                    break;

                case 4: //nenhuma das opções (cancelar) ----------------------------------> OK
                    Console.WriteLine("Voltando para o menu principal.");
                    Console.Clear();
                    saidaAtualizar = true;
                    break;

                default: //numero errado --------------------------------------------------> OK

                    Console.WriteLine("Opção inválida, digite outra.");
                    break;
            }
        }

        contas.Remove(contaAntiga);
        contas.Add(contaAtualizada);
        Console.WriteLine($"Seu novo número de conta é: {contaAtualizada.GetNumero()}");
        
    }
    public void Deletar(int numero) 
    {
        var contaBuscada = BuscarNaCollection(numero);
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numero} não existe no sistema.");
        }
        contas.Remove(contaBuscada);
    }
    public void Sacar(int numero, float valor) 
    {
        Conta contaBuscada = BuscarNaCollection(numero);
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numero} não existe no sistema.");
        }
        else if (contaBuscada.Sacar(valor))
        {
            Console.WriteLine($"Saque no valor: {valor}, realizado com sucesso.\nSaldo atual: {BuscarNaCollection(numero).GetSaldo()}");
        }

    }
    public void Depositar(int numero, float valor) 
    {
        Conta contaBuscada = BuscarNaCollection(numero);
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {numero} não existe no sistema.");
        }
        contaBuscada.Depositar(valor);
        Console.WriteLine($"Depósito realizado com sucesso, saldo atual: {BuscarNaCollection(numero).GetSaldo()}");
    }
       
    public void Transferir(int numeroOrigem, int numeroDestino, float valor) 
    {
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

        if (contaOrigem.Sacar(valor))
        {
           contaDestino.Depositar(valor);
            Console.WriteLine($"Transferência realizada com sucesso, saldo atual da conta de origem: {BuscarNaCollection(numeroOrigem).GetSaldo()}, saldo atual da conta de destino: {BuscarNaCollection(numeroDestino).GetSaldo()}");

        }
        else
        {
           throw new SaldoInsuficienteException($"Saldo insuficiente para realizar a transferência de R${valor} da conta {numeroOrigem} para a conta {numeroDestino}.");
        }
        
    }
    public int GerarNumero()
    {
        Random random = new Random();
        return random.Next(1, 1000);
    }


    public Conta BuscarNaCollection(int numero)
    {
        //pode ser feito de uma forma simples com foreach e uma forma mais eficiente com find
        //caso não ache o numero da conta ele retorna o valor padrão que vai ser null nesse caso

        return contas.FirstOrDefault(c => c.GetNumero() == numero);//coloquei qualquer coisa por enquanto

        //FAZER TRATAMENTO COM EXCEÇÃO PARA QUANDO A CONTA NÃO FOR ENCONTRADA
    }
    public Conta BuscarNaCollectionTipo(int tipo)
    {
        return contas.FirstOrDefault(c => c.GetTipo() == tipo);//coloquei qualquer coisa por enquanto

        //FAZER TRATAMENTO COM EXCEÇÃO PARA QUANDO A CONTA NÃO FOR ENCONTRADA
    }
}
