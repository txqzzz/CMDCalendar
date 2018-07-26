using CMDCalendar.Database;
using CMDCalendar.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Popups;

namespace CMDCalendar.ViewModels
{
    /// <summary>
    /// MyAssistant ViewModel
    /// </summary>
    public class MyAssistantViewModel : ViewModelBase
    {
        private int Dayoffset = 0;
        private int DDayoffset = 0;
        /// <summary>
        /// 获取所有事件接口
        /// </summary>
        private readonly IDatabaseUtils _databaseUtils;

        private Event _selectedEvent;
    
        ///<summary>
        ///刷新命令
        ///</summary>
        private RelayCommand _refreshCommand;

        ///<summary>
        ///查看前日命令
        ///</summary>>
        private RelayCommand _beforeCommand;

        ///<summary>
        ///查看明日命令
        ///</summary>
        private RelayCommand _afterCommand;

        /// <summary>
        /// 查看今日命令
        /// </summary>
        private RelayCommand _deleteCommand;

        public RelayCommand RefreshCommand =>
           _refreshCommand ?? (_refreshCommand =
               new RelayCommand(async() => { GetTodayEvents();await GetTodayTasks(); }));


        public RelayCommand BeforeCommand =>
           _beforeCommand ?? (_beforeCommand =
               new RelayCommand(async () => {  GetYesterdayEvents();await GetYesterdayTasks(); }));


        public RelayCommand AfterCommand =>
          _afterCommand ?? (_afterCommand =
              new RelayCommand(async () => {  GetTommorowEvents();await GetTommorowTasks(); }));

        public RelayCommand DeleteCommand =>
          _deleteCommand ?? (_deleteCommand =
              new RelayCommand(async () => { EventsList.Remove(SelectedEvent);await _databaseUtils.DeleteEventAsync(SelectedEvent);}));

        public Event SelectedEvent {
            get => _selectedEvent;
            set => Set(nameof(SelectedEvent), ref _selectedEvent, value);
        }
       

        public ObservableCollection<Event> EventsList
        {
            get;
            private set;
        }

        public ObservableCollection<DB.Task> TasksList
        {
            get;
            private set;
        }

        public MyAssistantViewModel(IDatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
            EventsList = new  ObservableCollection<Event>();
            TasksList = new ObservableCollection<DB.Task>();
        }
        public MyAssistantViewModel() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        { GetTodayEvents(); GetTodayTasks(); SelectedEvent = new Event();  }
        public async System.Threading.Tasks.Task GetTodayEvents() {
            EventsList.Clear();
            var contacts = await _databaseUtils.GetEventListAsync();
            foreach (var contact in contacts)
            {
                if (contact.EventDay.Date == DateTime.Now.Date)
                {
                    EventsList.Add(contact);
                }
            }
        }
        public async System.Threading.Tasks.Task GetTodayTasks()
        {
            TasksList.Clear();
            var tasks = await _databaseUtils.GetTaskListAsync();
            foreach (var task in tasks)
            {
                if (task.EventDay.Date == DateTime.Now.Date)
                {
                    TasksList.Add(task);
                }
            }
        }
        public async System.Threading.Tasks.Task GetTommorowEvents()
        {
            EventsList.Clear();
            var contacts = await _databaseUtils.GetEventListAsync();
            foreach (var contact in contacts)
            {
                if (contact.EventDay.Date == DateTime.Now.Date.AddDays(Dayoffset+1))
                {
                    EventsList.Add(contact);
                }
            }
            Dayoffset = Dayoffset + 1;
        }
        public async System.Threading.Tasks.Task GetTommorowTasks()
        {
            TasksList.Clear();
            var tasks = await _databaseUtils.GetTaskListAsync();
            foreach (var task in tasks)
            {
                if (task.EventDay.Date == DateTime.Now.Date.AddDays(DDayoffset+1))
                {
                    TasksList.Add(task);
                }
            }
            DDayoffset = DDayoffset + 1;
        }
        public async System.Threading.Tasks.Task GetYesterdayEvents()
        {
            EventsList.Clear();
            var contacts = await _databaseUtils.GetEventListAsync();
            foreach (var contact in contacts)
            {
                if (contact.EventDay.Date == DateTime.Now.Date.AddDays(Dayoffset-1))
                {
                    EventsList.Add(contact);
                }
            }
            Dayoffset = Dayoffset - 1;

        }
        public async System.Threading.Tasks.Task GetYesterdayTasks()
        {
            TasksList.Clear();
            var tasks = await _databaseUtils.GetTaskListAsync();
            foreach (var task in tasks)
            {
                if (task.EventDay.Date == DateTime.Now.Date.AddDays(DDayoffset - 1))
                {
                    TasksList.Add(task);
                }
            }
            DDayoffset = DDayoffset - 1;
        }
    }
}
