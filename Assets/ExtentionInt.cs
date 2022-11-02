namespace Bomberman.Grid
{
    internal static class ExtentionInt
    {
        internal static bool IsEven(this int value) => (value%2 == 0);
        internal static bool IsNotEven(this int value) => !IsEven(value);
        internal static bool IsNegative(this float value) => value < 0;
    }
}