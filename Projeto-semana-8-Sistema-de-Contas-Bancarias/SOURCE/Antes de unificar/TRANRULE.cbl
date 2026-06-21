       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANRULE.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                 SECTION. 
       EXEC SQL INCLUDE SQLCA END-EXEC.
       EXEC SQL BEGIN DECLARE SECTION END-EXEC.
       01  HV-CLI-ID    PIC S9(9).
       01  HV-CLI-NOME  PIC X(30).
       01  HV-CLI-SALDO PIC S9(9). 
       EXEC SQL END DECLARE SECTION END-EXEC. 
       LINKAGE                          SECTION.
       01  LS-CONTROLE-ARQ-READ.
           05  LS-EOF-TRAN      PIC X.
      *CONTABILIZACAO PARA A ESTATISTICA, ERRO E RELATORIO
       01  LS-CONTADORES.
           05  LS-COUNT-TRAN    PIC 9(02).
           05  LS-COUNT-ERRO    PIC 9(02).
      *VERIFICAR ERRO DE CLIENTE OU DE SALDO
       01  LS-ERRO.
           05  LS-TIPO-ERRO         PIC X.
           05  LS-SAIDA-ERRO        PIC X(40).
           05  LS-STATUS-ERRO       PIC X(30).
       COPY REGCLI.    
       COPY REGTRAN.
       PROCEDURE DIVISION USING REG-CLIENTE, REG-TRANSACAO, 
                                LS-CONTROLE-ARQ-READ, LS-CONTADORES, 
                                LS-ERRO.
       RULE-PROCEDURE.
           MOVE SPACES TO LS-ERRO.
           PERFORM BUSCA-CLIENTE.
           PERFORM COMPARA-IDS.
           GOBACK.
       BUSCA-CLIENTE.
           MOVE CLI-ID OF REG-TRANSACAO TO HV-CLI-ID.
           EXEC SQL
               SELECT CLI_NOME, CLI_SALDO 
                 INTO :HV-CLI-NOME, :HV-CLI-SALDO
                 FROM CLIENTES
                WHERE CLI_ID = :HV-CLI-ID
           END-EXEC.
           IF SQLCODE = 0
               MOVE HV-CLI-ID    TO CLI-ID OF REG-CLIENTE
               MOVE HV-CLI-NOME  TO CLI-NOME OF REG-CLIENTE
               MOVE HV-CLI-SALDO TO CLI-SALDO OF REG-CLIENTE
           END-IF. 
           
      *LOGICA PRICIPAL PARA FAZER AS OPERACOES
       COMPARA-IDS.
           IF SQLCODE = 100
              MOVE 'O' TO LS-TIPO-ERRO
              MOVE 'ERRO: CLIENTE NAO ENCONTRADO -       ID' 
                   TO LS-SAIDA-ERRO
              MOVE 'ERRO: CLIENTE NAO ENCONTRADO ' TO LS-STATUS-ERRO
           ELSE
              IF SQLCODE = 0
                 MOVE HV-CLI-ID    TO CLI-ID OF REG-CLIENTE
                 MOVE HV-CLI-NOME  TO CLI-NOME OF REG-CLIENTE
                 MOVE HV-CLI-SALDO TO CLI-SALDO OF REG-CLIENTE
                 PERFORM PROCESSA-TRANSACAO
              END-IF
           END-IF.    
                   
       PROCESSA-TRANSACAO.
      *SE FOR CREDITO ADICIONA NO SALDO E ACRECENTA OS CONTADORES
           IF TRX-TIPO = 'C'
              ADD TRX-VALOR TO CLI-SALDO
              ADD 1 TO LS-COUNT-TRAN
      *MANDAR PARA O BANCO ***************************************!!        
           ELSE
      *SE FOR DEBITO SUBTRAI O SALDO E ACRECENTA OS CONTADORES
              PERFORM CALCULA-DEBITO.
       CALCULA-DEBITO.
      *SE SALDO MAIOR QUE VALOR AI FAZ O DEBITO
           IF CLI-SALDO > TRX-VALOR OR CLI-SALDO = TRX-VALOR
              SUBTRACT TRX-VALOR FROM CLI-SALDO
              ADD 1 TO LS-COUNT-TRAN
      *MANDAR PARA O BANCO ***************************************!!        
           ELSE
              ADD 1 TO LS-COUNT-ERRO
              MOVE 'S' TO LS-TIPO-ERRO
              MOVE 'ERRO: SALDO INSUFICIENTE -           ID'
                    TO LS-SAIDA-ERRO
              MOVE 'ERRO: SALDO INSUFICIENTE  '
                    TO LS-STATUS-ERRO.       