using System;
using System.Collections.Generic;
using System.Text;
using Projeto1E2.Controller;
using Projeto1E2.Repository;

namespace Projeto1E2.Model;
/*
 Classe responsável por definir o Objeto Conta genérico
 */
public abstract class Conta
{
    protected Conta(int numero, int agencia, int tipo, string titular)
    {
        _numero = numero;
        _agencia = agencia;
        _tipo = tipo;
        _titular = titular;
    }
    //Atributos(campos) para ter mais segurança ao acessar as variaveis
    private int _numero;
    private int _agencia;
    private int _tipo;
    private string? _titular;

    //Métodos para todas as classes filhas acessarem os campos privados, garantindo a segurança dos dados
    public int GetNumero()
    {
        return _numero;
    }
    public int GetAgencia()
    {
        return _agencia;
    }
    public int GetTipo()
    {
        return _tipo;
    }
    public string GetTitular()
    {
        return _titular;
    }
    public float GetSaldo(float saldo)
    {
        return saldo;
    }
    public void SetNumero(int numero)
    {
        _numero = numero;
    }
    public void SetAgencia(int numero)
    {
        _agencia = numero;
    }
    public  void SetTipo(int tipo)
    {
        _tipo = tipo;
    }
    public void SetTitular(string titular)
    {
        _titular = titular;
    }
    public void SetSaldo(float saldo)
    {
            saldo = saldo;
    }
    public void Depositar(float valor)
    {
        // Implementação para realizar o depósito, considerando o valor a ser depositado
    }
    //Método virtual PODE ser sobrescrito, neste caso, a ContaCorrente vai sobrescrever o método Sacar
    public virtual bool Sacar(float valor)
    {
        return false;
    }
    //Método abstrato que TODOS devem implementar, para mostrar as informações da conta com suas características específicas
    public abstract void Visualizar();
}
