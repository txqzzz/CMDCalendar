using CMDCalendar.Database;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMDCalendar.DB;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.UI.Popups;

namespace CMDCalendar.ViewModels
{
    public class EditPageViewModel:ViewModelBase
    {
        /// <summary>
        /// EditPage ViewModel。
        /// </summary>
        private IDatabaseUtils _databaseUtils;

        public Event eventDisplay { get; set; }

        public DB.Task taskDisplay{ get; private set; }

        public EditPageViewModel(IDatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
            eventDisplay = new Event();
            taskDisplay = new DB.Task();
        }
        public EditPageViewModel():this(DesignMode.DesignModeEnabled?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        { }
    }
}
