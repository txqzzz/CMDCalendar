using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CMDCalendar.Database;
using CMDCalendar.DB;
using Microsoft.EntityFrameworkCore;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CMDCalendar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MigrateButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                db.Database.Migrate();
            }
        }

        private void InsertButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                var user = new User { Username = "Xingqi" };
                var user2 = new User {Username = "Shujie"};
                db.Users.Add(user);
                db.Users.Add(user2);
                db.SaveChanges();

                var evt = new Event { Content = "Debug" };
                db.Events.Add(evt);
                db.SaveChanges();

                var userevt = new UserEvent
                    { User = user, Event = evt };
                db.UserEvents.Add(userevt);
                db.SaveChanges();

                
                



                /*var sms = db.UserEvents.Include(p => p.Event)
                    .Include(p => p.User)
                    .Where(p => p.Event.Content.Contains("Debug"))
                    .ToList();*/
              
                
                /*var sms2 = db.Events
                    .Where(p => p.Content.Contains("Hello"))
                    .Include(p => p.UserEvent).ThenInclude(p => p.User)
                    .First();*/

                /*var missions = db.Events.Include(p => p.UserEvents)
                    .ThenInclude(p => p.Student).ToList*/
            }
        }

        private async void Test_Button_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                    
               // var user = db.Users.FromSql("SELECT * from User").ToList();
                //if (db.Users.Contains())
                //{
                    await new MessageDialog("hello").ShowAsync();
                //}
            }

            var dbu = new DatabaseUtils();
            var userlist = await dbu.GetUserList();


        }
    }
}
