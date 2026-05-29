       IDENTIFICATION                  DIVISION.                        00000101
       PROGRAM-ID.  SALRES.                                             00000201
       AUTHOR.      MATHEUS CRUZ.                                       00000301
       ENVIRONMENT                     DIVISION.                        00000401
       CONFIGURATION                   SECTION.                         00000506
       SPECIAL-NAMES.                                                   00000606
           DECIMAL-POINT IS COMMA.                                      00000706
       DATA                            DIVISION.                        00000801
       WORKING-STORAGE                 SECTION.                         00000905
           77 WRK-SALBASE-TELA       PIC ZZ.ZZ9,99.                     00001006
           77 WRK-SALFINAL-TELA      PIC ZZ.ZZ9,99.                     00001108
           77 WRK-BONUS-TELA         PIC ZZ.ZZ9,99.                     00001206
       LINKAGE                         SECTION.                         00001302
           77 LS-BONUS-CALC          PIC 9(04)V99.                      00001408
           77 LS-SALFINAL            PIC 9(05)V99.                      00001508
           01 LS-FUNCIONARIO.                                           00001602
              05 LS-NOME         PIC X(30).                             00001705
              05 LS-SALBASE      PIC 9(05)V99.                          00001805
       PROCEDURE DIVISION USING LS-FUNCIONARIO, LS-BONUS-CALC,          00001904
           LS-SALFINAL.                                                 00002007
           MOVE LS-BONUS-CALC TO WRK-BONUS-TELA.                        00002107
           MOVE LS-SALBASE TO WRK-SALBASE-TELA.                         00002207
           MOVE LS-SALFINAL TO WRK-SALFINAL-TELA.                       00002307
           DISPLAY '====== SALARIO FINAL ======'.                       00002407
           DISPLAY 'NOME          = ' LS-NOME.                          00002507
           DISPLAY 'SALARIO BASE  = ' WRK-SALBASE-TELA.                 00002607
           DISPLAY 'BONUS         = ' WRK-BONUS-TELA.                   00002707
           DISPLAY 'SALARIO FINAL = ' WRK-SALFINAL-TELA.                00002807
           GOBACK.                                                      00002907
