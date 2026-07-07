using System.Runtime.InteropServices;

namespace dotnet.Config.DllConfig;

public class DllConfig 
{
    //impota a função de inicialização do runtime COBOL
    [DllImport("libcob-4", CallingConvention = CallingConvention.Cdecl)]
    public static extern void cob_init(int argc, IntPtr argv);

    [DllImport(@"..\COBOL\SOURCE\CONSCLI", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void CONSCLI(byte[] argumento);

    [DllImport(@"..\COBOL\SOURCE\CADASCLI", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void CADASCLI(byte[] argumento);

    [DllImport(@"..\COBOL\SOURCE\ATUACLI", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void ATUACLI(byte[] argumento);
     
    [DllImport(@"..\COBOL\SOURCE\DELECLI", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void DELECLI(byte[] argumento);
}     