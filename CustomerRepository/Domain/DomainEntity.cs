using System;

namespace CustomerRepository.Domain
{
    public class DomainEntity
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string Image { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string ChildId { get; set; }
        public string ChildName { get; set; }
        public string PageClassification { get; set; }
    }
}
