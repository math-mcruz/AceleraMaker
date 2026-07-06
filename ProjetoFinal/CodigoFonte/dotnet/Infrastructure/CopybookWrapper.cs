using System.Text;

namespace dotnet.Infrastructure
{
    public class CopybookWrapper
    {
        private readonly CopybookParser _parser;
        public byte[] BufferMemoria { get; internal set; }

        //recebe a leitura do COPYBOOK e cria o buffer de memoria com o tamanho total
        public CopybookWrapper(CopybookParser parser)
        {
            _parser = parser;
            BufferMemoria = new byte[_parser.TamanhoBuffer];
        }

        //converte os dados do C# para os bytes do COBOL
        public void PayloadCobol(params string[] valores)
        {
            StringBuilder payload = new StringBuilder();

            //percorre todos os campos que o Parser mapeou no Copybook
            for (int i = 0; i < _parser.Campos.Count; i++)
            {
                string valorFormatar = i < valores.Length ? valores[i] : "";
                payload.Append(FormatarCampo(valorFormatar, _parser.Campos[i]));
            }

            BufferMemoria = Encoding.ASCII.GetBytes(payload.ToString());
        }

        //função auxiliar para aplicar Zeros ou Espaços
        private string FormatarCampo(string valor, Copybook campo)
        {
            valor = valor ?? ""; //se for nulo vira string vazia

            if (campo.Tipo == "9") 
            {
                //limita o tamanho da string de entrada para não estourar o buffer caso venha maior
                if (valor.Length > campo.Tamanho) valor = valor.Substring(0, campo.Tamanho);
                
                return valor.PadLeft(campo.Tamanho, '0'); //numerico: 00005
            }
            else 
            {
                if (valor.Length > campo.Tamanho) valor = valor.Substring(0, campo.Tamanho);
                
                return valor.PadRight(campo.Tamanho, ' '); //string: NOME      
            }
        }

        //le o array de bytes e extrai os valores
        public string ExtrairCampo(int indiceCampo)
        {
            int offset = 0;
            //calcula onde o campo começa somando o tamanho dos anteriores
            for (int i = 0; i < indiceCampo; i++)
            {
                offset += _parser.Campos[i].Tamanho;
            }
            string textoCompleto = Encoding.ASCII.GetString(BufferMemoria);
            return textoCompleto.Substring(offset, _parser.Campos[indiceCampo].Tamanho).Trim();
        }
    }
}