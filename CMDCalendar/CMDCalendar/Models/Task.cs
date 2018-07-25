using System;

namespace CMDCalendar.Models
{
    public class Task 
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
        /// deadline_time
        /// </summary>
        public DateTime DeadLine { get; set; }
        /// <summary>
        /// the_right_day_of_the_event
        /// </summary>
        public DateTime EventDay { get; set; }
        /// <summary>
        /// comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// if_is_notificable
        /// </summary>
        public bool IsNotify { get; set; }
        /// <summary>
        /// notification_time
        /// </summary>
        public int LeftTime { get; set; }
        /// <summary>
        /// if_task_is_repeatable
        /// </summary>
        public bool IsRepeatable { get; set; }
        /// <summary>
        /// emergency
        /// </summary>
        public int Emergency { get; set; }
        /// <summary>
        /// if_is_completed
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}