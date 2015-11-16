using System;

namespace Torchlight.Mvc5.Common.Libs.Helpers
{
    public class DateValidator
    {
        public static bool IsValid(string dt)
        {
            DateTime output;

            return !string.IsNullOrEmpty(dt) && DateTime.TryParse(dt, out output);
        }
    }
}
