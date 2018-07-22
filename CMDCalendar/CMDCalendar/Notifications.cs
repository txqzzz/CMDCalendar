using System;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

public class Notifcations
{
    public static void PopToast()
    {
        // Generate the toast notification content and pop the toast
        ToastContent content = GenerateToastContent();
        // content.DisplayTimestamp = new DateTime(2018, 7, 18, 19, 45, 0, DateTimeKind.Utc);
        ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
    }

    public static ToastContent GenerateToastContent()
    {
        return new ToastContent()
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
                            Text = "会议通知"
                        },
                        /*new AdaptiveText()
                        {
                            Text = "Conf Room 2001 / Building 135"
                        },*/

                        new AdaptiveText()
                        {
                            Text = "10:00 AM - 10:30 AM"
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
    }
}