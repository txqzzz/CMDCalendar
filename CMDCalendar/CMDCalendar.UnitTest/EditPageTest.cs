using CMDCalendar.DB;
using CMDCalendar.ViewModels;
using CMDCalendar.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etg.SimpleStubs;

namespace CMDCalendar.UnitTest
{
    [TestClass]
    public class EditPageTest
    {
        [TestMethod]
        public void TestSaveAndQuit()
        {
            Event savedEvent = null;
            var testEvent = new Event
            {
                Comments = "Unittest Event Comments",
                Content = "Unittest Event Content",
                EventDay = new DateTime(2018, 7, 7),
                StartTime = new DateTime(2018, 7, 6, 1, 1, 4),
                EndTime = new DateTime(2018, 7, 7, 5, 1, 4),
                Emergency = 1,
                Location = "Unittest Event Location"
            };

            var stubIDatabaseUtils =
                new StubIDatabaseUtils().UpdateEventAsync(async (c) =>
                    savedEvent = c);

            var viewModel = new EditPageViewModel(stubIDatabaseUtils);
            viewModel.SaveAndQuit.Execute(testEvent);

            Assert.AreSame(testEvent, savedEvent);
        }
    }
}
