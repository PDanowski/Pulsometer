using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pulsometer.Model.XMLSerialization;

namespace Pulsometer.Model.Models
{
    public static class PulseData
    {
        public static Range GetAverageRange(IUserConfiguration config)
        {
            int age = DateTime.Today.Year - config.Birthday.Year;

            if (config.Gender == Gender.Mężczyzna)
            {
                if (age < 1)
                {
                    return new Range(118, 137);
                }
                else if (age == 1)
                {
                    return new Range(110, 125);
                }
                else if (age >= 2 && age <= 3)
                {
                    return new Range(98, 114);
                }
                else if (age >= 4 && age <= 5)
                {
                    return new Range(87, 104);
                }
                else if (age >= 6 && age <= 8)
                {
                    return new Range(79, 94);
                }
                else if (age >= 9 && age <= 11)
                {
                    return new Range(76, 91);
                }
                else if (age >= 12 && age <= 15)
                {
                    return new Range(70, 87);
                }
                else if (age >= 16 && age <= 19)
                {
                    return new Range(69, 85);
                }
                else if (age >= 20 && age <= 39)
                {
                    return new Range(66, 82);
                }
                else if (age >= 40 && age <= 59)
                {
                    return new Range(64, 79);
                }
                else if (age >= 60 && age <= 79)
                {
                    return new Range(64, 78);
                }
                else if (age > 79)
                {
                    return new Range(64, 77);
                }
                else
                {
                    return null;
                }
            }
            else if (config.Gender == Gender.Kobieta)
            {
                if (age < 1)
                {
                    return new Range(115, 137);
                }
                else if (age == 1)
                {
                    return new Range(107, 122);
                }
                else if (age >= 2 && age <= 3)
                {
                    return new Range(96, 112);
                }
                else if (age >= 4 && age <= 5)
                {
                    return new Range(84, 100);
                }
                else if (age >= 6 && age <= 8)
                {
                    return new Range(76, 92);
                }
                else if (age >= 9 && age <= 11)
                {
                    return new Range(70, 86);
                }
                else if (age >= 12 && age <= 15)
                {
                    return new Range(66, 83);
                }
                else if (age >= 16 && age <= 19)
                {
                    return new Range(61, 78);
                }
                else if (age >= 20 && age <= 39)
                {
                    return new Range(61, 76);
                }
                else if (age >= 40 && age <= 59)
                {
                    return new Range(61, 77);
                }
                else if (age >= 60 && age <= 79)
                {
                    return new Range(60, 75);
                }
                else if (age > 79)
                {
                    return new Range(61, 78);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static Range GetFullRange(IUserConfiguration config)
        {
            int age = DateTime.Today.Year - config.Birthday.Year;

            if (config.Gender == Gender.Mężczyzna)
            {
                if (age < 1)
                {
                    return new Range(104, 156);
                }
                else if (age == 1)
                {
                    return new Range(95, 139);
                }
                else if (age >= 2 && age <= 3)
                {
                    return new Range(88, 125);
                }
                else if (age >= 4 && age <= 5)
                {
                    return new Range(76, 117);
                }
                else if (age >= 6 && age <= 8)
                {
                    return new Range(69, 106);
                }
                else if (age >= 9 && age <= 11)
                {
                    return new Range(65, 103);
                }
                else if (age >= 12 && age <= 15)
                {
                    return new Range(60, 99);
                }
                else if (age >= 16 && age <= 19)
                {
                    return new Range(58, 99);
                }
                else if (age >= 20 && age <= 39)
                {
                    return new Range(57, 95);
                }
                else if (age >= 40 && age <= 59)
                {
                    return new Range(56, 92);
                }
                else if (age >= 60 && age <= 79)
                {
                    return new Range(56, 92);
                }
                else if (age > 79)
                {
                    return new Range(56, 93);
                }
                else
                {
                    return null;
                }
            }
            else if (config.Gender == Gender.Kobieta)
            {
                if (age < 1)
                {
                    return new Range(102, 155);
                }
                else if (age == 1)
                {
                    return new Range(95, 137);
                }
                else if (age >= 2 && age <= 3)
                {
                    return new Range(85, 124);
                }
                else if (age >= 4 && age <= 5)
                {
                    return new Range(74, 112);
                }
                else if (age >= 6 && age <= 8)
                {
                    return new Range(66, 105);
                }
                else if (age >= 9 && age <= 11)
                {
                    return new Range(61, 97);
                }
                else if (age >= 12 && age <= 15)
                {
                    return new Range(57, 97);
                }
                else if (age >= 16 && age <= 19)
                {
                    return new Range(52, 92);
                }
                else if (age >= 20 && age <= 39)
                {
                    return new Range(52, 89);
                }
                else if (age >= 40 && age <= 59)
                {
                    return new Range(52, 90);
                }
                else if (age >= 60 && age <= 79)
                {
                    return new Range(50, 91);
                }
                else if (age > 79)
                {
                    return new Range(51, 94);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }

    public class Range
    {
        public int Lower { get; set; }
        public int Upper { get; set; }

        public Range(int lower, int upper)
        {
            this.Lower = lower;
            this.Upper = upper;
        }
    }
}
