using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.Model
{
    //TODO rename to ServiceSettingsEntity
    public class ServiceSettingsDto
    {
        public bool AutoStartService { get; set; }
        public bool ServiceEnabled { get; set; }
        public bool BeeperEnabled { get; set; }
        public int BeeperIntervalMinutes { get; set; }
        public bool SnoozeEnabled { get; set; }
        public int SnoozeIntervalMinutes { get; set; }
    }
}
