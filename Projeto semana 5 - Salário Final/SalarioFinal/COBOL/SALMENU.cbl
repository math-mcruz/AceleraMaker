       IDENTIFICATION                  DIVISION.                        00000101
       PROGRAM-ID.  SALMENU.                                            00000204
       AUTHOR.      MATHEUS CRUZ.                                       00000304
       ENVIRONMENT                     DIVISION.                        00000402
       DATA                            DIVISION.                        00000502
       WORKING-STORAGE                 SECTION.                         00000602
           77  WRK-OPCAO               PIC 9     VALUE ZEROS.           00000719
           77  WRK-BONUS-CALC          PIC 9(04)V99 VALUE ZEROS.        00000818
           77  WRK-SALFINAL            PIC 9(05)V99 VALUE ZEROS.        00000918
           01 WRK-FUNCIONARIO.                                          00001006
              05 WRK-NOME          PIC X(30) VALUE SPACES.              00001110
              05 WRK-SALBASE       PIC 9(05)V99 VALUE ZEROS.            00001206
              05 WRK-TEMPEMPRESA   PIC 9(02)V99 VALUE ZEROS.            00001306
       PROCEDURE                       DIVISION.                        00001402
       MAIN-PROCEDURE.                                                  00001519
            PERFORM VALIDA-DADOS UNTIL WRK-OPCAO = 2                    00001617
            DISPLAY 'ENCERRANDO MENU'.                                  00001717
            STOP RUN.                                                   00001817
      *================== LOGICA MENU ====================              00001917
       VALIDA-DADOS.                                                    00002007
            DISPLAY '==== MENU CALCULO DO SALARIO ===='.                00002107
            DISPLAY '[1] - CALCULAR SALARIO'.                           00002207
            DISPLAY '[2] - SAIR'.                                       00002307
            ACCEPT WRK-OPCAO.                                           00002406
            IF NOT WRK-OPCAO = 2                                        00002507
               PERFORM ENTRADA-DADOS                                    00002607
               PERFORM CALCULA-BONUS                                    00002707
               PERFORM CALCULA-SALARIO                                  00002807
               PERFORM EXIBE-RESULTADO.                                 00002923
       ENTRADA-DADOS.                                                   00003007
            ACCEPT WRK-FUNCIONARIO.                                     00003107
            CALL 'SALDADOS' USING WRK-FUNCIONARIO.                      00003207
       CALCULA-BONUS.                                                   00003307
            CALL 'SALBONUS' USING WRK-TEMPEMPRESA, WRK-BONUS-CALC.      00003411
       CALCULA-SALARIO.                                                 00003517
            MOVE WRK-SALBASE TO WRK-SALFINAL.                           00003614
            CALL 'SALCALC' USING WRK-SALFINAL, WRK-BONUS-CALC.          00003714
       EXIBE-RESULTADO.                                                 00003817
            CALL 'SALRES' USING WRK-FUNCIONARIO, WRK-BONUS-CALC,        00003916
            WRK-SALFINAL.                                               00004016
