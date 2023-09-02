using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows;
using System.Security.Principal;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Management;

namespace practice
{
    internal class Program
    {
        static void Main(string[] args)

        {
            bool isExist = true;
            string current_path = "Root";
            string[] paths = ["Root", "Home", "Documents", "Projects"];
            List<Dictionary<string, string>> memory = new List<Dictionary<string, string>>();
            DateTime dateTime = DateTime.Now;
            // Saving the current time in the memory
            Dictionary<string, string> date_time = new Dictionary<string, string>
        {
            { "name", "date" },
            { "content", dateTime.ToString() }
        };
            memory.Add(date_time);
            Console.Write("username: ");
            string user_name = Console.ReadLine();
            Dictionary<string, string> user_data = new Dictionary<string, string>
        {
            { "name", "user" },
            { "content", user_name }
        };
            
            memory.Add(user_data);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Welcome, {user_data["content"]}!");
            Console.WriteLine("--------------------------------------");
            while (isExist)
            {
                Console.Write($"Terminal::{current_path}-> ");
                string input = Console.ReadLine();
                string[] commandArgs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (input == "exit")
                {
                    isExist = false;
                }
                if (input.StartsWith("say"))
                {

                    string result = "";
                    for (int i = 0;i<commandArgs.Length;i++)
                    {
                        if (i == 0) { }
                        else if (i == commandArgs.Length)
                        {
                            result += commandArgs[i];
                        }
                        else {
                            result += commandArgs[i] + " ";
                        }
                    }
                    Console.WriteLine(result);
                }
                if (input.StartsWith("repeat"))
                {
                    string result = "";
                    

                    for (int i = 2; i < commandArgs.Length; i++)
                    {
                        if (i == 0) { }
                        else if (i == commandArgs.Length)
                        {
                            result += commandArgs[i];
                        }
                        else
                        {
                            result += commandArgs[i] + " ";
                        }
                    }
                    int repeat = int.Parse(commandArgs[1]);
                    for (int i = 0; i<repeat;i++) {
                        Console.WriteLine(result);
                    }
                }
                if (input.StartsWith("path"))
                {
                    if (commandArgs.Length == 1) 
                    {
                        Console.WriteLine("Error: Invalid path.");
                    } else
                    {
                        string result = string.Empty;
                        bool isContain = paths.Contains(commandArgs[1]);
                        if (isContain)
                        {
                            current_path = commandArgs[1];
                        }
                        else
                        {
                            Console.WriteLine("Path Not Found!");
                        }
                    }
                }
                if (input == "cpu_info")
                {
                    Console.WriteLine("-----------------------------------------");
                    RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);

                    if (processor_name != null)
                    {
                        if (processor_name.GetValue("ProcessorNameString") != null)
                        {
                            Console.WriteLine(processor_name.GetValue("ProcessorNameString"));
                        }
                    }
                    Console.WriteLine("-----------------------------------------");
                }
                if (input.StartsWith("create_path"))
                {
                    paths = paths.Append(commandArgs[1]).ToArray();
                }
                if (input.StartsWith("ls"))
                {
                    string result = "";
                    foreach (var item in paths)
                    {
                        result += item + " ";
                    }
                    Console.WriteLine(result);
                }
                if (input.StartsWith("login_time"))
                {
                    Dictionary<string, string> login_time = memory[0];
                    Console.WriteLine(login_time["content"]);
                }
                if (input.StartsWith("user_info"))
                {
                    foreach (var item in memory)
                    {
                        Dictionary<string, string> login_date = memory[0];
                        if (item["name"] == "user")
                        {
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine($"logged in at: {login_date["content"]}");
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine($"Username: {item["content"]}");
                            Console.WriteLine("--------------------------------------");
                        }
                    }
                }
            }
        }
    }
}
