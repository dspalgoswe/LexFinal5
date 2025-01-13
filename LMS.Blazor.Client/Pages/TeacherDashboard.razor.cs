using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Services.Contracts;



namespace LMS.Blazor.Client.Pages
{

    partial class TeacherDashboard
    {
        [Inject]
        public ITeacherService TeacherService { get; set; }

        private IEnumerable<CourseDto> courses;
        private IEnumerable<ModuleDto> modules;
        private IEnumerable<ActivityDto> activities;
        private CourseDto selectedCourse;
        private ModuleDto selectedModule;

        protected override async Task OnInitializedAsync()
        {
            courses = (IEnumerable<CourseDto>)await TeacherService.GetAllCoursesAsync();
        }

        private async Task SelectCourse(int courseId)
        {
            selectedCourse = courses.FirstOrDefault(c => c.CourseId == courseId);
            modules = (IEnumerable<ModuleDto>)await TeacherService.GetModulesByCourseIdAsync(courseId);
            selectedModule = null;
            activities = null;
        }

        private async Task SelectModule(int moduleId)
        {
            selectedModule = modules.FirstOrDefault(m => m.ModuleId == moduleId);
            activities = (IEnumerable<ActivityDto>)await TeacherService.GetActivitiesByModuleIdAsync(moduleId);
        }

        private void CreateNewCourse()
        {
            NavigationManager.NavigateTo("/create-course");
        }

        private void CreateNewModule()
        {
            if (selectedCourse != null)
            {
                NavigationManager.NavigateTo($"/create-module/{selectedCourse.CourseId}");
            }
        }

        private void CreateNewActivity()
        {
            if (selectedModule != null)
            {
                NavigationManager.NavigateTo($"/create-activity/{selectedModule.ModuleId}");
            }
        }

        private void CreateNewUser()
        {
            NavigationManager.NavigateTo("/create-user");
        }

        private void ViewAllUsers()
        {
            NavigationManager.NavigateTo("/users");
        }
    }
}
