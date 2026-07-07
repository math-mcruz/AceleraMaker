using BlogPessoal.DTOs.Mappings;
using dotnet.Config.DllConfig;
using dotnet.DTOs.Clientes;
using dotnet.Infrastructure;

namespace dotnet.Service.Clientes;

public class ClienteService : IClienteService
{
    private readonly string _caminhoCopybook;

    public ClienteService()
    {
        _caminhoCopybook = Path.Combine(AppContext.BaseDirectory, "REGCLI.cpy");
    }
    public ClienteResponseDTO Consultar(int id)
    {
        var parser = new CopybookParser(_caminhoCopybook);

        var wrapper = new CopybookWrapper(parser);
            
        wrapper.PayloadCobol(id.ToString(), "", "", "", "");

        DllConfig.cob_init(0, IntPtr.Zero);

        DllConfig.CONSCLI(wrapper.BufferMemoria);
                
        int cliId = Convert.ToInt32(wrapper.ExtrairCampo(0));
        string cliNome = wrapper.ExtrairCampo(1);
        string telefone = wrapper.ExtrairCampo(2);
        string email = wrapper.ExtrairCampo(3);    
        string statusRetorno = wrapper.ExtrairCampo(4);

        if (statusRetorno == "00")
        {
            var cliente = ClientesDTOMappings.ToResponse(cliId, cliNome, telefone, email);
            return cliente;
        }
        else if (statusRetorno == "44")
        {
            throw new KeyNotFoundException("Cliente não encontrado.");
        }
        else
        {
            throw new Exception("Erro ao consultar o cliente.");
        }
    }
    
    public ClienteResponseDTO Cadastrar(ClienteRequestDTO cliRequestDto)
    {
        var parser = new CopybookParser(_caminhoCopybook);

        var wrapper = new CopybookWrapper(parser);
            
        wrapper.PayloadCobol("0", cliRequestDto.Cli_Nome, cliRequestDto.Telefone, cliRequestDto.Email, "");

        DllConfig.cob_init(0, IntPtr.Zero);
        //executa o arquivo .dll na memoria da API
        DllConfig.CADASCLI(wrapper.BufferMemoria);
                
        int cliId = Convert.ToInt32(wrapper.ExtrairCampo(0));
        string cliNome = wrapper.ExtrairCampo(1);
        string telefone = wrapper.ExtrairCampo(2);
        string email = wrapper.ExtrairCampo(3);    
        string statusRetorno = wrapper.ExtrairCampo(4);

        if (statusRetorno == "00")
        {
            var cliente = ClientesDTOMappings.ToResponse(cliId, cliNome, telefone, email);
            return cliente;
        }
        else if (statusRetorno == "31")
        {
            throw new Exception("ID de cliente já cadastrado.");
        }
        else
        {
            throw new Exception("Não foi possível cadastrar.");
        }
    }

    public ClienteResponseDTO Atualizar(int id, ClienteUpdateDTO cliUpdateDto)
    {
        var parser = new CopybookParser(_caminhoCopybook);

        var wrapper = new CopybookWrapper(parser);
            
        wrapper.PayloadCobol(id.ToString(), "", cliUpdateDto.Telefone, cliUpdateDto.Email, "");

        DllConfig.cob_init(0, IntPtr.Zero);
        //executa o arquivo .dll na memoria da API
        DllConfig.ATUACLI(wrapper.BufferMemoria);
                
        int cliId = Convert.ToInt32(wrapper.ExtrairCampo(0));
        string cliNome = wrapper.ExtrairCampo(1);
        string telefone = wrapper.ExtrairCampo(2);
        string email = wrapper.ExtrairCampo(3);    
        string statusRetorno = wrapper.ExtrairCampo(4);

        if (statusRetorno == "00")
        {
            var cliente = ClientesDTOMappings.ToResponse(cliId, cliNome, telefone, email);
            return cliente;
        }
        else if (statusRetorno == "44")
        {
            throw new KeyNotFoundException("Cliente não encontrado.");
        }
        else
        {
            throw new Exception("Erro ao atualizar cliente.");
        }
    }

    public void Deletar(int id)
    {
        var parser = new CopybookParser(_caminhoCopybook);

        var wrapper = new CopybookWrapper(parser);
            
        wrapper.PayloadCobol(id.ToString(), "", "", "", "");

        DllConfig.cob_init(0, IntPtr.Zero);
        //executa o arquivo .dll na memoria da API
        DllConfig.DELECLI(wrapper.BufferMemoria);
                
        string statusRetorno = wrapper.ExtrairCampo(4);

        if (statusRetorno == "44")
        {
           throw new KeyNotFoundException("Cliente não encontrado.");
        }
        else if (statusRetorno != "00")
        {
            throw new Exception("Erro ao deletar o cliente.");
        }
    }
}