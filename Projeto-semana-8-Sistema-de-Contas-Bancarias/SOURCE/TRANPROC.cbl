       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. TRANPROC.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       DATA                            DIVISION.
       WORKING-STORAGE                  SECTION. 
       COPY REGTRAN.
 
       77  WRK-VALIDO             PIC X VALUE SPACES.
       01  WRK-TIPO-SAIDA         PIC X VALUE SPACES.
      *VARIAVEIS PARA ESCREVER NO ARQUIVO ERROS
       01  WRK-ERRO.
           05  WRK-TIPO-ERRO      PIC X VALUE SPACES.
           05  WRK-SAIDA-ERRO     PIC X(40) VALUE SPACES.
           05  WRK-STATUS-ERRO    PIC X(30) VALUE SPACES.
      *VARIAVEIS DE CONTROLE PARA OS ARQUIVOS DE ENTRADA
       01  WRK-CONTROLE-ARQ-READ.
           05  WRK-EOF-TRAN       PIC X VALUE 'N'.
           05  WRK-OPEN-READ      PIC X VALUE 'S'.
           05  WRK-CLOSE-READ     PIC X VALUE 'N'.
      *VARIAVEIS DE CONTROLE PARA OS ARQUIVOS DE SAIDA
       01  WRK-CONTROLE-ARQ-OUT.
           05  WRK-OPEN-OUT       PIC X VALUE 'S'.
           05  WRK-CLOSE-OUT      PIC X VALUE 'N'.
      *VARIAVEIS CONTADORES PARA RELATORIOS
       01  WRK-CONTADORES.
           05  WRK-COUNT-SUB      PIC 9(03) VALUE ZEROS. 
           05  WRK-COUNT-REG      PIC 9(03) VALUE ZEROS.
           05  WRK-COUNT-TRAN     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-ERRO     PIC 9(02) VALUE ZEROS.
       EXEC SQL INCLUDE SQLCA END-EXEC.
       LINKAGE                     SECTION.
       COPY REGCLI. 
       PROCEDURE DIVISION USING REG-CLIENTE.
       TRAN-PROCEDURE.
           PERFORM ABRIR-ARQUIVOS.
           PERFORM PROCESSAR-TRANSACOES.
           PERFORM FECHAR-ARQUIVOS.
           GOBACK.
       ABRIR-ARQUIVOS.
           PERFORM LER-ARQUIVOS.
       PROCESSAR-TRANSACOES.
              IF WRK-EOF-TRAN = 'S'
                 MOVE 'S' TO WRK-CLOSE-OUT
              ELSE
      *ADICIONA A CONTAGEM DE REGISTROS        
                 ADD 1 TO WRK-COUNT-REG  
                 PERFORM VALIDA-TRANSACAO
                 IF WRK-VALIDO = 'S'
                    PERFORM REGRA-TRANSACAO
                    PERFORM ANALISA-SAIDA
                 ELSE
                    MOVE 'O' TO WRK-TIPO-ERRO
                    PERFORM ANALISA-SAIDA
                 END-IF
                 PERFORM LER-ARQUIVOS
              END-IF.     
       ANALISA-SAIDA.
              IF WRK-TIPO-ERRO = 'S' OR WRK-TIPO-ERRO = 'O'
                 MOVE 'E' TO WRK-TIPO-SAIDA
                 PERFORM PROCESSAR-SAIDA
              END-IF.              
       FECHAR-ARQUIVOS.
           MOVE 'S' TO WRK-CLOSE-READ.
           MOVE 'S' TO WRK-CLOSE-OUT.
           PERFORM PROCESSAR-SAIDA.
           PERFORM LER-ARQUIVOS.
      *ONDE FICAM AS CHAMADAS DOS OUTROS MODULOS
       LER-ARQUIVOS.  
           CALL 'TRANREAD' USING REG-TRANSACAO, WRK-CONTROLE-ARQ-READ.
       VALIDA-TRANSACAO.
           CALL 'TRANVALD' USING REG-TRANSACAO, WRK-VALIDO,
                                 WRK-COUNT-ERRO, WRK-ERRO.
       REGRA-TRANSACAO.
           CALL 'TRANRULE' USING REG-CLIENTE, REG-TRANSACAO, 
                                 WRK-CONTROLE-ARQ-READ, WRK-CONTADORES, 
                                 WRK-ERRO.
       PROCESSAR-SAIDA.
           CALL 'TRANOUT' USING REG-CLIENTE, REG-TRANSACAO, 
                                WRK-CONTADORES, WRK-TIPO-SAIDA, 
                                WRK-ERRO, WRK-CONTROLE-ARQ-OUT.