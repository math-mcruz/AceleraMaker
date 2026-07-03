using System.Text;
namespace dotnet.Infrastructure
{
    public class CopybookWrapper
    {
        private readonly CopybookParser _parser;
        public byte[] BufferMemoria { get; private set; }

        // Construtor recebe o Parser já carregado
        public CopybookWrapper(CopybookParser parser)
        {
            _parser = parser;
            BufferMemoria = new byte[_parser.TamanhoBuffer];
        }

        // ESCRITA: Converte os dados do C# para os bytes do COBOL
        public void PayloadCobol(string id, string nome, string status)
        {
            StringBuilder payload = new StringBuilder();

            // Formata cada campo com base na regra que o Parser descobriu
            payload.Append(FormatarCampo(id, _parser.Campos[0]));
            payload.Append(FormatarCampo(nome, _parser.Campos[1]));
            payload.Append(FormatarCampo(status, _parser.Campos[2]));

            BufferMemoria = Encoding.ASCII.GetBytes(payload.ToString());
        }

        // Função auxiliar para aplicar Zeros ou Espaços
        private string FormatarCampo(string valor, Copybook campo)
        {
            valor = valor ?? ""; // Se for nulo, vira string vazia

            if (campo.Tipo == "9") 
            {
                return valor.PadLeft(campo.Tamanho, '0'); // Numérico: 00005
            }
            else 
            {
                return valor.PadRight(campo.Tamanho, ' '); // String: NOME      
            }
        }

        // LEITURA: Lê o array de bytes e extrai os valores
        public string ExtrairCampo(int indiceCampo)
        {
            int offset = 0; // Ponto de partida
            
            // Calcula onde o campo começa somando o tamanho dos anteriores
            for (int i = 0; i < indiceCampo; i++)
            {
                offset += _parser.Campos[i].Tamanho;
            }

            string textoCompleto = Encoding.ASCII.GetString(BufferMemoria);
            return textoCompleto.Substring(offset, _parser.Campos[indiceCampo].Tamanho).Trim();
        }
    }
}