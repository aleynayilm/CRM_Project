namespace CRMV2.Models
{
	public static class ConnectionStringProvider
	{
		public static string _connectionString { get; set; }
		static ConnectionStringProvider()
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}
	}
}
