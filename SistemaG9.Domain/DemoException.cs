using System;

namespace SistemaG9.Domain
{
    public class DemoException : Exception
    {
        public DemoException(string mensagem)
            : base(mensagem)
        {
        }

        public DemoException(string mensagem, Exception exception)
            : base(mensagem, exception)
        {
        }
    }
}
