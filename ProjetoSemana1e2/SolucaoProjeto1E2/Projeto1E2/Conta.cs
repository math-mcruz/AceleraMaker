using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2;
/*
 Classe responsável por definir o Objeto Conta genérico
 */
internal abstract class Conta
{
    //propriedades para ter mais segurança ao acessar as variaveis
    private int numero;
    public int Numero { get; set; }

    private int agencia;
    public int Agencia { get; set; }

    private int tipo;
    public int Tipo { get; set; }

    private string? titular;
    public string? Titular { get; set; }

    public abstract int getNumero();
    public abstract int getAgencia();
    public abstract int getTipo();
    public abstract string getTitular();

    public abstract float getSaldo(float saldo);

    public abstract void setNumero(int numero);

    public abstract void setAgencia(int numero);

    public abstract void setTipo(int tipo);

    public abstract void setTitular(string titular);

    public abstract void setSaldo(float saldo);

    public abstract bool sacar(float valor);

    public abstract void depositar(float valor);
}
