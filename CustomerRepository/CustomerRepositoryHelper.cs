using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using CustomerRepository.DbEntities;
using CustomerRepository.Domain;
using SPC.Library;
using SPCServices.Helpers;

namespace CustomerRepository
{
    public class CustomerRepositoryHelper
    {
        #region Customer

        /// <summary>
        /// Create New Customer
        /// </summary>
        public Boolean InsertCustomer(CustomerData customerData, out string licenseId)
        {
            var customer = new Customer
                           {
                               LicenseId = customerData.LicenseId,
                               FirstName = customerData.FirstName,
                               LastName = customerData.LastName,
                               DateOfBirth = customerData.DateOfBirth,
                               Email = AESHelper.Encrypt(customerData.Email),
                               IsEmailVerified = false,
                               Timezone = customerData.Timezone,
                               Hashpass = AESHelper.Encrypt(customerData.HashPass),
                               PaymentProfile = customerData.PaymentProfile,
                               Expires = customerData.Expires,
                               Obs = customerData.Obs,
                               Status = customerData.Status,
                               Type = customerData.Type,
                               Created = DateTime.UtcNow,
                               Updated = DateTime.UtcNow,
                               Version = "1.0"
                           };

            using (var context = new CustomerInfoRepository())
            {
                context.Customers.Add(customer);
                try
                {
                    context.SaveChanges();
                    licenseId = customerData.LicenseId;
                    return true;
                }
                catch (Exception)
                {
                    licenseId = "";
                    return false;
                }
            }
        }

        /// <summary>
        /// Create New Customer
        /// </summary>
        public Boolean InsertIPN(NameValueCollection ipn, string call)
        {
            var licenseid = "";

            try
            {
                licenseid = ipn["custom"].Split('|')[0];
            }
            catch
            { }

            var insertingIpn = new Ipn
                               {
                                   LicenseId = licenseid,
                                   Product = ipn["custom"].Split('|').Length > 2 ? ipn["custom"].Split('|')[1] : null,
                                   ReceiverEmail = ipn["receiver_email"],
                                   ReceiverId = ipn["receiver_id"],
                                   ResidenceCountry = ipn["residence_country"],
                                   TestIpn = ipn["test_ipn"],
                                   TransactionSubject = ipn["transaction_subject"],
                                   TxnId = ipn["txn_id"],
                                   TxnType = ipn["txn_type"],
                                   PayerEmail = ipn["payer_email"],
                                   PayerId = ipn["payer_id"],
                                   PayerStatus = ipn["payer_status"],
                                   FirstName = ipn["first_name"],
                                   LastName = ipn["last_name"],
                                   AddressCity = ipn["address_city"],
                                   AddressCountry = ipn["address_country"],
                                   AddressCountryCode = ipn["address_country_code"],
                                   AddressName = ipn["address_name"],
                                   AddressState = ipn["address_state"],
                                   AddressStatus = ipn["address_status"],
                                   AddressStreet = ipn["address_street"],
                                   AddressZip = ipn["address_zip"],
                                   Custom = ipn["custom"],
                                   HandlingAmount = ipn["handling_amount"],
                                   ItemName = ipn["item_name"],
                                   ItemNumber = ipn["item_number"],
                                   McCurrency = ipn["mc_currency"],
                                   McFee = ipn["mc_fee"],
                                   McGross = ipn["mc_gross"],
                                   PaymentDate = ipn["payment_date"],
                                   PaymentFee = ipn["payment_fee"],
                                   PaymentGross = ipn["payment_gross"],
                                   PaymentStatus = ipn["payment_status"],
                                   PaymentType = ipn["payment_type"],
                                   ProtectionEligibility = ipn["protection_eligibility"],
                                   Quantity = ipn["quantity"],
                                   Shipping = ipn["shipping"],
                                   Tax = ipn["tax"],
                                   Call = call,
                                   Created = DateTime.UtcNow,
                                   Updated = DateTime.UtcNow,
                                   Version = "1.0"
                               };

            using (var context = new CustomerInfoRepository())
            {
                context.Ipns.Add(insertingIpn);

                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception exception)
                {
                    SPCServices.ExceptionHandling.SPCExceptionLog.LogSPCException(exception, licenseid);
                    return false;
                }
            }
        }

