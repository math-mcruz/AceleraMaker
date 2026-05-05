using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2;
/*
 Classe responsável por definir o Objeto Conta Corrente
 */
internal class ContaCorrente: Conta
{
    private float limite;

    public float getLimite()
    {
        return Limite;
    }

    public void setLimite(float limite)
    {
        Limite = limite;
    }

    public override bool sacar(float valor)
    {
        // Implementação para realizar o saque, considerando o limite da conta corrente
        return true; // Retorna true se o saque for bem-sucedido, caso contrário, retorna false
    }
    public void visualizar()
    {}
    //---------------------------------------------------------------------------------------------------------------------------------
    //                                           Implementação dos métodos abstratos da classe Conta
    //----------------------------------------------------------------------------------------------------------------------------------
    public override int getNumero()
    {
        return Numero;
    }

    public override int getAgencia()
    {
        return Agencia;
    }

    public override int getTipo()
    {
        return Tipo;
    }

    public override string getTitular()
    {
        return Titular;
    }

    public override float getSaldo(float saldo)
    {
        return saldo;
    }

    public override void setNumero(int numero)
    {
        Numero = numero;
    }

    public override void setAgencia(int numero)
    {
        Agencia = numero;
    }

    public override void setTipo(int tipo)
    {
        Tipo = tipo;
    }

    public override void setTitular(string titular)
    {
        Titular = titular;
    }

    public override void setSaldo(float saldo)
    {
        // Implementação para definir o saldo da conta corrente
    }


    public override void depositar(float valor)
    {
        // Implementação para realizar o depósito na conta corrente
    }

}
