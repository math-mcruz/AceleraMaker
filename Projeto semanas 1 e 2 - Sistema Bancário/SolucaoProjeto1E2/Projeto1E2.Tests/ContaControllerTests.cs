using Projeto1E2.Contas;
using Projeto1E2.Controller;
using Projeto1E2.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Tests;

public class ContaControllerTests
{
    [Fact]
    public void TransferirSaldoSuficiente()
    {
        // 1. ARRANGE
        var controller = new ContaController();
        var origem = new ContaCorrente(1, 123, 1, "Origem", 0f);
        var destino = new ContaCorrente(2, 123, 1, "Destino", 0f);

        origem.Depositar(1000f); //saldo inicial: 1000
        controller.Cadastrar(origem);
        controller.Cadastrar(destino);

        // 2. ACT
        controller.Transferir(numeroOrigem: 1, numeroDestino: 2, valor: 400f);

        // 3. ASSERT
        Assert.Equal(600f, origem.Saldo);  // 1000 - 400
        Assert.Equal(400f, destino.Saldo); // 0 + 400
    }

    [Fact]
    public void SacarComLimite()
    {
        // 1. ARRANGE
        //saldo = 0, mas com 500 de limite
        var cc = new ContaCorrente(1, 123, 1, "Teste Limite", 500f);

        // 2. ACT
        cc.Sacar(300f); //tenta sacar mais do que tem de saldo, usando o limite

        // 3. ASSERT
        Assert.Equal(-300f, cc.Saldo); //saldo pode ser negativo por conta limite
    }

    [Fact]
    public void SacarAcimaDoLimite()
    {
        // 1. ARRANGE
        var cc = new ContaCorrente(1, 123, 1, "Teste Limite", 100f);

        //ACT e ASSERT
        //tenta sacar 200 com 100 de limite
        Assert.Throws<SaldoInsuficienteException>(() => cc.Sacar(200f));
    }

    [Fact]
    public void BuscarNaCollectionContaExistente()
    {
        // 1. ARRANGE
        var controller = new ContaController();
        var cc = new ContaCorrente(99, 123, 1, "Matheus", 0f);
        controller.Cadastrar(cc);

        // 2. ACT
        var resultado = controller.BuscarNaCollection(99);

        // 3. ASSERT
        Assert.NotNull(resultado);
        Assert.Equal("Matheus", resultado.Titular);
    }

    [Fact]
    public void BuscarNaCollectionContaInexistente()
    {
        // 1. ARRANGE
        var controller = new ContaController();

        // 2. ACT
        var resultado = controller.BuscarNaCollection(1001); // Conta que nunca foi criada

        // 3. ASSERT
        Assert.Null(resultado);
    }
}
