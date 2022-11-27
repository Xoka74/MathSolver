namespace MathSolver;
using static PostfixBuilder;
using static PostfixCalculator;
public static class Program
{
    public static void Main()
    {
        var expression = "sum(left(10)(20))(min(50)(60))"; //+right(30)(40)
        //Console.WriteLine((50+40+55)*5+20-35);
        //"((50*40*sum(54)(1))*(5+20-min(62)(47)/35))-((left(42)(20)-63)-(22+9+11+40))+((47/53+69)+(64-68*68)-(41-38+5))";
        //[TestCase("((mul(56)(sum(48)(16))*max(64)(37))/30*(69+sum(65)(32)+sum(61)(69)))*((right(right(73)(73))(sum(47)(12))*49+23)/71-(36*45))","-3573150680")]
        Console.WriteLine(Calculate(BuildPostfixExpression(expression)));
    }
}