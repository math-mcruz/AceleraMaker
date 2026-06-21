#  Projeto 6 - Sistema de Contas Bancárias

## Detalhes do Projeto
Esse projeto foi desenvolvido no Gix_IDE(baseada em GixSQL, encontrado nesse repositório: `github.com/mridoni/gix/releases`), com DB2 e COBOL, foi utilizado um driver da IBM ODBC para a conexão e configuração do ambiente. Para que o programa funcione, deve compilar o projeto na Gix_IDE, abrir o Prompt de comando (CMD) na pasta do projeto e ir até o `\bin`, e setar esse comando(importante adaptar para o seu caminho) para a IDE funcionar corretamente: 

`set PATH=%PATH%;D:\GIX-IDE\Gix-IDE\lib\x64\gcc;C:\Users\SeuPerfil\AppData\Local\Gix\compiler-pkgs\gnucobol-3.1.2-windows-mingw-x64\bin`

Depois desse comando, basta executar o executável do projeto:

`SISBANC.exe`

## Arquitetura do Sistema

* **`SISBANC.cbl`:** 

## Ambiente Mainframe

Crie os datasets(simulando mainframe):
- `HERC01.TRANBANC.SOURCE`
- `HERC01.TRANBANC.JCL`
- `HERC01.TRANBANC.LOADLIB`
- `HERC01.TRANBANC.COPYLIB`

## JCLs
- Foi feito apenas uma simulação do JCL, este projeto foi compilado por GnuCOBOL.

## Observações 
Deixei os arquivos usados como exemplo na pasta `ARQUIVOS(CONTAS)` e as prints da execução no TSO na pasta `Execução do programa`.                                                                                   

