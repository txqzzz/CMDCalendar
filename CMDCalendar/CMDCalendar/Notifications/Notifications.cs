using System;

public class Notifcations
{
	
    public void PopToast()
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
                        },
                    AppLogoOverride = new ToastGenericAppLogo()
                    {
                        Source = "C:\\Users\\asus\\Desktop\\Geeglo.png",
                        HintCrop = ToastGenericAppLogoCrop.Circle
                    },
                    Attribution = new ToastGenericAttributionText()
                    {
                        Text = "Geelgo"
                    }
                }
            },

            Actions = new ToastActionsCustom()
            {
                Inputs =
                    {
                        new ToastSelectionBox("snoozeTime")
                        {
                            DefaultSelectionBoxItemId = "15",
                            Items =
                            {
                                        new ToastSelectionBoxItem("1", "1 minute"),
                                        new ToastSelectionBoxItem("15", "15 minutes"),
                                        new ToastSelectionBoxItem("60", "1 hour"),
                                        new ToastSelectionBoxItem("240", "4 hours"),
                                        new ToastSelectionBoxItem("1440", "1 day")
                            }
                        }
                    },


                Buttons =
                    {
                        new ToastButton("Complete", "complete")
                        {
                            ActivationType = ToastActivationType.Background,
                            ImageUri = "Assets/ToastButtonIcons/Complete.png"
                        },
                        new ToastButton("Dismiss", "dismiss")
                        {
                            ActivationType = ToastActivationType.Background,
                            ImageUri = "Assets/ToastButtonIcons/Dismiss.png"
                        }

                    }

            },
            Header = new ToastHeader("aaa", "日程到期通知", "lll"),

            Audio = new ToastAudio()
            {
                Src = new Uri("ms-appx:///Assets/NewMessage.mp3")
            },

            /* AppLogoOverride = new ToastGenericAppLogo()
             {
                 Source = "https://picsum.photos/48?image=883",
                 HintCrop = ToastGenericAppLogoCrop.Circle
             }*/


            DisplayTimestamp = new DateTime(2018, 7, 18)


        };
    }
}
