using System;
using NUnit.Framework;

namespace CustomerRepositoryTests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        #region RemoteView
        [Test]
        public void CreateRemoteView()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertRemoteView("123", 1, 4, "hgf", new byte[]{0x00, 0x01}, new byte[]{0x02, 0x03}));
        }

        [Test]
        public void DeleteRemoteView()
        {
            var repository = new CustomerRepository.CustomerRepository();
            repository.DeleteRemoteView("1243", 1, DateTime.UtcNow.ToString("ddMMyyyy"));
        }

        [Test]
        public void GetAllRemoteViews()
        {
            string debugDate = "";
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllRDC("1243", new[] { 1 }, DateTime.Now, 10, 0, out debugDate, "UTC-11");
        }

        [Test]
        public void GetRdcStatistics()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetRdcStatistics("1243", 1, DateTime.UtcNow.AddMinutes(-30), DateTime.UtcNow.AddHours(3), "");
        }

        [Test]
        public void GetRDCImage()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetRDCImage("1243", "hgf");
        }
        #endregion

        #region CustomerHistories

        [Test]
        public void InsertHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertHistory("12434", 1, 5, "test history"));
        }

        [Test]
        public void DeleteHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            repository.DeleteHistory("1243", 2, DateTime.UtcNow.ToString("ddMMyyyy"));
        }

        [Test]
        public void GetAllHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllHistory("1243", 1);
        }

        [Test]
        public void GetAllHistory1()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllHistory("1243");
        }

        [Test]
        public void GetNumBrowsingHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetNumBrowsingHistory("1243", 30);
        }

        #endregion

        #region Bookmarks
        [Test]
        public void InsertBookmarks()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertBookmarks("12434", 1, 5, "bookmard1"));
        }

        [Test]
        public void DeleteBookmarks()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.DeleteBookmarks("12434", 5, DateTime.UtcNow.ToString("ddMMyyyy")));
        }

        [Test]
        public void GetAllBookmarks()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllBookmarks("12434", "", 5);
        }

        #endregion

        #region Blockers
        [Test]
        public void InsertBlocker()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertBlocker("12434", 2, 5, "bookmard1"));
        }

        [Test]
        public void UpdateBlocker()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.UpdateBlocker("12434", 1, 1, "---------"));
        }

        [Test]
        public void DeleteBlocker()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.DeleteBlocker("12434", 1));
        }

        [Test]
        public void GetAllBlocker()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllBlocker("12434", 1);
        }
        #endregion

        #region SMS
        [Test]
        public void InsertSMSMMS()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertSMSMMS("12434", 1, 5, "InBound", "12345", "message", DateTime.Now.ToString("ddMMyyyy")));
        }

        [Test]
        public void DeleteSMSMMS()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.DeleteSMSMMS("12434", 5, DateTime.Now.ToString("ddMMyyyy")));
        }

        [Test]
        public void GetAllSMSMMS()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllSMSMMS("12434", "qqq", 1);
        }

        [Test]
        public void GetNumSMSMMS()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.GetNumSMSMMS("12434", 100));
        }
        #endregion

        #region CallHistory
        [Test]
        public void InsertCallHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertCallHistory("12434", 1, 1, "12345", "InBound", "10", "sergei", DateTime.Now.ToString("ddMMyyyy")));
        }

        [Test]
        public void DeleteCallHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.DeleteCallHistory("12434", 1, DateTime.Now.ToString("ddMMyyyy")));
        }

        [Test]
        public void GetAllCallHistory()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllCallHistory("12434", "qqq", 1);
        }
        #endregion

        #region Location
        [Test]
        public void InsertGPSLocation()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertGPSLocation("12434", 1, 1, "latitude=1, longtitude=2"));
        }

        [Test]
        public void DeleteGPSLocation()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.DeleteGPSLocation("12434", 1, DateTime.Now.ToString("ddMMyyyy")));
        }

        [Test]
        public void GetAllGPSLocation()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetAllGPSLocation("12434", "qqq", 1);
        }
        #endregion

        #region OnlineTime
        [Test]
        public void InsertOnlineTime()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.InsertOnlineTime("12434", 1, 1, "online1",  ", online2"));
        }

        [Test]
        public void DeleteOnlineTime()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.DeleteOnlineTime("12434", 1));
        }

        [Test]
        public void UpdateOnlineTime()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.UpdateOnlineTime("12434", 1, 1, "online1---", ", online2---");
        }

        [Test]
        public void GetOnlineTimeTest()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var res = repository.GetOnlineTime("12434", 1);
        }

        #endregion

        [Test]
        public void GetLoginTest()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var msg = "";
            Console.WriteLine(repository.GetLogin("ret@gmail.com", "abcd", out msg));
            Console.WriteLine(msg);
        }

        [Test]
        public void GetPassTest()
        {
            var repository = new CustomerRepository.CustomerRepository();
            var msg = "";
            Console.WriteLine(repository.GetPass("ret@gmail.com", out msg));
            Console.WriteLine(msg);
        }

        [Test]
        public void GetLoginAnyDeviceTest()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.GetLoginAnyDevice("123", 30));
        }

        [Test]
        public void GetOnlineTimeByDeviceTest()
        {
            var repository = new CustomerRepository.CustomerRepository();
            Console.WriteLine(repository.GetOnlineTimeByDevice(2, 30));
        }
    }
}
