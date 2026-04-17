namespace ach.forest_shuffle.mobile_app.Data;

public static class Constants
{
	public const string DatabaseFilename = "AppSQLite.db3";

	public static string DatabasePath =>
		$"Data Source={Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename)}";
}