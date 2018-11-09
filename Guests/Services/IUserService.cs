using System.Collections.Generic;
using System.Threading.Tasks;
using Guests.Models;

namespace Guests.Services
{
    public interface IUserService
    {
        Task RegisterNewUser(UserInfo userInfo);
        Task UpdateUserState(int id, UserState state);
        Task<SystemUser> GetSystemUserOrDefault(string login, string password);
        Task<List<UserInfo>> SearchUsers(string searchString, UserState state);
        Task Finish();
    }
}