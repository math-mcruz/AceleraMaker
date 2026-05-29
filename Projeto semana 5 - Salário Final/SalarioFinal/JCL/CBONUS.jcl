//HERC01C  JOB (SALBONUS),                                              00000104
//             'COMPILA SALBONUS',                                      00000204
//             CLASS=A,                                                 00000304
//             MSGCLASS=H,                                              00000404
//             REGION=8M,TIME=1440,                                     00000504
//             MSGLEVEL=(1,1),                                          00000604
//             NOTIFY=HERC01                                            00000704
//COMPCOB EXEC COBUCL,                                                  00000804
//        PARM.COB='FLAGW,LOAD,SUPMAP,SIZE=2048K,BUF=1024K'             00000904
//COB.SYSPUNCH DD DUMMY                                                 00001004
//COB.SYSIN    DD DSN=HERC01.SALARIO.COBOL(SALBONUS),DISP=SHR           00001104
//LKED.SYSLIB  DD DSN=SYS1.COBLIB,DISP=SHR                              00001204
//             DD DSN=HERC01.SALARIO.LOAD,DISP=SHR                      00001304
//LKED.SYSLMOD DD DSN=HERC01.SALARIO.LOAD(SALBONUS),DISP=SHR            00001404
