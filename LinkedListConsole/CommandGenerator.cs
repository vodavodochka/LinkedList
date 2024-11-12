using System;
using System.Collections.Generic;
using System.IO;

namespace LinkedListsConsole
{
    public class CommandGenerator
    {
        private static Random random = new Random();

        // Генерация команд для стека
        public static void GenerateStackCommands(string filePath, int numberOfCommands)
        {
            List<string> commands = new List<string>();
            for (int i = 0; i < numberOfCommands; i++)
            {
                string command = GenerateRandomStackCommand();
                commands.Add(command);
            }
            File.WriteAllLines(filePath, commands);
            Console.WriteLine($"Генерация команд для стека завершена. Команды записаны в {filePath}");
        }

        // Генерация команд для очереди
        public static void GenerateQueueCommands(string filePath, int numberOfCommands)
        {
            List<string> commands = new List<string>();
            for (int i = 0; i < numberOfCommands; i++)
            {
                string command = GenerateRandomQueueCommand();
                commands.Add(command);
            }
            File.WriteAllLines(filePath, commands);
            Console.WriteLine($"Генерация команд для очереди завершена. Команды записаны в {filePath}");
        }

        // Генерация случайной команды для стека
        private static string GenerateRandomStackCommand()
        {
            int commandType = random.Next(0, 4); // Генерируем случайное число от 0 до 3
            switch (commandType)
            {
                case 0:
                    return $"Push {random.Next(1, 100)}";  // Push с случайным числом
                case 1:
                    return "Pop";  // Pop
                case 2:
                    return "Top";  // Top
                case 3:
                    return "Print";  // Print
                default:
                    return "Push 1";  // Default команда
            }
        }

        // Генерация случайной команды для очереди
        private static string GenerateRandomQueueCommand()
        {
            int commandType = random.Next(0, 5); // Генерируем случайное число от 0 до 4
            switch (commandType)
            {
                case 0:
                    return $"Enqueue {random.Next(1, 100)}"; // Enqueue с случайным числом
                case 1:
                    return "Dequeue"; // Dequeue
                case 2:
                    return "Peek"; // Peek
                case 3:
                    return "IsEmpty"; // IsEmpty
                case 4:
                    return "Print"; // Print
                default:
                    return "Enqueue 1"; // Default команда
            }
        }

        // Генерация смешанных команд для стека и очереди
        public static void GenerateMixedCommands(string filePath, int numberOfCommands, bool isStack)
        {
            List<string> commands = new List<string>();
            for (int i = 0; i < numberOfCommands; i++)
            {
                string command;
                if (isStack)
                {
                    command = GenerateRandomStackCommand();
                }
                else
                {
                    command = GenerateRandomQueueCommand();
                }
                commands.Add(command);
            }
            File.WriteAllLines(filePath, commands);
            Console.WriteLine($"Генерация смешанных команд завершена. Команды записаны в {filePath}");
        }
    }
}
