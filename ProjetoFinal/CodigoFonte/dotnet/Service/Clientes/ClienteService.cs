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
        wrapper.PayloadCobol(id.ToString(), "", "");
        try
            {
                DllConfig.cob_init(0, IntPtr.Zero);
                //executa o arquivo .dll na memoria da API
                DllConfig.CONSCLI(wrapper.BufferMemoria);
                int cliId = Convert.ToInt32(wrapper.ExtrairCampo(0));
                string cliNome = wrapper.ExtrairCampo(1);
                string statusRetorno = wrapper.ExtrairCampo(2);

                if (statusRetorno == "00")
                {
                    return new ClienteResponseDTO
                    {
                        Cli_Id = cliId,
                        Cli_Nome = cliNome,
                        StatusRetorno = statusRetorno
                    };
                }
                else if (statusRetorno == "44")
                {
                    throw new KeyNotFoundException("Não existem temas criados");
                }
                else
                {
                    throw new Exception("Erro ao consultar o cliente: " + statusRetorno);
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro interno");
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