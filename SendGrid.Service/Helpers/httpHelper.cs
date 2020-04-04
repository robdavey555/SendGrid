using System.Net;


namespace SendGrid.Service.Helpers
{
    public static class httpHelper
    {
        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
        {
            if (((int)statusCode >= 200) && ((int)statusCode <= 299))
                return true;

            return false;
        }
    }
}