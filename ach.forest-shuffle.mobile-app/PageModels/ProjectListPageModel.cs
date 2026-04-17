using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ach.forest_shuffle.mobile_app.Data;
using ach.forest_shuffle.mobile_app.Models;
using ach.forest_shuffle.mobile_app.Services;

namespace ach.forest_shuffle.mobile_app.PageModels;

public partial class ProjectListPageModel : ObservableObject
{
	private readonly ProjectRepository _projectRepository;

	[ObservableProperty]
	private List<Project> _projects = [];

	[ObservableProperty]
	private Project? selectedProject;

	public ProjectListPageModel(ProjectRepository projectRepository)
	{
		_projectRepository = projectRepository;
	}

	[RelayCommand]
	private async Task Appearing()
	{
		Projects = await _projectRepository.ListAsync();
	}

	[RelayCommand]
	Task? NavigateToProject(Project project)
		=> project is null ? Task.CompletedTask : Shell.Current.GoToAsync($"project?id={project.ID}");

	[RelayCommand]
	async Task AddProject()
	{
		await Shell.Current.GoToAsync($"project");
	}
}