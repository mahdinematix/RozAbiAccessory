namespace _01_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SystemUser = "5";
        public const string ContentUploader = "20004";
        public const string ColleagueUser = "20005";

        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 5:
                    return "مدیر سیستم";
                case 20004:
                    return "محتوا گذار";
                default:
                    return "";
            }
        }
    }
}
