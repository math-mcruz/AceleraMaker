using dotnet.Config.DllConfig;
using dotnet.DTOs.Clientes;
using dotnet.Infrastructure;

namespace dotnet.Service.Clientes;

public class ClienteService : IClienteService
{
    public ClienteResponseDTO Consultar(int id)
    {
        var parser = new CopybookParser(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\COPYLIB\REGCLI.cpy");
            // 2. Inicia o Wrapper
        var wrapper = new CopybookWrapper(parser);
            
            // 3. Monta o payload (ID, Nome vazio, Status vazio)
        wrapper.PayloadCobol(id.ToString(), "", "", "", "");

        DllConfig.cob_init(0, IntPtr.Zero);
                //executa o arquivo .dll na memoria da API
        DllConfig.CONSCLI(wrapper.BufferMemoria);
                
        int cliId = Convert.ToInt32(wrapper.ExtrairCampo(0));
        string cliNome = wrapper.ExtrairCampo(1);
        string telefone = wrapper.ExtrairCampo(2);
        string email = wrapper.ExtrairCampo(3);    
        string statusRetorno = wrapper.ExtrairCampo(4);

        if (statusRetorno == "00")
        {
            return new ClienteResponseDTO
            {
                Cli_Id = cliId,
                Cli_Nome = cliNome,
                Telefone = telefone,
                Email = email,
                StatusRetorno = statusRetorno
            };
        }
        else if (statusRetorno == "44")
        {
            throw new KeyNotFoundException("Cliente não encontrado: " + statusRetorno);
        }
        else
        {
                throw new Exception("Erro ao consultar o cliente: " + statusRetorno);
        }
    }
    

    public ClienteResponseDTO Cadastrar(ClienteRequestDTO cliRequestDto)
    {
        throw new NotImplementedException();
    }

    public ClienteResponseDTO Atualizar(int id, ClienteUpdateDTO cliUpdateDto)
    {
        throw new NotImplementedException();
    }

    public void Excluir(int id)
    {
        throw new NotImplementedException();
    }
}