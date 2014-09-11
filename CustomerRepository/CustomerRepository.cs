using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CustomerRepository.DbEntities;
using SPCServices.Helpers;

namespace CustomerRepository
{
    public class CustomerRepository
    {
        /// <summary>
        /// Create New RDC Record
        /// </summary>
        public bool InsertRemoteView(string licenseId, int childid, int deviceid, string snapshotlocation,
            byte[] imgtostore, byte[] thumbnail)
        {
            var remoteView = new Rdc
                             {
                                 LicenseId = licenseId,
                                 ChildId = childid,
                                 DeviceId = deviceid,
                                 SnapshotLocaltion = snapshotlocation,
                                 Snapshot = imgtostore,
                                 Thumbnail = thumbnail,
                                 Created = DateTime.UtcNow,
                                 Updated = DateTime.UtcNow,
                                 Version = "1.0"
                             };

            using (var context = new CustomerInfoRepository())
            {
                context.RemoteViews.Add(remoteView);

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
        /// Delete All RDC Records 
        /// </summary>
        public bool DeleteRemoteView(string licenseId, int? deviceid = null, string date = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var endDate = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1);
                var removing = context.RemoteViews.Where(x => x.LicenseId == licenseId
                                                                         && (deviceid == null || x.DeviceId == deviceid)
                                                                         && (date == null || x.Created < endDate)
                                                         );
                foreach (var rdc in removing)
                {
                    context.RemoteViews.Remove(rdc);
                }

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
        /// Get All 
        /// </summary>
        public IEnumerable<Rdc> GetAllRDC(string licenseid, IEnumerable<int> devices, DateTime startDate, int pagesize, int pageindex, out string debugdata, string timezone)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            startDate = TimeZoneInfo.ConvertTimeToUtc(startDate, timeZone).ToUniversalTime();
            var end = startDate.AddMinutes(30);//we take RDC in half of houar
            debugdata = "";

            using (var context = new CustomerInfoRepository())
            {
                var query = from a in context.RemoteViews
                        where a.LicenseId == licenseid
                        where a.Created >= startDate && a.Created < end
                        where devices.Contains(a.DeviceId)
                        orderby a.Created
                        select a;

                return query.Skip(pagesize*pageindex).Take(pagesize).ToArray();
            }
        }

        /// <summary>
        /// retuns number of images in period, for every day if mode is month, otherwise every 30 minutes
        /// </summary>
        public List<Statistics> GetRdcStatistics(string licenseid, int deviceid, DateTime startDate, DateTime endDate, string mode)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date can't be less than start date", "endDate");
            }

            var result = new List<Statistics>();
            var minutesToAdd = 30;
            if (mode == "month")
            {
                //we need data for month, count images for a day
                minutesToAdd = 60 * 24;
            }

            using (var context = new CustomerInfoRepository())
            {
                //count images in every time interval
                while (startDate <= endDate)
                {
                    var currentEndDate = startDate.AddMinutes(minutesToAdd);

                    var numberOfItems = (from a in context.RemoteViews
                        where a.LicenseId == licenseid
                        where a.DeviceId == deviceid
                        where a.Created >= startDate && a.Created < currentEndDate
                        select a).Count();

                    if (numberOfItems > 0)
                    {
                        var statistics = new Statistics
                                         {
                                             StartDate = startDate,
                                             EndDate = currentEndDate,
                                             Count = numberOfItems
                                         };
                        result.Add(statistics);
                    }

                    startDate = startDate.AddMinutes(minutesToAdd);
                }
            }

            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        public Byte[] GetRDCImage(string licenseid, string snapshotlocation)
        {
            using (var context = new CustomerInfoRepository())
            {
                var rdc = context.RemoteViews.SingleOrDefault(x => x.LicenseId == licenseid && x.SnapshotLocaltion == snapshotlocation);
                return rdc == null ? null : rdc.Snapshot;
            }
        }

