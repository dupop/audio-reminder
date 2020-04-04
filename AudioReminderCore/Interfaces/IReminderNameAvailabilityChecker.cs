using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IReminderNameAvailabilityChecker
    {
        /// <summary>
        /// Check if reminder with this name already exists
        /// </summary>
        [OperationContract]
        bool IsNameAvailable(string reminderName);
    }
}
