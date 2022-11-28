using Console = System.Console;

namespace MathSolver;
using static PostfixBuilder;
using static PostfixCalculator;
public static class Program
{
    public static void Main()
    {
        var expr = "((10-12+right(25)(73))*(7*left(20)(46)*sum(68)(min(15)(18)))+(24/19+13)+(sum(mul(74)(24))(3)%21-max(13)(54)-10))/30+((28+27)+(36-sum(20)(18))/18+(50-right(13)(2)-23))";
        //"825034+(-49))/30+((28+27)+(36-sum(20)(18))/18+(50-right(13)(2)-23))";
        var opened = "((10-12+73)*(7*20*83)+(24/19+13)+(1779%21-54-10))/30+((28+27)+(36-38)/18+(50-2-23))";
        Console.WriteLine(Calculate(BuildPostfixExpression(expr)));
        //Console.WriteLine("#####################################################################");
        //Console.WriteLine(Calculate(BuildPostfixExpression(opened)));
    }
}
// 825034 1779