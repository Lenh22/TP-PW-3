using Microsoft.EntityFrameworkCore;
using TP_PW3.Models;

namespace TP_PW3.Data
{

    public interface IUserServices
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserDetails(int id);
        Task<bool> InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);

        Task<bool> SaveUser(User user);

        Task<User> GetByUserName(string userName);

    }
    public class UserServices : IUserServices
    {
        private readonly MyContext MyBooksContext;

        public UserServices(MyContext myBooksContext) {
            this.MyBooksContext = myBooksContext;
        }
        public async Task<User> GetByUserName(string userName)
        {
            return await MyBooksContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
        }
        public async Task<bool> DeleteUser(int id)
        {
            var UserFind = await MyBooksContext.Users.FindAsync(id);

            MyBooksContext.Users.Remove(UserFind);

            return await MyBooksContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await MyBooksContext.Users.ToListAsync();
        }

        public async Task<User> GetUserDetails(int id)
        {
            return await MyBooksContext.Users.FindAsync(id);
        }

        public async Task<bool> InsertUser(User user)
        {
            MyBooksContext.Users.Add(user);

            return await MyBooksContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveUser(User user)
        {
            if (user.UserId > 0)
            {
                return await UpdateUser(user);
            }
            else
            {
                return await InsertUser(user);
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            MyBooksContext.Entry(user).State = EntityState.Modified;

            return await MyBooksContext.SaveChangesAsync() > 0;
        }
    }
}

