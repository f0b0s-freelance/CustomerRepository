using System.Data.Entity;
using CustomerRepository.DbEntities;

namespace CustomerRepository
{
    class CustomerInfoRepository : DbContext
    {
        public DbSet<Rdc> RemoteViews { get; set; }
        public DbSet<CustomerHistory> CustomerHistories { get; set; }
        public DbSet<CustomerBookmark> CustomerBookMarks { get; set; }
        public DbSet<CustomerBlocker> CustomerBlockers { get; set; }
        public DbSet<CustomerSmsmms> CustomerSmsmmss { get; set; }
        public DbSet<CallHistory> CustomerCallHistory { get; set; }
        public DbSet<GpsLocation> CustomerGpsLocations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Child> Childrens { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CustomerLogsEmails> CustomerLogsEmails { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<InstallerLog> InstallerLogs { get; set; }
        public DbSet<CustomerAlert> CustomerAlerts { get; set; }
        public DbSet<OnlineTime> OnlineTimes { get; set; }
        public DbSet<Ipn> Ipns { get; set; }
        public DbSet<ChildDevice> ChildDevices { get; set; }
    }
}
