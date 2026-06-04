//HERC01C  JOB (SALDADOS),                                              00000102
//             'COMPILA SALDADOS',                                      00000201
//             CLASS=A,                                                 00000301
//             MSGCLASS=H,                                              00000401
//             REGION=8M,TIME=1440,                                     00000501
//             MSGLEVEL=(1,1),                                          00000601
//             NOTIFY=HERC01                                            00000701
//COMPCOB EXEC COBUCL,                                                  00000801
//        PARM.COB='FLAGW,LOAD,SUPMAP,SIZE=2048K,BUF=1024K'             00000901
//COB.SYSPUNCH DD DUMMY                                                 00001003
//COB.SYSIN    DD DSN=HERC01.SALARIO.COBOL(SALDADOS),DISP=SHR           00001103
//LKED.SYSLIB  DD DSN=SYS1.COBLIB,DISP=SHR                              00001203
//             DD DSN=HERC01.SALARIO.LOAD,DISP=SHR                      00001303
//LKED.SYSLMOD DD DSN=HERC01.SALARIO.LOAD(SALDADOS),DISP=SHR            00001403
