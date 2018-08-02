using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMDCalendar.DB.Database;
using Microsoft.EntityFrameworkCore;

namespace CMDCalendar.DB.Database
{
    public class DatabaseUtils : IDatabaseUtils
    {
        /* new items */
        public async Task<Boolean> NewUserAsync(User user)
        {
            using (var db = new DataContext())
            {
                var userFlag = await db.Users.SingleOrDefaultAsync(p => p.Username == user.Username);
                if (userFlag != null)
                {
                    return false;
                    //throw new ArgumentException();
                }

                try
                {
                    var user1 = new User {Username = user.Username, Id = user.Id};
                    db.Users.Add(user1);
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }

            return true;
        }

        public async Task<Boolean> NewEventAsync(Event evt, User user)
        {
            using (var db = new DataContext())
            {
                var existedUser = await db.Users.FirstOrDefaultAsync(p => p.Id == user.Id);
                if (existedUser == null)
                {
                    throw new ArgumentException();
                }

                try
                {
                    var eventFlag = await db.Events.SingleOrDefaultAsync(
                        p => p.Id == evt.Id
                    );


                    try
                    {
                        var evt1 = new Event
                        {
                            IsNotify = evt.IsNotify,
                            Comments = evt.Comments,
                            Content = evt.Content,
                            Emergency = evt.Emergency,
                            EndTime = evt.EndTime,
                            EventDay = evt.EventDay,
                            LeftTime = evt.LeftTime,
                            Location = evt.Location,
                            StartTime = evt.StartTime
                        };
                        db.Events.Add(evt1);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e) when (eventFlag != null)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }

            return true;
        }

        public async Task<Boolean> NewTaskAsync(Task task, User user)
        {
            using (var db = new DataContext())
            {
                var existedUser = await db.Users.FirstOrDefaultAsync(p => p.Id == user.Id);
                if (existedUser == null)
                {
                    throw new ArgumentException();
                }

                try
                {
                    var eventFlag = await db.Events.SingleOrDefaultAsync(
                        p => p.Id == task.Id
                    );


                    try
                    {
                        var task1 = new Task
                        {
                            IsNotify = task.IsNotify,
                            Comments = task.Comments,
                            Content = task.Content,
                            Emergency = task.Emergency,
                            EndTime = task.EndTime,
                            EventDay = task.EventDay,
                            LeftTime = task.LeftTime,
                            Location = task.Location,
                            StartTime = task.StartTime,
                            Id = task.Id,
                            IsCompleted = task.IsCompleted,
                            IsReapeatable = task.IsReapeatable
                        };
                        db.Tasks.Add(task1);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e) when (eventFlag != null)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }

            return true;
        }


        /* delete items */
        public async Task<Boolean> DeleteUserAsync(User user)
        {
            using (var db = new DataContext())
            {
                var delFlag = await db.Users.SingleOrDefaultAsync(
                    p => p.Id == user.Id);
                if (delFlag != null)
                {
                    db.Users.Remove(delFlag);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Boolean> DeleteEventAsync(Event evt)
        {
            using (var db = new DataContext())
            {
                var delFlag = await db.Events.SingleOrDefaultAsync(
                    p => p.Id == evt.Id);
                if (delFlag != null)
                {
                    db.Events.Remove(delFlag);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Boolean> DeleteTaskAsync(Task task)
        {
            using (var db = new DataContext())
            {
                var delFlag = await db.Tasks.SingleOrDefaultAsync(
                    p => p.Id == task.Id);
                if (delFlag != null)
                {
                    db.Tasks.Remove(delFlag);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /* list items */
        public async Task<List<User>> GetUserListAsync()
        {
            using (var db = new DataContext())
            {
                return await db.Users.ToListAsync();
            }
        }

        public async Task<List<Event>> GetEventListAsync()
        {
            using (var db = new DataContext())
            {
                return await db.Events.ToListAsync();
            }
        }

        public async Task<List<Task>> GetTaskListAsync()
        {
            using (var db = new DataContext())
            {
                return await db.Tasks.ToListAsync();
            }
        }


        /* update items*/
        public async Task<Boolean> UpdateUserAsync(User user)
        {
            using (var db = new DataContext())
            {
                var userFlag = await db.Users.FirstOrDefaultAsync(p => p.Id == user.Id);
                if (userFlag != null)
                {
                    userFlag.Username = user.Username;

                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Boolean> UpdateEventAsync(Event evt)
        {
            using (var db = new DataContext())
            {
                var eventFlag = db.Events.AsNoTracking().FirstOrDefault(p => p.Id == evt.Id);
                if (eventFlag != null)
                {
                    eventFlag = evt;
                    db.Events.Update(eventFlag);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Boolean> UpdateTaskAsync(Task task)
        {
            using (var db = new DataContext())
            {
                var taskFlag = db.Tasks.AsNoTracking().FirstOrDefault(p => p.Id == task.Id);
                if (taskFlag != null)
                {
                    taskFlag = task;
                    db.Tasks.Update(taskFlag);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /* seed initialized data */
        public async void SeedDataAsync()
        {
            var dbu = new DatabaseUtils();
            var user1 = new User
            {
                Id = 1,
                Username = "Xingqi"
            };
            var user2 = new User
            {
                Id = 2,
                Username = "Jinhao"
            };
            var user3 = new User
            {
                Id = 3,
                Username = "Shujie"
            };

            await dbu.NewUserAsync(user1);
            await dbu.NewUserAsync(user2);
            await dbu.NewUserAsync(user3);

            var evt1 = new Event
            {
                Id = 1,
                Comments = "Release version publish and end this fucking summer intern.",
                Content = "Realease ",
                EventDay = new DateTime(2018, 8, 2),
                Emergency = 0,
                EndTime = new DateTime(2018, 8,2),
                IsNotify = false,
                LeftTime = -1,
                Location = "Northeastern University ",
                StartTime = new DateTime(2018, 8,2)
            };
            var evt2 = new Event
            {
                Id = 2,
                Comments = "the next level play",
                Content = "v2ray depoyment",
                EventDay = new DateTime(2018,8,2),
                Emergency = 0,
                EndTime = new DateTime(2018, 8,2),
                IsNotify = false,
                LeftTime = -1,
                Location = "Shenyang",
                StartTime = new DateTime(2018, 8,2)
            };

            var evt3 = new Event
            {
                Id = 3,
                Comments = "please notify me!",
                Content = "ddl1",
                EventDay = new DateTime(2018, 8, 2),
                Emergency = 0,
                EndTime = new DateTime(2018, 8, 2, 9, 30, 0),
                IsNotify = false,
                LeftTime = -1,
                Location = "Shenyang",
                StartTime = new DateTime(2018, 8, 2, 8, 30, 0)
            };

            var evt4 = new Event
            {
                Id = 3,
                Comments = "please notify me again!",
                Content = "ddl2",
                EventDay = new DateTime(2018, 8, 2),
                Emergency = 0,
                EndTime = new DateTime(2018, 8, 2, 12, 00, 0),
                IsNotify = false,
                LeftTime = -1,
                Location = "Shenyang",
                StartTime = new DateTime(2018, 8, 2, 8, 30, 0)
            };

            await dbu.NewEventAsync(evt1, user1);
            await dbu.NewEventAsync(evt2, user2);
            await dbu.NewEventAsync(evt3, user1);
            await dbu.NewEventAsync(evt4, user1);

            var task1 = new Task()
            {
                Id = 1,
                Comments = "At least 4 das 1 week.",
                Content = "Running",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 8,1),
                EndTime = new DateTime(2018, 8,8),
                EventDay = new DateTime(2018,8,8),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
            };

            var task2 = new Task()
            {
                Id = 2,
                Comments = "Once more",
                Content = "IELTS vocabulary",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 8,3),
                EndTime = new DateTime(2018, 8,10),
                EventDay = new DateTime(2018,8,10),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
            };

            var lastWeek1 = new Task()
            {
                Id = 3,
                Comments = "Once more",
                Content = "Let it go one!",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 7,23),
                EndTime = new DateTime(2018, 7,27),
                EventDay = new DateTime(2018, 7,27),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
            };
            var lastWeek2 = new Task()
            {
                Id = 4,
                Comments = "Once more",
                Content = "Let it go two!",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 7, 28),
                EndTime = new DateTime(2018, 7, 28),
                EventDay = new DateTime(2018, 7, 28),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
            };
            var lastWeek3 = new Task()
            {
                Id = 5,
                Comments = "Once more",
                Content = "Let it go three!",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 7, 27),
                EndTime = new DateTime(2018, 7, 30),
                EventDay = new DateTime(2018, 7, 30),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
            };
            var lastWeek4 = new Task()
            {
                Id = 6,
                Comments = "Once more",
                Content = "Let it go four!",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 7, 27),
                EndTime = new DateTime(2018, 7, 31),
                EventDay = new DateTime(2018, 7, 31),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = true,
            };

            var lastWeek5 = new Task()
            {
                Id = 7,
                Comments = "Once more",
                Content = "Let it go five!",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 7, 27),
                EndTime = new DateTime(2018, 7, 28),
                EventDay = new DateTime(2018, 7, 28),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
            };
            await dbu.NewTaskAsync(task1, user1);
            await dbu.NewTaskAsync(task2, user3);
            await dbu.NewTaskAsync(lastWeek1, user3);
            await dbu.NewTaskAsync(lastWeek2, user3);
            await dbu.NewTaskAsync(lastWeek3, user3);
            await dbu.NewTaskAsync(lastWeek4, user3);
            await dbu.NewTaskAsync(lastWeek5, user3);

            //EditPage test data


            var user4 = new User
            {
                Id = 4,
                Username = "Nagato"
            };

            var taskE = new Task()
            {
                Id = 3,
                Comments = "for someone like you",
                Content = "Preparation",
                IsReapeatable = false,
                StartTime = new DateTime(2018, 8, 10),
                EndTime = new DateTime(2018, 8, 31),
                EventDay = new DateTime(2018, 8, 31),
                LeftTime = -1,
                Emergency = 0,
                IsCompleted = false,
                Location = "Homeland"

            };

            var eventE = new Event
            {
                Id = 3,
                Comments = "ovo",
                Content = "Birthday",
                IsNotify = false,
                EventDay = new DateTime(2018, 8,31),
                StartTime = new DateTime(2018, 8, 31, 12, 0, 0),
                EndTime = new DateTime(2018, 8, 31, 14, 0, 0),

                LeftTime = -1,
                Emergency = 0,
                Location = "China"
            };

            await dbu.NewUserAsync(user4);
            await dbu.NewEventAsync(eventE, user4);
            await dbu.NewTaskAsync(taskE, user4);
        }
    }
}