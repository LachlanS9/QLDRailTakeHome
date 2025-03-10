using QLDRail.Models;

namespace QLDRail.Services
{
    public static class TrainStopServices
    {
        private static List<TrainStop> GetTrainStopData(string fileName)
        {
            List<TrainStop> trainStops = new List<TrainStop>();

            foreach (var line in File.ReadLines(fileName))
            {
                var split = line.Split(',');
                string station = split[0].Trim();

                if (split.Length == 2 && bool.TryParse(split[1].Trim(), out bool isStopping))
                {
                    trainStops.Add(new TrainStop(station, isStopping));
                }
            }
            return trainStops;
        }

        public static void DisplayTrainStops(string fileName)
        {
            List<TrainStop> trainStopData = GetTrainStopData(fileName);

            if (trainStopData.Count < 2 || trainStopData.Count(s => s.IsStopping) < 2 || !trainStopData.First().IsStopping)
            {
                Console.WriteLine("Error: Invalid Data - please check text file and try again");
                return;
            }

            if (trainStopData.Count == 2)
            {
                Console.WriteLine("This train stops at {0} and {1} only", trainStopData[0].StationName, trainStopData[1].StationName);
                return;
            }

            if (trainStopData.All(s => s.IsStopping))
            {
                Console.WriteLine("This train stops at all stations");
                return;
            }

            int firstStoppingIndex = trainStopData.FindIndex(s => s.IsStopping);
            int lastStoppingIndex = trainStopData.FindLastIndex(s => s.IsStopping);

            List<TrainStop> stopSequence = new List<TrainStop>();

            for (int i = firstStoppingIndex + 1; i < lastStoppingIndex; i++)
            {
                stopSequence.Add(trainStopData[i]);
            }

            if (stopSequence.Count(s => !s.IsStopping) == 1)
            {
                List<TrainStop> expressStops = stopSequence.FindAll(s => !s.IsStopping);
                if (expressStops.Count == 1)
                {
                    Console.WriteLine("This train stops at all stations except {0}", expressStops[0].StationName);
                    return;
                }
            }

            if (stopSequence.All(s => !s.IsStopping))
            {
                Console.WriteLine("This train runs express from {0} to {1}", trainStopData[firstStoppingIndex].StationName, trainStopData[lastStoppingIndex].StationName);
                return;
            }

            if (stopSequence.Count(s => s.IsStopping) == 1)
            {
                List<TrainStop> expressStops = stopSequence.FindAll(s => !s.IsStopping);
                int onlyStop = stopSequence.FindIndex(s => s.IsStopping);
                if (onlyStop > firstStoppingIndex + 1 && onlyStop < lastStoppingIndex - 1 && expressStops.Count > 1)
                {
                    Console.WriteLine("This train runs express from {0} to {1}, stopping only at {2}", trainStopData[firstStoppingIndex].StationName, trainStopData[lastStoppingIndex].StationName, stopSequence[onlyStop].StationName);
                    return;
                }
            }

            if (stopSequence.Count(s => s.IsStopping) >= 2)
            {
                bool isValid = true;
                for (int i = 0; i < stopSequence.Count; i++)
                {
                    if (stopSequence[i].IsStopping)
                    {
                        bool isPrevValid = (i > 0 && !stopSequence[i - 1].IsStopping); 
                        bool isNextValid = (i < stopSequence.Count - 1 && !stopSequence[i + 1].IsStopping); 

                        if (!isPrevValid && !isNextValid)
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (!isValid)
                {
                    Console.WriteLine("Error: Invalid Data - please check text file and try again");
                    return;
                }

                List<TrainStop> expressSections = new List<TrainStop>();
                List<TrainStop> stopStations = new List<TrainStop>();

                for (int i = 0; i < stopSequence.Count; i++)
                {
                    if (stopSequence[i].IsStopping)
                    {
                        stopStations.Add(stopSequence[i]);
                    }
                    else
                    {
                        expressSections.Add(stopSequence[i]);
                    }
                }

                if (expressSections.Count > 0 && stopStations.Count > 0)
                {
                    string firstExpressSection = $"This train runs express from {trainStopData[firstStoppingIndex].StationName} to {stopStations[1].StationName}, stopping only at {stopStations[0].StationName}";
                    string secondExpressSection = $"then runs express from {stopStations[1].StationName} to {trainStopData[lastStoppingIndex].StationName}";

                    Console.WriteLine(firstExpressSection + " " + secondExpressSection);
                    return;
                }
                else
                {
                    Console.WriteLine("Error: Invalid Data - please check text file and try again");
                    return;
                }
            }

        }
    }
}
