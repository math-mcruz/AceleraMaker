using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Utils;

public static class Cores
{
    public static void CorMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
    }
    public static void ExibirSucesso(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
    public static void ExibirErro(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
    public static void Continuar(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }

    public static void CorOriginal()
    {
        Console.ResetColor();
    }
}
