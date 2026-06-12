       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANVALD.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION.
       01  WRK-TIPO             PIC X VALUE SPACES.
           88  WRK-TIPO-VALIDO VALUE 'S'.
       01  WRK-VALOR            PIC X VALUE SPACES.
           88  WRK-VALOR-VALIDO VALUE 'S'.
       LINKAGE                          SECTION.
       77  LS-VALIDO            PIC X.
       77  LS-COUNT-ERRO        PIC 9(02).
       01  LS-ERRO.
           05  LS-TIPO-ERRO         PIC X.
           05  LS-SAIDA-ERRO        PIC X(45).
       01  LS-REG-TRANSACOES COPY REGTRAN.
       PROCEDURE DIVISION USING LS-REG-TRANSACOES, LS-VALIDO,
                                LS-COUNT-ERRO, LS-ERRO.
       VALIDATION-PROCEDURE.
           MOVE 'N' TO WRK-TIPO, WRK-VALOR.
           PERFORM VERIFICA-TIPO.
           PERFORM VERIFICA-VALOR.
           PERFORM VERIFICA-VALIDO.
           GOBACK.
       VERIFICA-TIPO.
           IF TIPO-CREDITO OR TIPO-DEBITO
              MOVE 'S' TO WRK-TIPO
           ELSE
              ADD 1 TO LS-COUNT-ERRO
              MOVE 'T' TO LS-TIPO-ERRO
              MOVE 'ERRO: TIPO DE TRANSACAO INVALIDO - '
                    TO LS-SAIDA-ERRO.
       VERIFICA-VALOR
           IF TRX-VALOR NOT = 0
              MOVE 'S' TO WRK-VALOR
           ELSE
              ADD 1 TO LS-COUNT-ERRO.
              MOVE 'V' TO LS-TIPO-ERRO
              MOVE 'ERRO: VALOR DE TRANSACAO INVALIDO - '
                    TO LS-SAIDA-ERRO.
       VERIFICA-VALIDO.
           IF WRK-TIPO-VALIDO AND WRK-VALOR-VALIDO
              MOVE 'S' TO LS-VALIDO
           ELSE
              MOVE 'N' TO LS-VALIDO.
