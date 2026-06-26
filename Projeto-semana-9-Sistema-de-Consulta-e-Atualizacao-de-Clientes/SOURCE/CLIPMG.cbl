       IDENTIFICATION DIVISION.
       PROGRAM-ID. CLIPMG.
       AUTHOR.     MATHEUS CRUZ.
       ENVIRONMENT DIVISION.
       DATA DIVISION.
       WORKING-STORAGE SECTION.
       01  WRK-COMMAREA    PIC X(01) VALUE SPACES.
      *IMPORTA AS VARIAVEIS DO MAPA
       COPY MAPSET1.
      *IMPORTA AS TECLAS
       COPY DFHAID.
       LINKAGE SECTION.
       01  DFHCOMMAREA.
           05  COMM-FUNCAO      PIC X(01).
           05  COMM-CODCLI      PIC 9(06).
           05  COMM-NOME        PIC X(30).
           05  COMM-TELEFONE    PIC X(15).
           05  COMM-CIDADE      PIC X(20).
           05  COMM-RETORNO     PIC 9(02).
       PROCEDURE DIVISION.
       CLIPMG-PROCEDURE.
      *EIBCALEN = 0 INDICA QUE NAO TEVE ACOES AINDA NA TRANSACAO
           IF EIBCALEN = 0
              PERFORM INICIAR-CICS
           ELSE
              IF EIBCALEN = 1
                 PERFORM RESPOSTA-CLIENTE
              ELSE
                 PERFORM RETORNO-VSAM.
           EXEC CICS RETURN
                TRANSID('CLIE')
                COMMAREA(WRK-COMMAREA)
                LENGTH(1)
           END-EXEC.
           GOBACK.
      *MOSTRANDO O MAPA DA TELA
       INICIAR-CICS.
           MOVE LOW-VALUES TO MAP1O.
           EXEC CICS
                SEND MAP('MAP1') MAPSET('MAPSET1') ERASE
           END-EXEC.
       RESPOSTA-CLIENTE.
      *IGNORA PARA NAO DERRUBAR A TRANSACAO SE MANDAR DADOS VAZIOS
           EXEC CICS IGNORE CONDITION
                MAPFAIL
           END-EXEC.
           EXEC CICS
                RECEIVE MAP('MAP1') MAPSET('MAPSET1')
           END-EXEC.
      *SAIR
           IF EIBAID = DFHPF3
              EXEC CICS SEND CONTROL ERASE FREEKB END-EXEC
              EXEC CICS RETURN END-EXEC
           ELSE
      *CONSULTAR
              IF EIBAID = DFHPF5
                 MOVE 'C' TO COMM-FUNCAO
                 MOVE CODCLII TO COMM-CODCLI
                 PERFORM LOGICA-VSAM
              ELSE
      *SALVAR ALTERACOES DE TELEFONE E CIDADE
                 IF EIBAID = DFHPF6
                    MOVE 'S' TO COMM-FUNCAO
                    MOVE CODCLII TO COMM-CODCLI
                    MOVE FONEI   TO COMM-TELEFONE
                    MOVE CITYI   TO COMM-CIDADE
                    PERFORM LOGICA-VSAM
                 ELSE
      *SE APERTAR ENTER LIMPA A TELA
                    IF EIBAID = DFHENTER
                       PERFORM LIMPA-TELA.
       RETORNO-VSAM.
      *ANALISA RETORNO DO VSAM
           IF COMM-RETORNO = 00
               MOVE COMM-NOME TO NAMEO
               MOVE COMM-TELEFONE TO FONEO
               MOVE COMM-CIDADE TO CITYO
               IF COMM-FUNCAO = 'C'
                  MOVE 'CLIENTE ENCONTRADO' TO MSGO
               ELSE
                  MOVE 'ALTERACAO REALIZADA' TO MSGO
           ELSE
               MOVE 'CLIENTE NAO ENCONTRADO' TO MSGO.
           EXEC CICS
               SEND MAP('MAP1') MAPSET('MAPSET1') ERASE
           END-EXEC.
       LOGICA-VSAM.
           EXEC CICS XCTL
                PROGRAM('CLIVSAM')
                COMMAREA(DFHCOMMAREA)
                LENGTH(74)
           END-EXEC.
       LIMPA-TELA.
      *MOVE VALORES NULOS PARA O MAPA
           MOVE LOW-VALUES TO MAP1O.
           EXEC CICS SEND MAP('MAP1')
                MAPSET('MAPSET1')
                ERASE
           END-EXEC.
