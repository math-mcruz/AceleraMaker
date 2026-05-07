using Projeto1E2.Contas;
using Projeto1E2.Exceptions;
using Projeto1E2.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projeto1E2.Controller;

internal class ContaController: IContaRepository
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
       // if(contas.Count != 0)
        //{
            Console.WriteLine("\nContas cadastradas:");
            foreach(var conta in contas)
            {
                Console.WriteLine($"\nTitular da conta: {conta.GetTitular()}");
                Console.WriteLine($"\nNúmero da conta: {conta.GetNumero()}");
                Console.WriteLine($"\nAgência: {conta.GetAgencia()}");
                string tipoString = conta.GetTipo() == 1 ? "Corrente" : "Poupança";
                Console.WriteLine($"\nTipo: {tipoString}");
            }
      //  }
      //  else
      //  {
       //     Console.WriteLine("Não existem contas cadastradas.");
      //  }
            
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
        }

    }
    public void Atualizar(Conta conta)
    {
        var contaBuscada = BuscarNaCollection(conta.GetNumero());
        if (contaBuscada == null)
        {
            throw new ContaNaoEncontradaException($"A conta de número {conta.GetNumero()} não existe no sistema.");
        }
        contas.Remove(contaBuscada);

            //Pode pedir para escolher o  que quer atualizar -------------------------------------------------------------------------------------*****

        Console.WriteLine("Digite o nome do titular");
        string? novoTitular = Console.ReadLine();

        Console.WriteLine("\nDigite o número da agência");
        var novaAgencia = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\n[1] - Corrente\t[2] - Poupança\n\nDigite qual o tipo da Conta");
            //tratamento de erro para o tipo da conta

        byte novoTipo = Convert.ToByte(Console.ReadLine());

        var numeroConta = GerarNumero();

        float novolimite = 0;

        int novoAniversario = DateTime.Now.Day;

        Conta novaConta = null;

        if (novoTipo == 1)
        {
            novaConta = new ContaCorrente(numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, limite: novolimite);
        }
        else
        {
        novaConta = new ContaPoupanca(numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, aniversario: novoAniversario);
        }
        contas.Add(novaConta);
        Console.WriteLine($"Seu novo número de conta é: {numeroConta}");
        
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
        
        if (contaBuscada.Sacar(valor))
        {
            Console.WriteLine($"Saque no valor: {valor} realizado com sucesso.");
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
}
