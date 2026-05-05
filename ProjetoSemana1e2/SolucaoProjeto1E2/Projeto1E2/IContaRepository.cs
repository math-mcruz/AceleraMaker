using System;
using System.Collections.Generic;
using System.Text;
/*
 Interface responsável por encapsular os Métodos que serão utilizados no Menu da aplicação
 */
namespace Projeto1E2;

public interface IContaRepository
{
    public void procurarPorNumeros(int numero);
    public void listarTodas();//da pra por static aqui?
    //public void cadastrar(Conta conta);
    //public void atualizar(Conta conta);
    public void deletar(int numero);
    public void sacar(int numero, float valor);
    public void depositar(int numero, float valor);
    public void transferir(int numeroOrigem, int numeroDestino, float valor);
}
