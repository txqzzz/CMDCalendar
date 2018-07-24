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
    public class MyAssistantViewModel:ViewModelBase
    {
        /// <summary>
        /// 获取所有事件接口
        /// </summary>
        private readonly IDatabaseUtils _databaseUtils;

        ///<summary>
        ///刷新命令
        ///</summary>
        private  RelayCommand _refreshCommand;

        ///<summary>
        ///查看前日命令
        ///</summary>>
        private  RelayCommand _beforeCommand;

        ///<summary>
        ///查看明日命令
        ///</summary>
        public RelayCommand RefreshCommand =>
           _refreshCommand ?? (_refreshCommand =
               new RelayCommand(async() => {await GetList(); var message = new MessageDialog("" +EventsList[0].Comments);await message.ShowAsync(); }));

        public List<Event> EventsList
        {
            get;
            private set;
        }
        public MyAssistantViewModel(IDatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
            EventsList = new  List<Event>();
        }
        public MyAssistantViewModel() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        { }
        public async System.Threading.Tasks.Task GetList() {
            EventsList.Clear();
            var contacts = await _databaseUtils.GetEventListAsync();
            foreach (var contact in contacts)
            {
                EventsList.Add(contact);
            }
        }
    }
}
