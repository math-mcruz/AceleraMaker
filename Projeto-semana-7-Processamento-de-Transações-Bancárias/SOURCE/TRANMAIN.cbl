       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANMAIN.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION.
       77  WRK-VALIDO             PIC X VALUE SPACES.
       01  WRK-TIPO-SAIDA         PIC X VALUE SPACES.
      *VARIAVEIS PARA ESCREVER NO ARQUIVO ERROS
       01  WRK-ERRO.
           05  WRK-TIPO-ERRO      PIC X VALUE SPACES.
           05  WRK-SAIDA-ERRO     PIC X(40) VALUE SPACES.
      *VARIAVEIS DE CONTROLE PARA OS ARQUIVOS DE ENTRADA
       01  WRK-CONTROLE-ARQ-READ.
           05  WRK-LER            PIC X VALUE 'A'.
           05  WRK-EOF-CLI        PIC X VALUE 'N'.
           05  WRK-EOF-TRAN       PIC X VALUE 'N'.
           05  WRK-OPEN-READ      PIC X VALUE 'S'.
           05  WRK-CLOSE-READ     PIC X VALUE 'N'.
      *VARIAVEIS DE CONTROLE PARA OS ARQUIVOS DE SAIDA
       01  WRK-CONTROLE-ARQ-OUT.
           05  WRK-OPEN-OUT       PIC X VALUE 'S'.
           05  WRK-CLOSE-OUT      PIC X VALUE 'N'.
      *VARIAVEIS CONTADORES PARA ESTATISTICA E RELATORIO
       01  WRK-CONTADORES.
           05  WRK-COUNT-CLI      PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-TRAN     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-CRED     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-DEB      PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-ERRO     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-TOTAL.
               07  WRK-TOTAL-DEB  PIC 9(09) VALUE ZEROS.
               07  WRK-TOTAL-CRED PIC 9(09) VALUE ZEROS.
      *COPYBOOKS
       01  REG-CLIENTES COPY REGCLI.
       01  REG-TRANSACOES COPY REGTRAN.
       PROCEDURE                       DIVISION.
       MAIN-PROCEDURE
           PERFORM ABRIR-ARQUIVOS.
           PERFORM PROCESSAR-TRANSACOES UNTIL WRK-EOF-CLI = 'S' AND
                                              WRK-EOF-TRAN = 'S'.
           PERFORM FECHAR-ARQUIVOS.
           STOP RUN.
       ABRIR-ARQUIVOS.
      *READ QUAL TIPO DE LEITURA VAI LER OS ARQUIVOS COMECA COM AMBOS
           MOVE 'A' TO WRK-LER.
           PERFORM LER-ARQUIVOS.
           PERFORM VALIDA-TRANSACAO.
           IF WRK-VALIDO = 'S'
              PERFORM REGRA-TRANSACAO
              PERFORM ANALISA-SAIDA
           ELSE
              PERFORM ANALISA-SAIDA.
       PROCESSAR-TRANSACOES.
              IF WRK-EOF-TRAN = 'S' AND WRK-EOF-CLI = 'S'
                 MOVE 'S' TO WRK-CLOSE-OUT
              ELSE
                 IF WRK-EOF-TRAN = 'N' AND WRK-EOF-CLI = 'S'
                    MOVE 'T' TO WRK-LER
                    PERFORM LOGICA-TRANSACAO
                 ELSE
                    IF WRK-EOF-CLI = 'N' AND WRK-EOF-TRAN = 'S'
                       MOVE 'C' TO WRK-LER
                       PERFORM LOGICA-CLIENTE
                    ELSE
                       IF WRK-LER = 'T'
                          PERFORM LOGICA-TRANSACAO
                       ELSE
                          PERFORM LOGICA-CLIENTE.
       LOGICA-TRANSACAO.
           PERFORM LER-ARQUIVOS.
           IF WRK-EOF-TRAN = 'N'
              PERFORM VALIDA-TRANSACAO
              IF WRK-VALIDO = 'S'
                 PERFORM REGRA-TRANSACAO
                 PERFORM ANALISA-SAIDA
              ELSE
                 MOVE 'O' TO WRK-TIPO-ERRO
                 PERFORM ANALISA-SAIDA.
       LOGICA-CLIENTE.
              MOVE 'R' TO WRK-TIPO-SAIDA.
              PERFORM PROCESSAR-SAIDA.
              PERFORM LER-ARQUIVOS.
              IF WRK-EOF-TRAN = 'N'
                 PERFORM REGRA-TRANSACAO
                 PERFORM ANALISA-SAIDA.
       ANALISA-SAIDA.
              IF WRK-TIPO-ERRO = 'S' OR WRK-TIPO-ERRO = 'O'
                 MOVE 'E' TO WRK-TIPO-SAIDA
                 PERFORM PROCESSAR-SAIDA.
       FECHAR-ARQUIVOS.
      *FECHA TODOS ARQUIVOS
           MOVE 'S' TO WRK-CLOSE-READ.
           MOVE 'S' TO WRK-CLOSE-OUT.
           PERFORM PROCESSAR-SAIDA.
           PERFORM LER-ARQUIVOS.
      *
      *ONDE FICAM AS CHAMADAS DOS OUTROS MODULOS
       LER-ARQUIVOS.
           CALL 'TRANREAD' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-CONTROLE-ARQ-READ.
       VALIDA-TRANSACAO.
           CALL 'TRANVALD' USING REG-TRANSACOES, WRK-VALIDO,
                                 WRK-COUNT-ERRO, WRK-ERRO,
                                 WRK-CONTROLE-ARQ-READ.
       REGRA-TRANSACAO.
           CALL 'TRANRULE' USING REG-CLIENTES, REG-TRANSACOES,
                                 WRK-CONTROLE-ARQ-READ,
                                 WRK-CONTADORES, WRK-ERRO.
       PROCESSAR-SAIDA.
           CALL 'TRANOUT' USING REG-CLIENTES, REG-TRANSACOES,
                                WRK-CONTADORES, WRK-TIPO-SAIDA,
                                WRK-ERRO, WRK-CONTROLE-ARQ-OUT.
