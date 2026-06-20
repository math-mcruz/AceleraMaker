       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANVALD.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION.
      *VARIAVEIS PARA GERENCIAR SE VAI PROCESSEGUIR OU SE FOR ERRO
       01  WRK-TIPO             PIC X VALUE SPACES.
           88  WRK-TIPO-VALIDO VALUE 'S'.
       01  WRK-VALOR            PIC X VALUE SPACES.
           88  WRK-VALOR-VALIDO VALUE 'S'.
       EXEC SQL INCLUDE SQLCA END-EXEC.    
       LINKAGE                          SECTION.
       77  LS-VALIDO            PIC X.
       01  LS-CONTADORES.
           05  LS-COUNT-ERRO        PIC 9(02).
       01  LS-ERRO.
           05  LS-TIPO-ERRO         PIC X.
           05  LS-SAIDA-ERRO        PIC X(40).
           05  LS-STATUS-ERRO       PIC X(30).

       COPY REGTRAN.
       PROCEDURE DIVISION USING REG-TRANSACAO, LS-VALIDO,
                                LS-CONTADORES, LS-ERRO.
       VALIDATION-PROCEDURE.
      *LIMPEZA
           MOVE 'N' TO LS-VALIDO.
           MOVE SPACES TO WRK-TIPO, WRK-VALOR.
           PERFORM VERIFICA-TIPO.
           PERFORM VERIFICA-VALOR.
           PERFORM VERIFICA-VALIDO.
           GOBACK.
       VERIFICA-TIPO.
      *QUALQUE COISA DIFERENTE VIRA INVALIDO E COLOCA NO ARQUIVO ERROS
           IF TRX-TIPO = 'C' OR TRX-TIPO = 'D'
              MOVE 'S' TO WRK-TIPO
           ELSE
              ADD 1 TO LS-COUNT-ERRO
              MOVE 'O' TO LS-TIPO-ERRO
              MOVE 'ERRO: TIPO DE TRANSACAO INVALIDO -  ID'
                    TO LS-SAIDA-ERRO
              MOVE 'ERRO: TIPO TRANSACAO INVALIDA'
                    TO LS-STATUS-ERRO.     
       VERIFICA-VALOR.
      *SE TRANSACAO FOR 0 COLOCA NO ARQUIVO ERROS
           IF TRX-VALOR NOT = 0
              MOVE 'S' TO WRK-VALOR
           ELSE
              ADD 1 TO LS-COUNT-ERRO
              MOVE 'O' TO LS-TIPO-ERRO
              MOVE 'ERRO: VALOR DE TRANSACAO INVALIDO - ID'
                    TO LS-SAIDA-ERRO
              MOVE 'ERRO: VALOR INVALIDO         '
                    TO LS-STATUS-ERRO.
       VERIFICA-VALIDO.
      *SE NAO TIVER ERROS A TRANSACAO FICA VALIDA
           IF WRK-TIPO-VALIDO AND WRK-VALOR-VALIDO
              MOVE 'S' TO LS-VALIDO
           ELSE
              MOVE 'N' TO LS-VALIDO.