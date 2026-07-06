       IDENTIFICATION DIVISION.
       PROGRAM-ID. DELECLI.
       AUTHOR. MATHEUS CRUZ.
       ENVIRONMENT DIVISION.
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLIENTES ASSIGN TO WRK-ARQ
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
       01  WRK-ARQ   PIC X(100).
       LINKAGE SECTION.
       COPY REGCLI.

       PROCEDURE DIVISION USING REG-CLIENTE.
       MAIN-PROCEDURE.
      *SE FOR TS ABRE O ARQUIVO DE TESTES AUTOMATIZADOS 
           IF STATUS-RETORNO = "TS"
               STRING
                   "D:/AceleraMaker/projetosAceleraMaker/"
                   "ProjetoFinal/CodigoFonte/COBOL/"
                   "DATATEST/CLITESTS.dat"
                   DELIMITED BY SIZE INTO WRK-ARQ
               END-STRING
           ELSE
               STRING
                   "../COBOL/DATA/CLIENTES.dat"
                   DELIMITED BY SIZE INTO WRK-ARQ
               END-STRING
           END-IF.
      *LIMPA A VARIAVEL DE STATUS
           MOVE SPACES TO STATUS-RETORNO. 
      *VERIFICA SE O ID E SO ZEROS
           IF CLI-ID = ZEROS
               MOVE "99"             TO STATUS-RETORNO
               GOBACK
           END-IF.

           OPEN I-O ARQ-CLIENTES.
           
           IF WRK-FILE-STATUS NOT = "00"
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
           