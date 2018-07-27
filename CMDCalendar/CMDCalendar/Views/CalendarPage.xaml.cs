using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.SfSchedule.XForms;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CMDCalendar.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
        }
        ScheduleAppointmentMapping _dataMapping = new ScheduleAppointmentMapping();
        //dataMapping.ColorMapping = "color";  
        _dataMapping.
        _dataMapping.EndTimeMapping = "EndTime";
        dataMapping.StartTimeMapping = "StartTime"; 
        dataMapping.SubjectMapping = "Content";
        calendar.AppointmentMapping = dataMapping;
    }
}
