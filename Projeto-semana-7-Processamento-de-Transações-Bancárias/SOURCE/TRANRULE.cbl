       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANRULE.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION.
       LINKAGE                          SECTION.
       01  LS-CONTADORES
           05  LS-COUNT-CLI     PIC 9(02).
           05  LS-COUNT-TRAN    PIC 9(02).
           05  LS-COUNT-CRED    PIC 9(02).
           05  LS-COUNT-DEB     PIC 9(02).
           05  LS-COUNT-ERRO    PIC 9(02).
       01  LS-ERRO
           05  LS-TIPO-ERRO         PIC X.
           05  LS-SAIDA-ERRO        PIC X(45).
       01  LS-REG-CLIENTES COPY REGCLI.
       01  LS-REG-TRANSACOES COPY REGTRAN.
       PROCEDURE DIVISION USING LS-REG-CLIENTES, LS-REG-TRANSACOES,
                                LS-LER, LS-CONTADORES,
                                LS-ERRO.
       RULE-PROCEDURE.
           MOVE SPACES TO LS-ERRO.
           PERFORM COMPARA-IDS.
           GOBACK.
       COMPARA-IDS.
           IF CLI-ID OF LS-REG-CLIENTES = CLI-ID OF LS-REG-TRANSACOES
              PERFORM PROCESSA-TRANSACAO
           ELSE
              IF CLI-ID OF LS-REG-CLIENTES <
                 CLI-ID OF LS-REG-TRANSACOES
                 PERFORM CLIENTE-MENOR
              ELSE
                 PERFORM TRANSACAO-MENOR.
       PROCESSA-TRANSACAO.
           IF TIPO-CREDITO
              ADD TRX-VALOR TO CLI-SALDO
              ADD 1 TO LS-COUNT-CRED
              ADD 1 TO LS-COUNT-TRAN
           ELSE
              IF CLI-SALDO > TRX-VALOR OR CLI-SALDO = TRX-VALOR
                 SUBTRACT TRX-VALOR FROM CLI-SALDO
                 ADD 1 TO LS-COUNT-DEB
                 ADD 1 TO LS-COUNT-TRAN
              ELSE
                 ADD 1 TO LS-COUNT-ERRO
                 MOVE 'S' TO LS-TIPO-ERRO
                 MOVE 'ERRO: SALDO INSUFICIENTE - ' TO LS-SAIDA-ERRO.
           MOVE 'T' TO LS-LER.
       CLIENTE-MENOR.
           ADD 1 TO LS-COUNT-CLI.
           MOVE 'C' TO LS-LER.
       TRANSACAO-MENOR.
           ADD 1 TO LS-COUNT-ERRO.
           MOVE 'C' TO LS-TIPO-ERRO.
           MOVE 'ERRO: CLIENTE NAO ENCONTRADO - ' TO LS-SAIDA-ERRO.
           MOVE 'T' TO LS-LER.
