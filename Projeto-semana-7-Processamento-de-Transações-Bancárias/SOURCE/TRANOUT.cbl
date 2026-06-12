       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANOUT.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       INPUT-OUTPUT                     SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLI-ATUALIZADO ASSIGN TO UT-S-CLIUP.
           SELECT ARQ-ERROS ASSIGN TO UT-S-ERROS.
       DATA                            DIVISION.
       FILE                             SECTION.
       FD  ARQ-CLI-ATUALIZADO
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           DATA RECORD IS REG-CLI-ATUALIZADO-FD.
           01 REG-CLI-ATUALIZADO-FD  PIC X(80).
       FD  ARQ-ERROS
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           DATA RECORD IS REG-ERROS-FD.
           01 REG-ERROS-FD               PIC X(80).
       WORKING-STORAGE                  SECTION.
       01  WRK-IMPRIME-ERRO.
           05  WRK-LINHA-VARIADA.
               07  WRK-TEXTO-VARIADO     PIC X(15) VALUE SPACES.
               07  WRK-VALOR             PIC 9(09) VALUE ZEROS.
           05  WRK-TEXTO-FIXO            PIC X(80) VALUE 'SAIDA:'.
           05  WRK-LINHA-ID
               07  WRK-TEXTO-ERRO        PIC X(40) VALUE SPACES.
               07  WRK-ID                PIC 9(05) VALUE ZEROS.
           05  WRK-LINHA-TRANSACAO       PIC X(20) VALUE SPACES.
       LINKAGE                          SECTION.
       01  LS-TIPO-SAIDA                 PIC X.
           88  TIPO-RELATORIO            VALUE 'R'.
           88  TIPO-ERRO                 VALUE 'E'.
           88  TIPO-FIM                  VALUE 'F'.
       01  LS-ERRO.
           05  LS-TIPO-ERRO              PIC X.
               88  SALDO-NEGATIVO        VALUE 'S'.
               88  OUTRO-TIPO            VALUE 'O'.
           05  LS-SAIDA-ERRO             PIC X(45).
       01  LS-CONTADORES.
           05  LS-COUNT-CLI              PIC 9(02).
           05  LS-COUNT-TRAN             PIC 9(02).
           05  LS-COUNT-CRED             PIC 9(02).
           05  LS-COUNT-DEB              PIC 9(02).
           05  LS-COUNT-ERRO             PIC 9(02).
           05  LS-COUNT-TOTAL.
               07  LS-TOTAL-DEB          PIC 9(09).
               07  LS-TOTAL-CRED         PIC 9(09).
      *VARIAVES DE ABRIR E FECHAR DOS ARQUIVOS DE SAIDA
       01  LS-CONTROLE-ARQ-OUT.
           05  LS-OPEN-OUT               PIC X.
           05  LS-CLOSE-OUT              PIC X.
       01  LS-REG-CLIENTES COPY REGCLI.
       01  LS-REG-TRANSACOES COPY REGTRAN.
       PROCEDURE DIVISION USING LS-REG-CLIENTES, LS-REG-TRANSACOES,
                                LS-CONTADORES, LS-TIPO-SAIDA,
                                LS-ERRO, LS-CONTROLE-ARQ-OUT.
       OUT-PROCEDURE.
      *    DISPLAY '>> ENTROU NO TRANOUT <<'.
      *    DISPLAY 'OPEN-OUT =' LS-OPEN-OUT.
      *    DISPLAY 'CLOSE-OUT =' LS-CLOSE-OUT.
           IF LS-OPEN-OUT = 'S'
              PERFORM ABRIR-ARQUIVOS.
           IF LS-CLOSE-OUT = 'N'
              PERFORM LOGICA-GRAVACAO
           ELSE
              PERFORM FECHAR-ARQUIVOS.
           GOBACK.
       ABRIR-ARQUIVOS.
           OPEN OUTPUT ARQ-CLI-ATUALIZADO, ARQ-ERROS.
           MOVE 'N' TO LS-OPEN-OUT.
       LOGICA-GRAVACAO.
           IF TIPO-RELATORIO
              PERFORM ATUALIZAR-CLIENTE
           ELSE
              IF TIPO-ERRO
                 PERFORM ESCREVER-ERRO
              ELSE
                 PERFORM EXIBIR-ESTATISTICA.
       ATUALIZAR-CLIENTE.
           WRITE REG-CLI-ATUALIZADO-FD FROM LS-REG-CLIENTES.
           DISPLAY '======================'.
           DISPLAY 'CLIENTE:' CLI-ID OF LS-REG-CLIENTES.
           DISPLAY 'TOTAL CREDITOS: ' LS-TOTAL-CRED.
           DISPLAY 'TOTAL DEBITOS: ' LS-TOTAL-DEB.
           MOVE ZEROS TO LS-TOTAL-CRED.
           MOVE ZEROS TO LS-TOTAL-DEB.
       ESCREVER-ERRO.
      *MONTANDO A SAIDA DE ERROS PARA O ARQUIVO
           IF SALDO-NEGATIVO
      *SAIDA PERSONALIZADA PARA SALDO NEGATIVO
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
              MOVE CLI-ID OF LS-REG-TRANSACOES TO WRK-ID
              WRITE REG-ERROS-FD FROM WRK-LINHA-ID
              MOVE SPACES TO WRK-TEXTO-ERRO
           ELSE
              MOVE LS-REG-TRANSACOES TO WRK-LINHA-TRANSACAO
              WRITE REG-ERROS-FD FROM WRK-LINHA-TRANSACAO
      *
              WRITE REG-ERROS-FD FROM WRK-TEXTO-FIXO
      *
              MOVE LS-SAIDA-ERRO TO WRK-TEXTO-ERRO
              MOVE CLI-ID OF LS-REG-TRANSACOES TO WRK-ID
              WRITE REG-ERROS-FD FROM WRK-LINHA-ID
              MOVE SPACES TO WRK-TEXTO-ERRO.
       EXIBIR-ESTATISTICA.
           DISPLAY '============================'.
           DISPLAY 'ESTATISTICA DE PROCESSAMENTO'.
           DISPLAY '============================'.
           DISPLAY 'CLIENTES PROCESSADOS.......: ' LS-COUNT-CLI.
           DISPLAY 'TRANSACOES PROCESSADAS.....: ' LS-COUNT-TRAN.
           DISPLAY 'CREDITOS PROCESSADOS.......: ' LS-COUNT-CRED.
           DISPLAY 'DEBITOS PROCESSADOS........: ' LS-COUNT-DEB.
           DISPLAY 'ERROS ENCONTRADOS..........: ' LS-COUNT-ERRO.
       FECHAR-ARQUIVOS.
           CLOSE ARQ-CLI-ATUALIZADO, ARQ-ERROS.
           PERFORM EXIBIR-ESTATISTICA.
