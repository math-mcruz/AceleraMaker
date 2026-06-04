       IDENTIFICATION                  DIVISION.                        00000101
       PROGRAM-ID.  SALDADOS.                                           00000201
       AUTHOR.      MATHEUS CRUZ.                                       00000301
       ENVIRONMENT                     DIVISION.                        00000401
       DATA                            DIVISION.                        00000501
       LINKAGE                         SECTION.                         00000602
           01 LS-FUNCIONARIO.                                           00000702
              05 LS-NOME          PIC X(30).                            00000803
              05 LS-SALBASE       PIC 9(05)V99.                         00000903
              05 LS-TEMPEMPRESA   PIC 9(02)V99.                         00001003
       PROCEDURE DIVISION USING LS-FUNCIONARIO.                         00001102
           DISPLAY 'INSIRA OS DADOS'.                                   00001202
           DISPLAY 'NOME = '.                                           00001302
           ACCEPT LS-NOME.                                              00001402
           DISPLAY 'SALARIO = '.                                        00001502
           ACCEPT LS-SALBASE.                                           00001602
           DISPLAY 'TEMPO DE EMPRESA = '.                               00001702
           ACCEPT LS-TEMPEMPRESA.                                       00001802
           GOBACK.                                                      00001902
