using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Model;
/*
 Classe responsável por definir o Objeto Conta Corrente
 */
public class ContaCorrente: Conta
{
   protected ContaCorrente(int numero, int agencia, int tipo, string titular) 
        : base(numero, agencia, tipo, titular)
    {
        float _limite = 0;
    }

    private float _limite;

    public float getLimite()
    {
        return _limite;
    }

    public void setLimite(float limite)
    {
        _limite = limite;
    }

    public override bool Sacar(float valor)
    {
        // Implementação para realizar o saque, considerando o limite da conta corrente
        return true; // Retorna true se o saque for bem-sucedido, caso contrário, retorna false
    }
    public override void Visualizar()
    {
        Console.WriteLine("isso");
    }
   
}
