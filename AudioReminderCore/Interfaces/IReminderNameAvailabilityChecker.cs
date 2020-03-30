using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.Interfaces
{
    public interface IReminderNameAvailabilityChecker
    {
        /// <summary>
        /// Check if reminder with this name already exists
        /// </summary>
        bool IsNameAvailable(string reminderName);
    }
}
