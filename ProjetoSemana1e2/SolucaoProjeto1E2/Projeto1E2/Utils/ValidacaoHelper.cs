using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Utils;

public static class ValidacaoHelper
{
    public static string TextoMenu(string texto)
    {
        while (true)
        {
            Console.WriteLine(texto);
            string? resposta = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(resposta))
            {
                return resposta;
            }

            Console.WriteLine("O campo não pode ser vazio, digite um texto válido.");
        }
    }

    public static byte OpcaoRestricao(string texto, byte primeiraOpcao, byte segundaOpcao)
    {
        while(true)
        {
            try
            {
                Console.WriteLine(texto);
                byte opcaoEscolhida = Convert.ToByte(Console.ReadLine());

                if (opcaoEscolhida == primeiraOpcao || opcaoEscolhida == segundaOpcao)
                {
                    return opcaoEscolhida;

                }
                else
                {
                    Console.WriteLine($"Opção inválida, digite {primeiraOpcao} ou {segundaOpcao}.");
                }
            }
            catch (FormatException)
            {
                // Se o usuário digitar string ou numero além de 1 ou 2 da erro
                Console.WriteLine("Digite apenas números.\n");
            }

        }

    }

    public static int ValorPositivo(string texto)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(texto);
                int valor = Convert.ToInt32(Console.ReadLine());

                if (valor > 0)
                {
                    return valor;
                }
                else
                {
                    Console.WriteLine("Digite um valor positivo.");
                }
            }
            catch (FormatException)
            {
                // Se o usuário digitar string
                Console.WriteLine("Digite apenas números.\n");
            }
        }
    }
}
            