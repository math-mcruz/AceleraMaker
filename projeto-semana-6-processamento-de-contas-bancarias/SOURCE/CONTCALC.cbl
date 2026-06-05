       IDENTIFICATION                  DIVISION.                        00000101
       PROGRAM-ID. CONTCALC.                                            00000202
       AUTHOR.     MATHEUS CRUZ.                                        00000301
       ENVIRONMENT                      DIVISION.                       00000408
       DATA                            DIVISION.                        00000501
       LINKAGE                          SECTION.                        00000605
       77  LS-TOTAL-CONTAS        PIC 9(04).                            00000709
       77  LS-SALDO-TOTAL         PIC 9(14)V99.                         00000809
       01  LS-REG-CONTAS COPY REGCONTA.                                 00000909
       PROCEDURE DIVISION USING LS-TOTAL-CONTAS, LS-SALDO-TOTAL,        00001007
                                LS-REG-CONTAS.                          00001107
           ADD 1 TO LS-TOTAL-CONTAS.                                    00001207
           ADD SALDO TO LS-SALDO-TOTAL.                                 00001307
           GOBACK.                                                      00001401
