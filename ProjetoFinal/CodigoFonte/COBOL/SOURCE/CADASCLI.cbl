       IDENTIFICATION DIVISION.
       PROGRAM-ID. CADASCLI.
       AUTHOR. MATHEUS CRUZ.
       ENVIRONMENT DIVISION.
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLIENTES ASSIGN TO 
           "D:/AceleraMaker/projetosAceleraMaker/ProjetoFinal/CodigoFont
      -    "e/COBOL/DATA/CLIENTES.dat"
           ORGANIZATION IS INDEXED
           ACCESS MODE IS DYNAMIC
           RECORD KEY IS REG-ID
           FILE STATUS IS WRK-FILE-STATUS.

           SELECT ARQ-CONTROLE ASSIGN TO
           "D:/AceleraMaker/projetosAceleraMaker/ProjetoFinal/CodigoFont
      -    "e/COBOL/DATA/CONTROLE.dat"
           ORGANIZATION IS LINE SEQUENTIAL
           FILE STATUS IS WRK-CTRL-STATUS.
       DATA DIVISION.
       FILE SECTION.
       FD  ARQ-CLIENTES.
       COPY FDCLI.

       FD  ARQ-CONTROLE.
           01  REG-CONTROLE.
               05 ULTIMO-ID  PIC 9(05).
           
       WORKING-STORAGE SECTION.
       01  WRK-FILE-STATUS   PIC X(02).
       01  WRK-CTRL-STATUS   PIC X(02).
       01  WRK-MAX-ID        PIC 9(05) VALUE ZEROS.
       01  WRK-EOF           PIC X(01) VALUE 'N'.
       LINKAGE SECTION.
       COPY REGCLI.

       PROCEDURE DIVISION USING REG-CLIENTE.
       MAIN-PROCEDURE.
      *ABRE O ARQUIVO DE CONTROLE PARA LER O ULTIMO ID
      *SE O ARQUIVO NAO FOI ENCONTRADO, VAI LER O ARQUIVO DE CLIENTE
      *PARA ENCONTRAR O MAIOR ID 
           OPEN INPUT ARQ-CONTROLE.
           IF WRK-CTRL-STATUS = "35" 
               OPEN INPUT ARQ-CLIENTES
               IF WRK-FILE-STATUS = "00"
                   MOVE ZEROS TO REG-ID
      *CONFERE SE EXISTE ALGUM REGISTRO NO ARQUIVO DE CLIENTES             
                   START ARQ-CLIENTES KEY IS GREATER THAN REG-ID
                       INVALID KEY
                           MOVE ZEROS TO WRK-MAX-ID
                       NOT INVALID KEY
                           MOVE 'N' TO WRK-EOF
      *LOOP PARA ENCONTRAR O MAIOR ID            
                           PERFORM UNTIL WRK-EOF = 'S'
                               READ ARQ-CLIENTES NEXT
                                   AT END
                                       MOVE 'S' TO WRK-EOF
                                   NOT AT END
                                       IF REG-ID > WRK-MAX-ID
                                           MOVE REG-ID TO WRK-MAX-ID
                                       END-IF
                               END-READ
                           END-PERFORM
                   END-START
                   CLOSE ARQ-CLIENTES
               ELSE
                   MOVE ZEROS TO WRK-MAX-ID
               END-IF
      *PASSA O ID DO PROXIMO CLIENTE QUE VAI SER CADASTRADO         
               OPEN OUTPUT ARQ-CONTROLE
               MOVE WRK-MAX-ID TO ULTIMO-ID
               WRITE REG-CONTROLE
               CLOSE ARQ-CONTROLE
           ELSE
               CLOSE ARQ-CONTROLE
           END-IF.
             
           OPEN INPUT ARQ-CONTROLE.
           READ ARQ-CONTROLE.
           CLOSE ARQ-CONTROLE.
           ADD 1 TO ULTIMO-ID.
      *SALVA O PROXIMO ULTIMO ID NOS REGISTROS
           MOVE ULTIMO-ID TO REG-ID.
           MOVE ULTIMO-ID TO CLI-ID.
      *ATUALIZA O ARQUIVO DE CONTROLE 
           OPEN OUTPUT ARQ-CONTROLE.
           WRITE REG-CONTROLE.
           CLOSE ARQ-CONTROLE.

           OPEN I-O ARQ-CLIENTES.
           
           IF WRK-FILE-STATUS NOT = "00"
               MOVE "ERRO AO ABRIR O ARQUIVO VSAM" TO CLI-NOME
               MOVE "30" TO STATUS-RETORNO
               GOBACK
           END-IF.

           MOVE CLI-NOME TO REG-NOME.
           MOVE TELEFONE TO REG-TELEFONE.
           MOVE EMAIL    TO REG-EMAIL.
           WRITE REG-CLIENTE-ARQ
      *CLIENTE JA EXISTE
                 INVALID KEY
                    MOVE "31" TO STATUS-RETORNO
                 NOT INVALID KEY                
                    MOVE "00" TO STATUS-RETORNO
           END-WRITE.         
      *CLIENTE CADASTRADO COM SUCESSO           
           CLOSE ARQ-CLIENTES.
           GOBACK.
               