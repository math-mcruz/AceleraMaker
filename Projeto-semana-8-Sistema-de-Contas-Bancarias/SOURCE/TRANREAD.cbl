       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANREAD.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       INPUT-OUTPUT                     SECTION.
       FILE-CONTROL.
           SELECT ARQ-TRAN ASSIGN 
           TO 'D:\Cobol\PROJETOBANC\DADOS\TRANSACOES.txt'
           ORGANIZATION IS LINE SEQUENTIAL.
       DATA                            DIVISION.
       FILE                             SECTION.
       FD  ARQ-TRAN
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           BLOCK CONTAINS 0 RECORDS
           DATA RECORD IS REG-TRAN-FD.
       01  REG-TRAN-FD       PIC X(80).
       WORKING-STORAGE                 SECTION. 
       EXEC SQL INCLUDE SQLCA END-EXEC.     
       LINKAGE                          SECTION.
       01  LS-CONTROLE-ARQ-READ.
           05 LS-EOF-TRAN      PIC X.
           05 LS-OPEN-READ     PIC X.
           05 LS-CLOSE-READ    PIC X.
       COPY REGTRAN.
       PROCEDURE DIVISION USING REG-TRANSACAO, LS-CONTROLE-ARQ-READ.
       READ-PROCEDURE.
           IF LS-OPEN-READ = 'S'
              PERFORM ABRIR-ARQUIVOS
           END-IF.   
           IF LS-CLOSE-READ = 'N'
      *VAI LER ATE A MAIN MANDAR FECHAR
              PERFORM LER-TRANSACAO
           ELSE
              PERFORM FECHAR-ARQUIVOS
           END-IF.   
           GOBACK.
       ABRIR-ARQUIVOS.
           OPEN INPUT ARQ-TRAN.
           MOVE 'N' TO LS-OPEN-READ.
                       
       LER-TRANSACAO.
           IF LS-EOF-TRAN = 'N'
              READ ARQ-TRAN INTO REG-TRANSACAO AT END
                        MOVE 'S' TO LS-EOF-TRAN
              END-READ
           END-IF.             
       FECHAR-ARQUIVOS.
           CLOSE ARQ-TRAN.