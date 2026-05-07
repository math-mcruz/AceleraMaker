using System;
using System.Collections.Generic;
using System.Text;
using Projeto1E2.Contas;
using Projeto1E2.Controller;
/*
 Interface responsável por encapsular os Métodos que serão utilizados no Menu da aplicação
 */
namespace Projeto1E2.Repository;

public interface IContaRepository
{
    public void ProcurarPorNumero(int numero);
    public void ListarTodas();
    public void Cadastrar(Conta conta);
    public void Atualizar(Conta conta);
    public void Deletar(int numero);
    public void Sacar(int numero, float valor);
    public void Depositar(int numero, float valor);
    public void Transferir(int numeroOrigem, int numeroDestino, float valor);
}
