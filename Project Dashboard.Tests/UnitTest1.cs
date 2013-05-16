using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDashboard.Models;
using ProjectDashboard.Domain;
using ProjectDashboard.Domain.TimeZoneIntegration;

namespace ProjectDashboard.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var model = new AgileZenModel(53698, "fda90b3f792f412e9a9b3139f9867ded");
        }

        [TestMethod]
        public void GetTimeZoneItems()
        {
            var model = new TimeZoneService();

            var entries = model.GetTimeZoneEntries();

            Assert.IsTrue(entries.Count > 0);
        }
    }
}
