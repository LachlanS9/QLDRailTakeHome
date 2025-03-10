using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDRail.Models
{
    public class TrainStop
    {
        public string StationName { get; set; }
        public bool IsStopping { get; set; }

        public TrainStop(string stationName, bool isStopping)
        {
            StationName = stationName;
            IsStopping = isStopping;
        }
    }
}
