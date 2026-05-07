using Projeto1E2.Contas;
using Projeto1E2.Controller;
using Projeto1E2.Exceptions;
using System;
using System.Collections.Generic;


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

    public static Conta ExisteCadastro(ContaController controller, string texto, out int numeroDeletar)
    {
        numeroDeletar = 0;
        try
        {
            //texto para digitar o numero da conta
            Console.WriteLine(texto);
            numeroDeletar = Convert.ToInt32(Console.ReadLine());

            var contaBuscada = controller.BuscarNaCollection(numeroDeletar);

            if (contaBuscada != null)
            {
                return contaBuscada;
            }
            Console.WriteLine("Conta não encontrada");
        }
        catch(FormatException)
        {
            // Se não tiver essa conta
            Console.WriteLine("Digite numeros inteiros");
            
        }
        return null;
    }

}
            