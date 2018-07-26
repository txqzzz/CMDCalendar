using CMDCalendar.Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media;

namespace CMDCalendar.ViewModels
{
    public class SliberPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 被选中的任务
        /// </summary>
        private DB.Task _selectedTask;
        /// <summary>
        /// 刷新命令
        /// </summary>
        private RelayCommand _listCommand;
        /// <summary>
        /// 删除命令
        /// </summary>
        private RelayCommand _deleteCommand;
        /// <summary>
        /// 标记命令
        /// </summary>
        private RelayCommand _pinCommand;
        /// <summary>
        /// 获取所有事件接口
        /// </summary>
        private readonly IDatabaseUtils _databaseUtils;
        /// <summary>
        /// 获取或设置用于呈现选定项的背景的画笔
        /// </summary>
        public Brush SelectedBackground { get; set; }
        /// <summary>
        /// Task集合
        /// </summary>
        public ObservableCollection<DB.Task> TaskCollection
        {
            get;
            private set;
        }


        public DB.Task SelectedTask
        {
            get => _selectedTask;
            set => Set(nameof(SelectedTask), ref _selectedTask, value);
        }


        public SliberPageViewModel(IDatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
            TaskCollection = new ObservableCollection<DB.Task>();
        }
        public SliberPageViewModel() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        { ListTaskItem(); }

        public RelayCommand ListCommand =>
            _listCommand ?? (_listCommand = new RelayCommand(
                async () => { await ListTaskItem(); }));

        public RelayCommand DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new RelayCommand(
                async () => { await DelTaskItem(_selectedTask); }));
        public RelayCommand PinCommand =>
            _pinCommand ?? (_pinCommand = new RelayCommand(
                async () => { await MarkTaskItem(_selectedTask); }));
        /// <summary>
        /// 初始化
        /// </summary>
        public async System.Threading.Tasks.Task ListTaskItem()
        {
            TaskCollection.Clear();
            var taskItems = await _databaseUtils.GetTaskListAsync();
            foreach(var taskItem in taskItems)
            {
                if (taskItem != null)
                    TaskCollection.Add(taskItem);
            }
        }
        public async System.Threading.Tasks.Task DelTaskItem(DB.Task _selectedTask)
        {
            TaskCollection.Remove(_selectedTask);
            var dbu = new DatabaseUtils();
            await dbu.DeleteTaskAsync(_selectedTask);
        }
        public async System.Threading.Tasks.Task MarkTaskItem(DB.Task _selectedTask)
        {

        }
    }
}
