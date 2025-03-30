namespace api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int calcualteAge(this DateOnly dob) {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - dob.Year;
            if (dob > today.AddYears(-age))
            {
                age = --age; 
            }
            return age;
        }
    }
}
