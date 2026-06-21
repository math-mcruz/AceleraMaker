       IDENTIFICATION                  DIVISION.
       PROGRAM-ID. BANCMAIN.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT                     DIVISION.
       
       DATA                            DIVISION.
       WORKING-STORAGE                 SECTION.
       01  WRK-CONTROLE-CLI.
           05 WRK-EOF-CLI  PIC X VALUE 'N'.
           05 WRK-OPEN-CLI PIC X VALUE 'S'. 
      *VARIAVEIS DE CONEXAO COM O BANCO  
       EXEC SQL INCLUDE SQLCA END-EXEC.
       EXEC SQL BEGIN DECLARE SECTION END-EXEC.
       01  DB-NOME      PIC X(45) 
                        VALUE 'odbc://MEU_DB2COBOL?fixup_params=on'.
       01  DB-USER      PIC X(20) VALUE 'db2inst1'.
       01  DB-PASS      PIC X(20) VALUE 'SenhaForte123!'.
       01  WS-PROX-ID   PIC 9(9)  VALUE ZEROS.
       01  HV-ERR-DESC  PIC X(100). 
      *VARIAVEIS DO DB2 
       COPY DBCLI.
       EXEC SQL END DECLARE SECTION END-EXEC. 
       COPY REGCLI. 
       COPY REGTRAN.
       PROCEDURE                       DIVISION.
       BANCMAIN-PROCEDURE.
           PERFORM CONECTA-BANCO.
           PERFORM VERIFICA-TABELAS.    
           PERFORM VERIFICA-CLIENTES.
           PERFORM PROCESSA-TRANSACOES.
           PERFORM DESCONECTAR-BANCO.  
           STOP RUN.

       VERIFICA-CLIENTES.
           PERFORM SALVA-CLIENTE UNTIL WRK-EOF-CLI = 'S'.
      *SALVA NO BANCO
           EXEC SQL COMMIT END-EXEC.
       SALVA-CLIENTE.
           CALL 'VERCLI' USING REG-CLIENTE, WRK-CONTROLE-CLI.
           IF WRK-EOF-CLI = 'N'  
              PERFORM PROCESSA-ARQUIVO
           END-IF.
       PROCESSA-ARQUIVO.
           MOVE CLI-ID OF REG-CLIENTE    TO HV-CLI-ID.
           MOVE CLI-NOME OF REG-CLIENTE  TO HV-CLI-NOME.
           MOVE CLI-SALDO OF REG-CLIENTE TO HV-CLI-SALDO.
           EXEC SQL 
               SELECT CLI_ID INTO :WS-PROX-ID 
               FROM CLIENTES 
               WHERE CLI_ID = :HV-CLI-ID 
           END-EXEC.
           IF SQLCODE = 100
               PERFORM CRIAR-CLIENTE
           ELSE 
              IF SQLCODE = 0
                 PERFORM ATUALIZAR-CLIENTE
              ELSE  
                 MOVE 'ERRO NO SELECT DE CLIENTE' TO HV-ERR-DESC
                 PERFORM GRAVA-ERRO
              END-IF
           END-IF.
       CRIAR-CLIENTE.
           EXEC SQL 
               INSERT INTO CLIENTES 
               (CLI_ID, CLI_NOME, CLI_SALDO, DT_ATUALIZACAO) 
               VALUES 
               (:HV-CLI-ID, :HV-CLI-NOME, :HV-CLI-SALDO, CURRENT DATE) 
           END-EXEC.
           IF SQLCODE NOT = 0 
               MOVE SQLERRMC TO HV-ERR-DESC
               PERFORM GRAVA-ERRO   
           END-IF.  
       ATUALIZAR-CLIENTE.
           EXEC SQL
               UPDATE CLIENTES
                  SET CLI_NOME       = :HV-CLI-NOME,
                      CLI_SALDO      = :HV-CLI-SALDO,
                      DT_ATUALIZACAO = CURRENT DATE
                WHERE CLI_ID         = :HV-CLI-ID
           END-EXEC.
           IF SQLCODE NOT = 0
               MOVE SQLERRMC TO HV-ERR-DESC
               PERFORM GRAVA-ERRO
           END-IF.

       PROCESSA-TRANSACOES.
           CALL 'TRANPROC' USING REG-CLIENTE. 

       GRAVA-ERRO.
           EXEC SQL 
               INSERT INTO ERROS_PROCESSAMENTO 
               (CLI_ID, DESCRICAO_ERRO, DT_OCORRENCIA)
               VALUES (:HV-CLI-ID, :HV-ERR-DESC, CURRENT TIMESTAMP)
           END-EXEC.

       CONECTA-BANCO.
           EXEC SQL
               CONNECT TO :DB-NOME USER :DB-USER USING :DB-PASS
           END-EXEC.
           IF SQLCODE NOT = 0
               PERFORM MOSTRA-ERRO
               STOP RUN
           END-IF. 

       DESCONECTAR-BANCO.
           EXEC SQL CONNECT RESET END-EXEC. 

       MOSTRA-ERRO.
           DISPLAY 'FALHA EM CONECTAR AO DB2'.
           DISPLAY "SQLCODE : " SQLCODE.
           DISPLAY "MENSAGEM: " SQLERRMC.

       VERIFICA-TABELAS.
           EXEC SQL DROP TABLE TRANSACOES END-EXEC.
           EXEC SQL COMMIT END-EXEC.
           EXEC SQL DROP TABLE ERROS_PROCESSAMENTO END-EXEC.
           EXEC SQL COMMIT END-EXEC.
           EXEC SQL DROP TABLE CLIENTES END-EXEC.
           EXEC SQL COMMIT END-EXEC.
           
           EXEC SQL 
               CREATE TABLE CLIENTES ( 
                   CLI_ID INTEGER NOT NULL, 
                   CLI_NOME VARCHAR(30) NOT NULL, 
                   CLI_SALDO DECIMAL(9,0) NOT NULL, 
                   DT_ATUALIZACAO DATE, 
                   PRIMARY KEY (CLI_ID) 
               )
           END-EXEC.
           IF SQLCODE NOT = 0
               DISPLAY "ERRO ao criar tabela CLIENTES!"
               DISPLAY "MENSAGEM: " SQLERRMC
           END-IF.
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
           EXEC SQL 
               CREATE TABLE ERROS_PROCESSAMENTO (
                   ID_ERRO INTEGER GENERATED ALWAYS AS IDENTITY, 
                   CLI_ID INTEGER, 
                   DESCRICAO_ERRO VARCHAR(100), 
                   DT_OCORRENCIA TIMESTAMP 
               )
           END-EXEC.
           EXEC SQL COMMIT END-EXEC.