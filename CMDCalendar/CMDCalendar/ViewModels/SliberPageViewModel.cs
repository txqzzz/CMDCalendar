using CMDCalendar.DB.Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media;
using CMDCalendar.DB;
using System.Linq;
using CMDCalendar.DB.Database;
using Windows.UI.Xaml.Controls;
using Windows.UI;

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
        private RelayCommand _slistCommand;
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
        /// Task集合
        /// </summary>
        public ObservableCollection<DB.Task> TaskCollection
        {
            get;
            private set;
        }
        public ObservableCollection<DB.Event> EventCollection
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
            EventCollection = new ObservableCollection<DB.Event>();
        }
        public SliberPageViewModel() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        {   ListTaskItem();
            ListEventItem();
        }

        public RelayCommand ListCommand =>
            _listCommand ?? (_listCommand = new RelayCommand(
                async () => { await ListTaskItem(); }));
        public RelayCommand SListCommand =>
            _slistCommand ?? (_slistCommand = new RelayCommand(
                async () => { await ListEventItem(); }));

        public RelayCommand DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new RelayCommand(
                async () => { await DelTaskItem(_selectedTask); }));

        /// <summary>
        /// 初始化
        /// </summary>
        
        public async System.Threading.Tasks.Task  RefreshTask(DB.Task _seletedTask)
        {
            var dbu = new DatabaseUtils();
            await dbu.UpdateTaskAsync(_selectedTask);
        }
       
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
        public async System.Threading.Tasks.Task ListEventItem()
        {
            EventCollection.Clear();
            var eventItems = await _databaseUtils.GetEventListAsync();
            foreach (var eventItem in eventItems)
            {
                if (eventItem != null)
                    EventCollection.Add(eventItem);
            }
            OderBy(EventCollection);
        }
        /// <summary>
        /// 日程优先级排序
        /// </summary>
        /// <param name="EventCollection"></param>
        public void OderBy(ObservableCollection<DB.Event> EventCollection)
        {
            for(int i=0;i<EventCollection.Count-1;i++)
            {
                for(int j=EventCollection.Count-1;j>i;j--)
                {
                    if(EventCollection[j].Emergency>EventCollection[j-1].Emergency)
                    {
                        DB.Event temp = EventCollection[j-1];
                        EventCollection[j - 1] = EventCollection[j];
                        EventCollection[j] = temp;
                    }
                }
            }
        }
        public async System.Threading.Tasks.Task DelTaskItem(DB.Task _selectedTask)
        {
            TaskCollection.Remove(_selectedTask);
            var dbu = new DatabaseUtils();
            await dbu.DeleteTaskAsync(_selectedTask);
        }
    }
}
