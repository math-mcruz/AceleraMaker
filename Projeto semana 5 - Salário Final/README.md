#  Projeto 3 - Salário Final 

## Arquitetura do Sistema

* **`SALMENU.cbl`:** MAIN que exibe o menu interativo no TSO, gerencia o fluxo de execução e invoca os submódulos.
* **`SALDADOS.cbl`:** Entrada de dados utilizando comandos ACCEPT para guardar o nome, salário base e tempo de empresa.
* **`SALBONUS.cbl`:** Calcula a porcentagem de bonus pelo tempo de empresa.
* **`SALCALC.cbl`:** Calcula o salário final com o bonus.
* **`EXIBERES.cbl`:** Exibe os dados com uma formatação.


## Ambiente Mainframe

Crie o dataset:
- `HERC01.SALARIO.COBOL`
- `HERC01.SALARIO.JCL`
- `HERC01.SALARIO.LOAD`

Submeta os JCLs de compilação na ordem:
- `CDADOS`, `CBONUS`, `CCALC`, `CRES`.
- `CMENU` por último, garantindo LINKAGE vai ter referências externas corretas.

Aperte f3 até aparecer console nativo do TSO (`READY`), e digite os comandos um por um (enter em cada um separado):
- `ALLOC FI(SYSOUT) DA(*)`
- `ALLOC FI(SYSIN) DA(*)`
- `CALL 'HERC01.SALARIO.LOAD(SALMENU)'`
