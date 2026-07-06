using dotnet.Config.DllConfig;
using dotnet.Infrastructure;


namespace ProjetoFinal.Testes;

public class ClienteServiceTests : IDisposable
{
    public ClienteServiceTests()
        { }

        //DISPOSE
        public void Dispose()
        {
            string pathCtrlTeste = @"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\DATATEST\CTRLTESTS.dat";

            try 
            {
                if (File.Exists(pathCtrlTeste)) File.Delete(pathCtrlTeste);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao acessar os arquivos DLL: " + e.Message);
            }
        }
    [Fact]
    public void Cadastar_Cliente_Valido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("0", "Aline", "(12) 99999-8888", "aline@email.com", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.CADASCLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("00", statusRetorno);
    }
    [Fact]
    public void Cadastar_Cliente_Invalido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("0", " ", "(11) 98888-7777", "luis@email.com", "TS");
    //*************************************************************************************!!!!!!    

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.CADASCLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("99", statusRetorno);
    }
    [Fact]
    public void Consultar_Cliente_Valido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("1", "", "", "", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.CONSCLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("00", statusRetorno);
    }
    [Fact]
    public void Consultar_Cliente_Invalido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("2", "", "", "", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.CONSCLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("44", statusRetorno);
    }
    [Fact]
    public void Atualizar_Cliente_Valido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("1", "", "(31) 94444-5555", "aline.montreal@email.com", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.ATUACLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("00", statusRetorno);
    }
    [Fact]
    public void Atualizar_Cliente_Invalido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("2", "", "", "", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.ATUACLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("44", statusRetorno);
    }
    [Fact]
    public void Deletar_Cliente_Valido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("1", "", "", "", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.DELECLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("00", statusRetorno);
    }
    [Fact]
    public void Deletar_Cliente_Invalido()
    {
        //ARRANGE
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
        var wrapper = new CopybookWrapper(parser);
        wrapper.PayloadCobol("2", "", "", "", "TS");

        //ACT
        DllConfig.cob_init(0, IntPtr.Zero);
        DllConfig.DELECLI(wrapper.BufferMemoria);

        //ASSERT
        string statusRetorno = wrapper.ExtrairCampo(4).Trim();
        Assert.Equal("44", statusRetorno);
    }
}