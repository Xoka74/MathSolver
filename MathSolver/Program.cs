using Console = System.Console;

namespace MathSolver;
using static PostfixBuilder;
using static PostfixCalculator;
public static class Program
{
    public static void Main()
    {
        //var expr1 = "((35+19+52-right(0)(7))+(sum(26)(sum(2)(64))+10/10-mul(30)(38)))-((max(67)(49)%60)-(42/27/59*67)+(min(13)(9)+31*min(0)(3)*max(40)(43))+(51+min(68)(22)-53*min(68)(70)))";
        var expr2 = "((23-mul(65)(65)%42+26)*(51-1)+(38/29+mul(68)(46))*(74+0+28))*((sum(55)(58)/52/73+37)+(min(32)(min(58)(14))+max(35)(26)+40))";
        //var expr3 = "((23-mul(65)(65)%42+26)*(51-1)+(38/29+mul(68)(46))*(74+0+28))*((sum(55)(58)/52/73+37)+(min(32)(min(58)(14))+max(35)(26)+40))";
        // [TestCase("40365108","((23-mul(65)(65)%42+26)*(51-1)+(38/29+mul(68)(46))*(74+0+28))*((sum(55)(58)/52/73+37)+(min(32)(min(58)(14))+max(35)(26)+40))")]
        // 23 mul 65 65 42 % - 26 + 51 1 - * 38 29 / mul 68 46 + 74 0 + 28 + * + sum 55 58 52 / 73 / 37 + min 32 min 58 14 max 35 26 + 40 + + *

        Console.WriteLine(Calculate(BuildPostfixExpression(expr2)));
    }
}
