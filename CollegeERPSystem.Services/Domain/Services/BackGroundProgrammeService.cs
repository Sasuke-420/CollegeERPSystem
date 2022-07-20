using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NCrontab;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class BackGroundProgrammeService : BackgroundService
    {
        private readonly Helpers _helpers;
        private CrontabSchedule crontab;
        private DateTime _nextRun;
        private string Schedule = "*/10 * * * * *";

        public BackGroundProgrammeService(Helpers helpers)
        {
            _helpers = helpers;
            crontab = CrontabSchedule.Parse(Schedule,new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = crontab.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var nextRun = crontab.GetNextOccurrence(DateTime.Now);
                if(DateTime.Now>_nextRun)
                {
                    //Run Service 
                    List<Programme> list = (List<Programme>)await _helpers.GetProgrammeWithNoStudents();

                    foreach(var a in list)
                    Console.WriteLine("Programmes: " + a.Name);

                    //Schedule Next Run timings
                    _nextRun = crontab.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
