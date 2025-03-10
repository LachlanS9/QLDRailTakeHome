using System;
using System.Collections.Generic;
using System.IO;

namespace QLDRail.TestData
{
    public static class TestData
    {
        private static string CreateTempFile(List<string> fileData)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
            File.WriteAllLines(tempFilePath, fileData);
            return tempFilePath;
        }

        public static string TestData1()
        {
            List<string> data = new List<string>
            {
                "Central, True",
                "Roma St, True"
            };

            return CreateTempFile(data);
        }

        public static string TestData2()
        {
            List<string> data = new List<string>
            {
                "Central, True",
                "Roma St, True",
                "South Brisbane, True",
                "South Bank, True",
                "Park Road, True",
                "Buranda, True",
                "Coorparoo, True",
                "Norman Park, True",
                "Cannon Hill, True"
            };

            return CreateTempFile(data);
        }

        public static string TestData3()
        {
            List<string> data = new List<string>
            {
                "Central, True",
                "Roma St, False",
                "South Brisbane, True",
                "South Bank, False",
                "Park Road, False",
                "Buranda, False"
            };

            return CreateTempFile(data);
        }

        public static string TestData4()
        {
            List<string> data = new List<string>
            {
                "Central, True",
                "Roma St, False",
                "South Brisbane, False",
                "South Bank, True"
            };

            return CreateTempFile(data);
        }

        public static string TestData5()
        {
            List<string> data = new List<string>
            {
                "Central, True",
                "Roma St, False",
                "South Brisbane, False",
                "South Bank, True",
                "Park Road, False",
                "Buranda, False"
            };

            return CreateTempFile(data);
        }

        public static string TestData6()
        {
            List<string> data = new List<string>
            {
                "Central, True",
                "Roma St, False",
                "South Brisbane, False",
                "South Bank, True",
                "Park Road, False",
                "Buranda, True",
                "Coorparoo, False",
                "Norman Park, False",
                "Cannon Hill, True"
            };

            return CreateTempFile(data);
        }

        public static string TestData7()
        {
            List<string> data = new List<string>
            {
                "Central, False"
            };

            return CreateTempFile(data);
        }
    }
}
