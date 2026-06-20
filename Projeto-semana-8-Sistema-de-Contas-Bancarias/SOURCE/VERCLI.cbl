       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. VERCLI.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       CONFIGURATION                   SECTION. 
       INPUT-OUTPUT                    SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLI ASSIGN 
             TO 'D:\Cobol\PROJETOBANC\DADOS\CLIENTES.txt'
           ORGANIZATION IS LINE SEQUENTIAL.       
       DATA                            DIVISION.
       FILE                            SECTION.
       FD  ARQ-CLI
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           DATA RECORD IS REG-CLI-FD. 
       01  REG-CLI-FD                  PIC X(80).
       WORKING-STORAGE                 SECTION.
       LINKAGE                          SECTION. 
       01  LS-CONTROLE-CLI. 
           05  LS-EOF-CLI                  PIC X.
           05  LS-OPEN-CLI                 PIC X. 
       COPY REGCLI.
       PROCEDURE DIVISION USING REG-CLIENTE, LS-CONTROLE-CLI.
       VERCLI-PROCEDURE.
           IF LS-OPEN-CLI = 'S' 
              PERFORM ABRIR-ARQUIVO
           ELSE
              PERFORM LER-ARQUIVO
           END-IF.   
           IF LS-EOF-CLI = 'S'   
              PERFORM FECHAR-ARQUIVO
           END-IF.   
           GOBACK.  
       ABRIR-ARQUIVO.
           OPEN INPUT ARQ-CLI.
           MOVE 'N' TO LS-OPEN-CLI.
           PERFORM LER-ARQUIVO.
       LER-ARQUIVO.
           READ ARQ-CLI INTO REG-CLIENTE 
                AT END MOVE 'S' TO LS-EOF-CLI
           END-READ.
       FECHAR-ARQUIVO.  
           CLOSE ARQ-CLI.