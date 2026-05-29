       IDENTIFICATION                  DIVISION.                        00000101
       PROGRAM-ID.  SALBONUS.                                           00000201
       AUTHOR.      MATHEUS CRUZ.                                       00000301
       ENVIRONMENT                     DIVISION.                        00000401
       DATA                            DIVISION.                        00000501
       LINKAGE                         SECTION.                         00000601
           77 LS-TEMPEMPRESA      PIC 9(02)V99.                         00000703
           77 LS-BONUS-CALC       PIC 9(04)V99.                         00000803
       PROCEDURE DIVISION USING LS-TEMPEMPRESA, LS-BONUS-CALC.          00000902
           DISPLAY 'CALCULANDO BONUS'.                                  00001003
           IF LS-TEMPEMPRESA < 2                                        00001103
              MOVE 1.05 TO LS-BONUS-CALC.                               00001203
           IF LS-TEMPEMPRESA NOT < 2 AND LS-TEMPEMPRESA NOT > 5         00001303
              MOVE 1.1 TO LS-BONUS-CALC.                                00001403
           IF LS-TEMPEMPRESA > 5                                        00001503
              MOVE 1.15 TO LS-BONUS-CALC.                               00001603
           GOBACK.                                                      00001703
