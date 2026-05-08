using Projeto1E2.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Contas;
/*
 Classe responsável por definir o Objeto Conta Corrente
 */
public class ContaCorrente: Conta
{
   public ContaCorrente(int numero, int agencia, int tipo, string titular, float limite) 
        : base(numero, agencia, tipo, titular)
    {
        Limite = limite;
    }

    public float Limite { get; protected set; }

    //public float GetLimite()
    //{
    //    return _limite;
    //}

    //public void SetLimite(float limite)
    //{
    //    _limite = limite;
    //}

    public override bool Sacar(float valor)
    {
        if (valor <= 0)
        {
            throw new ValorInvalidoException($"Valor inválido para saque: {valor}.\nO valor deve ser maior do que zero. ");
        }
        //Sobrescreve o método da classe pai, na conta corrente o cliente pode sacar com o seu saldo + valor do limite, podendo ficar negativado
        //observação: o this é para aceessar os métodos da classe pai o GetSaldo()
        if (valor <= this.Saldo + Limite)
        {
            //Aqui ele faz o saldo com o valor mesmo que tenha passado do saldo que tinha
            this.Saldo -= valor;
            return true;
        }
        else
        {
        throw new SaldoInsuficienteException($"Saldo: {this.Saldo}, limite: {Limite}\nInsuficiente para sacar: {valor}.");
        }
    }
    public override void Visualizar()
    {
        if (this != null)
        {
            Console.WriteLine($"\nTitular da conta: {this.Titular}");
            Console.WriteLine($"Número da conta: {this.Numero}");
            Console.WriteLine($"Agência: {this.Agencia}");
            string tipoString = this.Tipo == 1 ? "Corrente" : "Poupança";
            Console.WriteLine($"Tipo: {tipoString}");
            Console.WriteLine($"Limite: {Limite}");
            Console.WriteLine($"Saldo: {this.Saldo}");

        }
        else
        {
            Console.WriteLine("Conta não encontrada.");
        }
    }
   
}
