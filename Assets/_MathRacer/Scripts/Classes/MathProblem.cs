using System.Collections.Generic;

namespace MathRacer
{
    public class MathProblem
    {
        private readonly List<int> operands;
        private readonly MathOperation operation;
        private readonly int answer;

        public IReadOnlyList<int> Operands => operands;
        public MathOperation Operation => operation;
        public int Answer => answer;

        public MathProblem(List<int> operands, MathOperation operation)
        {
            this.operands = operands;
            this.operation = operation;
            answer = CalculateAnswer();
        }

        private int CalculateAnswer()
        {
            if (operands == null || operands.Count == 0)
                return 0;

            int result = operands[0];

            for (int i = 1; i < operands.Count; i++)
            {
                switch (operation)
                {
                    case MathOperation.Addition:
                        result += operands[i];
                        break;
                    case MathOperation.Subtraction:
                        result -= operands[i];
                        break;
                    case MathOperation.Multiplication:
                        result *= operands[i];
                        break;
                    case MathOperation.Division:
                        if (operands[i] == 0) return 0; // Safety check
                        result /= operands[i];
                        break;
                }
            }

            return result;
        }

        public override string ToString()
        {
            string opSymbol = operation switch
            {
                MathOperation.Addition => "+",
                MathOperation.Subtraction => "-",
                MathOperation.Multiplication => "×",
                MathOperation.Division => "÷",
                _ => "?"
            };

            return string.Join($" {opSymbol} ", operands);
        }
    }
}
