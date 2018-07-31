using CMDCalendar.DB.Database;
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
using CMDCalendar.DB;
using Windows.ApplicationModel;
using System.ComponentModel;
using CMDCalendar.Database;
using CMDCalendar.DB.Database;


namespace CMDCalendar.ViewModels
{
    public class EditPageViewModel:ViewModelBase
    {
        /// <summary>
        /// EditPage ViewModel。
        /// </summary>
        private IDatabaseUtils _databaseUtils;

        public RelayCommand _saveAndQuit;

        public RelayCommand _deleteCommand;

        public Event eventDisplay
        {
            get;
            set;
        }

        public DB.Task taskDisplay
        {
            get;
            set;
        }

        public RelayCommand SaveAndQuit =>
            _saveAndQuit ?? (_saveAndQuit =
            new RelayCommand(async () => 
            {
                var service = _databaseUtils;
                if (eventDisplay != null)
                    await service.UpdateEventAsync(eventDisplay);
                if (taskDisplay != null)
                    await service.UpdateTaskAsync(taskDisplay);
            }));

        public RelayCommand DeleteCommand =>
          _deleteCommand ?? (_deleteCommand =
              new RelayCommand(async () => 
              {
                  var service = _databaseUtils;
                  if(eventDisplay != null)
                      await service.DeleteEventAsync(eventDisplay);
                  else
                      await service.DeleteTaskAsync(taskDisplay);
              }));

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
