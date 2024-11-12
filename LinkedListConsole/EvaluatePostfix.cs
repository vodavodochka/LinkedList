using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedListsConsole
{
    public static class PostfixCalculator
    {
        public static double EvaluatePostfix(string expression)
        {
            Stack stack = new Stack();
            string[] tokens = expression.Split(' ');

            foreach (var token in tokens)
            {
                if (IsNumber(token))
                {
                    stack.Push(double.Parse(token));
                }
                else if (IsOperator(token))
                {
                    double operand2 = Convert.ToDouble(stack.Pop());
                    double operand1 = Convert.ToDouble(stack.Pop());
                    double result = PerformOperation(token, operand1, operand2);
                    stack.Push(result);
                }
                else if (IsFunction(token))
                {
                    double operand = Convert.ToDouble(stack.Pop());
                    double result = PerformFunction(token, operand);
                    stack.Push(result);
                }
            }

            return Convert.ToDouble(stack.Pop());
        }

        private static bool IsNumber(string token)
        {
            return double.TryParse(token, out _);
        }

        private static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }

        private static bool IsFunction(string token)
        {
            return token == "ln" || token == "cos" || token == "sin" || token == "sqrt";
        }

        private static double PerformOperation(string operatorSymbol, double operand1, double operand2)
        {
            switch (operatorSymbol)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                case "^":
                    return Math.Pow(operand1, operand2);
                default:
                    throw new InvalidOperationException("Unsupported operator: " + operatorSymbol);
            }
        }

        private static double PerformFunction(string functionName, double operand)
        {
            switch (functionName)
            {
                case "ln":
                    return Math.Log(operand);
                case "cos":
                    return Math.Cos(operand);
                case "sin":
                    return Math.Sin(operand);
                case "sqrt":
                    return Math.Sqrt(operand);
                default:
                    throw new InvalidOperationException("Unsupported function: " + functionName);
            }
        }
    }
}
