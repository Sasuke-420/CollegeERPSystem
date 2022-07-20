using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.Domain.Services;

namespace CollegeERPSystem.Services.Domain
{
    public static class ExtensionService
    {
        public static void ConfigureRepoServices(this IServiceCollection service)
        {
            service.AddSingleton<Helpers>();
            service.AddHostedService<BackGroundProgrammeService>();
            service.AddTransient<SchoolRepository>();
            service.AddTransient<SchoolService>();

            service.AddTransient<StudentRepository>();
            service.AddTransient<StudentService>();

            service.AddTransient<ProgrammeRepository>();
            service.AddTransient<ProgrammeService>();

            service.AddTransient<OrgRepository>();
            service.AddTransient<OrgService>();

            service.AddTransient<ClassRepository>();
            service.AddTransient<ClassService>();

            service.AddTransient<MarksRepository>();
            service.AddTransient<MarksService>();
        }
    }
}
