using CMDCalendar.DB.Database;
using CMDCalendar.DB;
using CMDCalendar.DB.Database;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace CMDCalendar.BackgroundTask
{
    public sealed class SayFarkTask : IBackgroundTask
    {
        private readonly IDatabaseUtils _databaseUtils;
       /* public SayFarkTask(IDatabaseUtils databaseUtils)
        {
            _databaseUtils = databaseUtils;
        }
        public SayFarkTask() : this(DesignMode.DesignModeEnabled ?
                    (DatabaseUtils)null :
                    new DatabaseUtils())
        { }*/
        public async  void Run(IBackgroundTaskInstance taskInstance)
        {
            var dbu = new DatabaseUtils();
            var eventList = await dbu.GetEventListAsync();
            var taskList = await dbu.GetTaskListAsync();

            TimeSpan thirtyMinutes = new TimeSpan(0, 2, 30, 0);
            foreach (var oneEvent in eventList)
            {
                if ((oneEvent.EndTime - DateTime.Now) < thirtyMinutes && oneEvent.EndTime > DateTime.Now)
                {
                    ToastContent content = new ToastContent()
                    {
                        Launch = "action=viewEvent&eventId=1983",
                        Scenario = ToastScenario.Reminder,

                        Visual = new ToastVisual()
                        {

                            BindingGeneric = new ToastBindingGeneric()
                            {
                                Children =
                                {
                                    new AdaptiveText()
                                    {
                                        Text = oneEvent.Content
                                    },
                                    new AdaptiveText()
                                    {
                                        Text =oneEvent.EndTime.ToString()
                                    }
                                }
                            }
                        },

                        Actions = new ToastActionsCustom(),

                        Audio = new ToastAudio()
                        {
                            Src = new Uri("ms-appx:///Assets/NewMessage.mp3")
                        }
                    };

                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
                }                
            }
            
          
            foreach (var oneTask in taskList)
            {
                    if ((oneTask.EndTime - DateTime.Now) < thirtyMinutes && oneTask.EndTime > DateTime.Now)
                    {
                        ToastContent content = new ToastContent()
                        {
                            Launch = "action=viewEvent&eventId=1983",
                            Scenario = ToastScenario.Reminder,

                            Visual = new ToastVisual()
                            {

                                BindingGeneric = new ToastBindingGeneric()
                                {
                                    Children =
                                        {
                                            new AdaptiveText()
                                            {
                                                Text = oneTask.Content
                                            },
                                            new AdaptiveText()
                                            {
                                                Text = oneTask.EndTime.ToString()
                                            }
                                        }
                                }
                            },

                            Actions = new ToastActionsCustom(),

                            Audio = new ToastAudio()
                            {
                                Src = new Uri("ms-appx:///Assets/NewMessage.mp3")
                            }
                        };

                        ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
                    }
            }
        }    
    }
}
