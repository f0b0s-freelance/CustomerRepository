using System;
using System.Collections.Specialized;
using CustomerRepository;
using NUnit.Framework;
using SPC.Library;

namespace CustomerRepositoryTests
{
    [TestFixture]
    public class CustomerRepositoryHelperTests
    {
        #region Customer

        [Test]
        public void InsertCustomerTest()
        {
            var customer = new CustomerData
                           {
                               LicenseId = "123",
                               FirstName = "Sergei",
                               LastName = "oak",
                               DateOfBirth = DateTime.Now.Date.ToString("yyyyMMdd"),
                               Email = "ret@gmail.com",
                               IsEmailVerified = false,
                               Timezone = "UTC",
                               HashPass = "abcd",
                               PaymentProfile = "test",
                               Expires = DateTime.Now,
                               Obs = "",
                               Status = "",
                               Type = ""
                           };
            var repository = new CustomerRepositoryHelper();
            string licenseId;
            Console.WriteLine(repository.InsertCustomer(customer, out licenseId));
        }

        [Test]
        public void InsertIPN()
        {
            var repository = new CustomerRepositoryHelper();
            var properties = new NameValueCollection
                             {
                                 {"custom","1234|product"},
                                 {"receiver_email","receiver_email"},
                                 {"receiver_id","receiver_id"},
                                 {"residence_country","residence_country"},
                                 {"test_ipn","test_ipn"},
                                 {"transaction_subject","transaction_subject"},
                             };
            Console.WriteLine(repository.InsertIPN(properties, "call"));
        }

        [Test]
        public void UpdateCustomer()
        {
            var repository = new CustomerRepositoryHelper();
            var customer = new CustomerData
                           {
                               LicenseId = "123",
                               FirstName = "Sergei1",
                               LastName = "oak2",
                               DateOfBirth = DateTime.Now.Date.ToString("yyyyMMdd"),
                               Email = "ret@gmail.com3",
                               IsEmailVerified = false,
                               Timezone = "UTC4",
                               HashPass = "abcd5",
                               PaymentProfile = "test6",
                               Expires = DateTime.Now,
                               Obs = "7",
                               Status = "8",
                               Type = "9"
                           };
            Console.WriteLine(repository.UpdateCustomer(customer));
        }

        [Test]
        public void ActivateEmailTest()
        {
            var repository = new CustomerRepositoryHelper();
            var customer = new CustomerData
                           {
                               LicenseId = "123"
                           };
            Console.WriteLine(repository.ActivateEmail(customer));
        }

