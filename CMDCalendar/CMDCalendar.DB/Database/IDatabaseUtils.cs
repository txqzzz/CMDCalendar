using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMDCalendar.DB;

namespace CMDCalendar.DB.Database
{

    /// <summary>
    /// <interface>database access interface</interface>
    /// </summary>
    public interface IDatabaseUtils
        {
            /* new items */
            Task<Boolean> NewUserAsync(User user);
            Task<Boolean> NewEventAsync(Event evt, User user);
            Task<Boolean> NewTaskAsync(DB.Task task, User user);

            /* delete items */
            Task<Boolean> DeleteUserAsync(User user);
            Task<Boolean> DeleteEventAsync(Event evt);
            Task<Boolean> DeleteTaskAsync(DB.Task task);

            /* list items */
            Task<List<User>> GetUserListAsync();
            Task<List<Event>> GetEventListAsync();
            Task<List<DB.Task>> GetTaskListAsync();

            /* update items*/
            Task<Boolean> UpdateUserAsync(User user);
            Task<Boolean> UpdateEventAsync(Event evt);
            Task<Boolean> UpdateTaskAsync(DB.Task task);

        /* initial data */
            void SeedDataAsync();
        }
}