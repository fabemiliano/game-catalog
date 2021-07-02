using System;
namespace ApiCatalogo.Expections
{
    public class AlreadyAddedException : Exception
    {
        public AlreadyAddedException() : base("Este já jogo está cadastrado")
        {

        }
    }
}
