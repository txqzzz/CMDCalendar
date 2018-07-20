
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using CMDCalendar.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMDCalendar.UnitTest
{
    [TestClass]
    public class DatabaseUnitTest
    {
        [TestMethod]
        public async Task TestDatabaseInsert()
        {
            var db = new DataContext();
            var userList = db.Users.ToList();
            

            Assert.AreEqual(1,userList.Count);
            Assert.AreEqual(3,userList.Count);

        }
        
    }
}
