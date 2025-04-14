using System.Collections.Generic;

namespace MathRacer
{
    public static class MathProblemGenerator
    {
        public static MathProblem GenerateProblem(MathOperation operation, int operandCount, int maxNumber)
        {
            List<int> operands = new List<int>();

            switch (operation)
            {
                case MathOperation.Division:
                    // Special logic for clean division
                    int result = UnityEngine.Random.Range(1, maxNumber);
                    int temp = result;

                    for (int i = 0; i < operandCount - 1; i++)
                    {
                        int factor = UnityEngine.Random.Range(1, maxNumber);
                        temp *= factor;
                        operands.Add(factor);
                    }

                    operands.Insert(0, temp); // start with the dividend
                    break;
                case MathOperation.Subtraction:
                    for (int i = 0; i < operandCount; i++)
                    {
                        operands.Add(UnityEngine.Random.Range(1, maxNumber));
                    }
                    operands.Sort((a, b) => b.CompareTo(a)); // largest to smallest
                    break;

                default:
                    for (int i = 0; i < operandCount; i++)
                    {
                        operands.Add(UnityEngine.Random.Range(1, maxNumber));
                    }
                    break;
            }

            return new MathProblem(operands, operation);
        }
    }
}
