       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANMAIN.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION.
       77  WRK-VALIDO            PIC X VALUE SPACES.
       01  WRK-CONTROLE-ARQ
           05  WRK-LER           PIC X VALUE 'A'.
           05  WRK-EOF-CLI       PIC X VALUE 'N'.
           05  WRK-EOF-TRAN      PIC X VALUE 'N'.
           05  WRK-OPEN          PIC X VALUE 'S'.
           05  WRK-CLOSE         PIC X VALUE 'N'.
       01  WRK-CONTADORES
           05  WRK-COUNT-CLI     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-TRAN    PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-CRED    PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-DEB     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-ERRO    PIC 9(02) VALUE ZEROS.
       01  REG-CLIENTES COPY REGCLI.
       01  REG-TRANSACOES COPY REGTRAN.
       PROCEDURE                       DIVISION.
       MAIN-PROCEDURE
           PERFORM PROCESSAR-TRANSACOES UNTIL WRK-EOF-CLI = 'S' AND
                                      WRK-EOF-TRAN = 'S'.
           PERFORM EXIBIR-ESTATISTICA.
           STOP RUN.
       PROCESSAR-TRANSACOES.
           PERFORM LER-ARQUIVOS.
           IF WRK-EOF-CLI = 'N' OR WRK-EOF-TRAN = 'N'
              PERFORM VALIDACAO-DADOS
              IF WRK-VALIDO = 'S'
                 PERFORM REGRA-TRANSACAO
              ELSE
      *LEMBRAR DE IMPLEMENTAR A LOGICA DE QUEM VAI ANDAR
                 MOVE 'A' TO WRK-LER.
       LER-ARQUIVOS.
           CALL 'TRANREAD' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-CONTROLE-ARQ.
       VALIDACAO-DADOS.
           CALL 'TRANVALD' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-VALIDO.
       REGRA-TRANSACAO.
           CALL 'TRANRULE' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-LER.
       EXIBIR-ESTATISTICA.
           CALL 'TRANOUT' USING REG-CLIENTES, REG-TRANSACOES,
                                WRK-CONTADORES.
           MOVE 'S' TO WRK-CLOSE.
               PERFORM LER-ARQUIVOS.
