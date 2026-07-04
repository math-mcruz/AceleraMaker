       IDENTIFICATION DIVISION.
       PROGRAM-ID. DELECLI.
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
      *VERIFICA SE O ID E SO ZEROS
           IF CLI-ID = ZEROS
               MOVE "ID REQUISICAO INVALIDO      " TO CLI-NOME
               MOVE "99"             TO STATUS-RETORNO
               GOBACK
           END-IF.

           OPEN I-O ARQ-CLIENTES.
           
           IF WRK-FILE-STATUS NOT = "00"
               MOVE "ERRO AO ABRIR O ARQUIVO VSAM" TO CLI-NOME
               MOVE "30" TO STATUS-RETORNO
               GOBACK
           END-IF.

           MOVE CLI-ID TO REG-ID.
           
           DELETE ARQ-CLIENTES
               INVALID KEY
                   MOVE "44" TO STATUS-RETORNO
               NOT INVALID KEY
                   MOVE "00"         TO STATUS-RETORNO
           END-DELETE.
           
           CLOSE ARQ-CLIENTES.
           GOBACK.
           