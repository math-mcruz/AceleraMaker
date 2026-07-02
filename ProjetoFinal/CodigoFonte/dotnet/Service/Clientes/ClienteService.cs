using dotnet.DTOs.Clientes;

namespace dotnet.Service.Clientes;

public class TemaService : IClienteService
{
    //para implemetar aqui primeiro preciso fazer a parte do Copybook
    public ClienteResponseDTO GET()
    {
        throw new NotImplementedException();
    }

    public ClienteResponseDTO POST(ClienteRequestDTO cliRequestDto)
    {
        throw new NotImplementedException();
    }

    public ClienteResponseDTO PUT(int id, ClienteUpdateDTO cliUpdateDto)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}