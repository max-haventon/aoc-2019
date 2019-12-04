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

            Console.WriteLine($"Testing 112233: {meetsCriteria(112233)}");
            Console.WriteLine($"Testing 123444: {meetsCriteria(123444)}");
            Console.WriteLine($"Testing 111122: {meetsCriteria(111122)}");
        
        }

        private static bool meetsCriteria(int number) {
            int[] digits = number.ToString().Select(o=> Convert.ToInt32(o)).ToArray();

            //Console.WriteLine($"Converted {number} into an integer array with {digits.Length} elements");
            //Console.WriteLine(digits[0]);

            bool foundDouble = false;
            for (int i = 1; i < digits.Length; i ++) {
                if (digits[i] < digits[i-1]) {
                    //Console.WriteLine($"The number {number} did not meet the criteria because {digit} is less than the previous digit {lastDigit}");
                    return false;
                } else if (digits[i] == digits[i-1]) {
                    //Console.WriteLine($"  Potential double found at position {i} in {number}.");
                    if (i - 2 >= 0 && digits[i-2] == digits[i]) {
                        // This was a triple or more
                        //Console.WriteLine("    Rejecting potential double due to preceeding value being the same");
                    } else if (i + 1 < digits.Length && digits[i] == digits[i+1]) {
                        // This was a triple or more
                        //Console.WriteLine("    Rejecting potential double due to following value being the same");
                    } else {
                        //Console.WriteLine("    The double was deemed valid!");
                        foundDouble = true;
                    }
                }
            }

            if (!foundDouble) {
                //Console.WriteLine($"No double found for number {number}");
            }

            return foundDouble;
        }
    }
}
