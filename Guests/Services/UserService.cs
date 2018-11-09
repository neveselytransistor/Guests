using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Guests.Models;
using Microsoft.EntityFrameworkCore;

namespace Guests.Services
{
    public class UserService : IUserService
    {
        public async Task RegisterNewUser(UserInfo userInfo)
        {
            using (var context = new ApplicationContext())
            {
                await context.Users.AddAsync(userInfo);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserState(int id, UserState state)
        {
            using (var context = new ApplicationContext())
            {
                var user = new UserInfo
                {
                    UserInfoId = id,
                    State = state
                };

                context.Attach(user);
                context.Entry(user).Property(u => u.State).IsModified = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<UserInfo>> SearchUsers(string searchString, UserState state)
        {
            using (var context = new ApplicationContext())
            {
                if (string.IsNullOrWhiteSpace(searchString))
                {
                    return await context.Users.ToListAsync();
                }

                var cursor = context.Users.Where(s => s.Name.Contains(searchString) ||
                                                      s.Email.Contains(searchString) ||
                                                      s.Phone.Contains(searchString));

                if (state != UserState.None)
                {
                    cursor = cursor.Where(u => u.State == state);
                }

                return await cursor.ToListAsync();
            }
        }

        public async Task<SystemUser> GetSystemUserOrDefault(string login, string password)
        {
            using (var context = new ApplicationContext())
            {
                return await context.SystemUsers.FirstOrDefaultAsync(u => u.Login == login &&
                                                                          u.Password == password);
            }
        }

        public async Task Finish()
        {
            using (var context = new ApplicationContext())
            {
                await context.Users.Where(u => u.State == UserState.None)
                             .ForEachAsync(u => u.State = UserState.NotParticipate);
                await context.SaveChangesAsync();
            }
        }
    }
}