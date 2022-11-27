using System;
using System.Numerics;
using Microsoft.Scripting.Utils;

namespace MathSolver
{
    public readonly struct BigIntComplex
    {
        public BigInteger Real { get; }
        public BigInteger Imaginary { get; }

        public BigIntComplex(BigInteger real, BigInteger imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static BigIntComplex operator +(BigIntComplex a, BigIntComplex b)
            => new(real: a.Real + b.Real,
                imaginary: a.Imaginary + b.Imaginary);

        public static BigIntComplex operator -(BigIntComplex a, BigIntComplex b)
            => new(real: a.Real - b.Real,
                imaginary: a.Imaginary - b.Imaginary);

        public static BigIntComplex operator *(BigIntComplex a, BigIntComplex b)
            => new(real: a.Real * b.Real - a.Imaginary * b.Imaginary,
                imaginary: a.Real * b.Imaginary + b.Real * a.Imaginary);

        public static BigIntComplex operator %(BigIntComplex a, BigIntComplex b)
        {
            if (a.Imaginary == 0 && b.Imaginary == 0)
            {
                return new BigIntComplex(real: a.Real % b.Real,
                    imaginary: 0);
            }

            throw new ArithmeticException("LongComplex can't be divided");
        }
        public static BigIntComplex operator /(BigIntComplex a, BigIntComplex b)
        {
            if (a.Imaginary == 0 && b.Imaginary == 0)
            {
                return new BigIntComplex(real: a.Real / b.Real,
                    imaginary: 0);
            }

            throw new ArithmeticException("LongComplex can't be divided");
        }
        public static BigIntComplex Max(BigIntComplex a, BigIntComplex b)
        {
            if (a.Imaginary == 0 && b.Imaginary == 0)
                if (a.Real > b.Real)
                    return a;
                return b;
            
            throw new ArithmeticException("LongComplex can't be compared!");
        }
        public static BigIntComplex Min(BigIntComplex a, BigIntComplex b)
        {
            if (a.Imaginary == 0 && b.Imaginary == 0)
                if (a.Real < b.Real)
                    return a;
            return b;
            
            throw new ArithmeticException("LongComplex can't be compared!");
        }
        public override string ToString()
        {
            if (Imaginary == 0)
                return $"{Real}";

            var imaginaryAbs = Imaginary.Abs();
            var imaginaryString = imaginaryAbs == 1 ? "i" : $"{imaginaryAbs}i";

            if (Real == 0)
                return Imaginary >= 0 ? imaginaryString : $"-{imaginaryString}";

            return Imaginary >= 0 ? $"{Real}+{imaginaryString}" : $"{Real}-{imaginaryString}";
        }
        
        public static BigIntComplex Parse(string str)
        {
            if (str.EndsWith("i"))
            {
                var strWithoutI = str[..^1];
                if (strWithoutI.Contains('+'))
                {
                    var parts = strWithoutI.Split("+");
                    return new BigIntComplex(
                        BigInteger.Parse(parts[0].Trim()),
                        ParseImaginary(parts[1]));
                }

                if (strWithoutI.Length <= 0 || strWithoutI.IndexOf("-", 1) < 0)
                    return new BigIntComplex(0, ParseImaginary(strWithoutI));
                {
                    var parts = strWithoutI.Split("-");
                    var real = BigInteger.Parse(parts[^2].Trim());
                    if (parts.Length == 3)
                        real *= -1;
                    return new BigIntComplex(real,
                        -ParseImaginary(parts[^1]));
                }
            }
            else
            {
                var real = BigInteger.Parse(str);
                return new BigIntComplex(real, 0);
            }
        }

        private static BigInteger ParseImaginary(string str)
            => str.Trim() switch
            {
                "-" => -1L,
                "" => 1L,
                _ => BigInteger.Parse(str)
            };
    }
}