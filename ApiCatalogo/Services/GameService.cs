using ApiCatalogo.Entity;
using ApiCatalogo.Expections;
using ApiCatalogo.InputModel;
using ApiCatalogo.Repository;
using ApiCatalogo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var games = await _gameRepository.Get(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entity = await _gameRepository.Get(game.Name, game.Developer);

            if (entity.Count > 0)
                throw new AlreadyAddedException();

            var newgame = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };

            await _gameRepository.Insert(newgame);

            return new GameViewModel
            {
                Id = newgame.Id,
                Name = newgame.Name,
                Developer = newgame.Developer,
                Price = newgame.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entity = await _gameRepository.Get(id);

            if (entity == null)
                throw new NotRegisteredGame();

            entity.Name = game.Name;
            entity.Developer = game.Developer;
            entity.Price = game.Price;

            await _gameRepository.Update(entity);
        }

        public async Task Update(Guid id, double price)
        {
            var entity = await _gameRepository.Get(id);

            if (entity == null)
                throw new NotRegisteredGame();

            entity.Price = price;

            await _gameRepository.Update(entity);
        }

        public async Task Delete(Guid id)
        {
            var jogo = await _gameRepository.Get(id);

            if (jogo == null)
                throw new NotRegisteredGame();

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
