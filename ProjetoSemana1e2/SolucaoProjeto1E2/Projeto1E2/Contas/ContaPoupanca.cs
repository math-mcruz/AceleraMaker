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
        this._aniversario = aniversario;
    }

    private int _aniversario;
    //renderJuros(int diaDeHoje) olhar mais sobre isso, uma funcionalidade opcional usando o aniversario.

    public int GetAniversario()
    {
        return _aniversario;
    }

    public void SetAniversario(int aniversario)
    {
        this._aniversario = aniversario;
    }

    public override void Visualizar()
    {

    }
 
}
