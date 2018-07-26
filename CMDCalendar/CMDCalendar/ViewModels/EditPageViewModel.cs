using CMDCalendar.Database;
using GalaSoft.MvvmLight;
using CMDCalendar.DB;
using Windows.ApplicationModel;

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
