using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto1E2.Exceptions;

public class ContaNaoEncontradaException : Exception
{
    public ContaNaoEncontradaException(string message) : base(message)
    {}
}
