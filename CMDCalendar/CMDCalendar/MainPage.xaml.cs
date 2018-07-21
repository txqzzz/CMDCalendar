﻿using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CMDCalendar.Database;
using CMDCalendar.DB;
using Microsoft.EntityFrameworkCore;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using CMDCalendar.Views;

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
            /*using (var db = new DataContext())
            {
                db.Database.Migrate();
            }*/
            throw new NotImplementedException();
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
            }
        }

        private void Test_Button_OnClick(object sender, RoutedEventArgs e)
        {
            //TestGetUserList();
            //TestDeleteUser();
            TestUpdateAsync();
        }
        public async void TestGetUserList()
        {
            var dbu = new DatabaseUtils();
            var userList = await dbu.GetUserListAsync();
            /*for (int i = 0; i < userList.Count(); i++)
            {
                var message = new MessageDialog(userList[i].Username);
                await message.ShowAsync();
                message = new MessageDialog("" + userList[i].Id);
                await message.ShowAsync();
            }*/
        }

        public async void TestDeleteUser()
        {
            var dbu = new DatabaseUtils();
            var user = new User {Id = 3};
            await dbu.UpdateUserAsync(user);
            
            
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void TestUpdateAsync()
        {
            var dbu = new DatabaseUtils();
            var user = new User {Id = 4, Username = "Yuyang"};
            await dbu.UpdateUserAsync(user);

            var userList = await dbu.GetUserListAsync();
            for (int i = 0; i < userList.Count(); i++)
            {
                var message = new MessageDialog(i + "   " + userList[i].Username);
                await message.ShowAsync();
                message = new MessageDialog(i + "   " + userList[i].Id);
                await message.ShowAsync();
            }
        }
        /// <summary>
        /// 从这之后是打开子窗口的代码，其中的变量名称冲突请改自己的变量名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static bool viewShown = false;
        static bool viewClosed = false;
        static int newViewId;
        static int currentViewId;
        static Frame frame;
        public async void Summon_Click(object sender, bool e)
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            if (viewShown)
            {
                if (viewClosed)
                {
                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
                    viewClosed = false;
                }
                else
                {
                    await ApplicationViewSwitcher.SwitchAsync(newViewId);
                }
            }
            else
            {
                await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var newWindow = Window.Current;
                    var newAppView = ApplicationView.GetForCurrentView();
                    frame = new Frame();
                    frame.Navigate(typeof(Myassistant));
                    newWindow.Content = frame;
                    newWindow.Activate();
                    newViewId = newAppView.Id;
                });
                viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
            }
        }

        private void NewAppView_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            viewClosed = true;
        }

        private void Summon_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
