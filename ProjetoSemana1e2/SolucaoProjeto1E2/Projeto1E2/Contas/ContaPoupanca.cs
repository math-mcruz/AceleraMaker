using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Contas;
/*
 Classe responsável por definir o Objeto Conta Poupanca
 */
public class ContaPoupanca: Conta
{
    public ContaPoupanca(int numero, int agencia, int tipo, string titular, int aniversario) 
        : base(numero, agencia, tipo, titular)
    {
        Aniversario = aniversario;
    }
    public int Aniversario { get; protected set; }
    
    //renderJuros(int diaDeHoje) olhar mais sobre isso, uma funcionalidade opcional usando o aniversario.

    //public int GetAniversario()
    //{
    //    return _aniversario;
    //}

    //public void SetAniversario(int aniversario)
    //{
    //    this._aniversario = aniversario;
    //}

    public override void Visualizar()
    {
        if (this != null)
        {
            Console.WriteLine($"\nTitular da conta: {this.Titular}");
            Console.WriteLine($"Número da conta: {this.Numero}");
            Console.WriteLine($"Agência: {this.Agencia}");
            string tipoString = this.Tipo == 1 ? "Corrente" : "Poupança";
            Console.WriteLine($"Tipo: {tipoString}");
            Console.WriteLine($"Aniversário: {Aniversario}");
            Console.WriteLine($"Saldo: {this.Saldo}");

        }
        else
        {
            Console.WriteLine("Conta não encontrada.");
        }
    }
 
}
