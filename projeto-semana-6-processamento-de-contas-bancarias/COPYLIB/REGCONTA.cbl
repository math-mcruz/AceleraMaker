            01 REG-CONTA.                                               00000100
                  05 NUM-CONTA    PIC 9(08).                            00000201
                  05 NOME-CLIENTE PIC A(30).                            00000300
                  05 AGENCIA      PIC 9(04).                            00000401
                  05 TIPO-CONTA   PIC A(01).                            00000501
                     88 CONTA-CORRENTE VALUE 'C'.                       00000600
                     88 CONTA-POUPANCA VALUE 'P'.                       00000700
                  05 SALDO        PIC S9(09)V99.                        00000801
                  05 FILLER       PIC X(26) VALUE SPACES.               00000901
