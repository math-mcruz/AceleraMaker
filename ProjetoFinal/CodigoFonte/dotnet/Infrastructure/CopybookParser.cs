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

        //faz o Split
        public CopybookParser(string caminhoCpy)
        {
            //le as linhas COPYBOOK
            string[] linhas = File.ReadAllLines(caminhoCpy);

            foreach (string linha in linhas)
            {
                //deixa sem os espaços
                string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                //procura o "PIC" no array
                int indexPic = Array.IndexOf(partes, "PIC");

                //se encontrou "PIC" e exite mais partes
                if (indexPic != -1 && partes.Length > indexPic + 1)
                {
                    //antes do PIC é o nome do campo
                    string nomeCampo = partes[indexPic - 1]; 
                    //depois do PIC é o tipo e tamanho
                    string tipoETamanho = partes[indexPic + 1]; 

                    //primeira letra é o tipo (X ou 9)
                    string tipo = tipoETamanho.Substring(0, 1);

                    //verifica o que esta entre os parenteses para pegar o tamanho
                    int startIndex = tipoETamanho.IndexOf('(') + 1;
                    int endIndex = tipoETamanho.IndexOf(')');

                    //se encontrou os parenteses e o tamanho é maior que 0
                    if (startIndex > 0 && endIndex > startIndex)
                    {
                        string tamanhoStr = tipoETamanho.Substring(startIndex, endIndex - startIndex);
                        
                        if (int.TryParse(tamanhoStr, out int tamanho))
                        {
                            //adiciona o campo na lista de campos
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