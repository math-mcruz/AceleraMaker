       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANOUT.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       INPUT-OUTPUT                     SECTION.
       FILE-CONTROL.
           SELECT ARQ-LOG ASSIGN TO 'LOG.txt'
           ORGANIZATION IS LINE SEQUENTIAL.
           SELECT ARQ-ERROS ASSIGN TO 'ERROS.txt'
           ORGANIZATION IS LINE SEQUENTIAL.
       DATA                            DIVISION.
       FILE                             SECTION.
       FD  ARQ-LOG
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           DATA RECORD IS REG-LOG-FD.
           01 REG-LOG-FD  PIC X(80).
       FD  ARQ-ERROS
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           DATA RECORD IS REG-ERROS-FD.
           01 REG-ERROS-FD               PIC X(80).
       WORKING-STORAGE                  SECTION. 
      *VARIAVEIS PARA GRAVAR NOS ARQUIVOS DE ERRO E LOG  
       01  WRK-IMPRIME-ERRO.
           05  WRK-LINHA-VARIADA.
               07  WRK-TEXTO-VARIADO     PIC X(15) VALUE SPACES.
               07  WRK-VALOR             PIC 9(09) VALUE ZEROS.
           05  WRK-TEXTO-FIXO            PIC X(80) VALUE 'SAIDA:'.
           05  WRK-LINHA-ID.
               07  WRK-TEXTO-ERRO        PIC X(40) VALUE SPACES.
               07  WRK-ID                PIC 9(05) VALUE ZEROS.
           05  WRK-LINHA-TRANSACAO       PIC X(20) VALUE SPACES.
       01  WRK-LINHA-RESUMO.
           05  WRK-RESUMO-TEXTO   PIC X(35) VALUE SPACES.
           05  WRK-RESUMO-VALOR   PIC 9(09).    
       01  WRK-IMPRIME-LOG.
           05  WRK-LOG-TEXTO      PIC X(20) VALUE SPACES.
           05  WRK-LOG-VALOR      PIC X(10) VALUE SPACES.   
       EXEC SQL INCLUDE SQLCA END-EXEC.   
       LINKAGE                          SECTION.
       01  LS-TIPO-SAIDA                 PIC X.
           88  TIPO-RELATORIO            VALUE 'R'.
           88  TIPO-ERRO                 VALUE 'E'.
       01  LS-ERRO.
           05  LS-TIPO-ERRO              PIC X.
               88  SALDO-NEGATIVO        VALUE 'S'.
               88  OUTRO-TIPO            VALUE 'O'.
           05  LS-SAIDA-ERRO             PIC X(40).
           05  LS-STATUS-ERRO            PIC X(30).
       01  LS-CONTADORES.
           05  LS-COUNT-SUB              PIC 9(03). 
           05  LS-COUNT-REG              PIC 9(03).
           05  LS-COUNT-TRAN             PIC 9(02).
           05  LS-COUNT-ERRO             PIC 9(02).
      *VARIAVES DE ABRIR E FECHAR DOS ARQUIVOS DE SAIDA
       01  LS-CONTROLE-ARQ-OUT.
           05  LS-OPEN-OUT               PIC X.
           05  LS-CLOSE-OUT              PIC X.
       COPY REGCLI.
       COPY REGTRAN.
       PROCEDURE DIVISION USING REG-CLIENTE, REG-TRANSACAO, 
                                LS-CONTADORES, LS-TIPO-SAIDA, 
                                LS-ERRO, LS-CONTROLE-ARQ-OUT.
       OUT-PROCEDURE.
           IF LS-OPEN-OUT = 'S'
              PERFORM ABRIR-ARQUIVOS
           END-IF.   
           IF LS-CLOSE-OUT = 'N'
              PERFORM LOGICA-GRAVACAO
           ELSE
              PERFORM FECHAR-ARQUIVOS
           END-IF.   
           GOBACK.
       ABRIR-ARQUIVOS.
           OPEN OUTPUT ARQ-LOG, ARQ-ERROS.
           MOVE 'N' TO LS-OPEN-OUT.
       LOGICA-GRAVACAO.
           IF TIPO-RELATORIO
              PERFORM RELATORIO-CLIENTE
           ELSE
              PERFORM ESCREVER-ERRO.
       RELATORIO-CLIENTE.
           MOVE 'CLIENTE ID:      ' TO WRK-LOG-TEXTO. 
           MOVE CLI-ID OF REG-TRANSACAO TO WRK-LOG-VALOR.
           WRITE REG-LOG-FD FROM WRK-IMPRIME-LOG.
           MOVE 'TRANSACAO ID:    ' TO WRK-LOG-TEXTO.
           MOVE TRX-ID OF REG-TRANSACAO  TO WRK-LOG-VALOR.
           WRITE REG-LOG-FD FROM WRK-IMPRIME-LOG.
           MOVE 'TIPO TRANSACAO:  ' TO WRK-LOG-TEXTO.
           IF TRX-TIPO = 'C' 
               MOVE 'CREDITO' TO WRK-LOG-VALOR
           ELSE
               MOVE 'DEBITO ' TO WRK-LOG-VALOR
           END-IF.
           WRITE REG-LOG-FD FROM WRK-IMPRIME-LOG.
           MOVE 'VALOR TRANSACAO: ' TO WRK-LOG-TEXTO.
           MOVE TRX-VALOR OF REG-TRANSACAO TO WRK-LOG-VALOR.
           WRITE REG-LOG-FD FROM WRK-IMPRIME-LOG.
           MOVE 'STATUS:          ' TO WRK-LOG-TEXTO.
           IF TIPO-RELATORIO
               MOVE 'SUCESSO' TO WRK-LOG-VALOR
           ELSE
               MOVE 'ERRO   ' TO WRK-LOG-VALOR
           END-IF.
           WRITE REG-LOG-FD FROM WRK-IMPRIME-LOG.     
      *FAZ O RELATORIO NO TERMINAL   
           DISPLAY '==========================='.
           DISPLAY 'CLIENTE:' CLI-ID OF REG-CLIENTE.
           IF TRX-TIPO = 'C'
              DISPLAY 'OPERACAO: CREDITO'
           ELSE 
              DISPLAY 'OPERACAO: DEBITO'.        
           IF TIPO-RELATORIO
               DISPLAY 'STATUS: SUCESSO' 
           ELSE
               DISPLAY 'STATUS: ' LS-STATUS-ERRO.

       ESCREVER-ERRO.
           IF SALDO-NEGATIVO
      *SAIDA PARA ARQUIVO DE ERRO
      *SALDO NEGATIVO  
              MOVE 'SALDO CLIENTE: ' TO WRK-TEXTO-VARIADO
              MOVE CLI-SALDO TO WRK-VALOR
              WRITE REG-ERROS-FD FROM WRK-LINHA-VARIADA
              MOVE SPACES TO WRK-TEXTO-VARIADO
      *
              MOVE 'DEBITO: ' TO WRK-TEXTO-VARIADO
              MOVE TRX-VALOR TO WRK-VALOR
              WRITE REG-ERROS-FD FROM WRK-LINHA-VARIADA
              MOVE SPACES TO WRK-TEXTO-VARIADO
      *
              WRITE REG-ERROS-FD FROM WRK-TEXTO-FIXO
      *
              MOVE LS-SAIDA-ERRO TO WRK-TEXTO-ERRO
              MOVE CLI-ID OF REG-TRANSACAO TO WRK-ID
              WRITE REG-ERROS-FD FROM WRK-LINHA-ID
              MOVE SPACES TO WRK-TEXTO-ERRO
           ELSE
      *MANEIRA GENERICA DE ERRO
              MOVE REG-TRANSACAO TO WRK-LINHA-TRANSACAO
              WRITE REG-ERROS-FD FROM WRK-LINHA-TRANSACAO
      *
              WRITE REG-ERROS-FD FROM WRK-TEXTO-FIXO
      *
              MOVE LS-SAIDA-ERRO TO WRK-TEXTO-ERRO
              MOVE CLI-ID OF REG-TRANSACAO TO WRK-ID
              WRITE REG-ERROS-FD FROM WRK-LINHA-ID
              MOVE SPACES TO WRK-TEXTO-ERRO
           END-IF.   
           MOVE SPACES TO LS-TIPO-ERRO.
           PERFORM RELATORIO-CLIENTE.
                    
       RELATORIO-PROCESSAMENTO.
           WRITE REG-LOG-FD FROM '============================'.
           WRITE REG-LOG-FD FROM ' RELATORIO DO PROCESSAMENTO '.
           WRITE REG-LOG-FD FROM '============================'.
           MOVE 'REGISTROS LIDOS............: ' TO WRK-RESUMO-TEXTO.
           MOVE LS-COUNT-REG                    TO WRK-RESUMO-VALOR.
           WRITE REG-LOG-FD FROM WRK-LINHA-RESUMO.
           MOVE 'TRANSACOES PROCESSADAS.....: ' TO WRK-RESUMO-TEXTO.
           MOVE LS-COUNT-TRAN                   TO WRK-RESUMO-VALOR.
           WRITE REG-LOG-FD FROM WRK-LINHA-RESUMO.
           MOVE 'ERROS ENCONTRADOS..........: ' TO WRK-RESUMO-TEXTO.
           MOVE LS-COUNT-ERRO                   TO WRK-RESUMO-VALOR.
           WRITE REG-LOG-FD FROM WRK-LINHA-RESUMO.
           
       FECHAR-ARQUIVOS.
           PERFORM RELATORIO-PROCESSAMENTO. 
           CLOSE ARQ-LOG, ARQ-ERROS.