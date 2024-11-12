using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListsConsole
{
    public static class PostfixToInfixConverter
    {
        public static string ConvertToInfix(string postfixExpression)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(postfixExpression))
            {
                throw new ArgumentException("Постфиксное выражение не может быть пустым.");
            }

            // Список допустимых бинарных и унарных операторов
            HashSet<string> binaryOperators = new HashSet<string> { "+", "-", "*", "/", "^" };
            HashSet<string> unaryOperators = new HashSet<string> { "ln", "cos", "sin", "sqrt" };

            // Стек для операндов
            Stack<string> stack = new Stack<string>();

            // Разбиение выражения на части по пробелам
            string[] tokens = postfixExpression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                // Если токен - это число или переменная, кладем его в стек
                if (IsOperand(token))
                {
                    stack.Push(token);
                }
                else if (binaryOperators.Contains(token))
                {
                    // Если токен - бинарный оператор, извлекаем операнды из стека
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Некорректное количество операндов для бинарного оператора.");
                    }

                    string operand2 = stack.Pop();
                    string operand1 = stack.Pop();

                    // Формируем инфиксное выражение для бинарного оператора
                    string infix = $"({operand1} {token} {operand2})";

                    // Ставим обратно в стек
                    stack.Push(infix);
                }
                else if (unaryOperators.Contains(token))
                {
                    // Если токен - унарный оператор, извлекаем один операнд из стека
                    if (stack.Count < 1)
                    {
                        throw new ArgumentException("Некорректное количество операндов для унарного оператора.");
                    }

                    string operand = stack.Pop();

                    // Формируем инфиксное выражение для унарного оператора
                    string infix = $"{token}({operand})";

                    // Ставим обратно в стек
                    stack.Push(infix);
                }
                else
                {
                    throw new ArgumentException($"Неизвестный токен: {token}");
                }
            }

            // В стеке должен остаться единственный элемент — это инфиксное выражение
            if (stack.Count != 1)
            {
                throw new ArgumentException("Некорректное постфиксное выражение.");
            }

            return stack.Pop();
        }

        // Проверка, является ли токен операндом (число или переменная)
        private static bool IsOperand(string token)
        {
            return double.TryParse(token, out _);
        }
    }
}
