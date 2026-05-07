using Projeto1E2.Controller;
using Projeto1E2.Exceptions;
using Projeto1E2.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Contas;
/*
 Classe responsável por definir o Objeto Conta genérico
 */
public abstract class Conta
{
    public Conta(int numero, int agencia, int tipo, string titular)
    {
        _numero = numero;
        _agencia = agencia;
        _tipo = tipo;
        _titular = titular;
        _saldo = 0;
    }
    //Atributos(campos) para ter mais segurança ao acessar as variaveis
    private int _numero;
    private int _agencia;
    private int _tipo;
    private string? _titular;
    //O _saldo foi criado para GetSaldo e SetSaldo(), pórem ele não estava no diagrama UML.
    private float _saldo;

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
    public float GetSaldo()
    {
        return _saldo;
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
            _saldo = saldo;
    }
    public void Depositar(float valor)
    {
        if(valor <= 0)
        {
            throw new ValorInvalidoException($"Valor inválido para depósito: {valor}.\nO valor deve ser maior do que zero. ");
        }
        SetSaldo(_saldo + valor);
    }
    //Método virtual PODE ser sobrescrito, neste caso, a ContaCorrente vai sobrescrever o método Sacar
    public virtual bool Sacar(float valor)
    {
        if(valor <= 0)
        {
            throw new ValorInvalidoException($"Valor inválido para saque: {valor}.\nO valor deve ser maior do que zero. ");
        }
        if (valor <= _saldo)
        {
            SetSaldo(_saldo - valor);
            return true;
        }
        else
        {
            throw new SaldoInsuficienteException($"Saldo: {_saldo} insuficiente para sacar: {valor}.");
        }
    }
    //Método abstrato que TODOS devem implementar, para mostrar as informações da conta com suas características específicas
    public abstract void Visualizar();
    
}
