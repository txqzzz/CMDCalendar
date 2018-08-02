using CMDCalendar.DB;
using CMDCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
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
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            initializeSubWindowGlass(GlassSubWindow);
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.Transparent;
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
            (App.Current.Resources["SliberPageViewModel"] as SliberPageViewModel).ListTaskItem();
            

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
            
        }
        private void Front_Click(object sender, RoutedEventArgs e)
        {
            Front.IsEnabled = false;
            Behind.IsEnabled = false;
            DateOffset = DateOffset - 1;
            Disappear.Completed += (o, s) =>
            {
                Date.Text = DateTime.Now.AddDays(DateOffset).ToShortDateString();
                Appear.Begin();
                Front.IsEnabled = true;
                Behind.IsEnabled = true;
            };
            Disappear.Begin();

            
        }

        private void Behind_Click(object sender, RoutedEventArgs e)
        {
            Front.IsEnabled = false;
            Behind.IsEnabled = false;
            DateOffset = DateOffset + 1;
            Disappear.Completed += (o, s) =>
            {
                Date.Text = DateTime.Now.AddDays(DateOffset).ToShortDateString();
                Appear.Begin();
                Front.IsEnabled = true;
                Behind.IsEnabled = true;
            };
            Disappear.Begin();
        }
        /// <summary>
        /// 用户可改变编辑状态函数
        /// </summary>

        private void TaskItem_Click(object sender, ItemClickEventArgs e)
        {
            //Delete.IsEnabled = true;
            var viewModel = (MyAssistantViewModel)this.DataContext;
            viewModel.SelectedTask = (DB.Task)e.ClickedItem;
        }

        private void initializeSubWindowGlass(UIElement MainPageMainGrid)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(MainPageMainGrid);
            Compositor compositor = hostVisual.Compositor;
            var backdropBrush = compositor.CreateHostBackdropBrush();
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = backdropBrush;
            ElementCompositionPreview.SetElementChildVisual(MainPageMainGrid, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }
    }
}

