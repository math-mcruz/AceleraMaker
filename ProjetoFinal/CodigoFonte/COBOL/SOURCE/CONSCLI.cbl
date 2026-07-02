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
       01  REG-CLIENTE-ARQ.
           05 REG-ID         PIC 9(05).
           05 REG-NOME       PIC X(30).
           05 REG-STATUS-ARQ PIC X(02).
       WORKING-STORAGE SECTION.
       01  WRK-FILE-STATUS    PIC X(02).
       LINKAGE SECTION.
       01  LS-ARGUMENTO.
           05 LS-ID-REQ      PIC X(05).
           05 LS-NOME        PIC X(30).
           05 LS-STATUS      PIC X(02).

       PROCEDURE DIVISION USING LS-ARGUMENTO.
       MAIN-PROCEDURE.
           IF LS-ID-REQ = SPACES OR LS-ID-REQ = ZEROS
               MOVE "ID REQUISICAO INVALIDO        " TO LS-NOME
               MOVE "99"             TO LS-STATUS
               GOBACK
           END-IF.

           OPEN INPUT ARQ-CLIENTES.
           
           IF WRK-FILE-STATUS NOT = "00"
               MOVE "ERRO AO ABRIR O ARQUIVO VSAM" TO LS-NOME
               MOVE "30" TO LS-STATUS
               GOBACK
           END-IF.

           MOVE LS-ID-REQ TO REG-ID.
           
           READ ARQ-CLIENTES
               INVALID KEY
                   MOVE "CLIENTE NAO ENCONTRADO" TO LS-NOME
                   MOVE "44" TO LS-STATUS
               NOT INVALID KEY
                   MOVE REG-NOME TO LS-NOME
                   MOVE "00" TO LS-STATUS
           END-READ.
           CLOSE ARQ-CLIENTES.
           GOBACK.
           