using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogo.InputModel;
using ApiCatalogo.ViewModel;

namespace ApiCatalogo.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);
    }
}
