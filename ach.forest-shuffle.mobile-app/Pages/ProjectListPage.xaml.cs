namespace ach.forest_shuffle.mobile_app.Pages;

public partial class ProjectListPage : ContentPage
{
	public ProjectListPage(ProjectListPageModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}
}