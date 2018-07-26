using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using CMDCalendar.DB;
namespace CMDCalendar.ViewModels
{
    /// <summary>   
    /// Represents collection of appointments.   
    /// </summary> 
    public class ViewModel
    {
        public ObservableCollection<Event> Events { get; set; }
        List<string> eventNameCollection;
        List<Color> colorCollection;
        public ViewModel()
        {
            Events = new ObservableCollection<Event>();
            CreateEventNameCollection();
            CreateColorCollection();
            CreateAppointments();
        }

        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        private void CreateAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = GettingTimeRanges();
            DateTime date;
            DateTime DateFrom = DateTime.Now.AddDays(-10);
            DateTime DateTo = DateTime.Now.AddDays(10);
            DateTime dataRangeStart = DateTime.Now.AddDays(-3);
            DateTime dataRangeEnd = DateTime.Now.AddDays(3);

            for (date = DateFrom; date < DateTo; date = date.AddDays(1))
            {
                if ((DateTime.Compare(date, dataRangeStart) > 0) && (DateTime.Compare(date, dataRangeEnd) < 0))
                {
                    for (int AdditionalAppointmentIndex = 0; AdditionalAppointmentIndex < 3; AdditionalAppointmentIndex++)
                    {
                        Event evt = new Event();
                        int hour = (randomTime.Next((int)randomTimeCollection[AdditionalAppointmentIndex].X, (int)randomTimeCollection[AdditionalAppointmentIndex].Y));
                        evt.StartTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        evt.EndTime = (evt.StartTime.AddHours(1));
                        evt.Content = eventNameCollection[randomTime.Next(9)];
                        //evt.Emergency = colorCollection[randomTime.Next(9)];
                        if (AdditionalAppointmentIndex % 3 == 0)
                            evt.IsNotify = true;
                        Events.Add(evt);
                    }
                }
                else
                {
                    Event evt1 = new Event();
                    evt1.StartTime = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    evt1.EndTime = (evt1.StartTime.AddHours(1));
                    evt1.Content = eventNameCollection[randomTime.Next(9)];
                    //evt1.color = colorCollection[randomTime.Next(9)];
                    Events.Add(evt1);
                }
            }
        }

        /// <summary>  
        /// Creates event names collection.  
        /// </summary>  
        private void CreateEventNameCollection()
        {
            eventNameCollection = new List<string>();
            eventNameCollection.Add("General Meeting");
            eventNameCollection.Add("Plan Execution");
            eventNameCollection.Add("Project Plan");
            eventNameCollection.Add("Consulting");
            eventNameCollection.Add("Performance Check");
            eventNameCollection.Add("Yoga Therapy");
            eventNameCollection.Add("Plan Execution");
            eventNameCollection.Add("Project Plan");
            eventNameCollection.Add("Consulting");
            eventNameCollection.Add("Performance Check");
        }

        /// <summary>  
        /// Creates color collection.  
        /// </summary>  
        private void CreateColorCollection()
        {
            colorCollection = new List<Color>();
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }

        /// <summary>
        /// Gets the time ranges.
        /// </summary>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));
            return randomTimeCollection;
        }
    }

}