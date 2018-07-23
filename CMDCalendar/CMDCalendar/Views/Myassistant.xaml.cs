using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class Myassistant : Page
    {
        public Myassistant()
        {
            this.InitializeComponent();
            Date.Text = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// 日期偏差
        /// </summary>
        private int DateOffset = 0;

        private int parentId;
        /// <summary>
        /// 重写，接受参数
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            parentId = Convert.ToInt32(e.Parameter.ToString());
        }

        private async void Minimize_Click(object sender, RoutedEventArgs e)
        {
            var currentView = ApplicationView.GetForCurrentView();
            var viewId = currentView.Id;
            await ApplicationViewSwitcher.SwitchAsync(parentId);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Front_Click(object sender, RoutedEventArgs e)
        {
            Date.Text=DateTime.Now.AddDays(DateOffset-1).ToShortDateString();
            DateOffset = DateOffset - 1;
        }

        private void Behind_Click(object sender, RoutedEventArgs e)
        {
            Date.Text = DateTime.Now.AddDays(DateOffset + 1).ToShortDateString();
            DateOffset = DateOffset + 1;
        }
    }
}

