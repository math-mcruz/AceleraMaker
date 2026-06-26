       IDENTIFICATION DIVISION.
       PROGRAM-ID. CLIVSAM.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT DIVISION.
       DATA DIVISION.
       WORKING-STORAGE SECTION.
       77  WRK-RESP            PIC S9(8) COMP.
       01  REG-CLIENTE.
           05 CODCLI         PIC 9(06).
           05 NOME           PIC X(30).
           05 TELEFONE       PIC X(15).
           05 CIDADE         PIC X(20).
      *COPY REGCLI.
       LINKAGE SECTION.
       01  DFHCOMMAREA.
           05  COMM-FUNCAO      PIC X(01).
           05  COMM-CODCLI      PIC 9(06).
           05  COMM-NOME        PIC X(30).
           05  COMM-TELEFONE    PIC X(15).
           05  COMM-CIDADE      PIC X(20).
           05  COMM-RETORNO     PIC 9(02).
      *COPY DFHCOMM.
       PROCEDURE DIVISION.
           IF EIBCALEN = 0
               EXEC CICS RETURN END-EXEC.
           IF COMM-FUNCAO = 'C'
               PERFORM CONSULTAR-CLIENTE
           ELSE
               IF COMM-FUNCAO = 'S'
                  PERFORM SALVAR-CLIENTE.
      *ENVIA DE VOLTA PARA O CLIPMG
           EXEC CICS XCTL
               PROGRAM('CLIPMG')
               COMMAREA(DFHCOMMAREA)
               LENGTH(74)
           END-EXEC.
       CONSULTAR-CLIENTE.
           EXEC CICS READ
                DATASET('CLIENTES')
                INTO(REG-CLIENTE)
                RIDFLD(COMM-CODCLI)
                RESP(WRK-RESP)
           END-EXEC.
      *SE CLIENTE EXISTE RESP = 0
           IF  WRK-RESP = 0
               MOVE NOME TO COMM-NOME
               MOVE TELEFONE TO COMM-TELEFONE
               MOVE CIDADE TO COMM-CIDADE
               MOVE 00 TO COMM-RETORNO
           ELSE
      *CLIENTE NAO EXISTE
               MOVE 04 TO COMM-RETORNO.
       SALVAR-CLIENTE.
      *LE PARA VER SE EXISTE O CLIENTE
           EXEC CICS READ
                DATASET('CLIENTES')
                INTO(REG-CLIENTE)
                RIDFLD(COMM-CODCLI)
                UPDATE
                RESP(WRK-RESP)
           END-EXEC.
      *CLIENTE EXISTE
           IF WRK-RESP = 0
               MOVE COMM-TELEFONE TO TELEFONE
               MOVE COMM-CIDADE TO CIDADE
      *REESCREVE NO ARQUIVO
               EXEC CICS REWRITE
                    DATASET('CLIENTES')
                    FROM(REG-CLIENTE)
                    RESP(WRK-RESP)
               END-EXEC
               PERFORM VERIFICA-ERRO
       VERIFICA-ERRO.
           IF WRK-RESP = 0
              MOVE 00 TO COMM-RETORNO
           ELSE
              MOVE WRK-RESP TO COMM-RETORNO.
