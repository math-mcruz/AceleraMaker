using dotnet.Infrastructure;

namespace tests;

public class CopyBookTests
{
    [Fact] 
    public void PayloadCobol_PreencheEspacos()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        //simulando a requisição enviada pelo usuario    
        string nome = "MATHEUS";
        string telefone = "11999999999";
        string email = "teste@teste.com";

        //ACT
        wrapper.PayloadCobol("0", nome, telefone, email, "");
            
        //ASSERT
        string nomeLimpo = wrapper.ExtrairCampo(1);
        //nome sem espaços no final
        Assert.Equal("MATHEUS", nomeLimpo);

        string memoriaNome = System.Text.Encoding.ASCII.GetString(wrapper.BufferMemoria, 5, 30);
        //nome com espaços no final e de tamanho 30    
        Assert.Equal(30, memoriaNome.Length);
        Assert.EndsWith(" ", memoriaNome);
    }

    [Fact]
    public void ExtrairCampo_RemoveEspacos()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        
        //simulando a memoria que o COBOL acabou de devolver
        string memoriaSimulada = "00015MATHEUS                       "; 
        
        //simulando o .dll
        wrapper.BufferMemoria = System.Text.Encoding.ASCII.GetBytes(memoriaSimulada);

        //ACT
        string nomeExtraido = wrapper.ExtrairCampo(1);

        //ASSERT

        //tamanho tem que ser 7 
        Assert.Equal("MATHEUS", nomeExtraido);
        Assert.Equal(7, nomeExtraido.Length);
    }

    [Fact]
    public void PayloadCobol_PreencheZeros()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);

        //ACT
        //tem que virar 00005
        wrapper.PayloadCobol("5", "", "", "", "");

        //ASSERT
        //ID é o indice 0
        string memoriaId = System.Text.Encoding.ASCII.GetString(wrapper.BufferMemoria, 0, 5);
        //deve ter os zeros a esquerda e tamanho 5
        Assert.Equal("00005", memoriaId);
    }

    [Fact]
    public void PayloadCobol_CortaString()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);

        string nomeMaior = "Matheus Cruz Testando com tamanho maior que 30 caracteres"; 

        //ACT
        wrapper.PayloadCobol("0", nomeMaior, "", "", "");

        //ASSERT
        //nome começa no byte 5 e tem tamanho 30
        string memoriaNome = System.Text.Encoding.ASCII.GetString(wrapper.BufferMemoria, 5, 30);
        Assert.Equal(30, memoriaNome.Length);
        
        //corta no com tamanho 30
        Assert.Equal("Matheus Cruz Testando com tama", memoriaNome); 
    }
}