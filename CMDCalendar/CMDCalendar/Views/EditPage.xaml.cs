using CMDCalendar.DB;
using CMDCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CMDCalendar.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditPage : Page
    {
        public EditPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = (EditPageViewModel)this.DataContext;
            if (e.Parameter == null)
            {
                viewModel.eventDisplay = new Event
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    EventDay = DateTime.Now
                };
                viewModel.taskDisplay = new DB.Task
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    EventDay = DateTime.Now
                };
            }
            else
            {
                try
                {
                    Event EventV = (Event)e.Parameter;
                    viewModel.eventDisplay = EventV;
                    EventStartDate.Date = (DateTimeOffset)EventV.StartTime;
                    EventEndDate.Date = (DateTimeOffset)EventV.EndTime;
                    EventStartTime.Time = new TimeSpan(EventV.StartTime.Hour, EventV.StartTime.Minute, EventV.StartTime.Second);
                    EventEndTime.Time = new TimeSpan(EventV.EndTime.Hour, EventV.EndTime.Minute, EventV.EndTime.Second);
                }
                catch (InvalidCastException)
                {
                    ChoosePivot.SelectedIndex = 1;
                    DB.Task TaskV = (DB.Task)e.Parameter;
                    viewModel.taskDisplay = TaskV;
                    TaskEndDate.Date = (DateTimeOffset)TaskV.EndTime;
                    TaskEndTime.Time = new TimeSpan(TaskV.EndTime.Hour, TaskV.EndTime.Minute, TaskV.EndTime.Second);
                }

            }

        }

        /// <summary>
        /// 导航回主页面。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var message = new ContentDialog()
            {
                Content = "所做的更改将不会被保存。",
                PrimaryButtonText = "确定",
                CloseButtonText = "取消"
            };
            ContentDialogResult result = await message.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var viewModel = (EditPageViewModel)this.DataContext;
                viewModel.taskDisplay = null;
                viewModel.eventDisplay = null;
                Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
            }
        }

        /// <summary>
        /// 左边日历和右边日期选择框绑定。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DateChoosing_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            TaskEndDate.Date = args.AddedDates[0];
            EventStartDate.Date = args.AddedDates[0];
            EventEndDate.Date = args.AddedDates[0];
        }

        private void SaveAndQuitButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (EditPageViewModel)this.DataContext;
            if (ChoosePivot.SelectedIndex == 0)
            {
                viewModel.eventDisplay.StartTime = DateTime.Parse(EventStartDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                viewModel.eventDisplay.StartTime.AddHours(EventStartTime.Time.Hours);
                viewModel.eventDisplay.StartTime.AddMinutes(EventStartTime.Time.Minutes);
                viewModel.eventDisplay.EndTime = DateTime.Parse(EventEndDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                viewModel.eventDisplay.EndTime.AddHours(EventEndTime.Time.Hours);
                viewModel.eventDisplay.EndTime.AddMinutes(EventEndTime.Time.Minutes);
                if (viewModel.eventDisplay.Id == 0)
                {
                    using (var db = new DataContext())
                    {
                        var NewEvent = viewModel.eventDisplay;
                        db.Events.Add(NewEvent);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                viewModel.taskDisplay.EndTime = DateTime.Parse(TaskEndDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                viewModel.taskDisplay.EndTime.AddHours(TaskEndTime.Time.Hours);
                viewModel.taskDisplay.EndTime.AddMinutes(TaskEndTime.Time.Minutes);
                if (viewModel.taskDisplay.Id == 0)
                {
                    using (var db = new DataContext())
                    {
                        var NewTask = viewModel.taskDisplay;
                        db.Tasks.Add(NewTask);
                        db.SaveChanges();
                    }
                }
            }
            Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());

        }
    }
}
