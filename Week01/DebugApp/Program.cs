Console.WriteLine("Debug Practice App");

int firstNumber = 10;
int secondNumber = 5;

int sum = AddNumbers(firstNumber, secondNumber);
int difference = SubtractNumbers(firstNumber, secondNumber);

Console.WriteLine($"{firstNumber} + {secondNumber} = {sum}");
Console.WriteLine($"{firstNumber} - {secondNumber} = {difference}");

static int AddNumbers(int numberOne, int numberTwo)
{
    int result = numberOne + numberTwo;
    return result;
}

static int SubtractNumbers(int numberOne, int numberTwo)
{
    int result = numberOne - numberTwo;
    return result;
}












