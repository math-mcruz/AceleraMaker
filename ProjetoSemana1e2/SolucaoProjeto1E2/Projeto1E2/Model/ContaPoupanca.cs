using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Model;
/*
 Classe responsável por definir o Objeto Conta Poupanca
 */
public class ContaPoupanca: Conta
{
    protected ContaPoupanca(int Numero, int Agencia, int Tipo, string Titular) 
        : base(Numero, Agencia, Tipo, Titular)
    {
        int _aniversario = 0;
    }

    private int aniversario;

    public int GetAniversario()
    {
        return aniversario;
    }

    public void SetAniversario(int aniversario)
    {
        this.aniversario = aniversario;
    }

    public override void Visualizar()
    {}
 
}
