//HERC01XX  JOB (CONTEXEC),CLASS=A,MSGCLASS=H,MSGLEVEL=(1,1),REGION=8M  00000103
//STEP0    EXEC PGM=IEFBR14
//LIMPEZA  DD DSN=HERC01.CONTBANC.RES,
//            DISP=(MOD,DELETE,DELETE),
//            UNIT=SYSDA,SPACE=(TRK,1)
//STEP1      EXEC PGM=SORT                                              00000203
//SYSPRINT   DD SYSOUT=*                                                00000303
//SYSOUT     DD SYSOUT=*                                                00000403
//SORTLIB    DD DSN=SYS1.SORTLIB,DISP=SHR                               00000503
//SORTIN   DD DSN=HERC01.CONTAS,DISP=SHR
//         DD DSN=HERC01.CONTAS.NOVAS,DISP=SHR
/*
//SORTOUT    DD DSN=&&ARQTEMP,DISP=(NEW,PASS),                          00001103
//            SPACE=(TRK,(1,1)),UNIT=SYSDA,                             00001200
//            DCB=(RECFM=FB,LRECL=80,BLKSIZE=800)                       00001300
//SORTWK01   DD UNIT=2314,SPACE=(CYL,(5,1)),VOL=SER=SORT01              00001503
//SORTWK02   DD UNIT=2314,SPACE=(CYL,(5,1)),VOL=SER=SORT02              00001603
//SORTWK03   DD UNIT=2314,SPACE=(CYL,(5,1)),VOL=SER=SORT03              00001703
//SYSIN      DD *
 SORT FIELDS=(39,4,CH,A)
/*                                                                      00002000
//STEP2      EXEC PGM=CONTMAIN                                          00002103
//STEPLIB    DD DSN=HERC01.CONTBANC.LOADLIB,DISP=SHR                    00002203
//SYSOUT   DD DSN=HERC01.CONTBANC.RES,
//            DISP=(NEW,CATLG,DELETE),
//            UNIT=SYSDA,
//            SPACE=(TRK,(5,2)),
//            DCB=(RECFM=FB,LRECL=80,BLKSIZE=800)
//CONTAS   DD DSN=&&ARQTEMP,DISP=(SHR,DELETE)                           00002401
