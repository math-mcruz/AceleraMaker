       IDENTIFICATION                   DIVISION.
       PROGRAM-ID. SISBANC.
       AUTHOR.     MATHEUS CRUZ.
       
       ENVIRONMENT                      DIVISION.
       CONFIGURATION                    SECTION. 
       INPUT-OUTPUT                     SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLI ASSIGN TO 'CLIENTES.txt'
           ORGANIZATION IS LINE SEQUENTIAL. 
           SELECT ARQ-TRAN ASSIGN TO 'TRANSACOES.txt'
           ORGANIZATION IS LINE SEQUENTIAL.
           SELECT ARQ-LOG ASSIGN TO 'LOG.txt'
           ORGANIZATION IS LINE SEQUENTIAL.
           SELECT ARQ-ERROS ASSIGN TO 'ERROS.txt'
           ORGANIZATION IS LINE SEQUENTIAL.
           
       DATA                             DIVISION.
       FILE                             SECTION.
       FD  ARQ-CLI
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           BLOCK CONTAINS 0 RECORDS
           DATA RECORD IS REG-CLI-FD.
       01  REG-CLI-FD                   PIC X(80).
       FD  ARQ-TRAN
           LABEL RECORDS ARE STANDARD
           RECORD CONTAINS 80 CHARACTERS
           BLOCK CONTAINS 0 RECORDS
           DATA RECORD IS REG-TRAN-FD.
       01  REG-TRAN-FD       PIC X(80).
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

      *        VARIAVEIS PARA O ARQUIVO DE CLIENTES  
       77  WRK-EOF-CLI                  PIC X VALUE 'N'.
       77  WRK-OPEN-CLI                 PIC X VALUE 'S'.
       77  FIM-CURSOR                   PIC X VALUE 'N'.

      *        VARIAVEIS PARA O ARQUIVO DE TRANSACOES  
       77  WRK-EOF-TRAN                 PIC X VALUE 'N'.
       77  WRK-OPEN-TRAN                PIC X VALUE 'S'.
       77  WRK-CLOSE-TRAN               PIC X VALUE 'N'. 
        
      *        VARIAVEIS PARA OS ARQUIVOS DE SAIDA  
       77  WRK-EOF-OUT                  PIC X VALUE 'N'.
       77  WRK-OPEN-OUT                 PIC X VALUE 'S'.
       77  WRK-CLOSE-OUT                PIC X VALUE 'N'.  
       
      *     VARIAVEIS PARA A ETAPA DE VALIDACAO DE TRANSACAO    
       77  WRK-VALIDO             PIC X VALUE SPACES.     
       77  WRK-TIPO               PIC X VALUE SPACES.
       77  WRK-STATUS-VALOR       PIC X VALUE SPACES.

      *     VARIAVEL PARA A ETAPA DE REGRAS DE TRANSACAO     
       77  WRK-TIPO-SAIDA         PIC X VALUE SPACES.

      *     VARIAVEIS PARA A ETAPA DE SAIDA DO PROCESSAMENTO
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

      *VARIAVEIS PARA ESCREVER NO ARQUIVO ERROS
       01  WRK-ERRO.
           05  WRK-TIPO-ERRO      PIC X VALUE SPACES.
           05  WRK-SAIDA-ERRO     PIC X(40) VALUE SPACES.
           05  WRK-STATUS-ERRO    PIC X(30) VALUE SPACES. 

      *               VARIAVEIS CONTADORES  
       01  WRK-CONTADORES.
           05  WRK-COUNT-SUB      PIC 9(03) VALUE ZEROS. 
           05  WRK-COUNT-REG      PIC 9(03) VALUE ZEROS.
           05  WRK-COUNT-TRAN     PIC 9(02) VALUE ZEROS.
           05  WRK-COUNT-ERRO     PIC 9(02) VALUE ZEROS. 
           05  WRK-COUNT-COMMIT   PIC 9(03) VALUE ZEROS.              
 
      *        VARIAVEIS PARA A MANIPULACAO DO BANCO DE DADOS 
       EXEC SQL INCLUDE SQLCA END-EXEC.
       
       EXEC SQL BEGIN DECLARE SECTION END-EXEC.
       01  DB-NOME      PIC X(45) 
                    VALUE 'odbc://MEU_DB2COBOL?fixup_params=on'.
       01  DB-USER      PIC X(20) VALUE 'db2inst1'.
       01  DB-PASS      PIC X(20) VALUE 'SenhaForte123!'.
      *DBCLI  
       01  HV-CLI-ID    PIC S9(9).
       01  HV-CLI-NOME  PIC X(32).
       01  HV-CLI-SALDO PIC S9(9) COMP-3.
      *DBTRAN  
       01  TRX-CLI-ID   PIC S9(9).
       01  HV-TRX-ID    PIC S9(9). 
       01  HV-TRX-TIPO  PIC X(1).
       01  HV-TRX-VALOR PIC S9(9) COMP-3. 

       01  WRK-LAST-ID   PIC S9(9) VALUE ZEROS.
       01  WRK-PROX-ID   PIC S9(9) VALUE ZEROS. 

      * COPY DBCLI.
      * COPY DBTRAN. 

       EXEC SQL END DECLARE SECTION END-EXEC.
       
      *                       COPYBOOKS  
       COPY REGCLI.
       COPY REGTRAN.    
           
       PROCEDURE                        DIVISION.
       SISTEMA-BANCARIO-PROCEDURE.
      *         PRIMEIRA FASE: POPULAR OU ATUALIZAR CLIENTES  
           PERFORM PRIMEIRA-FASE.
      *         SEGUNDA FASE: FAZER A LOGICA DAS TRANSACOES E SALVAR     
           PERFORM SEGUNDA-FASE.
           STOP RUN.
      
      *                   FASES E SUAS ETAPAS  
       PRIMEIRA-FASE.
           PERFORM CONECTAR-BANCO.
           PERFORM VERIFICA-TABELAS.
           PERFORM ABRIR-ARQCLI.
           PERFORM PROCESSA-ARQCLI UNTIL WRK-EOF-CLI = 'S'.
           DISPLAY '======================================'.
           DISPLAY 'FIM DO PROCESSAMENTO DE CLIENTES'.
           PERFORM FECHAR-ARQCLI.
           
       SEGUNDA-FASE.    
           PERFORM ABRIR-ARQTRAN.
           PERFORM PROCESSAR-TRANSACOES UNTIL WRK-EOF-TRAN = 'S'.
           DISPLAY '======================================'.
           DISPLAY 'FIM DO PROCESSAMENTO DE TRANSACOES'.
           DISPLAY '======================================'.
           PERFORM MOSTRAR-CLIENTES.
           PERFORM FECHAR-ARQUIVOS.
           PERFORM DESCONECTAR-BANCO.
           
      *==============================================================     
      *LOGICA DE CONEXAO, CRIACAO, CONSULTA, ATUALIZACAO E FECHAMENTO
      *DO BANCO DE DADOS DB2
      *==============================================================  

      *             USO DO BANCO NA PRIMEIRA FASE  
       CONECTAR-BANCO.
           EXEC SQL
               CONNECT TO :DB-NOME USER :DB-USER USING :DB-PASS
           END-EXEC.
           IF SQLCODE NOT = 0
               DISPLAY "ERRO DE CONEXAO."
               PERFORM MOSTRA-ERRO
               STOP RUN
           END-IF. 

       VERIFICA-TABELAS. 
      *LIMPANDO TABELAS  
           EXEC SQL DROP TABLE CLIENTES END-EXEC. 
           EXEC SQL DROP TABLE TRANSACOES END-EXEC. 
           EXEC SQL DROP TABLE ERROS_PROCESSAMENTO END-EXEC.
           EXEC SQL COMMIT END-EXEC.
      *CRIACAO TABELA CLIENTES     
           EXEC SQL 
               CREATE TABLE CLIENTES ( 
                   CLI_ID INTEGER NOT NULL, 
                   CLI_NOME VARCHAR(30) NOT NULL, 
                   CLI_SALDO DECIMAL(9,0) NOT NULL, 
                   DT_ATUALIZACAO DATE, 
                   PRIMARY KEY (CLI_ID) 
               )
           END-EXEC.
           
           EVALUATE SQLCODE
               WHEN 0
                   DISPLAY "TABELA CRIADA COM SUCESSO."
               WHEN -601
                   DISPLAY "TABELA JA EXISTE. IGNORANDO CRIACAO."
               WHEN OTHER
                   DISPLAY "ERRO AO CRIAR TABELA."
                   PERFORM MOSTRA-ERRO
                   STOP RUN
           END-EVALUATE.
      *CRIACAO TABELA TRANSACOES     
           EXEC SQL 
               CREATE TABLE TRANSACOES ( 
                   TRX_ID INTEGER NOT NULL, 
                   CLI_ID INTEGER NOT NULL, 
                   TRX_TIPO CHAR(1) NOT NULL, 
                   TRX_VALOR DECIMAL(9,0) NOT NULL, 
                   DT_PROCESSAMENTO DATE, 
                   PRIMARY KEY (TRX_ID) 
               ) 
           END-EXEC.
           EVALUATE SQLCODE
               WHEN 0
                   DISPLAY "TABELA CRIADA COM SUCESSO."
               WHEN -601
                   DISPLAY "TABELA JA EXISTE. IGNORANDO CRIACAO."
               WHEN OTHER
                   DISPLAY "ERRO AO CRIAR TABELA."
                   PERFORM MOSTRA-ERRO
                   STOP RUN
           END-EVALUATE.   
      
      *CRIACAO TABELA ERROS
           EXEC SQL 
               CREATE TABLE ERROS_PROCESSAMENTO (  
                   ID_ERRO INTEGER GENERATED ALWAYS AS IDENTITY, 
                   CLI_ID INTEGER, 
                   DESCRICAO_ERRO VARCHAR(100), 
                   DT_OCORRENCIA TIMESTAMP 
               )
           END-EXEC.
           EVALUATE SQLCODE
               WHEN 0
                   DISPLAY "TABELA CRIADA COM SUCESSO."
               WHEN -601
                   DISPLAY "TABELA JA EXISTE. IGNORANDO CRIACAO."
               WHEN OTHER
                   DISPLAY "ERRO AO CRIAR TABELA."
                   PERFORM MOSTRA-ERRO
                   STOP RUN
           END-EVALUATE.
           EXEC SQL COMMIT END-EXEC.

       PROCESSA-ARQCLI.
      *PASSA OS DADOS DO ARQUIVO PARA O BANCO  
           MOVE CLI-ID OF REG-CLIENTE TO HV-CLI-ID.
           MOVE CLI-NOME  TO HV-CLI-NOME.
           MOVE CLI-SALDO TO HV-CLI-SALDO.

           EXEC SQL 
               SELECT CLI_NOME INTO :HV-CLI-NOME 
               FROM CLIENTES 
               WHERE CLI_ID = :HV-CLI-ID 
           END-EXEC.

           IF SQLCODE = 100
               PERFORM CRIAR-CLIENTE
           ELSE IF SQLCODE = 0
               PERFORM ATUALIZAR-CLIENTE
           ELSE
               DISPLAY "ERRO NO SELECT DO CLIENTE: " HV-CLI-ID
               PERFORM MOSTRA-ERRO
           END-IF.
           PERFORM LER-ARQCLI.

       CRIAR-CLIENTE.
      *INSERE OS CLIENTES NA PRIMEIRA FASE  
           EXEC SQL 
               INSERT INTO CLIENTES 
               (CLI_ID, CLI_NOME, CLI_SALDO, DT_ATUALIZACAO) 
               VALUES 
               (:HV-CLI-ID, :HV-CLI-NOME, :HV-CLI-SALDO, CURRENT DATE) 
           END-EXEC.
           EXEC SQL COMMIT END-EXEC.

           IF SQLCODE NOT = 0
               DISPLAY "ERRO NO INSERT."
               PERFORM MOSTRA-ERRO
           END-IF.       

       MOSTRA-ERRO.
           DISPLAY "--- ERRO NO BANCO DE DADOS ---".
           DISPLAY "SQLCODE : " SQLCODE.
           DISPLAY "SQLSTATE: " SQLSTATE.
           DISPLAY "MENSAGEM: " SQLERRMC.
      *SE DER ERRO FAZ O ROLLBACK PARA EVITAR CONFLITOS     
           IF SQLCODE < 0
              EXEC SQL ROLLBACK END-EXEC
              EXEC SQL 
                  INSERT INTO ERROS_PROCESSAMENTO 
                  (CLI_ID, DESCRICAO_ERRO, DT_OCORRENCIA)
                  VALUES (:HV-CLI-ID, 'FALHA BD', CURRENT TIMESTAMP)
              END-EXEC
              EXEC SQL COMMIT END-EXEC
           END-IF.
      
      *             USO DO BANCO NA SEGUNDA FASE  

       BUSCA-CLIENTE.
      *PEGA O ID DO CLIENTE PELA TRANSACAO E BUSCA  
           MOVE CLI-ID OF REG-TRANSACAO TO HV-CLI-ID.
           EXEC SQL
               SELECT CLI_NOME, CLI_SALDO 
                 INTO :HV-CLI-NOME, :HV-CLI-SALDO
                 FROM CLIENTES
                WHERE CLI_ID = :HV-CLI-ID
           END-EXEC.
      *SE OCORREU TUDO CERTO ELE PASSA PARA O REGISTRO     
           IF SQLCODE = 0
               MOVE HV-CLI-ID    TO CLI-ID OF REG-CLIENTE
               MOVE HV-CLI-NOME  TO CLI-NOME OF REG-CLIENTE
               MOVE HV-CLI-SALDO TO CLI-SALDO OF REG-CLIENTE
           END-IF. 
           
       ATUALIZAR-CLIENTE.
      *FAZ A ATUALIZACAO DO CLIENTE NO BANCO  
           EXEC SQL
               UPDATE CLIENTES
                  SET CLI_NOME  = :HV-CLI-NOME,
                      CLI_SALDO = :HV-CLI-SALDO,
                      DT_ATUALIZACAO = CURRENT DATE
                WHERE CLI_ID    = :HV-CLI-ID
           END-EXEC.
           EXEC SQL COMMIT END-EXEC.

           IF SQLCODE NOT = 0
               DISPLAY "ERRO NO UPDATE."
               PERFORM MOSTRA-ERRO
           END-IF.
        
       INSERIR-TRANSACAO.
      *GRAVA A TRANSACAO NO BANCO  
           MOVE CLI-ID OF REG-TRANSACAO TO TRX-CLI-ID.
           MOVE TRX-ID  TO HV-TRX-ID.
           MOVE TRX-TIPO  TO HV-TRX-TIPO.
           MOVE TRX-VALOR TO HV-TRX-VALOR. 
           EXEC SQL 
               INSERT INTO TRANSACOES 
               (TRX_ID, CLI_ID, TRX_TIPO, TRX_VALOR, DT_PROCESSAMENTO) 
               VALUES (:HV-TRX-ID, :TRX-CLI-ID, :HV-TRX-TIPO, 
                       :HV-TRX-VALOR, CURRENT DATE) 
           END-EXEC.
           EXEC SQL COMMIT END-EXEC.

           IF SQLCODE NOT = 0
               DISPLAY "ERRO NO INSERT DA TRANSACAO."
               PERFORM MOSTRA-ERRO
           END-IF. 
         
       MOSTRAR-CLIENTES.
      *CURSOR MANUAL POIS ESSA IDE NAO ACEITOU  
           DISPLAY '-------- LISTA DE CLIENTES ATUALIZADO ---------'.
           MOVE 0 TO WRK-LAST-ID.
           MOVE 'N' TO FIM-CURSOR. 
           PERFORM LER-MANUAL UNTIL FIM-CURSOR = 'S'.
           DISPLAY '-----------------------------------------------'.

       LER-MANUAL.
           EXEC SQL 
               SELECT MIN(CLI_ID) INTO :WRK-PROX-ID 
               FROM CLIENTES 
               WHERE CLI_ID > :WRK-LAST-ID 
           END-EXEC.
      *SE ACHOU O PROXIMO ID ELE IMPRIME  
           IF SQLCODE = 0 AND WRK-PROX-ID > 0
               
      *BUSCA O CLIENTE
               EXEC SQL 
                   SELECT CLI_NOME, CLI_SALDO 
                   INTO :HV-CLI-NOME, :HV-CLI-SALDO 
                   FROM CLIENTES 
                   WHERE CLI_ID = :WRK-PROX-ID 
               END-EXEC
               
      *ATUALIZA AS VARIAVEIS
               MOVE WRK-PROX-ID TO HV-CLI-ID
               MOVE WRK-PROX-ID TO WRK-LAST-ID
               DISPLAY 'ID: ' HV-CLI-ID 
                       '| NOME: ' HV-CLI-NOME 
                       '| SALDO: ' HV-CLI-SALDO
           ELSE
               MOVE 'S' TO FIM-CURSOR
           END-IF.              
           
       DESCONECTAR-BANCO.
           EXEC SQL CONNECT RESET END-EXEC.
       
      *=====================================================          
      *       MANIPULACAO DE ARQUIVOS DE ENTRADA E SAIDA     
      *======================================================     

      *         MANIPULACAO DO ARQUIVO DE CLIENTES  
       ABRIR-ARQCLI.
           OPEN INPUT ARQ-CLI.
           MOVE 'N' TO WRK-OPEN-CLI.
           PERFORM LER-ARQCLI.

       LER-ARQCLI.
           READ ARQ-CLI INTO REG-CLIENTE 
                AT END MOVE 'S' TO WRK-EOF-CLI
           END-READ.    

       FECHAR-ARQCLI.
           IF WRK-OPEN-CLI = 'N' 
              CLOSE ARQ-CLI
           END-IF.
           
      *         MANIPULACAO DO ARQUIVO DE TRANSACOES
            
       ABRIR-ARQTRAN.
           OPEN INPUT ARQ-TRAN.
           MOVE 'N' TO WRK-OPEN-TRAN.
           PERFORM LER-ARQTRAN.

       LER-ARQTRAN.
           READ ARQ-TRAN INTO REG-TRANSACAO 
                AT END MOVE 'S' TO WRK-EOF-TRAN
           END-READ.     
       
      *          MANIPULACAO DOS ARQUIVOS DE SAIDA  
       ABRIR-ARQUIVOS-SAIDA.
           OPEN OUTPUT ARQ-LOG, ARQ-ERROS.
           MOVE 'N' TO WRK-OPEN-OUT.
        
           
      *        MANDA FECHAR O ARQUIVO TRANSACOES E OS DE SAIDA   
       FECHAR-ARQUIVOS. 
           IF WRK-COUNT-COMMIT > 0
              EXEC SQL COMMIT END-EXEC
           END-IF. 
           PERFORM RELATORIO-PROCESSAMENTO.
              
       
      *====================================================  
      *        CONTROLADOR DA LOGICA DE TRANSACOES 
      *====================================================
       PROCESSAR-TRANSACOES.       
           IF WRK-EOF-TRAN = 'S'
              EXEC SQL COMMIT END-EXEC
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
      *FAZ O COMMIT SE CHEGAR EM 100 TRANSACOES       
              ADD 1 TO WRK-COUNT-COMMIT
              IF WRK-COUNT-COMMIT = 100
                 EXEC SQL COMMIT END-EXEC
                 MOVE 0 TO WRK-COUNT-COMMIT
              END-IF
              PERFORM LER-ARQTRAN
           END-IF.     
       ANALISA-SAIDA.
              IF WRK-TIPO-ERRO = 'S' OR WRK-TIPO-ERRO = 'O'
                 MOVE 'E' TO WRK-TIPO-SAIDA
                 PERFORM PROCESSAR-SAIDA
              ELSE
                 MOVE 'R' TO WRK-TIPO-SAIDA
                 PERFORM PROCESSAR-SAIDA   
              END-IF.     
              
      *====================================================  
      *          LOGICA DE VALIDACAO DE TRANSACOES 
      *====================================================                  
        
       VALIDA-TRANSACAO.
      *LIMPEZA
           MOVE 'N' TO WRK-VALIDO.
           MOVE SPACES TO WRK-TIPO, WRK-STATUS-VALOR.
           PERFORM VERIFICA-TIPO.
           PERFORM VERIFICA-VALOR.
           PERFORM VERIFICA-VALIDO.
       VERIFICA-TIPO.
      *QUALQUER COISA DIFERENTE VIRA INVALIDO COLOCA NO ARQUIVO ERROS
           IF TRX-TIPO = 'C' OR TRX-TIPO = 'D'
              MOVE 'S' TO WRK-TIPO
           ELSE
              ADD 1 TO WRK-COUNT-ERRO
              MOVE 'O' TO WRK-TIPO-ERRO
              MOVE 'ERRO: TIPO DE TRANSACAO INVALIDO -  ID'
                    TO WRK-SAIDA-ERRO
              MOVE 'ERRO: TIPO TRANSACAO INVALIDA'
                    TO WRK-STATUS-ERRO
           END-IF.              
       VERIFICA-VALOR.
      *SE TRANSACAO FOR 0 COLOCA NO ARQUIVO ERROS
           IF TRX-VALOR NOT = 0
              MOVE 'S' TO WRK-STATUS-VALOR
           ELSE
              ADD 1 TO WRK-COUNT-ERRO
              MOVE 'O' TO WRK-TIPO-ERRO
              MOVE 'ERRO: VALOR DE TRANSACAO INVALIDO - ID'
                    TO WRK-SAIDA-ERRO
              MOVE 'ERRO: VALOR INVALIDO         '
                    TO WRK-STATUS-ERRO
           END-IF.
       VERIFICA-VALIDO.
      *SE NAO TIVER ERROS A TRANSACAO FICA VALIDA
           IF WRK-TIPO = 'S' AND WRK-STATUS-VALOR = 'S'
              MOVE 'S' TO WRK-VALIDO
           ELSE
              MOVE 'N' TO WRK-VALIDO. 
              
      *====================================================  
      *          LOGICA DE REGRAS DAS TRANSACOES 
      *====================================================           
       REGRA-TRANSACAO.
           MOVE SPACES TO WRK-ERRO.
           PERFORM BUSCA-CLIENTE.
           PERFORM COMPARA-IDS.  
           
      *LOGICA PRICIPAL PARA FAZER AS OPERACOES
       COMPARA-IDS.
           IF SQLCODE = 100
              ADD 1 TO WRK-COUNT-ERRO  
              MOVE 'O' TO WRK-TIPO-ERRO
              MOVE 'ERRO: CLIENTE NAO ENCONTRADO -       ID' 
                   TO WRK-SAIDA-ERRO
              MOVE 'ERRO: CLIENTE NAO ENCONTRADO ' TO WRK-STATUS-ERRO
           ELSE
              IF SQLCODE = 0
                 PERFORM PROCESSA-TRANSACAO
              END-IF
           END-IF.    
                   
       PROCESSA-TRANSACAO.
      *SE FOR CREDITO ADICIONA NO SALDO E ACRECENTA OS CONTADORES
           IF TRX-TIPO = 'C'
              ADD TRX-VALOR TO CLI-SALDO OF REG-CLIENTE
              ADD 1 TO WRK-COUNT-TRAN 
              MOVE CLI-SALDO OF REG-CLIENTE TO HV-CLI-SALDO  
      *MANDAR PARA O BANCO ***************************************!! 
              PERFORM ATUALIZAR-CLIENTE
              PERFORM INSERIR-TRANSACAO  
           ELSE
      *SE FOR DEBITO SUBTRAI O SALDO E ACRECENTA OS CONTADORES
              PERFORM CALCULA-DEBITO.
       CALCULA-DEBITO.
      *SE SALDO MAIOR QUE VALOR AI FAZ O DEBITO
           IF CLI-SALDO > TRX-VALOR OR CLI-SALDO = TRX-VALOR
              SUBTRACT TRX-VALOR FROM CLI-SALDO OF REG-CLIENTE
              ADD 1 TO WRK-COUNT-TRAN
              MOVE CLI-SALDO OF REG-CLIENTE TO HV-CLI-SALDO
      *MANDAR PARA O BANCO ***************************************!!
              PERFORM ATUALIZAR-CLIENTE
              PERFORM INSERIR-TRANSACAO  
           ELSE
              ADD 1 TO WRK-COUNT-ERRO
              MOVE 'S' TO WRK-TIPO-ERRO
              MOVE 'ERRO: SALDO INSUFICIENTE -           ID'
                    TO WRK-SAIDA-ERRO
              MOVE 'ERRO: SALDO INSUFICIENTE  '
                    TO WRK-STATUS-ERRO. 
                    
      *====================================================  
      *          LOGICA DE SAIDA DO PROCESSAMENTO 
      *==================================================== 
       PROCESSAR-SAIDA.
           IF WRK-OPEN-OUT = 'S'
              PERFORM ABRIR-ARQUIVOS-SAIDA
           END-IF.   
           IF WRK-CLOSE-OUT = 'N'
              PERFORM LOGICA-GRAVACAO
           END-IF.      
       LOGICA-GRAVACAO.  
           IF WRK-TIPO-SAIDA = 'R'
              PERFORM RELATORIO-CLIENTE
           ELSE
              PERFORM ESCREVER-ERRO
           END-IF.   
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
           IF WRK-TIPO-SAIDA = 'R'
               MOVE 'SUCESSO' TO WRK-LOG-VALOR
           ELSE
               MOVE 'ERRO   ' TO WRK-LOG-VALOR
           END-IF.
           WRITE REG-LOG-FD FROM WRK-IMPRIME-LOG.     
      *FAZ O RELATORIO NO TERMINAL   
           DISPLAY '==========================='.
           DISPLAY 'CLIENTE:' CLI-ID OF REG-TRANSACAO.
           IF TRX-TIPO = 'C'
              DISPLAY 'OPERACAO: CREDITO'
           ELSE 
              DISPLAY 'OPERACAO: DEBITO'
           END-IF.           
           IF WRK-TIPO-SAIDA = 'R'
               DISPLAY 'STATUS: SUCESSO' 
           ELSE
               DISPLAY 'STATUS: ' WRK-STATUS-ERRO
           END-IF.
       ESCREVER-ERRO.
           IF WRK-TIPO-ERRO = 'S'
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
              MOVE WRK-SAIDA-ERRO TO WRK-TEXTO-ERRO
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
              MOVE WRK-SAIDA-ERRO TO WRK-TEXTO-ERRO
              MOVE CLI-ID OF REG-TRANSACAO TO WRK-ID
              WRITE REG-ERROS-FD FROM WRK-LINHA-ID
              MOVE SPACES TO WRK-TEXTO-ERRO
           END-IF.
      *INSERE O ERRO NO BANCO      
           EXEC SQL 
               INSERT INTO ERROS_PROCESSAMENTO 
               (CLI_ID, DESCRICAO_ERRO, DT_OCORRENCIA)
               VALUES (:HV-CLI-ID, :WRK-STATUS-ERRO, CURRENT TIMESTAMP)
           END-EXEC
           EXEC SQL COMMIT END-EXEC.  
           MOVE SPACES TO WRK-TIPO-ERRO.
           PERFORM RELATORIO-CLIENTE.
                    
       RELATORIO-PROCESSAMENTO.
           WRITE REG-LOG-FD FROM '============================'.
           WRITE REG-LOG-FD FROM ' RELATORIO DO PROCESSAMENTO '.
           WRITE REG-LOG-FD FROM '============================'.
           MOVE 'REGISTROS LIDOS............: ' TO WRK-RESUMO-TEXTO.
           MOVE WRK-COUNT-REG                    TO WRK-RESUMO-VALOR.
           WRITE REG-LOG-FD FROM WRK-LINHA-RESUMO.
           MOVE 'TRANSACOES PROCESSADAS.....: ' TO WRK-RESUMO-TEXTO.
           MOVE WRK-COUNT-TRAN                   TO WRK-RESUMO-VALOR.
           WRITE REG-LOG-FD FROM WRK-LINHA-RESUMO.
           MOVE 'ERROS ENCONTRADOS..........: ' TO WRK-RESUMO-TEXTO.
           MOVE WRK-COUNT-ERRO                   TO WRK-RESUMO-VALOR.
           WRITE REG-LOG-FD FROM WRK-LINHA-RESUMO. 
      *FECHANDO TODOS OS ARQUIVOS  
           CLOSE ARQ-TRAN.
           CLOSE ARQ-LOG.
           CLOSE ARQ-ERROS.     
           