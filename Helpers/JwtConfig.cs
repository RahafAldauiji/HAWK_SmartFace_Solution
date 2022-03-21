namespace SmartfaceSolution.Helpers
{
    /// <summary>
    /// <c>JwtConfig</c> is class have properties that are defined in the appsettings.json file 
    /// that will be used to accessing the system through a object injected into classes using .net built in DI system 
    /// </summary>
    public class JwtConfig
    {
        public string Secret { get; set; }
    }
}