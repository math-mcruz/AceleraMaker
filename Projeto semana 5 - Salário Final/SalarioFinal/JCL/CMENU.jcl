//HERC01C  JOB (SALMENU),                                               00000101
//             'COMPILA SALMENU',                                       00000200
//             CLASS=A,                                                 00000300
//             MSGCLASS=H,                                              00000400
//             REGION=8M,TIME=1440,                                     00000500
//             MSGLEVEL=(1,1),                                          00000600
//             NOTIFY=HERC01                                            00000700
//COMPCOB EXEC COBUCL,                                                  00000800
//        PARM.COB='FLAGW,LOAD,SUPMAP,SIZE=2048K,BUF=1024K'             00000900
//COB.SYSPUNCH DD DUMMY                                                 00001002
//COB.SYSIN    DD DSN=HERC01.SALARIO.COBOL(SALMENU),DISP=SHR            00001102
//LKED.SYSLIB  DD DSN=SYS1.COBLIB,DISP=SHR                              00001202
//             DD DSN=HERC01.SALARIO.LOAD,DISP=SHR                      00001302
//LKED.SYSLMOD DD DSN=HERC01.SALARIO.LOAD(SALMENU),DISP=SHR             00001402
