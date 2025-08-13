using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.ConstantsAndUtilities.CourseUtilities
{
    public static class CourseUtlity
    {
        public static string CalculateProgressColor(double score, double maxScore)
        {
            double percentage = (score / maxScore) * 100;
            if (percentage >= 90) return "green"; // Excellent
            if (percentage >= 75) return "blue"; // Good
            if (percentage >= 50) return "yellow"; // Average
            return "red"; // Poor
        }
    }
}