        /// <summary>
        /// Create New History Record
        /// </summary>
        public bool InsertHistory(string licenseId, int childid, int deviceid, string history)
        {
            var customerHistory = new CustomerHistory
                                  {
                                      LicenseId = licenseId,
                                      ChildId = childid,
                                      DeviceId = deviceid,
                                      Obs = string.Empty,
                                      History = history,
                                      Created = DateTime.UtcNow,
                                      Updated = DateTime.UtcNow,
                                      Version = "1.0"
                                  };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerHistories.Add(customerHistory);
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
        /// Delete All History Records 
        /// </summary>
        public Boolean DeleteHistory(string licenseId, int? deviceid = null, string date = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var dateTime = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1);
                var removing = context.CustomerHistories.Where(x => x.LicenseId == licenseId
                                                                    && (deviceid == null || x.DeviceId == deviceid)
                                                                    && (date == null || x.Created < dateTime)
                    );
                foreach (var customerHistory in removing)
                {
                    context.CustomerHistories.Remove(customerHistory);
                }

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
        /// Get All 
        /// </summary>
        public IEnumerable<CustomerHistory> GetAllHistory(string licenseid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerHistories.Where(x => x.LicenseId == licenseid).ToArray();
            }
        }

        /// <summary>
        /// Get All 
        /// </summary>
        public IEnumerable<CustomerHistory> GetAllHistory(string licenseid, int deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerHistories.Where(x => x.LicenseId == licenseid && x.DeviceId == deviceid).ToArray();
            }
        }

        /// <summary>
        /// Get #BrowsingHistory by customer in the last hour
        /// </summary>
        public int GetNumBrowsingHistory(string licenseid, int minutes)
        {
            using (var context = new CustomerInfoRepository())
            {
                var date = DateTime.UtcNow.AddMinutes(-minutes);
                return context.CustomerHistories.Count(x => x.LicenseId == licenseid && x.Created >= date);
            }
        }

        /// <summary>
        /// Create New Blocker Record
        /// </summary>
        public bool InsertBookmarks(string licenseId, int childid, int deviceid, string bookmarks)
        {
            var customerBookmark = new CustomerBookmark
                                  {
                                      LicenseId = licenseId,
                                      ChildId = childid,
                                      DeviceId = deviceid,
                                      Obs = string.Empty,
                                      BookMarks = bookmarks,
                                      Created = DateTime.UtcNow,
                                      Updated = DateTime.UtcNow,
                                      Version = "1.0"
                                  };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerBookMarks.Add(customerBookmark);
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
        /// Delete All Blocker Records 
        /// </summary>
        public Boolean DeleteBookmarks(string licenseId, int? deviceid = null, string date = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var maxDate = DateTime.MinValue;
                if (date != null)
                {
                    maxDate = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1);
                }
                var removing = context.CustomerBookMarks.Where(x => x.LicenseId == licenseId
                                                              && (deviceid == null || x.DeviceId == deviceid)
                                                              && (date == null || x.Created < maxDate));
                foreach (var customerBookmark in removing)
                {
                    context.CustomerBookMarks.Remove(customerBookmark);
                }

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
        /// Get All 
        /// </summary>
        public IEnumerable<CustomerBookmark> GetAllBookmarks(string licenseid, string childid, int deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerBookMarks.Where(x => x.LicenseId == licenseid && x.DeviceId == deviceid).ToArray();
            }
        }

