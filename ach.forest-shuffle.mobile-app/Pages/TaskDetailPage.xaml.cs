namespace ach.forest_shuffle.mobile_app.Pages;

public partial class TaskDetailPage : ContentPage
{
	public TaskDetailPage(TaskDetailPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}