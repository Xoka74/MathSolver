using System;
using System.Collections.Generic;
using System.Linq;

namespace MathSolver
{
    public static class PostfixBuilder
    {
        private static Dictionary<string, int> _priorities = new()
        {
            { "(", -1 },
            { ")", -1 },
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "%", 2 },
            { "/", 2 },
            { "mul", 3 },
            { "max", 3 },
            { "min", 3 },
            { "left", 3 },
            { "right", 3 },
            { "sum", 3 }
        };

        private static string[] _stringFunctionOperations = new[] { "mul", "max", "min", "left", "right", "sum" };
        public static string BuildPostfixExpression(string infixExpression)
        {
            if (infixExpression == null)
                throw new FormatException();

            var tokens = infixExpression.SplitWithSeparators(_priorities.Keys.ToArray());
            foreach (var token in tokens)
            {
                //Console.WriteLine(token);
            }
            
            if (tokens.Length == 0)
                return string.Empty;

            var result = new List<string>();
            var stack = new Stack<string>();
            var isUnary = true;
            foreach (var token in tokens)
            {
                //Console.WriteLine($"token:|{token}|length: {token.Length}");

                //Console.WriteLine(token);
                
                if (IsNumber(token))
                {
                    result.Add(token);
                    isUnary = false;
                }
                else if (token == "(")
                {
                    stack.Push(token);
                    isUnary = true;
                }
                else if (token == ")")
                {
                    if (stack.Count == 0)
                        throw new FormatException();
                    while (stack.Peek() != "(")
                    {
                        result.Add(stack.Pop());
                        if (stack.Count == 0)
                            throw new FormatException();
                    }

                    if (stack.Count > 0)
                        stack.Pop();
                    isUnary = false;
                }
                else if (IsStringOperation(token))
                {
                    result.Add(token);
                }
                else // if Operation
                {
                    while (stack.Count > 0 && GetPriority(token) <= GetPriority(stack.Peek()))
                    {
                        result.Add(stack.Pop());
                    }
                    
                    if (isUnary)
                    {
                        result.Add("0");
                    }

                    stack.Push(token);
                    isUnary = false;
                }
            }

            while (stack.Count > 0)
            {
                var token = stack.Pop();
                if (token == "(")
                    throw new FormatException();
                result.Add(token);
            }

            if (result.Count == 0)
                throw new FormatException();
            return string.Join(' ', result);
        }

        private static bool IsNumber(string token) => !_priorities.Keys.Contains(token);

        private static bool IsStringOperation(string token) => _stringFunctionOperations.Contains(token);

        private static int GetPriority(string value) =>  _priorities.GetValueOrDefault(value.ToString(), 0);
    }
}