        public Customer UpdateCustomer(CustomerData customerData)
        {
            using (var context = new CustomerInfoRepository())
            {
                var updating = context.Customers.FirstOrDefault(x => x.LicenseId == customerData.LicenseId);
                if (updating != null)
                {
                    if (customerData.FirstName != null) updating.FirstName = customerData.FirstName;
                    if (customerData.LastName != null) updating.LastName = customerData.LastName;
                    if (customerData.Email != null) updating.Email = AESHelper.Encrypt(customerData.Email);
                    if (customerData.Timezone != null) updating.Timezone = customerData.Timezone;
                    if (customerData.HashPass != null) updating.Hashpass = AESHelper.Encrypt(customerData.HashPass);
                    if (customerData.PaymentProfile != null) updating.PaymentProfile = customerData.PaymentProfile;
                    if (customerData.Expires != DateTime.MinValue) updating.Expires = customerData.Expires;
                    if (customerData.DateOfBirth != null) updating.DateOfBirth = customerData.DateOfBirth;
                    if (customerData.Obs != null) updating.Obs = customerData.Obs;
                    if (customerData.Status != null) updating.Status = customerData.Status;
                    if (customerData.Type != null) updating.Type = customerData.Type;

                    try
                    {
                        context.SaveChanges();
                        return updating;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Update IsEmailVerified flag to True
        /// </summary>
        public Customer ActivateEmail(CustomerData customerData)
        {
            using (var context = new CustomerInfoRepository())
            {
                var user = context.Customers.FirstOrDefault(x => x.LicenseId == customerData.LicenseId);
                if (user != null)
                {
                    user.Updated = DateTime.UtcNow;
                    user.IsEmailVerified = true;

                    try
                    {
                        context.SaveChanges();
                        return user;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Delete New Customer
        /// </summary>
        public Boolean DeleteCustomer(string licenseId)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deleting = context.Customers.FirstOrDefault(x => x.LicenseId == licenseId);
                if (deleting != null)
                {
                    try
                    {
                        context.Customers.Remove(deleting);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Customers.ToArray();
            }
        }

        /// <summary>
        /// Get Specific Customer
        /// </summary>
        public Customer GetCustomer(string licenseId)
        {
            if (licenseId == null)
                return null;

            using (var context = new CustomerInfoRepository())
            {
                var targetCustomer = context.Customers.FirstOrDefault(x => x.LicenseId == licenseId);
                if (targetCustomer != null)
                {
                    targetCustomer.Email = AESHelper.Decrypt(targetCustomer.Email);
                    targetCustomer.Hashpass = AESHelper.Decrypt(targetCustomer.Hashpass);
                    return targetCustomer;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get Specific Customer
        /// </summary>
        public Customer GetCustomerEmail(string email)
        {
            using (var context = new CustomerInfoRepository())
            {
                var targetCustomer = context.Customers.FirstOrDefault(x => x.Email == email);
                if (targetCustomer != null)
                {
                    return targetCustomer;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Child
        /// <summary>
        /// Create New Child
        /// </summary>
        public Boolean InsertChild(ChildData childData)
        {
            var child = new Child
                        {
                            LicenseId = childData.LicenseId,
                            Name = childData.Name,
                            Avatar = childData.Avatar,
                            Obs = childData.Obs,
                            Category = childData.Category,
                            Created = DateTime.UtcNow,
                            Updated = DateTime.UtcNow,
                            Version = "1.0"
                        };

            using (var context = new CustomerInfoRepository())
            {
                context.Childrens.Add(child);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Update New Child
        /// </summary>
        public object UpdateChild(ChildData childData)
        {
            using (var context = new CustomerInfoRepository())
            {
                var updating = context.Childrens.FirstOrDefault(x => x.Id == childData.Id);
                if (updating != null)
                {
                    updating.Updated = DateTime.UtcNow;
                    if (childData.Name != null) updating.Name = childData.Name;
                    if (childData.Avatar != null) updating.Avatar = childData.Avatar;
                    if (childData.Obs != null) updating.Obs = childData.Obs;
                    if (childData.Category != null) updating.Category = childData.Category;

                    try
                    {
                        context.SaveChanges();
                        return updating;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Delete New Child
        /// </summary>
        public Boolean DeleteChild(string licenseId, int? id)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deleting = context.Childrens.FirstOrDefault(x => x.LicenseId == licenseId && (id == null || x.Id == id));
                if (deleting != null)
                {
                    context.Childrens.Remove(deleting);
                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get All Childs
        /// </summary>
        public IEnumerable<Child> GetAllChilds(string licenseId)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Childrens.Where(x => x.LicenseId == licenseId).ToArray();
            }
        }

        /// <summary>
        /// Get Specific Child
        /// </summary>
        public Child GetChild(int? id)
        {
            if (id == null)
                return null;
            
            using (var context = new CustomerInfoRepository())
            {
                return context.Childrens.Where(x => x.Id == id).ToArray().FirstOrDefault();
            }
        }

        /// <summary>
        /// Get #childs by customer
        /// </summary>
        public int GetNumChilds(string licenseId)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Childrens.Count(x => x.LicenseId == licenseId);
            }
        }

        #endregion

        #region Devices
        public Boolean InsertDevice(ChildDevices childDevices)
        {
            // define defaults blocked classes
            if (childDevices.BlockedURLs == null)
                childDevices.BlockedURLs = ConfigurationManager.AppSettings["DefaultBlockedURLs"];

            var device = new Device
                         {
                             LicenseId = childDevices.LicenseId,
                             TbpId = childDevices.Tbpid,
                             Name = childDevices.Name,
                             Avatar = childDevices.Avatar,
                             OnlineTimeFrames = childDevices.OnlineTimeFrames,
                             OnlineWebTimeFrames = childDevices.OnlineWebTimeFrames,
                             BlockedUrls = childDevices.BlockedURLs,
                             Obs = "",
                             Created = DateTime.UtcNow,
                             Updated = DateTime.UtcNow,
                             Version = "1.0",
                             Type = childDevices.Type,
                             ChildDevices = new Collection<ChildDevice>
                                            {
                                                new ChildDevice
                                                {
                                                    ChildId = childDevices.ChildId
                                                }
                                            }
                         };

            using (var context = new CustomerInfoRepository())
            {
                context.Devices.Add(device);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            var repository = new CustomerRepository();
            repository.InsertBlocker(childDevices.LicenseId, childDevices.ChildId, device.Id, childDevices.BlockedURLs);
            repository.InsertOnlineTime(childDevices.LicenseId, childDevices.ChildId, device.Id, childDevices.OnlineTimeFrames, childDevices.OnlineWebTimeFrames);

            return true;
        }

        /// <summary>
        /// Update New Device
        /// </summary>
        public Device UpdateDevice(ChildDevices childDevices)
        {
            using (var context = new CustomerInfoRepository())
            {
                var updating = context.Devices.SingleOrDefault(x => x.Id == childDevices.Id);
                if (updating != null)
                {
                    updating.Updated = DateTime.UtcNow;

                    foreach (var childDevice in updating.ChildDevices.ToArray())
                    {
                        context.ChildDevices.Remove(childDevice);
                    }

                    var newChildDevice = new ChildDevice
                                 {
                                     DeviceId = updating.Id,
                                     ChildId = childDevices.ChildId
                                 };
                    context.ChildDevices.Add(newChildDevice);
                    updating.ChildDevices.Add(newChildDevice);
                    if (childDevices.Name != null) updating.Name = childDevices.Name;
                    if (childDevices.Avatar != null) updating.Avatar = childDevices.Avatar;
                    if (childDevices.OnlineTimeFrames != null) updating.OnlineTimeFrames = childDevices.OnlineTimeFrames;
                    if (childDevices.OnlineWebTimeFrames != null) updating.OnlineWebTimeFrames = childDevices.OnlineWebTimeFrames;
                    if (childDevices.BlockedURLs != null) updating.BlockedUrls = childDevices.BlockedURLs;
                    if (childDevices.Obs != null) updating.Obs = childDevices.Obs;
                    if (childDevices.Type != null) updating.Type = childDevices.Type;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                    var repository = new CustomerRepository();
                    repository.UpdateBlocker(childDevices.LicenseId, childDevices.ChildId, childDevices.Id, childDevices.BlockedURLs);
                    repository.UpdateOnlineTime(childDevices.LicenseId, childDevices.ChildId, childDevices.Id, childDevices.OnlineTimeFrames, childDevices.OnlineWebTimeFrames);
                    return updating;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Delete New Device
        /// </summary>
        public Boolean DeleteDevice(string licenseId, int? id = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deleting = context.Devices.FirstOrDefault(x => x.LicenseId == licenseId && (id == null || x.Id == id));
                if (deleting != null)
                {
                    context.Devices.Remove(deleting);
                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get All Devices
        /// </summary>
        public IEnumerable<DtoDevice> GetAllDevices(string licenseid, int minutes)
        {
            using (var context = new CustomerInfoRepository())
            {
                var devices = context.Devices.Where(x => x.LicenseId == licenseid)
                    .Select(x => new DtoDevice
                                 {
                                     LicenseId = x.LicenseId,
                                     TbpId = x.TbpId,
                                     ChildIds = x.ChildDevices.Select(c => c.ChildId),
                                     Id = x.Id,
                                     Name = x.Name,
                                     Avatar = x.Avatar,
                                     Onlinetimeframes = x.OnlineTimeFrames,
                                     Onlinewebtimeframes = x.OnlineWebTimeFrames,
                                     BlockedUrls = x.BlockedUrls,
                                     Obs = x.Obs,
                                     CreationTime = x.Created,
                                     UpdateTime = x.Updated,
                                     Version = x.Version,
                                     Type = x.Type
                                 }).ToArray();
                var dbloggerhelper = new CustomerRepository();

                foreach (var device in devices)
                {
                    if (dbloggerhelper.GetOnlineTimeByDevice(device.Id, minutes))
                        device.Online = true;
                    else
                        device.Online = false;
                }

                return devices;
            }
        }

        /// <summary>
        /// Get All Devices
        /// </summary>
        public IEnumerable<Device> GetAllDevices(string licenseid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Devices.Where(x => x.LicenseId == licenseid).ToArray();
            }
        }

        /// <summary>
        /// Get Specific Device
        /// </summary>
        public Device GetDevice(int id)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Devices.Where(x => x.Id == id).ToArray().FirstOrDefault();
            }
        }

        /// <summary>
        /// Get #devices by customer
        /// </summary>
        public int GetNumDevices(string licenseid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Devices.Count(x => x.LicenseId == licenseid);
            }
        }

        #endregion

        #region Logs
        /// <summary>
        /// Create New Logs
        /// </summary>
        public Boolean InsertLog(string licenseId, string actionId, string description)
        {
            var log = new Log
                      {
                          LicenseId = licenseId,
                          Action = actionId,
                          Descrtiption = description,
                          Created = DateTime.UtcNow,
                          Updated = DateTime.UtcNow,
                          Version = "1.0"
                      };

            using (var context = new CustomerInfoRepository())
            {
                context.Logs.Add(log);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Create New Logs
        /// </summary>
        public Boolean customerslogs_Emails(string from, string to, string body, Boolean result)
        {
            var customerLogsEmail = new CustomerLogsEmails
                                    {
                                        From = from,
                                        To = to,
                                        Body = body,
                                        Result = result,
                                        Created = DateTime.UtcNow,
                                        Updated = DateTime.UtcNow,
                                        Version = "1.0"
                                    };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerLogsEmails.Add(customerLogsEmail);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Create New Logs
        /// </summary>
        public Boolean InsertExceptionLog(SPCServices.ExceptionHandling.ExceptionObj onewEx)
        {
            var exceptionLog = new ExceptionLog
                               {    
                                   LicenseId = onewEx.LicenseId,
                                   Environment = onewEx.Environment,
                                   Type = onewEx.Type,
                                   RemoteIp = onewEx.remoteIP,
                                   Exception = onewEx.Exception,
                                   OsName = onewEx.OsName,
                                   OsEdition = onewEx.OsEdition,
                                   Sp = onewEx.SP,
                                   Processor = onewEx.Processor,
                                   Osbits = onewEx.OSBits,
                                   SpcAppVersion = onewEx.SPCAppVersion,
                                   Browser = onewEx.Browser,
                                   BrowserVersion = onewEx.BrowserVersion,
                                   Created = DateTime.UtcNow,
                                   Updated = DateTime.UtcNow,
                                   Version = "1.0"
                               };

            using (var context = new CustomerInfoRepository())
            {
                context.ExceptionLogs.Add(exceptionLog);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Create New Logs
        /// </summary>
        public Boolean InsertInstallerLog(string result, string license, string type, string remoteIp, string step, string message, out int? objId)
        {
            objId = null;
            var installerLog = new InstallerLog
                               {
                                   Result = result,
                                   LicenseId = license,
                                   Type = type,
                                   RemoteIp = remoteIp,
                                   Step = step,
                                   Message = message,
                                   Created = DateTime.UtcNow,
                                   Updated = DateTime.UtcNow,
                                   Version = "1.0"
                               };

            using (var context = new CustomerInfoRepository())
            {
                context.InstallerLogs.Add(installerLog);
                try
                {
                    context.SaveChanges();
                    objId = installerLog.Id;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get All Logss
        /// </summary>
        public IEnumerable<Log> GetAllLogs(string licenseid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.Logs.Where(x => x.LicenseId == licenseid).ToArray();
            }
        }

        /// <summary>
        /// Insert Customer Alerts Logs
        /// </summary>
        public Boolean InsertCustomerAlert(string licenseId, int deviceId, string msg, string severity, out int? objId)
        {
            objId = null;
            var customalerts = new CustomerAlert
                               {
                                   LicenseId = licenseId,
                                   DeviceId = deviceId,
                                   Obs = "",
                                   Msg = msg,
                                   Severity = severity,
                                   Created = DateTime.UtcNow,
                                   Updated = DateTime.UtcNow,
                                   Version = "1.0"
                               };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerAlerts.Add(customalerts);
                try
                {
                    context.SaveChanges();
                    objId = customalerts.Id;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get All Customer Alerts Logss
        /// </summary>
        public IEnumerable<CustomerAlert> GetAllCustomerAlerts(string licenseid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerAlerts.Where(x => x.LicenseId == licenseid).ToArray();
            }
        }

        #endregion

        #region Specific Timeline Methods

        /// <summary>
        /// Validate Specific Device
        /// </summary>
        public Boolean GetDevice(string licenseId, int? id)
        {
            if (licenseId == null || id == null)
                return false;

            using (var context = new CustomerInfoRepository())
            {
                var devices = context.Devices.Count(x => x.LicenseId == licenseId && x.Id == id);
                return devices == 1;
            }
        }

        /// <summary>
        /// Get All RDC for the last x minutes
        /// </summary>
        public IEnumerable<DomainEntity> GetAllRDC(string licenseId, int deviceId, DateTime startTime, DateTime endTime)
        {
            //todo childs
            using (var context = new CustomerInfoRepository())
            {
                return context.RemoteViews.Where(x => x.LicenseId == licenseId 
                    && x.DeviceId == deviceId
                    && x.Created >= startTime
                    && x.Created < endTime)
                    .Select(x => new DomainEntity
                                 {
                                     Type = "RemoteView",
                                     DateTime = x.Created,
                                     Image = x.SnapshotLocaltion,
                                     DeviceId = x.DeviceId,
                                     DeviceName = x.Device.Name,
                                     DeviceType = x.Device.Type,
                                     //ChildName = x.
                                     PageClassification = ""
                                 })
                    .ToArray();
            }
        }

        /// <summary>
        /// Get All Browse History for the last x minutes
        /// </summary>
        public IEnumerable<DomainEntity> GetAllHistory(string licenseid, int deviceid, DateTime startTime, DateTime endTime)
        {
            using (var context = new CustomerInfoRepository())
            {
                var pageClassification = new PageClassification();
                return context.CustomerHistories
                    .Where(x => x.LicenseId == licenseid
                                    && x.DeviceId == deviceid
                                    && x.Created >= startTime
                                    && x.Created < endTime)
                    .AsEnumerable()
                    .Select(x => new DomainEntity
                                 {
                                     Type = "BrowserHistory",
                                     DateTime = x.Created,
                                     Text = x.History,
                                     Image = "",
                                     DeviceId = x.DeviceId,
                                     DeviceName = x.Device.Name,
                                     DeviceType = x.Device.Type,
                                     //ChildName = x.
                                     PageClassification = pageClassification.GetClassification(x.History)
                                 })
                    .ToArray();
            }
        }

        /// <summary>
        /// Get All Bookmarks for the last x minutes
        /// </summary>
        public IEnumerable<DomainEntity> GetAllBookmarks(string licenseid, int deviceid, DateTime startTime, DateTime endTime)
        {
            using (var context = new CustomerInfoRepository())
            {
                var pageClassification = new PageClassification();
                return context.CustomerBookMarks
                    .Where(x => x.LicenseId == licenseid
                                && x.DeviceId == deviceid
                                && x.Created >= startTime
                                && x.Created < endTime)
                    .AsEnumerable()
                    .Select(x => new DomainEntity
                                 {
                                     Type = "BrowserBookmark",
                                     DateTime = x.Created,
                                     Text = x.BookMarks,
                                     Image = "",
                                     DeviceId = x.DeviceId,
                                     DeviceName = x.Device.Name,
                                     DeviceType = x.Device.Type,
                                     //ChildName = x.
                                     PageClassification = pageClassification.GetClassification(x.BookMarks)
                                 })
                    .ToArray();
            }
        }

        /// <summary>
        /// Get All SMSMMS for the last x minutes
        /// </summary>
        public IEnumerable<DomainEntity> GetAllSMSMMS(string licenseid, int deviceid, DateTime startTime, DateTime endTime)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerSmsmmss
                    .Where(x => x.LicenseId == licenseid
                                && x.DeviceId == deviceid
                                && x.Created >= startTime
                                && x.Created < endTime)
                    .AsEnumerable()
                    .Select(x => new DomainEntity
                                 {
                                     Type = "SMSMMS",
                                     DateTime = x.Created,
                                     Text = AESHelper.Decrypt(x.Smsmms),
                                     Image = "",
                                     DeviceId = x.DeviceId,
                                     DeviceName = x.Device.Name,
                                     DeviceType = x.Device.Type,
                                     //ChildName = x.
                                     PageClassification = ""
                                 })
                    .ToArray();
            }
        }

        /// <summary>
        /// Get All GPS for the last x minutes
        /// </summary>
        public IEnumerable<DomainEntity> GetAllGPSLocations(string licenseid, int deviceid, DateTime startTime, DateTime endTime)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerGpsLocations
                    .Where(x => x.LicenseId == licenseid
                                && x.DeviceId == deviceid
                                && x.Created >= startTime
                                && x.Created < endTime)
                    .Select(x => new DomainEntity
                                 {
                                     Type = "GPS",
                                     DateTime = x.Created,
                                     Text = x.Location,
                                     Image = "",
                                     DeviceId = x.DeviceId,
                                     DeviceName = x.Device.Name,
                                     DeviceType = x.Device.Type,
                                     //ChildName = x.
                                     PageClassification = ""
                                 })
                    .ToArray();
            }
        }

        /// <summary>
        /// Get All Calls for the last x minutes
        /// </summary>
        public IEnumerable<DomainEntity> GetAllCallHistory(string licenseid, int deviceid, DateTime startTime, DateTime endTime)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerCallHistory
                    .Where(x => x.LicenseId == licenseid
                                && x.DeviceId == deviceid
                                && x.Created >= startTime
                                && x.Created < endTime)
                    .AsEnumerable()
                    .Select(x => new DomainEntity
                                 {
                                     Type = "CallHistory",
                                     DateTime = x.Created,
                                     Text = AESHelper.Decrypt(x.Number),
                                     Image = "",
                                     DeviceId = x.DeviceId,
                                     DeviceName = x.Device.Name,
                                     DeviceType = x.Device.Type,
                                     //ChildName = x.
                                     PageClassification = ""
                                 })
                    .ToArray();
            }
        }

        #endregion

        #region Specific Activity Methods

        /// <summary>
        /// Get Last GPS position
        /// </summary>
        public string GetLastGPSLocation(string licenseid, string childid, int deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                var location = context.CustomerGpsLocations
                    .Where(x => x.LicenseId == licenseid && x.DeviceId == deviceid)
                    .OrderBy(x => x.Created)
                    .AsEnumerable()
                    .LastOrDefault();

                return location == null ? "No GPS location available" : location.Location;
            }
        }

        /// <summary>
        /// Get time spent using the web by period
        /// </summary>
        public TimeInWebStatictics GetTimeinWeb(string licenseid, string childid, int deviceid, string period)
        {
            var doc = new TimeInWebStatictics();
            TimeSpan totaltime = new TimeSpan();
            
            switch (period)
            {
                case "TODAY":
                    totaltime = CalcTime(licenseid, childid, deviceid, DateTime.UtcNow.Date);
                    doc.TimeWebUsedToday = totaltime.ToString();
                    break;

                case "WEEK":
                    var dayweek = DateTime.UtcNow.DayOfWeek;
                    int dayNumber = (int)dayweek;
                    for (int i = 0; i < dayNumber; i++)
                    {
                        totaltime += CalcTime(licenseid, childid, deviceid, DateTime.UtcNow.Date.AddDays(-i));
                    }
                    doc.TimeWebUsedThisWeek = totaltime.ToString();
                    break;
                case "MONTH":
                    var day = DateTime.UtcNow.Day;
                    for (int i = 0; i < day; i++)
                    {
                        totaltime += CalcTime(licenseid, childid, deviceid, DateTime.UtcNow.Date.AddDays(-i));
                    }
                    doc.TimeWebUsedThisMonth = totaltime.ToString();
                    break;
            }
            return doc;
        }

        /// <summary>
        /// Calculate time foa a day
        /// </summary>
        public TimeSpan CalcTime(string licenseid, string childid, int deviceid, DateTime day)
        {
            var starttime = new TimeSpan();
            var endtime = new TimeSpan();

            using (var context = new CustomerInfoRepository())
            {
                var histories = context.CustomerHistories
                    .Where(x => x.DeviceId == deviceid && x.LicenseId == licenseid)
                    .OrderBy(x => x.Created)
                    .ToArray();

                foreach (var customerHistory in histories)
                {
                    if (customerHistory.Created.Date == day)
                    {
                        starttime = customerHistory.Created.TimeOfDay;
                        var history = histories.Last();
                        endtime = history.Created.TimeOfDay;
                        break;
                    }
                }
            }

            var time = endtime - starttime;

            return time;
        }


        /// <summary>
        /// Get time spent using the pc by period  TODO
        /// </summary>
        public object GetTimeatPC(string licenseid, string childid, string deviceid, string period)
        {
            return new object();
        }

        #endregion

        #region Specific Delete Methods

        /// <summary>
        /// Delete All CustomersAlerts Records 
        /// </summary>
        public Boolean DeleteCustomersAlerts(string licenseId, int? alertId = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deletingItem = context.CustomerAlerts
                    .SingleOrDefault(x => x.LicenseId == licenseId && (alertId == null || x.Id == alertId));

                if (deletingItem != null)
                {
                    try
                    {
                        context.CustomerAlerts.Remove(deletingItem);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Delete All CustomersIPNs Records 
        /// </summary>
        public Boolean DeleteCustomersIPNs(string licenseId)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deletingItem = context.Ipns
                    .SingleOrDefault(x => x.LicenseId == licenseId);

                if (deletingItem != null)
                {
                    try
                    {
                        context.Ipns.Remove(deletingItem);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Delete All AnalyticsInstalle Records 
        /// </summary>
        public Boolean DeleteAnalyticsInstaller(string licenseId)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deletingItem = context.InstallerLogs
                    .SingleOrDefault(x => x.LicenseId == licenseId);

                if (deletingItem != null)
                {
                    try
                    {
                        context.InstallerLogs.Remove(deletingItem);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Delete All AnalyticsExceptions Records 
        /// </summary>
        public Boolean DeleteAnalyticsExceptions(string licenseId)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deletingItem = context.ExceptionLogs
                    .SingleOrDefault(x => x.LicenseId == licenseId);

                if (deletingItem != null)
                {
                    try
                    {
                        context.ExceptionLogs.Remove(deletingItem);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
