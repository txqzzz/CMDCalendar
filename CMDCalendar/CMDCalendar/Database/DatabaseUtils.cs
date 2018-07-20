using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMDCalendar.DB;
using Microsoft.EntityFrameworkCore;

namespace CMDCalendar.Database
{
    public class DatabaseUtils
    {
        /// <summary>
        /// database_user
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUserList()
        {
            var db = new DataContext();
            return await db.Users.ToListAsync();
        }

        public async Task<List<Event>> GetEventList()
        {
            var db = new DataContext();
            return await db.Events.ToListAsync();
        }

        public void AddUser(User user)
        {
            // TODO add user 
            var db = new DataContext();
            db.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            var db = new DataContext();
            db.Users.Remove(user);
        }


        public async Task<List<Event>> GetEventList()
        {
            // TODO get event list
        }


    }
}