        /// <summary>
        /// Create New Record
        /// </summary>
        public bool InsertBlocker(string licenseId, int childid, int deviceid, string blocker)
        {
            var customerBlocker = new CustomerBlocker
                          {
                              LicenseId = licenseId,
                              ChildId = childid,
                              DeviceId = deviceid,
                              Obs = "",
                              Blocker = blocker,
                              Created = DateTime.UtcNow,
                              Updated = DateTime.UtcNow,
                              Version = "1.0"
                          };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerBlockers.Add(customerBlocker);
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
        /// Update Blocker Record 
        /// </summary>
        public CustomerBlocker UpdateBlocker(string licenseId, int childid, int deviceid, string blocker)
        {
            using (var context = new CustomerInfoRepository())
            {
                var updateing =
                    context.CustomerBlockers.FirstOrDefault(x => x.LicenseId == licenseId && x.DeviceId == deviceid);
                    
                if (updateing == null)
                    return null;

                updateing.Updated = DateTime.UtcNow;
                updateing.Blocker = blocker;

                context.SaveChanges();
                return updateing;
            }
        }

        /// <summary>
        /// Delete Blocker Records 
        /// </summary>
        public Boolean DeleteBlocker(string licenseId, int? deviceid = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var deleting = context.CustomerBlockers.Where(x => x.LicenseId == licenseId && (deviceid == null || x.DeviceId == deviceid));

                foreach (var customerBlocker in deleting)
                {
                    context.CustomerBlockers.Remove(customerBlocker);
                }
                
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
        /// Get All 
        /// </summary>
        public IEnumerable<CustomerBlocker> GetAllBlocker(string licenseId, int deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return context.CustomerBlockers.Where(x => x.LicenseId == licenseId && x.DeviceId == deviceid).ToArray();
            }
        }

        /// <summary>
        /// Create New SMSMMS Record
        /// </summary>
        public bool InsertSMSMMS(string licenseId, int childid, int deviceid, string type, string number, string msgtolog, string date)
        {
            var customerSmsmms = new CustomerSmsmms
                                 {
                                     LicenseId = licenseId,
                                     ChildId = childid,
                                     DeviceId = deviceid,
                                     Obs = "",
                                     Smsmms = AESHelper.Encrypt(msgtolog),
                                     Number = AESHelper.Encrypt(number),
                                     MsgDate = date,
                                     Type = type,         //InBound or OutBound
                                     Created = DateTime.UtcNow,
                                     Updated = DateTime.UtcNow,
                                     Version = "1.0"
                                 };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerSmsmmss.Add(customerSmsmms);
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
        /// Delete All SMSMMS Records 
        /// </summary>
        public Boolean DeleteSMSMMS(string licenseId, int? deviceid = null, string date = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var maxDate = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1);
                var removing = context.CustomerSmsmmss.Where(x => x.LicenseId == licenseId
                                                              && (deviceid == null || x.DeviceId == deviceid)
                                                              && (date == null || x.Created < maxDate));
                foreach (var customerSmsmms in removing)
                {
                    context.CustomerSmsmmss.Remove(customerSmsmms);
                }

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
        /// Get All 
        /// </summary>
        public IEnumerable<CustomerSmsmms> GetAllSMSMMS(string licenseid, string childid, int? deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                var result = context.CustomerSmsmmss.Where(x => x.LicenseId == licenseid && x.DeviceId == deviceid).ToArray();

                foreach (var customerBlocker in result)
                {
                    customerBlocker.Number = AESHelper.Decrypt(customerBlocker.Number);
                    customerBlocker.Smsmms = AESHelper.Decrypt(customerBlocker.Smsmms);
                }

                return result;
            }
        }

        /// <summary>
        /// Get #SMSMMS by customer in the last hour
        /// </summary>
        public int GetNumSMSMMS(string licenseid, int minutes)
        {
            using (var context = new CustomerInfoRepository())
            {
                var maxDate = DateTime.UtcNow.AddMinutes(-minutes);
                return context.CustomerSmsmmss.Count(x => x.LicenseId == licenseid && x.Created >= maxDate);
            }
        }

        /// <summary>
        /// Create New CallHistory Record
        /// </summary>
        public bool InsertCallHistory(string customerid, int childid, int deviceid, string number, string type, string duration, string name, string date)
        {
            var callHistory = new CallHistory
                                 {
                                     LicenseId = customerid,
                                     ChildId = childid,
                                     DeviceId = deviceid,
                                     Obs = "",
                                     Number = AESHelper.Encrypt(number),
                                     Duration = duration,
                                     Name = name,
                                     Date = date,
                                     Type = type, //InBound or OutBound
                                     Created = DateTime.UtcNow,
                                     Updated = DateTime.UtcNow,
                                     Version = "1.0"
                                 };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerCallHistory.Add(callHistory);
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
        /// Delete All CallHistory Records 
        /// </summary>
        public Boolean DeleteCallHistory(string licenseId, int? deviceid = null, string date = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var maxDate = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1);
                var removing = context.CustomerCallHistory.Where(x => x.LicenseId == licenseId
                                                              && (deviceid == null || x.DeviceId == deviceid)
                                                              && (date == null || x.Created < maxDate));
                foreach (var callHistory in removing)
                {
                    context.CustomerCallHistory.Remove(callHistory);
                }

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
        /// Get All 
        /// </summary>
        public IEnumerable<CallHistory> GetAllCallHistory(string licenseid, string childid, int? deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                var result = context.CustomerCallHistory
                    .Where(x => x.LicenseId == licenseid && x.DeviceId == deviceid).ToArray();

                foreach (var callHistory in result)
                {
                    callHistory.Number = AESHelper.Decrypt(callHistory.Number);
                }

                return result;
            }
        }

        /// <summary>
        /// Create New GPSLocation Record
        /// </summary>
        public bool InsertGPSLocation(string licenseId, int childid, int deviceid, string gpsLocation)
        {
            var location = new GpsLocation
                           {
                               LicenseId = licenseId,
                               ChildId = childid,
                               DeviceId = deviceid,
                               Obs = "",
                               Location = gpsLocation,
                               Created = DateTime.UtcNow,
                               Updated = DateTime.UtcNow,
                               Version = "1.0"
                           };

            using (var context = new CustomerInfoRepository())
            {
                context.CustomerGpsLocations.Add(location);
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
        /// Delete All GPSLocation Records 
        /// </summary>
        public Boolean DeleteGPSLocation(string licenseId, int? deviceid = null, string date = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var maxDate =  DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1);
                var removing = context.CustomerGpsLocations.Where(x => x.LicenseId == licenseId
                                                              && (deviceid == null || x.DeviceId == deviceid)
                                                              && (date == null || x.Created <  maxDate));
                foreach (var gpsLocation in removing)
                {
                    context.CustomerGpsLocations.Remove(gpsLocation);
                }

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
        /// Get All 
        /// </summary>
        public IEnumerable<GpsLocation> GetAllGPSLocation(string licenseid, string childid, int? deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                var result = context.CustomerGpsLocations
                    .Where(x => x.LicenseId == licenseid && x.DeviceId == deviceid).ToArray();

                return result;
            }
        }

        /// <summary>
        /// Get licenseid by email and pass
        /// </summary>
        public string GetLogin(string Email, string pass, out string msg)
        {
            var hashpass = AESHelper.Encrypt(pass);
            using (var context = new CustomerInfoRepository())
            {
                var targetCustomer = context.Customers
                    .SingleOrDefault(x => x.Hashpass == hashpass);

                if (targetCustomer == null)
                {
                    msg = " Could not Validate License.";
                    return "";
                }

                String email = AESHelper.Decrypt(targetCustomer.Email).ToLower();
                if (email == Email.ToLower())
                {
                    if (!targetCustomer.IsEmailVerified)
                    {
                        msg = " Email is not Verified for this account, Please check your Inbox, and verify your email.";
                        return string.Empty;
                    }
                    else if ((targetCustomer.Status != "Active") && (targetCustomer.Status != "Expired"))
                    {
                        msg = " Account is not Active. Please Contact Technical Support Team";
                        return string.Empty;
                    }
                    else
                    {
                        msg = targetCustomer.LicenseId;
                        return targetCustomer.LicenseId;
                    }
                }
            }

            msg = " Could not Validate License.";
            return string.Empty;
        }

        /// <summary>
        /// Get password by email
        /// </summary>
        public string GetPass(string email, out string licenseid)
        {
            using (var context = new CustomerInfoRepository())
            {
                var targetCustomer = context.Customers
                    .SingleOrDefault(x => x.Email == email);
                
                if (targetCustomer != null)
                {
                    licenseid = targetCustomer.LicenseId;
                    return targetCustomer.Hashpass;
                }
            }

            licenseid = "";
            return string.Empty;
        }

        /// <summary>
        /// Create New OnlineTime Record
        /// </summary>
        public bool InsertOnlineTime(string licenseid, int childid, int deviceid, string onlineTime, string onlinewebTime)
        {
            var entity = new OnlineTime
                             {
                                 LicenseId = licenseid,
                                 ChildId = childid,
                                 DeviceId = deviceid,
                                 Obs = "",
                                 Time = onlineTime,
                                 OnlineWebTime = onlinewebTime,
                                 Created = DateTime.UtcNow,
                                 Updated = DateTime.UtcNow,
                                 Version = "1.0"
                             };

            using (var context = new CustomerInfoRepository())
            {
                context.OnlineTimes.Add(entity);
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
        /// Update OnlineTime Record 
        /// </summary>
        public OnlineTime UpdateOnlineTime(string licenseid, int childid, int deviceid, string onlineTime, string onlinewebTime)
        {
            using (var context = new CustomerInfoRepository())
            {
                var updating = context.OnlineTimes
                    .SingleOrDefault(x => x.LicenseId == licenseid && x.DeviceId == deviceid);
                
                if (updating != null)
                {
                    updating.Updated = DateTime.UtcNow;
                    updating.Time = onlineTime ?? "";
                    updating.OnlineWebTime = onlinewebTime ?? "";

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
        /// Delete OnlineTime Records 
        /// </summary>
        public Boolean DeleteOnlineTime(string licenseId, int? deviceid = null)
        {
            using (var context = new CustomerInfoRepository())
            {
                var removing = context.OnlineTimes
                    .Where(x => x.LicenseId == licenseId && (deviceid == null || x.DeviceId == deviceid)).ToArray();

                foreach (var onlineTime in removing)
                {
                    context.OnlineTimes.Remove(onlineTime);
                }

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
        /// Get logging by licenseid
        /// </summary>
        public bool GetLoginAnyDevice(string licenseid, int minutes)
        {
            using (var context = new CustomerInfoRepository())
            {
                var startDate = DateTime.UtcNow.AddMinutes(-minutes);
                return
                    context.RemoteViews.Count(
                        x => x.LicenseId == licenseid && x.Created >= startDate) > 0;
            }
        }

        /// <summary>
        /// Get logging in last 5 min by deviceid
        /// </summary>
        public Boolean GetOnlineTimeByDevice(int deviceid, int minutes)
        {
            using (var context = new CustomerInfoRepository())
            {
                var startDate = DateTime.UtcNow.AddMinutes(-minutes);
                return
                    context.RemoteViews.Count(
                        x => x.DeviceId == deviceid && x.Created >= startDate) > 0;
            }
        }

        /// <summary>
        /// Get All 
        /// </summary>
        public IEnumerable<OnlineTime> GetOnlineTime(string licenseid, int deviceid)
        {
            using (var context = new CustomerInfoRepository())
            {
                return
                    context.OnlineTimes.Where(
                        x => x.LicenseId == licenseid && x.DeviceId == deviceid).ToArray();
            }
        }

        /// <summary>
        /// Get #Alerts by customer in the last hour
        /// </summary>
        public int GetNumAlerts(string licenseid, int minutes)
        {
            return 0;
        }

        /// <summary>
        /// Get Percent usage by customer in the last hour
        /// </summary>
        public int GetPercentUsage(string licenseid, int minutes)
        {
            return 0;
        }
    }
}
