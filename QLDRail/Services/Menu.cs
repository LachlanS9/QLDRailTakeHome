using System;
using System.IO;
using QLDRail.Services;

namespace QLDRail.TestData
{
    public static class Menu
    {
        public static void DisplayMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Queensland Rail - Train Stop Information");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Run Predefined Test Data");
                Console.WriteLine("2. Uploaded Custom Data");
                Console.WriteLine("3. Exit");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        RunPredefinedTestData();
                        break;
                    case "2":
                        RunCustomDataUpload();
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Exiting application...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        private static void RunPredefinedTestData()
        {
            Console.Clear();
            Console.WriteLine("Select the predefined test data to run:");
            Console.WriteLine("1. TestData1");
            Console.WriteLine("2. TestData2");
            Console.WriteLine("3. TestData3");
            Console.WriteLine("4. TestData4");
            Console.WriteLine("5. TestData5");
            Console.WriteLine("6. TestData6");
            Console.WriteLine("7. TestData7");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    RunTestData(TestData.TestData1);
                    break;
                case "2":
                    RunTestData(TestData.TestData2);
                    break;
                case "3":
                    RunTestData(TestData.TestData3);
                    break;
                case "4":
                    RunTestData(TestData.TestData4);
                    break;
                case "5":
                    RunTestData(TestData.TestData5);
                    break;
                case "6":
                    RunTestData(TestData.TestData6);
                    break;
                case "7":
                    RunTestData(TestData.TestData7);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Returning to the main menu.");
                    break;
            }
        }

        private static void RunTestData(Func<string> testDataMethod)
        {
            Console.Clear();
            string tempFilePath = testDataMethod.Invoke();
            TrainStopServices.DisplayTrainStops(tempFilePath);

            Console.WriteLine("Press any key to return to Main Menu...");
            Console.ReadKey();
        }

        private static void RunCustomDataUpload()
        {
            string uploadFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Upload");
            string[] files = Directory.Exists(uploadFolderPath) ? Directory.GetFiles(uploadFolderPath, "*.txt") : new string[0];

            if (files.Length > 0)
            {
                Console.Clear();
                Console.WriteLine("Select a custom file:");

                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }

                Console.Write("\nEnter the number of the file to select: ");
                string userChoice = Console.ReadLine();

                if (int.TryParse(userChoice, out int choice) && choice >= 1 && choice <= files.Length)
                {
                    string selectedFile = files[choice - 1];
                    Console.WriteLine($"Running with file: {selectedFile}");
                    TrainStopServices.DisplayTrainStops(selectedFile);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Returning to the main menu.");
                }

                Console.WriteLine("Press any key to return to Main Menu...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nNo files found in the Upload folder. Please upload a .txt file to the QLDRail/Upload folder.");
                Console.WriteLine("Press any key to return to Main Menu...");
                Console.ReadKey();
            }
        }
    }
    }
