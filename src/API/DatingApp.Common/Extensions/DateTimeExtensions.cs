using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var age = today.Year - dob.Year;

            return age;
        }
    }
}
