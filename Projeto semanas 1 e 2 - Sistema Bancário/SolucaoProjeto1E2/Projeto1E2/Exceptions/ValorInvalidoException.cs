using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Exceptions;

public class ValorInvalidoException : Exception
{
    public ValorInvalidoException(string message) : base(message)
    {}
}
