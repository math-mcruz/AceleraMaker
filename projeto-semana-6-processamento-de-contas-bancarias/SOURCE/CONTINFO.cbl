       IDENTIFICATION                  DIVISION.                        00000102
       PROGRAM-ID. CONTINFO.                                            00000202
       AUTHOR.     MATHEUS CRUZ.                                        00000302
       ENVIRONMENT                     DIVISION.                        00000402
       CONFIGURATION                    SECTION.                        00000502
       SPECIAL-NAMES.                                                   00000602
           DECIMAL-POINT IS COMMA.                                      00000702
       DATA                            DIVISION.                        00000802
       WORKING-STORAGE                  SECTION.                        00000902
       77  WRK-ST-TELA PIC ZZ.ZZZ.ZZZ.ZZZ.ZZ9,99.                       00001008
       77  WRK-TC-TELA PIC ZZ.ZZ9.                                      00001108
       77  WRK-SA-TELA PIC ZZ.ZZZ.ZZZ.ZZZ.ZZ9,99.                       00001219
       77  WRK-IDX                PIC 9(02) VALUE 1.                    00001312
       LINKAGE                          SECTION.                        00001402
       77  LS-TOTAL-CONTAS        PIC 9(04).                            00001509
       77  LS-SALDO-TOTAL         PIC 9(14)V99.                         00001606
       77  LS-COUNT               PIC 9(02).                            00001711
       01  LS-LISTA-AGENCIAS.                                           00001812
           05 LS-LISTA-AGENCIA OCCURS 50 TIMES.                         00001915
              10  LS-NUM-AGENCIA      PIC 9(04).                        00002012
              10  LS-TOTAL-AGENCIA    PIC 9(14)V99.                     00002112
       PROCEDURE DIVISION USING LS-TOTAL-CONTAS, LS-SALDO-TOTAL,        00002210
                                LS-COUNT, LS-LISTA-AGENCIAS.            00002316
           MOVE LS-SALDO-TOTAL TO WRK-ST-TELA.                          00002404
           MOVE LS-TOTAL-CONTAS TO WRK-TC-TELA.                         00002504
           DISPLAY '========= RELATORIO DO PROCESSAMENTO =========='.   00002607
           DISPLAY 'TOTAL DE CONTAS: ' WRK-TC-TELA.                     00002705
           DISPLAY 'SALDO TOTAL: ' WRK-ST-TELA.                         00002805
           DISPLAY 'QUANTIDADE AGENCIAS: ' LS-COUNT.                    00002914
           PERFORM EXIBIR-AGENCIAS VARYING WRK-IDX FROM 1 BY 1          00003016
                                   UNTIL WRK-IDX > LS-COUNT.            00003118
           DISPLAY '================================================'.  00003205
           GOBACK.                                                      00003302
       EXIBIR-AGENCIAS.                                                 00003416
           MOVE LS-TOTAL-AGENCIA (WRK-IDX) TO WRK-SA-TELA.              00003519
           DISPLAY '--------------------------------------'.            00003621
           DISPLAY 'AGENCIA: ' LS-NUM-AGENCIA (WRK-IDX).                00003717
           DISPLAY 'SALDO: ' WRK-SA-TELA.                               00003819
           MOVE ZEROS TO WRK-SA-TELA.                                   00003919
