using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioUserInterface
{
    public interface ReminderNameAvailabilityChecker
    {
        /// <summary>
        /// Check if reminder with this name already exists
        /// </summary>
        bool IsNameAvailable(string reminderName);
    }
}
