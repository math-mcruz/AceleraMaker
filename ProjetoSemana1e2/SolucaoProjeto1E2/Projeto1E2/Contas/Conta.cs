using Projeto1E2.Controller;
using Projeto1E2.Exceptions;
using Projeto1E2.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projeto1E2.Contas;

[JsonDerivedType(typeof(ContaCorrente), typeDiscriminator: "corrente")]
[JsonDerivedType(typeof(ContaPoupanca), typeDiscriminator: "poupanca")]
public abstract class Conta
{
    public Conta(int numero, int agencia, int tipo, string titular)
    {
        Numero = numero;
        Agencia = agencia;
        Tipo = tipo;
        Titular = titular;
        Saldo = 0;
    }
    //tive que mudar para propriedades para conseguir usar JSON para salvar os dados
    public int Numero { get; set; }
    public int Agencia { get; set; }
    public int Tipo { get; set; }
    public string Titular { get; set; }

    // protected set pois so as regras de Sacar e Depositar podem acessar
    public float Saldo { get; protected set; }

    //Métodos para todas as classes filhas acessarem os campos privados, garantindo a segurança dos dados
    //public int GetNumero()
    //{
    //    return _numero;
    //}
    //public int GetAgencia()
    //{
    //    return _agencia;
    //}
    //public int GetTipo()
    //{
    //    return _tipo;
    //}
    //public string GetTitular()
    //{
    //    return _titular;
    //}
    //public float GetSaldo()
    //{
    //    return _saldo;
    //}
    //public void SetNumero(int numero)
    //{
    //    _numero = numero;
    //}
    //public void SetAgencia(int numero)
    //{
    //    _agencia = numero;
    //}
    //public  void SetTipo(int tipo)
    //{
    //    _tipo = tipo;
    //}
    //public void SetTitular(string titular)
    //{
    //    _titular = titular;
    //}
    //public void SetSaldo(float saldo)
    //{
    //        _saldo = saldo;
    //}
    public void Depositar(float valor)
    {
        if(valor <= 0)
        {
            throw new ValorInvalidoException($"Valor inválido para depósito: {valor}.\nO valor deve ser maior do que zero. ");
        }
        this.Saldo += valor;
    }
    //Método virtual PODE ser sobrescrito, neste caso, a ContaCorrente vai sobrescrever o método Sacar
    public virtual bool Sacar(float valor)
    {
        if(valor <= 0)
        {
            throw new ValorInvalidoException($"Valor inválido para saque: {valor}.\nO valor deve ser maior do que zero. ");
        }
        if (valor <= Saldo)
        {
            Saldo -= valor;
            return true;
        }
        else
        {
            throw new SaldoInsuficienteException($"Saldo: {Saldo} insuficiente para sacar: {valor}.");
        }
    }
    //Método abstrato que todos devem implementar, para mostrar as informações da conta com suas características específicas
    public abstract void Visualizar();
    
}
