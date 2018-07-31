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
        private bool Edited;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.Activate();
            var viewModel = (EditPageViewModel)this.DataContext;
            if (e.Parameter == null)
            {
                viewModel.eventDisplay = new Event
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    EventDay = DateTime.Now,
                    Emergency = 0
                };
                viewModel.taskDisplay = new DB.Task
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    EventDay = DateTime.Now,
                    Emergency = 0
                };
                EventStartDate.Date = viewModel.eventDisplay.StartTime;
                EventEndDate.Date = viewModel.eventDisplay.EndTime;
                TaskEndDate.Date = viewModel.taskDisplay.EndTime;
            }
            else
            {
                try
                {
                    Event EventV = (Event)e.Parameter;
                    viewModel.eventDisplay = EventV;
                    EmergencyStage.SelectedIndex = EventV.Emergency;
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
                    EmergencyStage.SelectedIndex = TaskV.Emergency;
                    TaskEndDate.Date = (DateTimeOffset)TaskV.EndTime;
                    TaskEndTime.Time = new TimeSpan(TaskV.EndTime.Hour, TaskV.EndTime.Minute, TaskV.EndTime.Second);
                }
            }
            Edited = false;
        }

        /// <summary>
        /// 导航回主页面。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var viewModel = (EditPageViewModel)this.DataContext;
            if (Edited == true)
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
                    viewModel.taskDisplay = null;
                    viewModel.eventDisplay = null;
                    Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
                }
            }
            else
                Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// 左边日历和右边日期选择框绑定。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DateChoosing_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            try
            {
                TaskEndDate.Date = args.AddedDates[0];
                EventStartDate.Date = args.AddedDates[0];
                EventEndDate.Date = args.AddedDates[0];
            }
            catch(System.Runtime.InteropServices.COMException)
            { }
        }

        private async void SaveAndQuitButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (EditPageViewModel)this.DataContext;
            if (ChoosePivot.SelectedIndex == 0)
            {
                if ((EventStartDate.Date > EventEndDate.Date) || (EventStartTime.Time > EventEndTime.Time))
                {
                    var message = new ContentDialog()
                    {
                        Content = "开始时间不能晚于结束时间。",
                        CloseButtonText = "确定"
                    };
                    await message.ShowAsync();
                }
                else
                {
                    viewModel.eventDisplay.StartTime = DateTime.Parse(EventStartDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                    viewModel.eventDisplay.StartTime = viewModel.eventDisplay.StartTime.AddHours(EventStartTime.Time.Hours);
                    viewModel.eventDisplay.StartTime = viewModel.eventDisplay.StartTime.AddMinutes(EventStartTime.Time.Minutes);
                    viewModel.eventDisplay.EndTime = DateTime.Parse(EventEndDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                    viewModel.eventDisplay.EndTime = viewModel.eventDisplay.EndTime.AddHours(EventEndTime.Time.Hours);
                    viewModel.eventDisplay.EndTime = viewModel.eventDisplay.EndTime.AddMinutes(EventEndTime.Time.Minutes);
                    viewModel.eventDisplay.EventDay = DateTime.Parse(EventEndDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                    viewModel.eventDisplay.EventDay = viewModel.eventDisplay.EventDay.AddHours(EventEndTime.Time.Hours);
                    viewModel.eventDisplay.EventDay = viewModel.eventDisplay.EventDay.AddMinutes(EventEndTime.Time.Minutes);
                    if (viewModel.eventDisplay.Id == 0)
                    {
                        using (var db = new DataContext())
                        {
                            var NewEvent = viewModel.eventDisplay;
                            db.Events.Add(NewEvent);
                            db.SaveChanges();
                        }
                    }
                    Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
                }
            }
            else
            {
                viewModel.taskDisplay.EndTime = DateTime.Parse(TaskEndDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                viewModel.taskDisplay.EndTime = viewModel.taskDisplay.EndTime.AddHours(TaskEndTime.Time.Hours);
                viewModel.taskDisplay.EndTime = viewModel.taskDisplay.EndTime.AddMinutes(TaskEndTime.Time.Minutes);
                viewModel.taskDisplay.EventDay = DateTime.Parse(TaskEndDate.Date.Value.DateTime.ToString("yyyy-MM-dd"));
                viewModel.taskDisplay.EventDay = viewModel.taskDisplay.EventDay.AddHours(TaskEndTime.Time.Hours);
                viewModel.taskDisplay.EventDay = viewModel.taskDisplay.EventDay.AddMinutes(TaskEndTime.Time.Minutes);
                if (viewModel.taskDisplay.Id == 0)
                {
                    using (var db = new DataContext())
                    {
                        var NewTask = viewModel.taskDisplay;
                        db.Tasks.Add(NewTask);
                        db.SaveChanges();
                    }
                }
                Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
            }

        }

        private void DeleteBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Edited = true;
        }

        private void EmergencyStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (EditPageViewModel)this.DataContext;
            viewModel.eventDisplay.Emergency = EmergencyStage.SelectedIndex;
            viewModel.taskDisplay.Emergency = EmergencyStage.SelectedIndex;
        }
    }
}
