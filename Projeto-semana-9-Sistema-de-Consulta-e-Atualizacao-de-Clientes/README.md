#  Projeto 7 - Sistema de Consulta e Atualização de Clientes

## Arquitetura do Sistema

* **`CLIPMG.cbl`:** Responsável por gerenciar o menu principal do sistema, onde o usuário pode escolher entre consultar ou atualizar os dados dos clientes.
* **`CLIVSAM.cbl`:** Faz a leitura dos dados dos clientes armazenados no VSAM, permitindo a consulta das informações ou alterações dos dados.

## Ambiente Mainframe

Crie os datasets(simulando mainframe):
- `HERC01.SUABIBLIOTECA.SOURCE`
- `HERC01.SUABIBLIOTECA.JCL`
- `HERC01.SUABIBLIOTECA.LOADLIB`
- `HERC01.SUABIBLIOTECA.COPYLIB`

## JCLs
- Primeiro JCL a submeter é o `MAPCLI` é o mais importante, sua função é criar o mapa de tela do programa(o mapa esta na pasta BMS), que será utilizado pelo `CLIPMG` e `CLIVSAM`.
- Segundo JCL a submeter é o `CLIPMG` e o terceiro JCL a submeter é o `CLIVSAM`, que são os programas que irão executar as funções de consulta e atualização dos clientes.
- O quarto é o `CRIVSAM` que é o JCL responsável por criar o VSAM, onde os dados dos clientes serão armazenados, mas só é necessário submeter se não tiver um arquivo VSAM já criado.

## Observações 
- Prints da execução no TSO na pasta `Execução do programa`.
                                                                              

