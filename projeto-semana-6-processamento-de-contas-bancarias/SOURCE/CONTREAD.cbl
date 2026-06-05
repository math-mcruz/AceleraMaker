       IDENTIFICATION                  DIVISION.                        00000102
       PROGRAM-ID. CONTREAD.                                            00000202
       AUTHOR.     MATHEUS CRUZ.                                        00000302
       ENVIRONMENT                     DIVISION.                        00000402
       INPUT-OUTPUT                    SECTION.                         00000502
       FILE-CONTROL.                                                    00000602
           SELECT CONTAS ASSIGN TO UT-S-CONTAS.                         00000710
       DATA                            DIVISION.                        00000802
       FILE                            SECTION.                         00000902
       FD  CONTAS                                                       00001004
           LABEL RECORDS ARE STANDARD                                   00001105
           RECORD CONTAINS 80 CHARACTERS                                00001205
           BLOCK CONTAINS 0 RECORDS                                     00001305
           DATA RECORD IS REG-CONTAS.                                   00001405
           01 FD-REGISTRO PIC X(80).                                    00001509
       WORKING-STORAGE                 SECTION.                         00001605
       77  WRK-STATUS-ARQUIVO PIC X(02) VALUE SPACES.                   00001712
       77  WRK-OPEN           PIC X(01) VALUE 'S'.                      00001812
       LINKAGE                         SECTION.                         00001905
       77  LS-EOF             PIC X(01).                                00002012
       01  LS-REG-CONTAS COPY REGCONTA.                                 00002112
       PROCEDURE DIVISION USING LS-REG-CONTAS, LS-EOF.                  00002205
           IF WRK-OPEN = 'S'                                            00002306
              OPEN INPUT CONTAS                                         00002406
              MOVE 'N' TO WRK-OPEN.                                     00002506
           READ CONTAS INTO LS-REG-CONTAS AT END MOVE 'S' TO LS-EOF.    00002609
           IF LS-EOF = 'S'                                              00002706
              CLOSE CONTAS.                                             00002806
           GOBACK.                                                      00002905
