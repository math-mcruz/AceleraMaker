using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Exceptions;

public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException(string mensagem) : base(mensagem)
    { }
    
}
