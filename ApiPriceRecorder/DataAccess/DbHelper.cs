using System.Configuration;

namespace ApiPriceRecorder.DataAccess
{
    public static class DbHelper
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
