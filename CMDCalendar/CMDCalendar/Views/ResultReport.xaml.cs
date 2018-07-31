
using CMDCalendar.DB;
using CMDCalendar.DB.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CMDCalendar.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ResultReport : Page
    {
        public ResultReport() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        {
            this.InitializeComponent();
            var uri = new Uri("ms-appdata:///local/Web/test.html");
            webView2.Navigate(uri);
        }
        public ResultReport(DatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
        }
        public DatabaseUtils _databaseUtils;
        /// <summary>
        /// 生成的数据
        /// </summary>
        private IEnumerable<String> result1;
        private IEnumerable<String> result2;
        private IEnumerable<String> result3;
        private IEnumerable<String> result4;
        private IEnumerable<String> result5;
        private IEnumerable<String> result6;
        private IEnumerable<String> result7;
        /// <summary>
        /// 初始化所有event和task
        /// 
        /// </summary>
        public ObservableCollection<Event> EventsList {
            get;
            set;
        }
        public ObservableCollection<DB.Task> TasksList
        {
            get;
            private set;
        }
        /// <summary>
        /// 生成对应的报告数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MakeData()
        {
            var today = DateTime.Now.Date;
            var today1 = DateTime.Now.Date.AddDays(-1);
            var today2 = DateTime.Now.Date.AddDays(-2);
            var today3 = DateTime.Now.Date.AddDays(-3);
            var today4 = DateTime.Now.Date.AddDays(-4);
            var today5 = DateTime.Now.Date.AddDays(-5);
            var today6 = DateTime.Now.Date.AddDays(-6);
            decimal maxPercent = 0;
            decimal minPercent = 0;
            decimal finishPercent = 0;
            int maxPositionCount = 0;
            int minPositionCount = 0;
            int maxPosition = 0;
            int minPosition = 0;
            int max = 0; //表示数量最多的天的任务数量
            int min = 0; //表示数量最少的天的任务数量
            int[] EventsNumber = new int[7];//表示用户哪天的时间有多少任务的数组
            int failNumber = 0; //表示用户未完成的Task数量
            int doing; //表示的用户现在还在进行的任务
            List<Task> MaxDayTasks = await _databaseUtils.GetTaskListAsync(); ;//表示的是在最忙那天的任务
            MaxDayTasks.Clear();
            List<Task> MinDayTasks = await _databaseUtils.GetTaskListAsync(); ;//表示的是在最闲那天的任务
            MinDayTasks.Clear();
            List<Task> PastTasks = await _databaseUtils.GetTaskListAsync(); ;//表示的是在七天之内应该完成的任务
            PastTasks.Clear();
            List<Task> AllTasks = await _databaseUtils.GetTaskListAsync(); ;//表示的是在顶层的任务列表
            AllTasks.Clear();
            List<Task> tasks = await _databaseUtils.GetTaskListAsync();
            foreach (var task in tasks)
            {
                if (task.StartTime.Date == DateTime.Now.Date || task.StartTime.Date == DateTime.Now.Date.AddDays(-1) || task.StartTime.Date == DateTime.Now.Date.AddDays(-2) || task.StartTime.Date == DateTime.Now.Date.AddDays(-3) || task.StartTime.Date == DateTime.Now.Date.AddDays(-4) || task.StartTime.Date == DateTime.Now.Date.AddDays(-5) || task.StartTime.Date == DateTime.Now.Date.AddDays(-6))
                {
                    AllTasks.Add(task);
                }
            }
            if (AllTasks.Count == 0) { return ; }
            foreach (var task2 in AllTasks)
            {
                if (task2.EndTime.Date == DateTime.Now.Date || task2.EndTime.Date == DateTime.Now.Date.AddDays(-1) || task2.EndTime.Date == DateTime.Now.Date.AddDays(-2) || task2.EndTime.Date == DateTime.Now.Date.AddDays(-3) || task2.EndTime.Date == DateTime.Now.Date.AddDays(-4) || task2.EndTime.Date == DateTime.Now.Date.AddDays(-5) || task2.EndTime.Date == DateTime.Now.Date.AddDays(-6))
                {
                    PastTasks.Add(task2);
                }
            }
            doing = AllTasks.Count() - PastTasks.Count();//doing的值已经完成了
            if (PastTasks.Count == 0) { return; }
            foreach (var task3 in PastTasks) {
                if (task3.IsCompleted == false) {
                    failNumber = failNumber + 1;
                }
                for (int i = 0; i < 7; i++) {
                    if (task3.EndTime.Date == DateTime.Now.Date.AddDays(0-i)) {
                        EventsNumber[6 - i] = EventsNumber[6 - i] + 1;
                    }
                }
            }//解决了完成率还繁忙日期和空闲日期
            for (int c = 0; c < 7; c++) {
                if (EventsNumber[c] > max) {
                    max = EventsNumber[c];
                }
                if (EventsNumber[c] < min) {
                    min = EventsNumber[c];
                }
            }//算出最大值和最小值
            for (int d = 0; d < 7; d++) {
                if (EventsNumber[d] == max) {
                    maxPosition =6-d;
                }
                if (EventsNumber[d] == min) {
                    minPosition =6-d;
                }
            }//算出第几天是事情最多的，第几天是事情最少的
            foreach (var task4 in PastTasks) {
                if (task4.EndTime == DateTime.Now.Date.AddDays(0 - maxPosition)) {
                    MaxDayTasks.Add(task4);
                }
                if (task4.EndTime == DateTime.Now.Date.AddDays(0 - minPosition))
                {
                    MinDayTasks.Add(task4);
                }
            }//生成正常的Max和Min事件列表
            foreach (var task5 in MaxDayTasks) {
                if (task5.IsCompleted == true) {
                    maxPositionCount = maxPositionCount + 1;
                }
            }
            foreach (var task6 in MinDayTasks) {
                if (task6.IsCompleted == true) {
                    minPositionCount = minPositionCount + 1;
                }
            }
            if (MaxDayTasks.Count != 0)
            {
                maxPercent = Math.Round((decimal)maxPositionCount/MaxDayTasks.Count, 2);
            }
            else {
                maxPercent = 0;
            }
            if (MinDayTasks.Count != 0)
            {
                minPercent =Math.Round((decimal)minPositionCount/MinDayTasks.Count,2);
            }
            else {
                minPercent = 0;
            }
            finishPercent = Math.Round(((decimal)(PastTasks.Count() - failNumber) / PastTasks.Count()), 2);
            //开始导入数据
            await webView2.InvokeScriptAsync("taskNumber", new string[] {PastTasks.Count()+"个任务" });
            await webView2.InvokeScriptAsync("task2", new string[] {"其中完成了"+(PastTasks.Count()-failNumber)+"个任务" });
            await webView2.InvokeScriptAsync("task3", new string[] {"您的任务完成率为"+finishPercent*100 + "%" });
            await webView2.InvokeScriptAsync("task4", new string[] { "您本周最忙的一天是" + DateTime.Now.Date.AddDays(0 - maxPosition).Month+"."+ DateTime.Now.Date.AddDays(0 - maxPosition).Day+ "号" });
            await webView2.InvokeScriptAsync("task5", new string[] { "共制定了"+max+"个任务" });
            await webView2.InvokeScriptAsync("task6", new string[] { "完成了其中的"+maxPositionCount+"个" });
            await webView2.InvokeScriptAsync("task7", new string[] { "完成率为"+maxPercent*100+"%" });
            await webView2.InvokeScriptAsync("task8", new string[] { "您本周最闲的一天是" + DateTime.Now.Date.AddDays(0 - minPosition).Month + "." + DateTime.Now.Date.AddDays(0 - minPosition).Day + "号" });
            await webView2.InvokeScriptAsync("task9", new string[] { "共制定了" + min + "个任务" });
            await webView2.InvokeScriptAsync("task10", new string[] { "完成了其中的" + minPositionCount + "个" });
            await webView2.InvokeScriptAsync("task11", new string[] { "完成率为" + minPercent*100 + "%" });
            await webView2.InvokeScriptAsync("task12", new string[] { "感谢您的使用!希望您在新的一周能努力完成剩下的"+doing+"个任务!" });
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //var uri = new Uri("ms-appdata:///local/CMDCalendar/Views/result.html");
            //var message = new MessageDialog(uri+"");
            //message.ShowAsync();
            ApplicationView view = ApplicationView.GetForCurrentView();
            view.TryEnterFullScreenMode();
            Click.Visibility = Visibility.Collapsed;
            MakeData();
        }
    }
}