        [Test]
        public void DeleteCustomerTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteCustomer("123"));
        }

        [Test]
        public void GetCustomerTest()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetCustomer("123");
        }

        [Test]
        public void GetAllCustomers()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllCustomers();
        }

        [Test]
        public void GetCustomerEmail()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetCustomerEmail("ret@gmail.com");
        }
        #endregion

        #region Child
        [Test]
        public void InsertChildTest()
        {
            var repository = new CustomerRepositoryHelper();
            var childData = new ChildData
                            {
                                LicenseId = "1234",
                                Name = "Roman",
                                Avatar = "eagle",
                                Obs = "",
                                Category = "category"
                            };
            Console.WriteLine(repository.InsertChild(childData));
        }

        [Test]
        public void UpdateChild()
        {
            var repository = new CustomerRepositoryHelper();
            var childData = new ChildData
                            {
                                Id = 1,
                                LicenseId = "12345",
                                Name = "Sergei1",
                                Avatar = "oak1",
                                Obs = "1",
                                Category = "category1"
                            };
            Console.WriteLine(repository.UpdateChild(childData));
        }

        [Test]
        public void DeleteChildTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteChild("123", null));
        }

        [Test]
        public void GetAllChilds()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllChilds("123");
        }

        [Test]
        public void GetChild()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetChild(2);
        }

        [Test]
        public void GetNumChildsTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.GetNumChilds("1234"));
        }
        #endregion

        #region Devices
        [Test]
        public void InsertDeviceTest()
        {
            var repository = new CustomerRepositoryHelper();
            var device = new ChildDevices
                         {
                             LicenseId = "123",
                             Tbpid = "432",
                             ChildId = 2,
                             Name = "Name",
                             Avatar = "avatar",
                             OnlineTimeFrames = "otf",
                             OnlineWebTimeFrames = "owtf"
                         };
            Console.WriteLine(repository.InsertDevice(device));
        }

        [Test]
        public void DeleteDevice()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteDevice("123", 4));
        }

        [Test]
        public void UpdateDeviceTest()
        {
            var repository = new CustomerRepositoryHelper();
            var device = new ChildDevices
                         {
                             Id = 1,
                             LicenseId = "11",
                             Tbpid = "42232",
                             ChildId = 2,
                             Name = "Name",
                             Avatar = "avatar",
                             OnlineTimeFrames = "otf",
                             OnlineWebTimeFrames = "owtf"
                         };
            Console.WriteLine(repository.UpdateDevice(device));
        }

        [Test]
        public void GetAllDevices()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllDevices("1234", 30);
        }

        [Test]
        public void GetAllDevices1()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllDevices("1234");
        }

        [Test]
        public void GetDevice()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetDevice(2);
        }

        [Test]
        public void GetNumDevices()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.GetNumDevices("12334"));
        }
        #endregion

        #region Logs

        [Test]
        public void InsertLog()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.InsertLog("4321", "asd", "description"));
        }

        [Test]
        public void customerslogs_Emails()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.customerslogs_Emails("from", "to", "body", true));
        }

        [Test]
        public void InsertExceptionLog()
        {
            var repository = new CustomerRepositoryHelper();
            var exc = new SPCServices.ExceptionHandling.ExceptionObj
                      {
                          LicenseId = "aasdasd",
                          Environment = "Environment",
                          OsEdition = "OsEdition",
                          OsName = "OsName",
                          OSBits = "OsBits"
                      };
            Console.WriteLine(repository.InsertExceptionLog(exc));
        }

        [Test]
        public void InsertInstallerLogTest()
        {
            var repository = new CustomerRepositoryHelper();
            int? id = 0;
            Console.WriteLine(repository.InsertInstallerLog("result", "license", "type", "1.1.1.1","step", "message", out id));
            Console.WriteLine(id);
        }

        [Test]
        public void GetAllLogssTest()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllLogs("4321");
        }

        [Test]
        public void InsertCustomerAlert()
        {
            var repository = new CustomerRepositoryHelper();
            int? id = null;
            Console.WriteLine(repository.InsertCustomerAlert("1234", 1, "message", "severity", out id));
            Console.WriteLine(id);
        }

        [Test]
        public void GetAllCustomerAlerts()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllCustomerAlerts("1234");
        }
        #endregion

        #region Specific Timeline Methods

        [Test]
        public void GetDeviceTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.GetDevice("1234", 2));
        }

        [Test]
        public void GetAllRDCTest()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllRDC("1243", 2, DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(15));
        }

        [Test]
        public void GetAllHistory()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllHistory("12434", 2, DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(15));
        }

        [Test]
        public void GetAllBookmarks()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllBookmarks("12434", 2, DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(15));
        }

        [Test]
        public void GetAllSMSMMS()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllSMSMMS("12434", 2, DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(15));
        }

        [Test]
        public void GetAllGPSLocations()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllGPSLocations("12434", 2, DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(15));
        }

        [Test]
        public void GetAllCallHistory()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetAllCallHistory("12434", 1, DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(15));
        }
        #endregion

        #region Specific Activity Methods

        [Test]
        public void GetLastGPSLocationTest()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetLastGPSLocation("12434", null, 1);
        }

        [Test]
        public void GetTimeinWebTest()
        {
            var repository = new CustomerRepositoryHelper();
            var res = repository.GetTimeinWeb("12434", null, 1, "TODAY");
        }
        #endregion

        #region Specific Delete Methods

        [Test]
        public void DeleteCustomersAlertsTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteCustomersAlerts("1234", 1));
        }

        [Test]
        public void DeleteCustomersIPNsTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteCustomersIPNs("1234"));
        }

        [Test]
        public void DeleteAnalyticsInstallerTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteAnalyticsInstaller("license"));
        }
        [Test]
        public void DeleteAnalyticsExceptionsTest()
        {
            var repository = new CustomerRepositoryHelper();
            Console.WriteLine(repository.DeleteAnalyticsExceptions("aasdasd"));
        }

        #endregion
    }
}
