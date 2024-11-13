using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LinkedListsConsole
{
    public class MainProgram
    {
        private int count_commands;
        private Stack stack = new Stack();
        private Queue queue = new Queue();
        private CustomLinkedList<int> additionalList = new CustomLinkedList<int>();
        Stopwatch stopwatch = new Stopwatch();
        private BinaryTree<object> binaryTree;

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
            Console.WriteLine("7. Перевод из инфиксной формы в постфиксную");
            Console.WriteLine("8. Применение структур данных");
            Console.WriteLine("9. Binary Tree");


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
                case "8":
                    RunStructureExamples();
                    break;
                case "9":
                    ProcessBinaryTree();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    Run();
                    break;
            }
        }

        private void ProcessBinaryTree()
        {
            if (binaryTree == null)
            {
                Console.WriteLine("Введите корневой элемент дерева:");
                object rootData = Console.ReadLine();
                binaryTree = new BinaryTree<object>(rootData);
            }

            bool exitTreeMenu = false;
            while (!exitTreeMenu)
            {
                Console.Clear();
                Console.WriteLine("Binary Tree Operations:");
                Console.WriteLine("1. Добавить узел");
                Console.WriteLine("2. Прямой обход (Pre-order)");
                Console.WriteLine("3. Симметричный обход (In-order)");
                Console.WriteLine("4. Обратный обход (Post-order)");
                Console.WriteLine("0. Назад в главное меню");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNodeToTree();
                        break;
                    case "2":
                        Console.WriteLine("Прямой обход:");
                        BinaryTree<object>.PreOrderTraversal(binaryTree.Root);
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("Симметричный обход:");
                        BinaryTree<object>.InOrderTraversal(binaryTree.Root);
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine("Обратный обход:");
                        BinaryTree<object>.PostOrderTraversal(binaryTree.Root);
                        Console.WriteLine();
                        break;
                    case "0":
                        exitTreeMenu = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
                Run();
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
                count_commands = 0;
                foreach (var command in commands)
                {
                    count_commands++;
                    ProcessCommand(command);
                }
                stopwatch.Stop();
                Console.WriteLine("Времени затрачено: " + stopwatch.Elapsed);
                Console.WriteLine("Операций выполнено: " + count_commands);
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
                count_commands = 0;
                foreach (var command in commands)
                {
                    count_commands++;
                    ProcessCommand(command);
                }
                stopwatch.Stop();
                Console.WriteLine("Времени затрачено: " + stopwatch.Elapsed);
                Console.WriteLine("Операций выполнено: " + count_commands);
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

        private BinaryTree<object>.Node FindNode(BinaryTree<object>.Node node, object data)
        {
            if (node == null) return null;
            if (node.Data.Equals(data)) return node;
            var left = FindNode(node.Left, data);
            return left ?? FindNode(node.Right, data);
        }

        private void AddNodeToTree()
        {
            Console.WriteLine("Введите родительский узел для нового элемента:");
            object parentData = Console.ReadLine();

            var parentNode = FindNode(binaryTree.Root, parentData);
            if (parentNode == null)
            {
                Console.WriteLine("Узел не найден.");
                return;
            }

            Console.WriteLine("Введите значение нового узла:");
            object data = Console.ReadLine();
            Console.WriteLine("Добавить узел влево (L) или вправо (R)?");
            string direction = Console.ReadLine().ToUpper();

            if (direction == "L")
            {
                binaryTree.AddLeft(parentNode, data);
            }
            else if (direction == "R")
            {
                binaryTree.AddRight(parentNode, data);
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }


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
            int count = rand.Next(10000, 100000);
            string[] commands = new string[100000];
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

            string filePath = OpenFileDialog();
            string[] commands = File.ReadAllLines(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (string command in commands)
            {
                sb.Append(command);
            }

            string expression = sb.ToString();

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
            string infixExpression = Console.ReadLine();
            try
            {
                string postfixExpression = InfixToPostfixConverter.ConvertToPostfix(infixExpression);
                Console.WriteLine("Постфиксное выражение: " + postfixExpression);
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
        private void RunStructureExamples()
        {
            Console.Clear();
            Console.WriteLine("Выберите пример:");
            Console.WriteLine("1. Стек(Проверка правильности скобок в строке)");
            Console.WriteLine("2. Список(Список работников)");
            Console.WriteLine("3. Очередь(Очередь в колл-центре)");
            Console.WriteLine("4. Дерево(Дерево комментариев в блоге)");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите строку со скобками: ");
                    if (Validate(Console.ReadLine()))
                    {
                        Console.WriteLine("Ошибок нет");
                    }
                    else
                    {
                        Console.WriteLine("Ошибки есть");
                    }
                    break;
                case "2":
                    List<string> employees = new List<string>();
                    EmployeeList(employees);
                    break;
                case "3":
                    Queue callQueue = new Queue();
                    CallCenterQueue(callQueue);
                    break;
                case "4":
                    CommentTree tree = new CommentTree();
                    BlogTree(tree);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
            Run();
        }
        public bool Validate(string expression)
        {
            Stack stack = new Stack();

            foreach (char c in expression)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == '}' || c == ']')
                {
                    if (stack.Size() == 0) return false;

                    char top = (char)stack.Pop();
                    if (!IsMatchingPair(top, c)) return false;
                }
            }

            return stack.Size() == 0;
        }

        private bool IsMatchingPair(char open, char close)
        {
            return (open == '(' && close == ')') ||
                    (open == '{' && close == '}') ||
                    (open == '[' && close == ']');
        }

        private void EmployeeList(List<string> employees)
        {
            Console.Clear();
            Console.WriteLine("Выберите команду");
            Console.WriteLine("1. Добавить сотрудника в список ");
            Console.WriteLine("2. Удалить сотрудника из списка");
            Console.WriteLine("3. Вывести список сотрудников");
            Console.WriteLine("4. Выход");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddEmployee(employees);
                    EmployeeList(employees);
                    break;
                case "2":
                    RemoveEmployee(employees);
                    EmployeeList(employees);
                    break;
                case "3":
                    ShowEmployee(employees);
                    EmployeeList(employees);
                    break;
                case "4":
                    Run();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
        private List<string> AddEmployee(List<string> list)
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            list.Add(name);
            return list;
        }
        private List<string> RemoveEmployee(List<string> list)
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            if (list.Contains(name))
            {
                list.Remove(name);
            }
            else
            {
                Console.WriteLine($"Сотрудник с именем {name} не найден.");
            }
            return list;
        }
        private void ShowEmployee(List<string> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список работников пуст.");
            }
            else
            {
                Console.WriteLine("Список работников:");
                foreach (var employee in list)
                {
                    Console.WriteLine(employee);
                }
            }
        }
        private void CallCenterQueue(Queue callQueue)
        {
            Console.WriteLine("Выберите команду");
            Console.WriteLine("1. Добавить номер в очередь на звонок ");
            Console.WriteLine("2. Совершить звонок");
            Console.WriteLine("3. Вывести очередь");
            Console.WriteLine("4. Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddCall(callQueue);
                    CallCenterQueue(callQueue);
                    break;
                case "2":
                    ProcessCall(callQueue);
                    CallCenterQueue(callQueue);
                    break;
                case "3":
                    DisplayQueue(callQueue);
                    CallCenterQueue(callQueue);
                    break;
                case "4":
                    Run();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
        public Queue AddCall(Queue callQueue)
        {
            Console.WriteLine("Введите номер");
            string caller = Console.ReadLine();
            callQueue.Enqueue(caller);
            Console.WriteLine($"Звонок от {caller} добавлен в очередь.");
            return callQueue;
        }

        public Queue ProcessCall(Queue callQueue)
        {
            if (callQueue.Size() > 0)
            {
                string caller = callQueue.Dequeue().ToString();
                Console.WriteLine($"Звонок от {caller} обработан.");
            }
            else
            {
                Console.WriteLine("Очередь звонков пуста.");
            }
            return callQueue;
        }

        public void DisplayQueue(Queue callQueue)
        {
            if (callQueue.Size() == 0)
            {
                Console.WriteLine("Очередь звонков пуста.");
            }
            else
            {
                Console.WriteLine("Текущая очередь звонков:");
                Console.WriteLine(callQueue.Print());
            }
        }
        private void BlogTree(CommentTree tree)
        {
            Console.WriteLine("Выберите команду");
            Console.WriteLine("1. Добавить комментарий ");
            Console.WriteLine("2. Удалить комментарий");
            Console.WriteLine("3. Вывести дерево комментария");
            Console.WriteLine("4. Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите текст комментария:");
                    string text = Console.ReadLine();
                    Console.WriteLine("Введите текст родительского комментария (или оставьте пустым для корневого комментария):");
                    string parentText = Console.ReadLine();
                    tree.AddComment(text, string.IsNullOrWhiteSpace(parentText) ? null : parentText);
                    BlogTree(tree);
                    break;
                case "2":
                    Console.WriteLine("Введите текст комментария для удаления:");
                    string removeText = Console.ReadLine();
                    tree.RemoveComment(removeText);
                    BlogTree(tree);
                    break;
                case "3":
                    tree.DisplayComments();
                    BlogTree(tree);
                    break;
                case "4":
                    Run();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }
}