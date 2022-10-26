using System;

namespace MergerBay.Utilities {
  public static class Config {
    public static String ConnectionString { get { return "Server=eventsolution.uaenorth.cloudapp.azure.com;User ID=eventsolution;Password=abc.123;Database=MergerBay;TrustServerCertificate=True;Encrypt=True;MultipleActiveResultSets=true;"; } }
        public static String From_Email { get; set; }
        public static String To_Email { get; set; }
        public static String Server { get; set; }
        public static int Port { get; set; }
        public static String Login_UserName { get; set; }
        public static String Login_Password { get; set; }
        public static bool Is_SSL { get; set; }

    }
}
