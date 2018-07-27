﻿using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace BackgroundTask
{
    public sealed class SayFarkTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            TimeSpan FifteenMinute = new TimeSpan(0, 0, 15, 0);
            // Debug.Write("================ Fark the farking farkers ================");
            /*ObservableCollection<Event> EventsList = GetEventListAsync();
            foreach (var OneEvent in EventsList)
            {
                if ((OneEvent.EndTime - DateTime.Now) < FifteenMinute && OneEvent.EndTime > DateTime.Now)
                {
                    PopToast(OneEvent.Content);
                }
            }*/
            //CMDCalendar.PopToast.Toast.PopToast("Meeting Notification");
            var content = new ToastContent()
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
                                Text = "Meeting Notification"
                            },
                            /*new AdaptiveText()
                            {
                                Text = "Conf Room 2001 / Building 135"
                            },*/

                            new AdaptiveText()
                            {
                                Text = DateTime.Now.ToString()
                            }
                        }
                        }
                    },

                    Actions = new ToastActionsCustom(),

                    /*Audio = new ToastAudio()
                    {
                        Src = new Uri("ms-appx:///Assets/NewMessage.mp3")
                    }*/
                };
            
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
           

            // content.DisplayTimestamp = new DateTime(2018, 7, 18, 19, 45, 0, DateTimeKind.Utc);
        }

    

        /*public async Task<List<Event>> GetEventListAsync()
        {
            using (var db = new DataContext())
            {
                return await db.Events.ToListAsync();
            }
        }*/

        /*public async Task<List<Event>> GetEventListAsync()
        {
            using (var db = new DataContext())
            {
                return await db.Events.ToListAsync();
            }
        }*/

    }
}
