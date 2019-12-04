using System;
using System.Linq;

namespace _4_1
{
    class Program
    {
        static void Main(string[] args)
        {   
            const int MIN_VAL = 278384;
            const int MAX_VAL = 824795;
            
            int validNumbers = 0;
            for(int number = MIN_VAL; number <= MAX_VAL; number++) {
                if (meetsCriteria(number)) validNumbers++;
            }

            Console.WriteLine($"Found {validNumbers} valid numbers");
        }

        private static bool meetsCriteria(int number) {
            int[] digits = number.ToString().Select(o=> Convert.ToInt32(o)).ToArray();

            bool foundDouble = false;
            for (int i = 1; i < digits.Length; i ++) {
                if (digits[i] < digits[i-1]) {
                    return false;
                } else if (digits[i] == digits[i-1]) {
                    if (i - 2 >= 0 && digits[i-2] == digits[i]) {
                        // This was a triple or more
                    } else if (i + 1 < digits.Length && digits[i] == digits[i+1]) {
                        // This was a triple or more
                    } else {
                        foundDouble = true;
                    }
                }
            }

            return foundDouble;
        }
    }
}
