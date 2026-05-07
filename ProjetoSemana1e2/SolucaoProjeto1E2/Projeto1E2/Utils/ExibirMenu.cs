using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Utils;

public static class ExibirMenu
{
    public static byte Principal()
    //byte pois o numero é muito pequeno e economiza memória
    {
        //criar uma classe para aplicar cores no menu para não poluir o código principal
        Console.WriteLine("    Sistema Bancário");
        Console.WriteLine("\n\tMenu:\n");
        Console.WriteLine("[1] - Cadastrar");
        Console.WriteLine("[2] - Listar todas as contas");
        Console.WriteLine("[3] - Atualizar conta");
        Console.WriteLine("[4] - Deletar conta");
        //posso fazer um segundo menu para gerenciar as operações
        Console.WriteLine("[5] - Realizar operações");
        Console.WriteLine("[6] - Sair\n");

        return Convert.ToByte(Console.ReadLine());
    }
    public static byte Operacoes()
    //byte pois o numero é muito pequeno e economiza memória
    {
        Console.Clear();
        Console.WriteLine("\tOPERAÇÕES BANCÁRIAS\n");
        Console.WriteLine("[1] - Sacar");
        Console.WriteLine("[2] - Depositar");
        Console.WriteLine("[3] - Transferir");
        Console.WriteLine("[4] - Visualizar dados da conta");
        Console.WriteLine("[5] - Voltar para o menu principal");
        Console.WriteLine("\nDigite a operação desejada: ");

        return Convert.ToByte(Console.ReadLine());
    }
}
