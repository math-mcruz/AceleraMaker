namespace BlogPessoal.Services.IA;

public class PromptBuilder
{
    //prompt com padrão
    public static string PromptResumo(string texto)
    {
        return $@"Você é um assistente de sistema analisando um blog. Leia o texto: '{texto}'.
                Retorne ESTRITAMENTE um objeto JSON válido (sem formatação markdown, sem blocos ```json) com as seguintes chaves exatas:
                - 'Resumo': um resumo muito curto de no máximo 3 linhas.
                - 'Tags': até 3 palavras-chave(Tags) separadas por vírgula.
                - 'Categoria': uma única palavra representando a categoria.
                Não escreva absolutamente mais nada além do JSON.";
    }
}
