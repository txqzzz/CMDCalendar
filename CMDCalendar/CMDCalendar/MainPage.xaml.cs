using CMDCalendar.Database;
using CMDCalendar.DB;
using CMDCalendar.Views;
using System;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using CMDCalendar.ViewModels;
using Windows.UI.Xaml.Media;
using Windows.UI;
using CMDCalendar.DB.Database;


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
                var user = new DB.User {Username = "Xingqi"};
                var user2 = new DB.User {Username = "Shujie"};

                db.Users.Add(user);
                db.Users.Add(user2);
                db.SaveChanges();


                var evt = new Event {Content = "Debug"};
                db.Events.Add(evt);
                db.SaveChanges();

                var userevt = new UserEvent
                    {User = user, Event = evt};
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
            var user = new DB.User {Id = 3};

            await dbu.UpdateUserAsync(user);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        public async void TestUpdateAsync()
        {
            var dbu = new DatabaseUtils();

            var user = new DB.User {Id = 4, Username = "Yuyang"};

            await dbu.UpdateUserAsync(user);

            var userList = await dbu.GetUserListAsync();
            for (int i = 0; i < userList.Count(); i++)
            {
                var message = new MessageDialog(i + "   " + userList[i].Username);
                await message.ShowAsync();
                message = new MessageDialog(i + "   " + userList[i].Id);
                await message.ShowAsync();
            }

            var eventList = await dbu.GetEventListAsync();
            for (int i = 0; i < eventList.Count(); i++)
            {
                var message = new MessageDialog(i + "   " + eventList[i].Comments);
                await message.ShowAsync();
                message = new MessageDialog(i + "   " + eventList[i].Id);
                await message.ShowAsync();
            }
        }

        /// <summary>
        /// 下面是打开子窗口的功能，变量冲突请修改你的变量。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static bool viewShown = false;

        static bool viewClosed = false;
        static int newViewId;
        static int currentViewId;
        static Frame iframe;

        private async void SummonDragon(object sender, RoutedEventArgs e)
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            var currentView = ApplicationView.GetForCurrentView();
            var viewId = currentView.Id;

            if (viewShown)
            {
                if (viewClosed)
                {
                    await ApplicationViewSwitcher.SwitchAsync(newViewId);

                    viewClosed = false;
                }
                else
                {
                    await ApplicationViewSwitcher.SwitchAsync(newViewId);
                }
            }
            else
            {
                await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>

                {
                    var newWindow = Window.Current;
                    var newAppView = ApplicationView.GetForCurrentView();
                    newAppView.Consolidated += NewAppView_Consolidated;

                    iframe = new Frame();
                    iframe.Navigate(typeof(Myassistant), currentView.Id);

                    newWindow.Content = iframe;
                    newWindow.Activate();
                    newViewId = newAppView.Id;
                });
                await ApplicationViewSwitcher.SwitchAsync(newViewId);

            }
        }

        private void NewAppView_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            viewClosed = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditPage), null,
                new DrillInNavigationTransitionInfo());
        }

        public class List
        {
            public string text { get; set; }
        }

        private System.Collections.ObjectModel.ObservableCollection<List> list =
            new System.Collections.ObjectModel.ObservableCollection<List>();
        /// <summary>
        /// 完成右击菜单显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodoListView_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            ListView listView = (ListView) sender;
            toDoMenuFlayout.ShowAt(listView, e.GetPosition(listView));
            var a = ((FrameworkElement) e.OriginalSource).DataContext;
        }

       

        
        
        /* calendar */
        public class CalendarView
        {
            // TODO
        }

        private void CalendarView_OnCalendarViewDayItemChanging(Windows.UI.Xaml.Controls.CalendarView sender,
            CalendarViewDayItemChangingEventArgs args)
        {
              // TODO
            // Render basic day items.
            if (args.Phase == 0)
            {
                // Register callback for next phase.
                args.RegisterUpdateCallback(CalendarView_OnCalendarViewDayItemChanging);
            }
//            // Set blackout dates.
//            else if (args.Phase == 1)
//            {
//                // Blackout dates in the past, Sundays, and dates that are fully booked.
//                if (args.Item.Date < DateTimeOffset.Now ||
//                    args.Item.Date.DayOfWeek == DayOfWeek.Sunday ||
//                    Events.HasOpenings(args.Item.Date) == false)
//                {
//                    args.Item.IsBlackout = true;
//                }
//                // Register callback for next phase.
//                args.RegisterUpdateCallback(CalendarView_OnCalendarViewDayItemChanging);
//            }
//            // Set density bars.
//            else if (args.Phase == 2)
//            {
//                // Avoid unnecessary processing.
//                // You don't need to set bars on past dates or Sundays.
//                if (args.Item.Date > DateTimeOffset.Now &&
//                    args.Item.Date.DayOfWeek != DayOfWeek.Sunday)
//                {
//                    // Get bookings for the date being rendered.
//                    var currentBookings = Bookings.GetBookings(args.Item.Date);
//
//                    List<Color> densityColors = new List<Color>();
//                    // Set a density bar color for each of the days bookings.
//                    // It's assumed that there can't be more than 10 bookings in a day. Otherwise,
//                    // further processing is needed to fit within the max of 10 density bars.
//                    foreach (booking in currentBookings)
//                    {
//                        if (booking.IsConfirmed == true)
//                        {
//                            densityColors.Add(Colors.Green);
//                        }
//                        else
//                        {
//                            densityColors.Add(Colors.Blue);
//                        }
//                    }
//                    args.Item.SetDensityColors(densityColors);
//                }
//            }
//        }
        }

        private void SwipeItem_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {//To-Do
            var x = args.SwipeControl.DataContext;
        }


        private async void TestReadTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var dbu = new DatabaseUtils();
            var taskList = await dbu.GetTaskListAsync();
            Task testTask = taskList[taskList.Count() - 1];

            Frame.Navigate(typeof(EditPage), testTask, new DrillInNavigationTransitionInfo());
        }

        private async void TestReadEventButton_Click(object sender, RoutedEventArgs e)
        {
            var dbu = new DatabaseUtils();
            var eventList = await dbu.GetEventListAsync();
            Event testEvent = eventList[eventList.Count() - 1];

            Frame.Navigate(typeof(EditPage), testEvent, new DrillInNavigationTransitionInfo());
        }
        
        /// <summary>
        /// 完成获取选定项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TodoListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var viewModel = (SliberPageViewModel)DataContext;
            viewModel.SelectedTask = (Task)e.ClickedItem;

            _SlectedItem = (Task)e.ClickedItem;
        }
       /// <summary>
       /// 完成标记功能
       /// </summary>
        public Task _SlectedItem;
        private void Pin_Click(object sender, RoutedEventArgs e)
        {
            dynamic clickedItem = _SlectedItem;
            ListViewItem item = TodoListView.ContainerFromItem(clickedItem) as ListViewItem;
            item.Background = new SolidColorBrush(Color.FromArgb(81, 12, 252, 122));
        }

        private void NotificationButton_OnClick(object sender, RoutedEventArgs e)
        {
            {
                foreach (var cur in BackgroundTaskRegistration.AllTasks)
                {
                    if (cur.Value.Name == "CMDCalendar")

                    {
                        cur.Value.Unregister(true);
                    }
                }
            }
        }
    }
}
