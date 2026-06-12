       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANRULE.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       LINKAGE                          SECTION.
       01  LS-CONTROLE-ARQ-READ.
           05  LS-LER           PIC X.
           05  LS-EOF-CLI       PIC X.
           05  LS-EOF-TRAN      PIC X.
      *CONTABILIZACAO PARA A ESTATISTICA, ERRO E RELATORIO
       01  LS-CONTADORES.
           05  LS-COUNT-CLI     PIC 9(02).
           05  LS-COUNT-TRAN    PIC 9(02).
           05  LS-COUNT-CRED    PIC 9(02).
           05  LS-COUNT-DEB     PIC 9(02).
           05  LS-COUNT-ERRO    PIC 9(02).
           05  LS-COUNT-TOTAL.
               07  LS-TOTAL-DEB          PIC 9(09).
               07  LS-TOTAL-CRED         PIC 9(09).
      *VERIFICAR ERRO DE CLIENTE OU DE SALDO
       01  LS-ERRO.
           05  LS-TIPO-ERRO         PIC X.
           05  LS-SAIDA-ERRO        PIC X(40).
       01  LS-REG-CLIENTES COPY REGCLI.
       01  LS-REG-TRANSACOES COPY REGTRAN.
       PROCEDURE DIVISION USING LS-REG-CLIENTES, LS-REG-TRANSACOES,
                                LS-CONTROLE-ARQ-READ, LS-CONTADORES,
                                LS-ERRO.
       RULE-PROCEDURE.
           MOVE SPACES TO LS-ERRO.
           PERFORM COMPARA-IDS.
           GOBACK.
      *LOGICA PRICIPAL PARA FAZER AS OPERACOES
       COMPARA-IDS.
           IF LS-EOF-CLI = 'S' AND LS-EOF-TRAN = 'S'
      *FAZER A ULTIMA LEITURA
              MOVE 'F' TO LS-LER
           ELSE
      *SE TRANSACAO ACABOU SO LE CLIENTE
              IF LS-EOF-TRAN = 'S'
                 MOVE 'C' TO LS-LER
              ELSE
      *SE TIVER TRANSACAO E FOR MENOR QUE CLIENTE PASSA PARA A PROX
                 IF LS-EOF-CLI = 'S'
                    PERFORM TRANSACAO-MENOR
                 ELSE
      *SE FOR IGUAL FAZ A TRANSACAO
                    IF CLI-ID OF LS-REG-CLIENTES =
                       CLI-ID OF LS-REG-TRANSACOES
                       PERFORM PROCESSA-TRANSACAO
                    ELSE
      *ID DO CLIENTE MENOR TROCA CLIENTE
                       IF CLI-ID OF LS-REG-CLIENTES <
                          CLI-ID OF LS-REG-TRANSACOES
                          PERFORM CLIENTE-MENOR
                       ELSE
                          PERFORM TRANSACAO-MENOR.
       PROCESSA-TRANSACAO.
      *SE FOR CREDITO ADICIONA NO SALDO E ACRECENTA OS CONTADORES
           IF TIPO-CREDITO
              ADD TRX-VALOR TO CLI-SALDO
              ADD TRX-VALOR TO LS-TOTAL-CRED
              ADD 1 TO LS-COUNT-CRED
              ADD 1 TO LS-COUNT-TRAN
           ELSE
      *SE FOR DEBITO SUBTRAI O SALDO E ACRECENTA OS CONTADORES
              PERFORM CALCULA-DEBITO.
           MOVE 'T' TO LS-LER.
       CLIENTE-MENOR.
           ADD 1 TO LS-COUNT-CLI.
           MOVE 'C' TO LS-LER.
       TRANSACAO-MENOR.
           ADD 1 TO LS-COUNT-ERRO.
           MOVE 'O' TO LS-TIPO-ERRO.
           MOVE 'ERRO: CLIENTE NAO ENCONTRADO -       ID'
                    TO LS-SAIDA-ERRO.
           MOVE 'T' TO LS-LER.
       CALCULA-DEBITO.
      *SE SALDO MAIOR QUE VALOR AI FAZ O DEBITO
           IF CLI-SALDO > TRX-VALOR OR CLI-SALDO = TRX-VALOR
              SUBTRACT TRX-VALOR FROM CLI-SALDO
              ADD 1 TO LS-COUNT-DEB
              ADD 1 TO LS-COUNT-TRAN
              ADD TRX-VALOR TO LS-TOTAL-DEB
           ELSE
              ADD 1 TO LS-COUNT-ERRO
              MOVE 'S' TO LS-TIPO-ERRO
              MOVE 'ERRO: SALDO INSUFICIENTE -           ID'
                    TO LS-SAIDA-ERRO.
