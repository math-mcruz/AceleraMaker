       IDENTIFICATION                  DIVISION.                        00000103
       PROGRAM-ID.  SALCALC.                                            00000203
       AUTHOR.      MATHEUS CRUZ.                                       00000303
       ENVIRONMENT                     DIVISION.                        00000403
       DATA                            DIVISION.                        00000503
       LINKAGE                         SECTION.                         00000603
           77 LS-SALFINAL         PIC 9(05)V99.                         00000707
           77 LS-BONUS-CALC       PIC 9(04)V99.                         00000803
       PROCEDURE DIVISION USING LS-SALFINAL, LS-BONUS-CALC.             00000907
           DISPLAY 'CALCULANDO SALARIO TOTAL'.                          00001005
           COMPUTE LS-SALFINAL = LS-SALFINAL * LS-BONUS-CALC.           00001107
           GOBACK.                                                      00001203
