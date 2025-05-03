namespace UniDwe.Helpers
{
    public class Converter
    {

        public static Guid? StringToGuidDef(string str)
        {
            Guid value;
            if (Guid.TryParse(str, out value))
            {
                return value;
            }
            return null;
        }
    }
}
