       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANREAD.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       INPUT-OUTPUT                     SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLI ASSIGN TO UT-S-CLIENTES.
           SELECT ARQ-TRAN ASSIGN TO UT-S-TRANSAC.
       DATA                            DIVISION.
       FILE                             SECTION.
       FD  ARQ-CLI
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           BLOCK CONTAINS 0 RECORDS
           DATA RECORD IS REG-CLI-FD.
           01 REG-CLI-FD       PIC X(80).
       FD  ARQ-TRAN
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           BLOCK CONTAINS 0 RECORDS
           DATA RECORD IS REG-TRAN-FD.
           01 REG-TRAN-FD      PIC X(80).
       LINKAGE                          SECTION.
       01  LS-CONTROLE-ARQ-READ.
           05 LS-LER           PIC X.
      *C = CLIENTE, T = TRANSACAO, A = AMBOS
              88  LER-CLI      VALUE 'C'.
              88  LER-TRAN     VALUE 'T'.
              88  LER-AMBOS    VALUE 'A'.
           05 LS-EOF-CLI       PIC X.
           05 LS-EOF-TRAN      PIC X.
           05 LS-OPEN-READ     PIC X.
           05 LS-CLOSE-READ    PIC X.
       01  LS-REG-CLIENTES COPY REGCLI.
       01  LS-REG-TRANSACOES COPY REGTRAN.
       PROCEDURE DIVISION USING LS-REG-CLIENTES, LS-REG-TRANSACOES,
                                LS-CONTROLE-ARQ-READ.
       READ-PROCEDURE.
           IF LS-OPEN-READ = 'S'
              PERFORM ABRIR-ARQUIVOS.
           IF LS-CLOSE-READ = 'N'
      *VAI LER ATE A MAIN MANDAR FECHAR
              PERFORM LOGICA-MATCH
           ELSE
              PERFORM FECHAR-ARQUIVOS.
           GOBACK.
       ABRIR-ARQUIVOS.
           OPEN INPUT ARQ-CLI, ARQ-TRAN.
           MOVE 'N' TO LS-OPEN-READ.
       LOGICA-MATCH.
           IF LER-CLI
              PERFORM LER-CLIENTES
           ELSE
              IF LER-TRAN
                 PERFORM LER-TRANSACOES
              ELSE
                 IF LER-AMBOS
                    PERFORM LER-CLIENTES
                    PERFORM LER-TRANSACOES.
      *QUANDO ACABAR ELE ATUALIZA O EOF DE CLI E TRAN
       LER-CLIENTES.
           IF LS-EOF-CLI = 'N'
              READ ARQ-CLI INTO LS-REG-CLIENTES AT END
                        MOVE 'S' TO LS-EOF-CLI
           ELSE
              MOVE 'S' TO LS-EOF-CLI.
       LER-TRANSACOES.
           IF LS-EOF-TRAN = 'N'
              READ ARQ-TRAN INTO LS-REG-TRANSACOES AT END
                        MOVE 'S' TO LS-EOF-TRAN
           ELSE
              MOVE 'S' TO LS-EOF-TRAN.
       FECHAR-ARQUIVOS.
           CLOSE ARQ-CLI, ARQ-TRAN.
