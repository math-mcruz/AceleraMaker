using System;
using System.Collections.Generic;
using System.IO;

namespace dotnet.Infrastructure
{
    public class Copybook
    {
        public string? Nome { get; set; }
        //"X" String e "9" para numerico
        public string? Tipo { get; set; } 
        public int Tamanho { get; set; }
    }

    public class CopybookParser
    {
        public List<Copybook> Campos { get; private set; } = new List<Copybook>();
        public int TamanhoBuffer { get; private set; } = 0;

        // O construtor lê o ficheiro e faz a magia do Split
        public CopybookParser(string caminhoCpy)
        {
            // Lê todas as linhas do ficheiro .cpy
            string[] linhas = File.ReadAllLines(caminhoCpy);

            foreach (string linha in linhas)
            {
                // Limpa os espaços em branco para criar um array perfeito
                string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Procura onde está a palavra "PIC" no array
                int indexPic = Array.IndexOf(partes, "PIC");

                // Se encontrou "PIC" e ainda há texto depois dele...
                if (indexPic != -1 && partes.Length > indexPic + 1)
                {
                    string nomeCampo = partes[indexPic - 1]; // O texto antes de "PIC" (ex: LK-NOME)
                    string tipoETamanho = partes[indexPic + 1]; // O texto depois de "PIC" (ex: X(30).)

                    // A primeira letra é o tipo (X ou 9)
                    string tipo = tipoETamanho.Substring(0, 1);

                    // Apanha o que está entre parênteses para saber o tamanho
                    int startIndex = tipoETamanho.IndexOf('(') + 1;
                    int endIndex = tipoETamanho.IndexOf(')');

                    if (startIndex > 0 && endIndex > startIndex)
                    {
                        string tamanhoStr = tipoETamanho.Substring(startIndex, endIndex - startIndex);
                        
                        if (int.TryParse(tamanhoStr, out int tamanho))
                        {
                            // Adiciona à nossa lista de campos!
                            Campos.Add(new Copybook 
                            { 
                                Nome = nomeCampo, 
                                Tipo = tipo, 
                                Tamanho = tamanho 
                            });
                            //soma o total do tamanho do registro
                            TamanhoBuffer += tamanho;
                        }
                    }
                }
            }
        }
    }
}