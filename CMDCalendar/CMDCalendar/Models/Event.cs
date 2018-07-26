using System;
using GalaSoft.MvvmLight;

namespace CMDCalendar.Models
{
    public class Event : ObservableObject
    {
        /// <summary>
        /// primary_key:id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// basic_description_of_tasks
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// task_location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// start_time
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// end_time
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// the_right_day_of_the_event
        /// </summary>
        public DateTime EventDay { get; set; }
        /// <summary>
        /// comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// if_event_is_notificable
        /// </summary>
        public bool IsNotify { get; set; }
        /// <summary>
        /// notification_time
        /// </summary>
        public int LeftTime { get; set; }
        /// <summary>
        /// emergency
        /// </summary>
        public int Emergency { get; set; }

    }
}