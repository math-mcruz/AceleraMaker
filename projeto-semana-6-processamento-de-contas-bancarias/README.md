#  Projeto 4 - Processamento de Contas Bancárias 

## Arquitetura do Sistema

* **`CONTMAIN.cbl`:** MAIN onde está o fluxo de execução e invoca os submódulos.
* **`CONTREAD.cbl`:** Leitura do arquivo ordenado, com entrada de dados utilizando REGCONTA(COPYBOOK) e verificação EOF para final de arquivo.
* **`CONTDATA.cbl`:** Exibe cada conta com dados formatados.
* **`CONTCALC.cbl`:** Calcula o total de contas e seus saldos.
* **`CONTINFO.cbl`:** Exibe o relatório formatado, com o total de contas, saldo total, quantidade de agências, qual agência e seu saldo total.

## Ambiente Mainframe

Crie o dataset:
- `HERC01.CONTBANC.SOURCE`
- `HERC01.CONTBANC.JCL`
- `HERC01.CONTBANC.LOADLIB`
- `HERC01.CONTBANC.COPYLIB`

## JCLs
- Primeiro submeta (SUB) o `CONTCOMP` para compilar o projeto(todos os membros cobol(steps) são compilados no mesmo jcl).
- Depois submeta o `CONTEXE` para executar o projeto, e para que funcione deve ter os arquivos CONTAS.TXT e CONTAS.NOVAS.TXT (HERC01.CONTAS e HERC01.CONTAS.NOVAS)

## Observações 
Deixei os arquivos usados como exemplo na pasta `Execução do programa`, deve ter também o arquivo `HERC01.CONTBANC.RES`, se tiver problemas apenas comente o step 0 do CONTEXE, que o proprio JCL criara o arquivo.                                                                                    

