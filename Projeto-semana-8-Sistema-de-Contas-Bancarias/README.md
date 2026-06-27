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
- Deixei os arquivos usados como exemplo na pasta `ARQUIVOS(CONTAS)` e as prints da execução na pasta `Execução do programa`.
- Não consegui configurar a IDE para deixar o sistema mais modulado com submodulos recebendo os dados pela LINKAGE SECTION, não descobri o motivo e deixei em uma ssó arquivo que funcionou, mas o código está todo comentado e organizado para facilitar a leitura e entendimento do programa. 
- Algumas limitações não consegui resolver, como usar o COPY do DBCLI e DBTRAN e usar o Cursor (tive que simular fazendo manualmente)                                                                                 

