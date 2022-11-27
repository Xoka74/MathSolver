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
            var elementsStack = new List<BigIntComplex>();
            var operationStack = new  List<string>();
            for (int i = 0; i < elements.Length; i++)
            {

                if (IsOperation(elements[i]))
                {
                    while (operationStack.Count > 0)
                    {
                        elementsStack[0] = PerformOperation(elementsStack[0], elementsStack[1], operationStack[^1]);
                        operationStack.RemoveAt(operationStack.Count - 1);
                        elementsStack.RemoveAt(1);
                    }
                    if (elementsStack.Count <= 1)
                        throw new FormatException("Not enough elements to perform an operation");
                    var firstNumber = elementsStack[^1];
                    elementsStack.RemoveAt(elementsStack.Count - 1);
                    var secondNumber = elementsStack[^1];
                    elementsStack.RemoveAt(elementsStack.Count - 1);
                    elementsStack.Add(PerformOperation(secondNumber, firstNumber, elements[i]));
                }
                else if (i + 1 == elements.Length)
                {
                    elementsStack.Add(BigIntComplex.Parse(elements[i]));
                    while (operationStack.Count > 0)
                    {
                        elementsStack[0] = PerformOperation(elementsStack[0], elementsStack[1], operationStack[^1]);
                        operationStack.RemoveAt(operationStack.Count - 1);
                        elementsStack.RemoveAt(1);
                    }
                }
                else if (IsFunction(elements[i]))
                {
                    if (elementsStack.Count > 1)
                    {
                        while (operationStack.Count > 0)
                        {
                            elementsStack[0] = PerformOperation(elementsStack[0], elementsStack[1], operationStack[^1]);
                            Console.WriteLine(elementsStack[0]);
                            operationStack.RemoveAt(operationStack.Count - 1);
                            elementsStack.RemoveAt(1);
                        }
                    }
                    operationStack.Add(elements[i]);
                }
                else
                    elementsStack.Add(BigIntComplex.Parse(elements[i]));
            }

            if (elementsStack.Count == 2)
                throw new FormatException();

            return elementsStack[0].ToString();
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