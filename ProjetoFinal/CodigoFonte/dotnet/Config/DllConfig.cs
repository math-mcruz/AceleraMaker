using System.Runtime.InteropServices;

namespace dotnet.Config.DllConfig;

public class DllConfig 
{
    [DllImport(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\SOURCE\CONSCLI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern void CONSCLI(byte[] argumento);
            //impota a função de inicialização do runtime COBOL
    [DllImport("libcob-4.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void cob_init(int argc, IntPtr argv);
}