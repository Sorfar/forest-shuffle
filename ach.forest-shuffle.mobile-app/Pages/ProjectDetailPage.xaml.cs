using ach.forest_shuffle.mobile_app.Models;

namespace ach.forest_shuffle.mobile_app.Pages;

public partial class ProjectDetailPage : ContentPage
{
	public ProjectDetailPage(ProjectDetailPageModel model)
	{
		InitializeComponent();

		BindingContext = model;
	}
}
