using Console = System.Console;

namespace MathSolver;
using static PostfixBuilder;
using static PostfixCalculator;
public static class Program
{
    public static void Main()
    {
        var expr = "((15-mul(26)(6))+(54+26*29/51)-(68*9)+(min(51)(50)+sum(61)(mul(65)(4))*69))-((8+11-39)*(9/34)-(47+32*66))*((15+62/55-right(69)(47))+(22%2*36+min(30)(72))+(61%56+36/48)+(65-right(63)(16)*left(39)(72)))*((max(34)(6)+25)-(12-sum(17)(32)-36-65)-(61+14))";
        //"825034+(-49))/30+((28+27)+(36-sum(20)(18))/18+(50-right(13)(2)-23))";
        Console.WriteLine(Calculate(BuildPostfixExpression(expr)));
        //Console.WriteLine("#####################################################################");
        //Console.WriteLine(Calculate(BuildPostfixExpression(opened)));
    }
}
// 825034 1779