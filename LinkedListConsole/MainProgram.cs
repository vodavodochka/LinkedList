using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LinkedListsConsole
{
    public class MainProgram
    {
        private Stack stack = new Stack();
        private Queue queue = new Queue();
        private CustomLinkedList<int> additionalList = new CustomLinkedList<int>();
        Stopwatch stopwatch = new Stopwatch();

        [STAThread]
        public static void Main(string[] args)
        {
            MainProgram program = new MainProgram();
            program.Run();
        }

        public void Run()
        {

            Console.WriteLine("Выберите структуру данных или калькулятор:");
            Console.WriteLine("1. Стек");
            Console.WriteLine("2. Очередь");
            Console.WriteLine("3. Пользовательский связный список");
            Console.WriteLine("4. Генерация команд для стека");
            Console.WriteLine("5. Генерация команд для очереди");
            Console.WriteLine("6. Калькулятор (Постфиксная запись)");
            Console.WriteLine("7. Перевод из постфиксной записи в инфиксную");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ProcessStack();
                    break;
                case "2":
                    ProcessQueue();
                    break;
                case "3":
                    ProcessCustomLinkedList();
                    break;
                case "4":
                    GenerateStackCommands();
                    break;
                case "5":
                    GenerateQueueCommands();
                    break;
                case "6":
                    RunPostfixCalculator();
                    break;
                case "7":
                    ConvertPostfixToInfix();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    Run();
                    break;
            }
        }

        private void ProcessStack()
        {
            Console.Clear();
            string filePath = OpenFileDialog();

            if (File.Exists(filePath))
            {
                string[] commands = File.ReadAllLines(filePath);
                stopwatch.Restart();
                foreach (var command in commands)
                {
                    ProcessCommand(command);
                }
                stopwatch.Stop();
                Console.WriteLine("Времени затрачено: " + stopwatch.Elapsed);
            }
            else
            {
                Console.WriteLine("Файл не найден. Попробуйте снова.");
            }
            Run();
        }

        private void ProcessQueue()
        {
            Console.Clear();
            string filePath = OpenFileDialog();

            if (File.Exists(filePath))
            {
                string[] commands = File.ReadAllLines(filePath);
                stopwatch.Restart();
                foreach (var command in commands)
                {
                    ProcessCommand(command);
                }
                stopwatch.Stop();
                Console.WriteLine("Времени затрачено: " + stopwatch.Elapsed);
            }
            else
            {
                Console.WriteLine("Файл не найден. Попробуйте снова.");
            }
            Run();
        }

        private void ProcessCommand(string command)
        {
            string[] parts = command.Split(',');

            int operationType = int.Parse(parts[0]);

            switch (operationType)
            {
                case 1:
                    // Push или Enqueue
                    if (parts.Length > 1)
                    {
                        string value = parts[1];
                        Console.WriteLine($"Добавлено в структуру данных: {value}");

                        if (stack != null) stack.Push(value); // Для стека
                        if (queue != null) queue.Enqueue(value); // Для очереди
                    }
                    break;

                case 2:
                    // Pop или Dequeue
                    if (stack != null)
                    {
                        Console.WriteLine($"Удалено из стека: {stack.Pop()}");
                    }
                    if (queue != null)
                    {
                        Console.WriteLine($"Удалено из очереди: {queue.Dequeue()}");
                    }
                    break;

                case 3:
                    // Top или Peek
                    if (stack != null)
                    {
                        Console.WriteLine($"Верхний элемент стека: {stack.Top()}");
                    }
                    if (queue != null)
                    {
                        Console.WriteLine($"Первый элемент очереди: {queue.Peek()}");
                    }
                    break;

                case 4:
                    // isEmpty
                    if (stack != null)
                    {
                        Console.WriteLine($"Стек пуст? {stack.IsEmpty()}");
                    }
                    if (queue != null)
                    {
                        Console.WriteLine($"Очередь пуста? {queue.IsEmpty()}");
                    }
                    break;

                case 5:
                    // Print
                    if (stack != null)
                    {
                        Console.WriteLine("Содержимое стека: " + stack.Print());
                    }
                    if (queue != null)
                    {
                        Console.WriteLine("Содержимое очереди: " + queue.Print());
                    }
                    break;

                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }

        // Очередь
        

        // Открытие диалогового окна для выбора файла
        private string OpenFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

        // Генерация команд для стека
        private void GenerateStackCommands()
        {
            Console.WriteLine("Генерация команд для стека...");
            string filePath = OpenFileDialog();
            if (string.IsNullOrEmpty(filePath)) return;

            var commands = GenerateCommands();
            File.WriteAllLines(filePath, commands);
            Console.WriteLine($"Команды записаны в файл: {filePath}");
            Run();
        }

        // Генерация команд для очереди
        private void GenerateQueueCommands()
        {
            Console.WriteLine("Генерация команд для очереди...");
            string filePath = OpenFileDialog();
            if (string.IsNullOrEmpty(filePath)) return;

            var commands = GenerateCommands();
            File.WriteAllLines(filePath, commands);
            Console.WriteLine($"Команды записаны в файл: {filePath}");
            Run();
        }

        // Генерация случайных команд
        private string[] GenerateCommands()
        {
            Random rand = new Random();
            string[] commands = new string[100000]; // Генерация 100 команд
            for (int i = 0; i < 100000; i++)
            {
                int commandType = rand.Next(1, 7); // Команды от 1 до 6
                if (commandType == 1)
                {
                    commands[i] = $"1,{rand.Next(1, 10000)}"; // Push с случайным числом
                }
                else if (commandType == 2)
                {
                    commands[i] = "2"; // Pop
                }
                else if (commandType == 3)
                {
                    commands[i] = "3"; // Top/Peek
                }
                else if (commandType == 4)
                {
                    commands[i] = "4"; // IsEmpty
                }
                else if (commandType == 5)
                {
                    commands[i] = "5"; // Print
                }
                else
                {
                    commands[i] = "6"; // Size
                }
            }
            return commands;
        }

        // Постфиксный калькулятор
        private void RunPostfixCalculator()
        {
            Console.Clear();
            Console.WriteLine("Введите выражение в постфиксной записи:");

            string expression = Console.ReadLine().Trim();

            // Проверка на пустую строку
            if (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine("Ошибка: выражение не может быть пустым.");
                return;
            }

            // Оценка выражения
            double result = EvaluatePostfix(expression);

            if (double.IsNaN(result))
            {
                Console.WriteLine("Ошибка: Некорректное выражение.");
            }
            else
            {
                Console.WriteLine("Результат: " + result);
            }
            Run();
        }

        private double EvaluatePostfix(string expression)
        {
            Stack<double> stack = new Stack<double>();
            string[] tokens = expression.Split(' ');

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (IsOperator(token))
                {
                    if (stack.Count < 2)
                    {
                        Console.WriteLine("Ошибка: Недостаточно операндов для операции.");
                        return double.NaN;
                    }

                    double b = stack.Pop();
                    double a = stack.Pop();

                    if (token == "/" && b == 0)
                    {
                        Console.WriteLine("Ошибка: Деление на ноль.");
                        return double.NaN;
                    }

                    double result = PerformBinaryOperation(a, b, token);
                    stack.Push(result);
                }
                else if (IsFunction(token))
                {
                    if (stack.Count < 1)
                    {
                        Console.WriteLine("Ошибка: Недостаточно операндов для функции.");
                        return double.NaN;
                    }

                    double a = stack.Pop();
                    double result = PerformUnaryOperation(a, token);
                    stack.Push(result);
                }
                else
                {
                    Console.WriteLine($"Ошибка: Некорректный символ '{token}' в выражении.");
                    return double.NaN;
                }
            }

            if (stack.Count != 1)
            {
                Console.WriteLine("Ошибка: Недостаточно операторов.");
                return double.NaN;
            }

            return stack.Pop();
        }

        // Проверка, является ли строка оператором
        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == ":" || token == "^";
        }

        // Проверка, является ли строка функцией
        private bool IsFunction(string token)
        {
            return token == "ln" || token == "cos" || token == "sin" || token == "sqrt";
        }

        // Выполнение бинарной операции
        private double PerformBinaryOperation(double a, double b, string operatorSymbol)
        {
            return operatorSymbol switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                ":" => a / b,
                "^" => Math.Pow(a, b),
                _ => throw new InvalidOperationException("Неподдерживаемая операция")
            };
        }

        // Выполнение унарной операции
        private double PerformUnaryOperation(double a, string function)
        {
            return function switch
            {
                "ln" => Math.Log(a),
                "cos" => Math.Cos(a),
                "sin" => Math.Sin(a),
                "sqrt" => Math.Sqrt(a),
                _ => throw new InvalidOperationException("Неподдерживаемая функция")
            };
        }


        // Перевод из постфиксной записи в инфиксную
        private void ConvertPostfixToInfix()
        {
            Console.Clear();
            Console.Write("Введите постфиксное выражение: ");
            string postfixExpression = Console.ReadLine();
            try
            {
                string infixExpression = PostfixToInfixConverter.ConvertToInfix(postfixExpression);
                Console.WriteLine("Инфиксное выражение: " + infixExpression);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка при преобразовании: " + ex.Message);
            }
            Run();
        }

        private void ProcessCustomLinkedList()
        {
            Console.WriteLine("Выберите операцию для связного списка:");
            Console.WriteLine("1. Добавить элемент в начало");
            Console.WriteLine("2. Перевернуть список");
            Console.WriteLine("3. Переместить последний элемент в начало");
            Console.WriteLine("4. Переместить первый элемент в конец");
            Console.WriteLine("5. Подсчитать количество уникальных элементов");
            Console.WriteLine("6. Удалить неуникальные элементы");
            Console.WriteLine("7. Вставить другой список после первого вхождения элемента");
            Console.WriteLine("8. Вставить элемент в отсортированный список");
            Console.WriteLine("9. Удалить все вхождения элемента");
            Console.WriteLine("10. Вставить элемент перед первым вхождением другого элемента");
            Console.WriteLine("11. Дописать другой список к текущему");
            Console.WriteLine("12. Разделить список по первому вхождению элемента");
            Console.WriteLine("13. Удвоить список");
            Console.WriteLine("14. Поменять местами два элемента");
            Console.WriteLine("0. Выход");

            string choice = Console.ReadLine();
            int value;
            switch (choice)
            {
                case "1":
                    Console.Write("Введите значение для добавления: ");
                    value = int.Parse(Console.ReadLine());
                    additionalList.AddToFront(value);
                    break;
                case "2":
                    additionalList.Reverse();
                    Console.WriteLine("Список перевернут.");
                    break;
                case "3":
                    additionalList.MoveLastToFirst();
                    Console.WriteLine("Последний элемент перемещен в начало.");
                    break;
                case "4":
                    additionalList.MoveFirstToLast();
                    Console.WriteLine("Первый элемент перемещен в конец.");
                    break;
                case "5":
                    Console.WriteLine($"Количество уникальных элементов: {additionalList.CountDistinct()}");
                    break;
                case "6":
                    additionalList.RemoveNonUnique();
                    Console.WriteLine("Неуникальные элементы удалены.");
                    break;
                case "7":
                    Console.Write("Введите значение, после которого нужно вставить другой список: ");
                    value = int.Parse(Console.ReadLine());
                    CustomLinkedList<int> newList = new CustomLinkedList<int>();
                    newList.AddToFront(1);
                    newList.AddToFront(2);
                    additionalList.InsertListAfterFirstOccurrence(newList, value);
                    break;
                case "8":
                    Console.Write("Введите значение для вставки в отсортированный список: ");
                    value = int.Parse(Console.ReadLine());
                    additionalList.InsertSorted(value);
                    break;
                case "9":
                    Console.Write("Введите значение для удаления всех вхождений: ");
                    value = int.Parse(Console.ReadLine());
                    additionalList.RemoveAll(value);
                    break;
                case "10":
                    Console.Write("Введите значение для вставки: ");
                    int newValue = int.Parse(Console.ReadLine());
                    Console.Write("Введите значение перед которым нужно вставить: ");
                    value = int.Parse(Console.ReadLine());
                    additionalList.InsertBefore(newValue, value);
                    break;
                case "11":
                    CustomLinkedList<int> listToAppend = new CustomLinkedList<int>();
                    listToAppend.AddToFront(3);
                    additionalList.AppendList(listToAppend);
                    break;
                case "12":
                    Console.Write("Введите значение для разделения списка: ");
                    value = int.Parse(Console.ReadLine());
                    additionalList.SplitByFirstOccurrence(value, out var list1, out var list2);
                    Console.WriteLine("Список 1:");
                    list1.PrintList();
                    Console.WriteLine("Список 2:");
                    list2.PrintList();
                    break;
                case "13":
                    additionalList.DoubleList();
                    Console.WriteLine("Список удвоен.");
                    break;
                case "14":
                    Console.Write("Введите первое значение для обмена: ");
                    int elem1 = int.Parse(Console.ReadLine());
                    Console.Write("Введите второе значение для обмена: ");
                    int elem2 = int.Parse(Console.ReadLine());
                    additionalList.SwapElements(elem1, elem2);
                    break;
                case "0":
                    Run();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
            additionalList.PrintList();
            Run();

        }
    }
}