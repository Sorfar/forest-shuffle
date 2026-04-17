using CommunityToolkit.Mvvm.Input;
using ach.forest_shuffle.mobile_app.Models;

namespace ach.forest_shuffle.mobile_app.PageModels;

public interface IProjectTaskPageModel
{
	IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
	bool IsBusy { get; }
}