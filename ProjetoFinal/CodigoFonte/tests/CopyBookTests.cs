using dotnet.Infrastructure;


namespace ProjetoFinal.Testes;

public class CopyBookTests
{
    [Fact] 
    public void PayloadCobol_PreencheEspacos()
    {
            //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
            
        string nome = "MATHEUS";
        string telefone = "11999999999";
        string email = "teste@teste.com";

        //ACT
        wrapper.PayloadCobol("0", nome, telefone, email, "");
            
        //ASSERT
        string nomeLimpo = wrapper.ExtrairCampo(1);
        Assert.Equal("MATHEUS", nomeLimpo);

        string memoriaBrutaDoNome = System.Text.Encoding.ASCII.GetString(wrapper.BufferMemoria, 5, 30);
            
        Assert.Equal(30, memoriaBrutaDoNome.Length);
        Assert.EndsWith(" ", memoriaBrutaDoNome);
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
}