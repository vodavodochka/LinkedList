using System;
using System.Collections.Generic;

namespace LinkedListsConsole
{
    public static class InfixToPostfixConverter
    {
        public static string ConvertToPostfix(string infixExpression)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(infixExpression))
            {
                throw new ArgumentException("Инфиксное выражение не может быть пустым.");
            }

            // Список допустимых бинарных и унарных операторов
            HashSet<string> binaryOperators = new HashSet<string> { "+", "-", "*", "/", "^" };
            HashSet<string> unaryOperators = new HashSet<string> { "ln", "cos", "sin", "sqrt" };

            // Приоритет операторов
            Dictionary<string, int> operatorPrecedence = new Dictionary<string, int>
            {
                { "+", 1 },
                { "-", 1 },
                { "*", 2 },
                { "/", 2 },
                { "^", 3 },
                { "ln", 4 },
                { "cos", 4 },
                { "sin", 4 },
                { "sqrt", 4 }
            };

            // Стек для операторов
            Stack<string> operators = new Stack<string>();
            // Список для итогового постфиксного выражения
            List<string> output = new List<string>();

            // Разбиение выражения на части по пробелам
            string[] tokens = infixExpression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                // Если токен - это число или переменная, добавляем в выходной список
                if (IsOperand(token))
                {
                    output.Add(token);
                }
                else if (binaryOperators.Contains(token) || unaryOperators.Contains(token))
                {
                    // Если токен - оператор
                    while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token) && token != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
                else if (token == "(")
                {
                    // Открывающая скобка, добавляем в стек
                    operators.Push(token);
                }
                else if (token == ")")
                {
                    // Закрывающая скобка, выталкиваем операторы из стека до открывающей скобки
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }

                    if (operators.Count == 0 || operators.Peek() != "(")
                    {
                        throw new ArgumentException("Некорректное количество скобок.");
                    }

                    operators.Pop(); // Удаляем открытую скобку
                }
                else
                {
                    throw new ArgumentException($"Неизвестный токен: {token}");
                }
            }

            // Выталкиваем все оставшиеся операторы из стека
            while (operators.Count > 0)
            {
                string operatorInStack = operators.Pop();
                if (operatorInStack == "(")
                {
                    throw new ArgumentException("Некорректное количество скобок.");
                }
                output.Add(operatorInStack);
            }

            return string.Join(" ", output);
        }

        // Проверка, является ли токен операндом (число или переменная)
        private static bool IsOperand(string token)
        {
            return double.TryParse(token, out _) || IsVariable(token);
        }

        // Проверка, является ли токен переменной (например, x, y, z)
        private static bool IsVariable(string token)
        {
            return token.All(char.IsLetter);
        }

        // Получение приоритета оператора
        private static int Precedence(string operatorToken)
        {
            return operatorToken switch
            {
                "+" or "-" => 1,
                "*" or "/" => 2,
                "^" => 3,
                "ln" or "cos" or "sin" or "sqrt" => 4,
                _ => 0
            };
        }
    }
}
