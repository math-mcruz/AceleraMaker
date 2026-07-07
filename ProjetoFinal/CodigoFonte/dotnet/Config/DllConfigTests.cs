using System.Runtime.InteropServices;

namespace dotnet.Config.DllConfigTests;

public class DllConfigTests 
{
    //impota a função de inicialização do runtime COBOL
    [DllImport("libcob-4.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void cob_init(int argc, IntPtr argv);

    [DllImport(@"..\..\..\..\COBOL\SOURCE\CONSCLI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void CONSCLI(byte[] argumento);

    [DllImport(@"..\..\..\..\COBOL\SOURCE\CADASCLI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void CADASCLI(byte[] argumento);

    [DllImport(@"..\..\..\..\COBOL\SOURCE\ATUACLI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void ATUACLI(byte[] argumento);
     
    [DllImport(@"..\..\..\..\COBOL\SOURCE\DELECLI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void DELECLI(byte[] argumento);
}     