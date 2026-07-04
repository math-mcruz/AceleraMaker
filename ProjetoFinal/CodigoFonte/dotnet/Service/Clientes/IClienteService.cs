using dotnet.DTOs.Clientes;

namespace dotnet.Service.Clientes;

public interface IClienteService
{
    ClienteResponseDTO Consultar(int id);
    ClienteResponseDTO Cadastrar(ClienteRequestDTO cliRequestDto);
    ClienteResponseDTO Atualizar(int id, ClienteUpdateDTO cliUpdateDto);
    void Deletar(int id);
}