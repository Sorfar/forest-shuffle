using ach.forest_shuffle.mobile_app.Models;
using ach.forest_shuffle.mobile_app.PageModels;

namespace ach.forest_shuffle.mobile_app.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}