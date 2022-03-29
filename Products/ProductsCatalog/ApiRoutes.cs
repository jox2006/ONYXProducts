namespace ONYX.Configuration
{
    static public class ApiRoutes
    {
        //Endpoints:
        public const string Base = "/Products";
        public const string HealthCheck = "healthCheck";
        public const string GetAll = "/GetAll";
        public const string Authenticate = "/Authenticate";
        public const string GetByColour = "/{colour}";
    }
}