using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogo.Entity;

namespace ApiCatalogo.Repository
{

    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string developer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }

}
