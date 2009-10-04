using System;
using System.Collections.Generic;
using System.Text;

namespace OperaPictureViewer
{
    class TimeDiagnose
    {
        DateTime startTime;
        Boolean bStarted = false;

        public void Start()
        {
            startTime = DateTime.Now;
            bStarted = true;
        }

        /// <summary>
        /// Returns measured time in miliseconds
        /// </summary>
        /// <returns></returns>
        public double Stop()
        {
            if (!bStarted) throw new Exception("Run Start() first.");
            DateTime stopTime = DateTime.Now;
            TimeSpan duration = stopTime - startTime;
            return duration.TotalMilliseconds;
        }

        public double Stop(int repetitions)
        {
            return Stop() / repetitions;
        }

    }
}
