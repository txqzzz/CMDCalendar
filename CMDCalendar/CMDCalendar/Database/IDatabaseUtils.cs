using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMDCalendar.DB;

namespace CMDCalendar.Database
{
    public class IDatabaseUtils
    {
        /// <summary>
        /// <interface>database access interface</interface>
        /// </summary>
        public interface IDatabaseAccess
        {
            /* new items */
            Task<Boolean> NewUserAsync(User user);
            Task<Boolean> NewEventAsync(Event evt, User user);

            /* delete items */
            Task<Boolean> DeleteUserAsync(User user);
            Task<Boolean> DeleteEventAsync(Event evt);

            /* list items */
            Task<List<User>> GetUserListAsync();
            Task<List<Event>> GetEventListAsync();


            /* update items*/
            Task<Boolean> UpdateUserAsync(User user);
            Task<Boolean> UpdateEventAsync(Event evt);
        }
    }
}