using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            this.InitializeComponent();
        }

        /// <summary>
        /// 导航回主页面。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null,
                    new DrillInNavigationTransitionInfo());
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
        
    }
}
