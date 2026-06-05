       IDENTIFICATION                  DIVISION.                        00000103
       PROGRAM-ID. CONTMAIN.                                            00000203
       AUTHOR.     MATHEUS CRUZ.                                        00000303
       ENVIRONMENT                     DIVISION.                        00000403
       DATA                            DIVISION.                        00000503
       WORKING-STORAGE                  SECTION.                        00000609
       77  WRK-EOF               PIC X(01) VALUE 'N'.                   00000719
       77  WRK-TOTAL-CONTAS      PIC 9(04) VALUE ZEROS.                 00000819
       77  WRK-SALDO-TOTAL       PIC 9(14)V99 VALUE ZEROS.              00000919
       77  WRK-SALDO-AGENCIA     PIC 9(14)V99 VALUE ZEROS.              00001019
       77  WRK-AGENCIA-ANTERIOR  PIC 9(04) VALUE ZEROS.                 00001119
       77  WRK-COUNT             PIC 9(02) VALUE ZEROS.                 00001219
       01  LISTA-AGENCIAS                                               00001319
           05 LISTA-AGENCIA OCCURS 50 TIMES.                            00001423
              10  NUM-AGENCIA    PIC 9(04).                             00001523
              10  TOTAL-AGENCIA  PIC 9(14)V99.                          00001623
       01  REG-CONTAS COPY REGCONTA.                                    00001711
       PROCEDURE                       DIVISION.                        00001806
       MAIN-PROCEDURE                                                   00001906
            PERFORM LER-ARQUIVO UNTIL WRK-EOF = 'S'.                    00002006
            PERFORM EXIBIR-INFO.                                        00002110
            STOP RUN.                                                   00002206
       LER-ARQUIVO.                                                     00002306
            CALL 'CONTREAD' USING REG-CONTAS, WRK-EOF.                  00002406
            IF WRK-EOF = 'N'                                            00002507
               PERFORM PROCESSAR-CONTA.                                 00002626
       EXIBIR-DADOS.                                                    00002707
            CALL 'CONTDATA' USING REG-CONTAS.                           00002807
       CALCULAR-CONTAS.                                                 00002907
            IF SALDO > 0                                                00003018
              CALL 'CONTCALC' USING WRK-TOTAL-CONTAS, WRK-SALDO-TOTAL,  00003118
                                  REG-CONTAS.                           00003213
       EXIBIR-INFO.                                                     00003307
            IF WRK-AGENCIA-ANTERIOR NOT = ZEROS                         00003424
                   PERFORM LOGICA-AGENCIA.                              00003524
            CALL 'CONTINFO' USING WRK-TOTAL-CONTAS, WRK-SALDO-TOTAL,    00003619
                                  WRK-COUNT, LISTA-AGENCIAS.            00003722
       PROCESSAR-CONTA.                                                 00003826
             IF WRK-AGENCIA-ANTERIOR = ZEROS                            00003926
                MOVE AGENCIA TO WRK-AGENCIA-ANTERIOR.                   00004026
             IF AGENCIA NOT = WRK-AGENCIA-ANTERIOR                      00004126
                PERFORM LOGICA-AGENCIA.                                 00004226
             ADD SALDO TO WRK-SALDO-AGENCIA.                            00004327
             PERFORM EXIBIR-DADOS.                                      00004427
             PERFORM CALCULAR-CONTAS.                                   00004526
       LOGICA-AGENCIA.                                                  00004628
            ADD 1 TO WRK-COUNT.                                         00004728
            MOVE WRK-AGENCIA-ANTERIOR TO NUM-AGENCIA (WRK-COUNT).       00004828
            MOVE WRK-SALDO-AGENCIA    TO TOTAL-AGENCIA (WRK-COUNT).     00004928
            MOVE ZEROS   TO WRK-SALDO-AGENCIA.                          00005028
            MOVE AGENCIA TO WRK-AGENCIA-ANTERIOR.                       00005128
