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

namespace CMDCalendar.ViewModels
{
    public class SliberPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 被选中的任务
        /// </summary>
        private DB.Task _selectedTask;
        /// <summary>
        /// 删除命令
        /// </summary>
        private  RelayCommand _deleteCommand;


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
        public RelayCommand DeleteCommand(DB.Task) : IDatabaseUtils
        {

            return;
        }

        

            
    }
}
