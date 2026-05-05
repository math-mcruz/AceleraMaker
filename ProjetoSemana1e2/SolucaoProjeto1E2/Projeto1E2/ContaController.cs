using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2;

internal class ContaController: IContaRepository
{
    public void procurarPorNumeros(int numero)
    {}
    public void listarTodas() //da pra por static aqui?
    { }
    //public void cadastrar(Conta conta)
    //{}
    //public void atualizar(Conta conta)
    //{}
    public void deletar(int numero) 
    { }
    public void sacar(int numero, float valor) 
    { }
    public void depositar(int numero, float valor) 
    { }
    public void transferir(int numeroOrigem, int numeroDestino, float valor) 
    { }
    public int gerarNumero()
    {
        Random random = new Random();
        return random.Next(1000, 9999);
    }

    //public Conta buscarNaCollection(int numero)
    //{}
}
