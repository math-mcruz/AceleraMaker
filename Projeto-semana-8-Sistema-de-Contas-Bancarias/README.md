#  Projeto 6 - Sistema de Contas Bancárias

## Arquitetura do Sistema

* **`TRANMAIN.cbl`:** Controlador do código que guia abertura e fechamento de arquivos, validações, controle de variáveis e de saída de dados.
* **`TRANREAD.cbl`:** Leitura dos arquivos de clientes e transações.
* **`TRANVALD.cbl`:** Validação de transações, se existe o cliente no arquivo clientes e se existe valor na transação.
* **`TRANRULE.cbl`:** Regra de transação e de gerenciamento da leitura do arquivo.
* **`TRANOUT.cbl`:**  Exibe relatório, estatística e envia dados para os arquivos de Erros e de Atualização.

## Ambiente Mainframe

Crie os datasets:
- `HERC01.TRANBANC.SOURCE`
- `HERC01.TRANBANC.JCL`
- `HERC01.TRANBANC.LOADLIB`
- `HERC01.TRANBANC.COPYLIB`

## JCLs

## Observações 
Deixei os arquivos usados como exemplo na pasta `ARQUIVOS(CONTAS)` e as prints da execução no TSO na pasta `Execução do programa`.                                                                                   

