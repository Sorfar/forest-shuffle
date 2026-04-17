using System.Text.Json.Serialization;

namespace ach.forest_shuffle.mobile_app.Models;

public class ProjectTask
{
	public int ID { get; set; }
	public string Title { get; set; } = string.Empty;
	public bool IsCompleted { get; set; }

	[JsonIgnore]
	public int ProjectID { get; set; }
}