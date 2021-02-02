using System.Collections;

namespace ParityFactory.Weather
{
    public static class InputValidation
    {
        public static bool IsValid(string[] args)
        {
            if (args.Length == 0) // no argument
            {
                return false;
            }

            // argument out of range
            return ((IList) new[] {"download", "import", "aggregate"}).Contains(args[0]);
        }
    }
}
