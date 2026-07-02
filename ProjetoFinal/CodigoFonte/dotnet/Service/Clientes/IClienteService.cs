using dotnet.DTOs.Clientes;

namespace dotnet.Service.Clientes;

public interface IClienteService
{
    ClienteResponseDTO GET();
    ClienteResponseDTO POST(ClienteRequestDTO cliRequestDto);
    ClienteResponseDTO PUT(int id, ClienteUpdateDTO cliUpdateDto);
    void Delete(int id);
}