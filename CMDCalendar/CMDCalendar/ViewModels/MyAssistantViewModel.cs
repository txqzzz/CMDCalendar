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
        /// <summary>
        /// 获取所有事件接口
        /// </summary>
        private readonly IDatabaseUtils _databaseUtils;

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

        public RelayCommand RefreshCommand =>
           _refreshCommand ?? (_refreshCommand =
               new RelayCommand(async() => { await GetTodayEvents(); }));
        public RelayCommand BeforeCommand =>
           _beforeCommand ?? (_beforeCommand =
               new RelayCommand(async () => { await GetYesterdayEvents(); }));
        public RelayCommand AfterCommand =>
          _afterCommand ?? (_afterCommand =
              new RelayCommand(async () => { await GetTommorowEvents(); }));

        public ObservableCollection<Event> EventsList
        {
            get;
            private set;
        }
        public MyAssistantViewModel(IDatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
            EventsList = new  ObservableCollection<Event>();
        }
        public MyAssistantViewModel() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        { GetTodayEvents();  }
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
    }
}
