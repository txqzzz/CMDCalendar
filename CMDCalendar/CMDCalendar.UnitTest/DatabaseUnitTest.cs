using System;
using System.Linq;
using Windows.UI.Popups;
using CMDCalendar.Database;
using CMDCalendar.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMDCalendar.UnitTest
{
    [TestClass]
    public class DatabaseUnitTest
    {
        /* migrate and update */
        //[TestMethod]
        public void TestDatabaseMigrate()
        {
            var db = new DataContext();
            db.Database.Migrate();
        }
        /* new items test */
        
        [TestMethod]
        
        public async  System.Threading.Tasks.Task TestNewUserAsync()
        {
            var db = new DataContext();
            await db.Database.MigrateAsync();
            var dbu = new DatabaseUtils();
            var users = dbu.GetUserListAsync().ToAsyncEnumerable();
            int actual = await users.Count();
            //Console.Out.WriteLine(actual);
            Assert.AreEqual(1, actual);

            var testUser1 = new User { Id = 4, Username = "Xingqi"};
            var testUser2 = new User() {Id = 5, Username = "Jinhao"};
            await dbu.NewUserAsync(testUser1);
            await dbu.NewUserAsync(testUser2);
            users = dbu.GetUserListAsync().ToAsyncEnumerable();
            //actual = await users.Count();
            //Assert.AreEqual(5, actual);
        }



        /* delete items test*/

        /* update items test*/

        /* list items test */
        [TestMethod]
        public async void TestGetUserList()
        {
            var dbu = new DatabaseUtils();
            var userList = await dbu.GetUserListAsync();
            for (int i = 0; i < userList.Count(); i++)
            {
                var message = new MessageDialog(userList[i].Username);
                await message.ShowAsync();
                 message = new MessageDialog(""+userList[i].Id);
                await message.ShowAsync();
            }
        }
    }
}
