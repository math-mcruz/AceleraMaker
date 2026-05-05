using Projeto1E2.Model;
using Projeto1E2.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Controller;

internal class ContaController: IContaRepository
{
    private List<Conta> contas = new List<Conta>();
    public void ProcurarPorNumeros(int numero)
    {}
    public void ListarTodas() //da pra por static aqui?
    { }
    public void Cadastrar(Conta contas)
    {
        //contas.Add(contas);
    }
    public void Atualizar(Conta contas)
    {
        
    }
    public void Deletar(int numero) 
    { }
    public void Sacar(int numero, float valor) 
    { }
    public void Depositar(int numero, float valor) 
    { }
    public void Transferir(int numeroOrigem, int numeroDestino, float valor) 
    { }
    public int GerarNumero()
    {
        Random random = new Random();
        return random.Next(1000, 9999);
    }


    public Conta BuscarNaCollection(int numero)
    { 
        return contas.Find(c => c.GetNumero() == numero);//coloquei qualquer coisa por enquanto
    }
}
