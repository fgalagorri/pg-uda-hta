﻿namespace Entities
{
    public class DeviceReference
    {
        public DeviceReference()
        {
            
        }

        public DeviceReference(int devType, string devRefId)
        {
            deviceType = devType;
            deviceReferenceId = devRefId;
        }
        public int deviceType { get; set; }
        public string deviceReferenceId { get; set; }
    }
}
