namespace UnitTestingDemo.Services
{
    public class NumbersService
    {
        public int? Sum(int num1, int num2)
        {
            var res = num1 + num2;
            // int.MaxValue + 1 = -2_147.... Negative overflow
            if (num1 > 0 && num2 > 0 && res < 0) return null;
            return res;
        }

        public bool IsFirstNumberLarger(int num1, int num2)
        {
            return num1 > num2;
        }

        public string GetDigitName(int num)
        {
            List<string> digitNames = new List<string>()
            {
                "Zero",
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine"
            };
            return digitNames[num];
        }
    }
}
