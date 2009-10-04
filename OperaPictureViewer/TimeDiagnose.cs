using System;
using System.Collections.Generic;
using System.Text;

namespace OperaPictureViewer
{
    class TimeDiagnose
    {
        DateTime startTime;
        Boolean bStarted = false;

        /// <summary>
        /// Start time measurement
        /// </summary>
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

        /// <summary>
        /// Stop after multiple repetitions
        /// </summary>
        /// <param name="repetitions"></param>
        /// <returns></returns>
        public double Stop(int repetitions)
        {
            return Stop() / repetitions;
        }


        /// <summary>
        /// This is method for issue 21
        /// </summary>
        /// <returns></returns>
        private int Issue21()
        {


        }
    }
}
