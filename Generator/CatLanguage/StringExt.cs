namespace System
{
    public partial class String
    {
        public string Replace(int index, char symbol, string value)
        {
            return value.Replace(value.Substring(index, value.Length - index), "") + symbol + value.Substring(index, value.Length - index).Substring(1);
        }
    }
}
