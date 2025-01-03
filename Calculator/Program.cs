namespace Calculator
{
    internal class Program //https://www.youtube.com/watch?app=desktop&v=9hhMUT2U2L4&t=0s
    {
        static Dictionary<char, int> priorities = new Dictionary<char, int>();
        static void Main(string[] args)
        {
            Initialize();
            string input = Console.ReadLine();
            Stack<int> numbers = new Stack<int>();
            string rpn = string.Join(" ", ConvertToRPN(input));
            Console.WriteLine(rpn);
            double sum = Calculate(rpn);
            Console.WriteLine(sum);
        }
        static void Initialize()
        {
            priorities.Add('+', 1);
            priorities.Add('-', 1);
            priorities.Add('*', 2);
            priorities.Add('/', 2);
            priorities.Add('^', 3);
        }
        static List<char> ConvertToRPN(string input)
        {
            List<char> result = new List<char>();
            Stack<char> operands = new Stack<char>();
            string currentNum = "";
            //for (int i = 0; i < input.Length; i++)
            //{
            //    char current = input[i];
                
            //    if (current == '+' || current == '-' || current == '*' || current == '/' || current == '^')
            //    {
            //        Console.WriteLine(currentNum);
            //        Console.WriteLine(current);
            //        currentNum = "";
            //        continue;
            //    }
            //    currentNum = currentNum + current;
            //}
            Console.WriteLine(currentNum);
            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];

                if (current == '+' || current == '-' || current == '*' || current == '/' || current == '^')
                {
                    if (operands.Count == 0)
                    {
                        operands.Push(current);
                        continue;
                    }

                    int currPrio = priorities[current];
                    char topOp = operands.Peek();

                    bool trigger = false;



                    while (priorities[topOp] >= currPrio)
                    {
                        if (current == topOp)
                        {
                            if (trigger)
                            {
                                result.Add(operands.Pop());
                                if (operands.Count != 0)
                                {
                                    topOp = operands.Peek();
                                }
                                trigger = false;
                            }
                            else
                            {
                                operands.Push(current);
                                break;
                            }

                        }
                        else if (priorities[topOp] > currPrio)
                        {
                            result.Add(operands.Pop());
                            topOp = operands.Peek();
                            trigger = true;
                        }
                        else
                        {
                            result.Add(operands.Pop());
                            operands.Push(current);
                            topOp = operands.Peek();
                            break;
                        }
                    }
                    while (priorities[topOp] < currPrio)
                    {
                        operands.Push(current);
                        topOp = operands.Peek();
                    }
                }


                else
                {
                    result.Add(current);
                }
            }
            foreach (char op in operands)
            {
                result.Add(op);
            }
            return result;
        }
        static double Calculate(string expression)
        {
            Stack<double> numbers = new Stack<double>();
            for (int i = 0; i < expression.Length; i+=2)
            {
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/' || expression[i] == '^')
                {
                    double num1 = numbers.Pop();
                    double num2 = numbers.Pop();
                    switch (expression[i])
                    {
                        case '+':
                            numbers.Push(num2 + num1);
                            break;
                        case '-':
                            numbers.Push(num2 - num1);
                            break;
                        case '*':
                            numbers.Push(num2 * num1);
                            break;
                        case '/':
                            numbers.Push(num2 / num1);
                            break;
                        case '^':
                            numbers.Push(Math.Pow(num2, num1));
                            break;
                        
                    }
                }
                else 
                {
                    string converted = expression[i].ToString();
                    numbers.Push(double.Parse(converted));
                }
            }
            return numbers.Pop();
        }
    }
}
