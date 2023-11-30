using BLL.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ExpiredAssignmentService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly IAssignmentService _assignmentservices;

        public ExpiredAssignmentService(IServiceProvider services, IAssignmentService assignmentservices)
        {
            _services = services;
            _assignmentservices = assignmentservices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);

                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        await _assignmentservices.UpdateExpiredAssignmentsStatus();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                }
            }
        }
    }
}
