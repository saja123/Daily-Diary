using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiaryManager
{
    public class DailyDiary
    {
        private readonly string _filePath;

        public DailyDiary(string filePath)
        {
            _filePath = filePath;
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Daily Diary Manager");
                Console.WriteLine("1. Read Diary");
                Console.WriteLine("2. Add Entry");
                Console.WriteLine("3. Delete Entry");
                Console.WriteLine("4. Count Entries");
                Console.WriteLine("5. Read Entries by Date");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ReadDiaryFile();
                        break;
                    case "2":
                        AddEntry();
                        break;
                    case "3":
                        DeleteEntry();
                        break;
                    case "4":
                        CountEntries();
                        break;
                    case "5":
                        ReadEntriesByDate();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        public void ReadDiaryFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        public void AddEntry()
        {
            Console.Write("Enter date (YYYY-MM-DD): ");
            string date = Console.ReadLine();
            Console.Write("Enter content: ");
            string content = Console.ReadLine();
            Entry entry = new Entry { Date = date, Content = content };

            try
            {
                File.AppendAllText(_filePath, entry.ToString() + Environment.NewLine);
                Console.WriteLine("Entry added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
        public void DeleteEntry()
        {
            Console.Write("Enter date (YYYY-MM-DD) to delete entries: ");
            string date = Console.ReadLine();
            try
            {
                List<string> lines = new List<string>(File.ReadAllLines(_filePath));
                bool isEntryFound = false;

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith(date))
                    {
                        isEntryFound = true;
                        lines.RemoveAt(i);
                        if (i < lines.Count) 
                        {
                            lines.RemoveAt(i);
                        }
                        break;
                    }
                }

                if (isEntryFound)
                {
                    File.WriteAllLines(_filePath, lines);
                    Console.WriteLine("Entry deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Entry not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entries: {ex.Message}");
            }
        }


        public int CountEntries()
        {
                int counter = 0;
                bool isTrue = false;
                string[] lines = File.ReadAllLines(_filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (!isTrue)
                    {
                    bool isValid = DateTime.TryParseExact(lines[i], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);
                    isTrue = isValid;
                    }

                    if (isTrue && (string.IsNullOrEmpty(lines[i]) || i == lines.Length - 1))
                    {
                        counter++;
                        isTrue = false;
                    }
                }
            Console.WriteLine("Count of line is: "  +  counter);
            return counter;            
        }

        public void ReadEntriesByDate()
        {
            Console.Write("Enter date (YYYY-MM-DD) to retrieve entries: ");
            string date = Console.ReadLine();
            try
            {
                string[] lines = File.ReadAllLines(_filePath);
                bool found = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(date))
                    {
                        Console.WriteLine(lines[i]); 
                        if (i + 1 < lines.Length)
                        {
                            Console.WriteLine(lines[i + 1]);
                        }
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No entries found for the specified date.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading entries: {ex.Message}");
            }
        }

    }
}
