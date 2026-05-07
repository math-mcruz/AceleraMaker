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
        this._limite = limite;
    }

    private float _limite;

    public float GetLimite()
    {
        return _limite;
    }

    public void SetLimite(float limite)
    {
        _limite = limite;
    }

    public override bool Sacar(float valor)
    {
        if (valor <= 0)
        {
            throw new ValorInvalidoException($"Valor inválido para saque: {valor}.\nO valor deve ser maior do que zero. ");
        }
        //Sobrescreve o método da classe pai, na conta corrente o cliente pode sacar com o seu saldo + valor do limite, podendo ficar negativado
        //observação: o this é para aceessar os métodos da classe pai o GetSaldo()
        if (valor <= this.GetSaldo() + _limite)
        {
            //Aqui ele faz o saldo com o valor mesmo que tenha passado do saldo que tinha
            this.SetSaldo(this.GetSaldo() - valor);
            return true;
        }
        else
        {
        throw new SaldoInsuficienteException($"Saldo: {this.GetSaldo()}, limite: {_limite}\nInsuficiente para sacar: {valor}.");
        }
    }
    public override void Visualizar()
    {
        if (this != null)
        {
            Console.WriteLine($"\nTitular da conta: {this.GetTitular()}");
            Console.WriteLine($"Número da conta: {this.GetNumero()}");
            Console.WriteLine($"Agência: {this.GetAgencia()}");
            string tipoString = this.GetTipo() == 1 ? "Corrente" : "Poupança";
            Console.WriteLine($"Tipo: {tipoString}");
            Console.WriteLine($"Limite: {this.GetLimite()}");
            Console.WriteLine($"Saldo: {this.GetSaldo()}");

        }
        else
        {
            Console.WriteLine("Conta não encontrada.");
        }
    }
   
}
