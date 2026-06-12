#  Projeto 5 - Processamento de Transações Bancárias 

## Arquitetura do Sistema

* **`TRANMAIN.cbl`:** Controlador do codigo que guia abertura e fechamento de arquivos, validações, controle de variaveis e de saida de dados.
* **`TRANREAD.cbl`:** Leitura dos arquivos de clientes e transações, com fechamento de arquivo apenas quando os dois tiverem acabado.
* **`TRANVALD.cbl`:** Validação de transações se existe o cliente no arquivo clientes e se existe valor na transação.
* **`TRANRULE.cbl`:** Regra de transação e de grenciamento de qual arquivo vai ser lido.
* **`TRANOUT.cbl`:**  Exibe relatório, estatistica e envia dados para os arquivos de Erro e de Atualização.

## Ambiente Mainframe

Crie os datasets:
- `HERC01.TRANBANC.SOURCE`
- `HERC01.TRANBANC.JCL`
- `HERC01.TRANBANC.LOADLIB`
- `HERC01.TRANBANC.COPYLIB`

## JCLs
- Primeiro submeta (SUB) o `CONTCOMP` para compilar o projeto(todos os membros cobol(steps) são compilados no mesmo jcl).
- Em seguida submeta os arquivos de GDG e Molde caso ainda não possua no seu ambiente.
- Depois submeta o `CONTEXE` para executar o projeto, e para que funcione deve ter os arquivos CLIENTES.TXT e TRANSACOES.TXT (HERC01.TRANBANC.CLIENTES e HERC01.TRANBANC.TRANSAC)

## Observações 
Deixei os arquivos usados como exemplo na pasta `ARQUIVOS(CONTAS)` e as prints da execução no TSO na pasta `Execução do programa`.                                                                                   

