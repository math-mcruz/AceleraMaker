       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANMAIN.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION.
       77  WRK-VALIDO             PIC X VALUE SPACES.
       01  WRK-TIPO-SAIDA         PIC X VALUE SPACES.
       01  WRK-ERRO
           05  WRK-TIPO-ERRO      PIC X VALUE SPACES.
           05  WRK-SAIDA-ERRO     PIC X(45) VALUE SPACES.
       01  WRK-CONTROLE-ARQ-READ
           05  WRK-LER            PIC X VALUE 'A'.
           05  WRK-EOF-CLI        PIC X VALUE 'N'.
           05  WRK-EOF-TRAN       PIC X VALUE 'N'.
           05  WRK-OPEN-READ      PIC X VALUE 'S'.
           05  WRK-CLOSE-READ     PIC X VALUE 'N'.
       01  WRK-CONTROLE-ARQ-OUT
           05  WRK-OPEN-OUT       PIC X VALUE 'S'.
           05  WRK-CLOSE-OUT      PIC X VALUE 'N'.
       01  WRK-CONTADORES
           05  WRK-COUNT-CLI      PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-TRAN     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-CRED     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-DEB      PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-ERRO     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-TOTAL
               07  WRK-TOTAL-DEB  PIC 9(09) VALUE ZEROS.
               07  WRK-TOTAL-CRED PIC 9(09) VALUE ZEROS.
       01  REG-CLIENTES COPY REGCLI.
       01  REG-TRANSACOES COPY REGTRAN.
       PROCEDURE                       DIVISION.
       MAIN-PROCEDURE
           PERFORM PROCESSAR-TRANSACOES UNTIL WRK-EOF-CLI = 'S' AND
                                      WRK-EOF-TRAN = 'S'.
           MOVE 'F' TO WRK-TIPO-SAIDA.
           PERFORM PROCESSAR-SAIDA.
           MOVE 'S' TO WRK-CLOSE-READ.
           MOVE 'S' TO WRK-CLOSE-OUT.
           PERFORM LER-ARQUIVOS.
           STOP RUN.
       PROCESSAR-TRANSACOES.
           PERFORM LER-ARQUIVOS.
           IF WRK-EOF-CLI = 'N' OR WRK-EOF-TRAN = 'N'
              PERFORM VALIDA-TRANSACAO
              IF WRK-VALIDO = 'S'
                 PERFORM REGRA-TRANSACAO
                 IF WRK-TIPO-ERRO = 'S'
                    MOVE 'E' TO WRK-TIPO-SAIDA
                    PERFORM PROCESSAR-SAIDA
                 ELSE
                    IF WRK-LER = 'C'
                       MOVE 'R' TO WRK-TIPO-SAIDA
                       PERFORM PROCESSAR-SAIDA
              ELSE
                 MOVE 'E' TO WRK-TIPO-SAIDA
                 PERFORM PROCESSAR-SAIDA
                 MOVE 'T' TO WRK-LER.
       LER-ARQUIVOS.
           CALL 'TRANREAD' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-CONTROLE-ARQ-READ.
       VALIDA-TRANSACAO.
           CALL 'TRANVALD' USING REG-TRANSACOES, WRK-VALIDO,
                                 WRK-COUNT-ERRO, WRK-ERRO.
       REGRA-TRANSACAO.
           CALL 'TRANRULE' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-LER, WRK-CONTADORES,
                                 WRK-ERRO.
       PROCESSAR-SAIDA.
           CALL 'TRANOUT' USING REG-CLIENTES, REG-TRANSACOES,
                                WRK-CONTADORES, WRK-TIPO-SAIDA,
                                WRK-ERRO, WRK-CONTROLE-ARQ-READ.
