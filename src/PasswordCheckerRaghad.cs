using System;
using System.Linq;

namespace mcvtodoapp.Models
{
    public class PasswordChecker
    {
        // دالة مساعدة معزولة لتقليل تعقيد الدالة الأساسية (Extract Method)
        private (bool hasUpper, bool hasLower, bool hasDigit) GetPasswordComponents(string password)
        {
            return (
                password.Any(char.IsUpper),
                password.Any(char.IsLower),
                password.Any(char.IsDigit)
            );
        }

        public string CheckPasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return "Weak: Too short";
            }

            // استدعاء المكونات المستخرجة
            var (hasUpper, hasLower, hasDigit) = GetPasswordComponents(password);

            if (!hasUpper) return "Medium: Missing uppercase";
            if (!hasLower) return "Medium: Missing lowercase";
            if (!hasDigit) return "Medium: Missing digit";

            return "Strong Password";
        }
    }
}