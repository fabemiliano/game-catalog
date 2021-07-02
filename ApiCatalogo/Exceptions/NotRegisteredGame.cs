using System;
namespace ApiCatalogo.Expections
{
    public class NotRegisteredGame : Exception
    {
        public NotRegisteredGame() : base("Este jogo não está cadastrado")
        {
        }
    }
}
