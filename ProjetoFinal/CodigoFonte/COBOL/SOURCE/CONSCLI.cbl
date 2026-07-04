       IDENTIFICATION DIVISION.
       PROGRAM-ID. CONSCLI.
       AUTHOR. MATHEUS CRUZ.
       ENVIRONMENT DIVISION.
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLIENTES ASSIGN TO 
           "D:/AceleraMaker/projetosAceleraMaker/ProjetoFinal/CodigoFont
      -    "e/COBOL/DATA/CLIENTES.dat"
           ORGANIZATION IS INDEXED
           ACCESS MODE IS RANDOM
           RECORD KEY IS REG-ID
           FILE STATUS IS WRK-FILE-STATUS.
       DATA DIVISION.
       FILE SECTION.
       FD  ARQ-CLIENTES.
       COPY FDCLI.
           
       WORKING-STORAGE SECTION.
       01  WRK-FILE-STATUS   PIC X(02).
       LINKAGE SECTION.
       COPY REGCLI.

       PROCEDURE DIVISION USING REG-CLIENTE.
       MAIN-PROCEDURE.
      * Como CLI-ID é numérico PIC 9(05), verificamos apenas ZEROS
           IF CLI-ID = ZEROS
               MOVE "ID REQUISICAO INVALIDO      " TO CLI-NOME
               MOVE "99"             TO STATUS-RETORNO
               GOBACK
           END-IF.

           OPEN INPUT ARQ-CLIENTES.
           
           IF WRK-FILE-STATUS NOT = "00"
               MOVE "ERRO AO ABRIR O ARQUIVO VSAM" TO CLI-NOME
               MOVE "30" TO STATUS-RETORNO
               GOBACK
           END-IF.

           MOVE CLI-ID TO REG-ID.
           
           READ ARQ-CLIENTES
               INVALID KEY
                   MOVE "CLIENTE NAO ENCONTRADO" TO CLI-NOME
                   MOVE "44" TO STATUS-RETORNO
               NOT INVALID KEY
                   MOVE REG-NOME     TO CLI-NOME
                   MOVE REG-TELEFONE TO TELEFONE
                   MOVE REG-EMAIL    TO EMAIL
                   MOVE "00"         TO STATUS-RETORNO
           END-READ.
           
           CLOSE ARQ-CLIENTES.
           GOBACK.
           