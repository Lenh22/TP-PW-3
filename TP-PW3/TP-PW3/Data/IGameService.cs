using Microsoft.EntityFrameworkCore;
using TP_PW3.Models;

namespace TP_PW3.Data
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameDetails(int id);
        Task<bool> InsertGame(Game game);
        Task<bool> UpdateGame(Game game);
        Task<bool> DeleteGame(int id);

        Task<bool> SaveGame(Game game);

    }

    public class GameService : IGameService
    {

        private readonly MyContext MyBooksContext;

        public GameService(MyContext myBooksContext)
        {
            this.MyBooksContext = myBooksContext;
        }
        public async Task<bool> DeleteGame(int id)
        {
            var GameFind = await MyBooksContext.Games.FindAsync(id);

            MyBooksContext.Games.Remove(GameFind);

            return await MyBooksContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await MyBooksContext.Games.ToListAsync();
        }

        public async Task<Game> GetGameDetails(int id)
        {
            return await MyBooksContext.Games.FindAsync(id);
        }

        public async Task<bool> InsertGame(Game game)
        {
            MyBooksContext.Games.Add(game);

            return await MyBooksContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveGame(Game game)
        {
            if (game.GameId > 0)
            {
                return await UpdateGame(game);
            }
            else
            {
                return await InsertGame(game);
            }
        }

        public async Task<bool> UpdateGame(Game game)
        {
            MyBooksContext.Entry(game).State = EntityState.Modified; ;

            return await MyBooksContext.SaveChangesAsync() > 0;
        }
    }
}
