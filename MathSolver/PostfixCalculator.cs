using System;
using System.Collections.Generic;

namespace MathSolver
{
    public static class PostfixCalculator
    {
        public static string Calculate(string postfixExpression)
        {
            Console.WriteLine(postfixExpression);
            switch (postfixExpression)
            {
                case "":
                    return 0.ToString();
                case null:
                    throw new FormatException();
            }

            var elements = postfixExpression.Split(' ');
            var elementStack = new List<BigIntComplex>();
            var operationStack = new List<string>();
            for (int i = 0; i < elements.Length; i++)
            {
                //elementStack.PrintList();
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"depthCount:{operationStack.Count}");
                if (IsOperation(elements[i]))
                {
                    Console.WriteLine(elements[i]);
                    if (elementStack.Count <= 1)
                        throw new FormatException("Not enough elements to perform an operation");
                    var firstNumber = elementStack[^1];
                    elementStack.RemoveAt(elementStack.Count - 1);
                    var secondNumber = elementStack[^1];
                    elementStack.RemoveAt(elementStack.Count - 1);
                    elementStack.Add(PerformOperation(secondNumber, firstNumber, elements[i]));
                }
                else if (IsFunction(elements[i]))
                {
                    if (IsFunction(elements[i + 1]))
                    {
                        Console.WriteLine("this element and next element is also a function");
                        operationStack.Add(elements[i]);
                    }
                    else if (IsFunction(elements[i + 2]))
                    {
                        Console.WriteLine("this element and next after next element is also a function");
                        operationStack.Add(elements[i]);
                        elementStack.Add(BigIntComplex.Parse(elements[i + 1]));
                        i = i + 1;
                    }
                    else
                    {
                        Console.WriteLine($"element: {elements[i]} is function");
                        var element1 = BigIntComplex.Parse(elements[i + 1]);
                        var element2 = BigIntComplex.Parse(elements[i + 2]);
                        elementStack.Add(PerformOperation(element1, element2, elements[i]));
                        elementStack.PrintList();
                        if (operationStack.Count == 0)
                        {
                            Console.WriteLine("not a nested function");
                            i = i + 2; 
                        }
                        else
                        {
                            Console.WriteLine($"nested function, perform operation: {operationStack[^1]} on elements: {elementStack[^2]} and {elementStack[^1]}");
                            operationStack.PrintList();
                            elementStack[^2] = PerformOperation(elementStack[^2], elementStack[^1], operationStack[^1]);
                            elementStack.RemoveAt(elementStack.Count - 1);
                            operationStack.RemoveAt(operationStack.Count - 1);
                            i = i + 2;
                        }
                    }
                }
                else
                {
                    if (operationStack.Count != 0)
                    {
                        Console.WriteLine($"nested function, should calculate {operationStack[^1]} on: {elementStack[0]} and {elementStack[1]}");
                        elementStack[0] = PerformOperation(elementStack[0], elementStack[1], operationStack[^1]);
                        elementStack.RemoveAt(elementStack.Count - 1);
                        operationStack.RemoveAt(operationStack.Count - 1);
                    }

                    elementStack.Add(BigIntComplex.Parse(elements[i]));
                }
                
                Console.WriteLine("-----------------------");
            }
            
            if (elementStack.Count == 2)
                throw new FormatException();
            return elementStack[0].ToString();
        }

        private static bool IsOperation(string element) => element is "+" or "-" or "*" or "/" or "%";

        private static bool IsFunction(string element) =>
            element is "mul" or "max" or "min" or "left" or "right" or "sum";

        private static BigIntComplex PerformOperation(BigIntComplex number1, BigIntComplex number2, string operand)
            => operand switch
            {
                "+" => number1 + number2,
                "-" => number1 - number2,
                "*" => number1 * number2,
                "%" => number1 % number2,
                "/" => number1 / number2,
                "mul" => number1 * number2,
                "max" => BigIntComplex.Max(number1, number2),
                "min" => BigIntComplex.Min(number1, number2),
                "left" => number1,
                "right" => number2,
                "sum" => number1 + number2,
                _ => throw new FormatException("The given literal is not an operand")
            };
    }
}