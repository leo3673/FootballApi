using Microsoft.EntityFrameworkCore;

namespace FootballApi.Service
{
    public class KeepAliveService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public KeepAliveService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    try
                    {
                        await context.leagues.FirstOrDefaultAsync();
                        Console.WriteLine("DB keep-alive ping executed at " + DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("KeepAlive error: " + ex.Message);
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(20), stoppingToken);
            }
        }
    }
}
