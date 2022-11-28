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
            var functionElementsStack = new List<BigIntComplex>();
            for (int i = 0; i < elements.Length; i++)
            {
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"element: {elements[i]}");
                Console.Write("element stack: ");
                elementStack.PrintList();
                Console.Write("operation stack: ");
                operationStack.PrintList();
                Console.Write("function stack: ");
                functionElementsStack.PrintList();
                //Console.WriteLine($"i: {i}");
                //Console.WriteLine($"depthCount:{operationStack.Count}");
                if (IsOperation(elements[i]))
                {
                    if (functionElementsStack.Count == operationStack.Count && functionElementsStack.Count != 0 && operationStack.Count != 0)
                    {
                        functionElementsStack.Add(BigIntComplex.Parse(elements[i]));
                    }
                    else if (functionElementsStack.Count == 2 && operationStack.Count == 1)
                    {
                        while (operationStack.Count > 0)
                        {
                            functionElementsStack[^2] = PerformOperation(functionElementsStack[^2], functionElementsStack[^1], operationStack[^1]);
                            functionElementsStack.RemoveAt(functionElementsStack.Count - 1);
                            operationStack.RemoveAt(operationStack.Count - 1);
                        }
                        
                        elementStack.Add(functionElementsStack[0]);
                        functionElementsStack.RemoveAt(0);
                    }

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
                        operationStack.Add(elements[i]);
                    else if (IsFunction(elements[i + 2]))
                    {
                        operationStack.Add(elements[i]);
                        functionElementsStack.Add(BigIntComplex.Parse(elements[i + 1]));
                        i = i + 1;
                    }
                    else
                    {
                        var element1 = BigIntComplex.Parse(elements[i + 1]);
                        var element2 = BigIntComplex.Parse(elements[i + 2]);
                        var result = PerformOperation(element1, element2, elements[i]);

                        if (operationStack.Count > 0)
                        {
                            functionElementsStack.Add(result);   
                        }
                        else
                        {
                            elementStack.Add(result);
                        }
                        i = i + 2;
                    }
                }
                else
                {
                    if (functionElementsStack.Count == operationStack.Count && functionElementsStack.Count != 0 && operationStack.Count != 0)
                    {
                        functionElementsStack.Add(BigIntComplex.Parse(elements[i]));
                    }
                    else if (functionElementsStack.Count == 2 && operationStack.Count == 1)
                    {
                        while (operationStack.Count > 0)
                        {
                            functionElementsStack[^2] = PerformOperation(functionElementsStack[^2], functionElementsStack[^1], operationStack[^1]);
                            functionElementsStack.RemoveAt(functionElementsStack.Count - 1);
                            operationStack.RemoveAt(operationStack.Count - 1);
                        }
                        
                        elementStack.Add(functionElementsStack[0]);
                        functionElementsStack.RemoveAt(0);
                        elementStack.Add(BigIntComplex.Parse(elements[i]));
                    }
                    else
                        elementStack.Add(BigIntComplex.Parse(elements[i]));
                }

                // Console.WriteLine("-----------------------");
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