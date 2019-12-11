using System;
namespace Common
{
    public static class Util
    {
        public static long[] ParseIntCodeInput(string input) {
            string[] stringData = input.Split(',');
            long[] data = new long[stringData.Length];

            for (int i = 0; i < stringData.Length; i++)
            {
                data[i] = Int64.Parse(stringData[i]);
            }

            return data;
        }
    }
}
