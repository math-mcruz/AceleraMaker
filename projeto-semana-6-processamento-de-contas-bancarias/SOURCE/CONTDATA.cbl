       IDENTIFICATION                  DIVISION.                        00000101
       PROGRAM-ID. CONTDATA.                                            00000202
       AUTHOR.     MATHEUS CRUZ.                                        00000301
       ENVIRONMENT                     DIVISION.                        00000401
       CONFIGURATION                    SECTION.                        00000504
       SPECIAL-NAMES.                                                   00000604
           DECIMAL-POINT IS COMMA.                                      00000704
       DATA                            DIVISION.                        00000803
       WORKING-STORAGE                  SECTION.                        00000904
       77  WRK-SALDO-TELA PIC ZZZ.ZZZ.ZZ9,99.                           00001007
       LINKAGE                          SECTION.                        00001104
       01  LS-REG-CONTAS COPY REGCONTA.                                 00001206
       PROCEDURE DIVISION USING LS-REG-CONTAS.                          00001302
           MOVE SALDO TO WRK-SALDO-TELA.                                00001402
           IF SALDO > 0                                                 00001508
              PERFORM EXIBIR-CONTA                                      00001614
           ELSE                                                         00001708
              DISPLAY 'SALDO DA CONTA' NUM-CONTA 'INVALIDO!'.           00001815
           GOBACK.                                                      00001901
       EXIBIR-CONTA.                                                    00002014
              DISPLAY '=========== DADOS DA CONTA ============='.       00002113
              DISPLAY 'NUMERO DA CONTA: ' NUM-CONTA.                    00002213
              DISPLAY 'NOME: ' NOME-CLIENTE.                            00002313
              DISPLAY 'AGENCIA DA CONTA: ' AGENCIA.                     00002413
              IF CONTA-CORRENTE                                         00002513
                 DISPLAY 'TIPO DA CONTA: CORRENTE'.                     00002613
              IF CONTA-POUPANCA                                         00002713
                 DISPLAY 'TIPO DA CONTA: POUPANCA'.                     00002813
              IF NOT CONTA-CORRENTE AND NOT CONTA-POUPANCA              00002913
                 DISPLAY 'TIPO DA CONTA: NAO EXISTENTE'.                00003013
              DISPLAY 'SALDO: ' WRK-SALDO-TELA.                         00003113
              DISPLAY '========================================'.       00003213
