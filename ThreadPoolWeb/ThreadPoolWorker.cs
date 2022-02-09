namespace ThreadPoolWeb
{
    public class ThreadPoolWorker : IHostedService
    {
        private readonly ILogger<ThreadPoolWorker> _logger;

        public ThreadPoolWorker(ILogger<ThreadPoolWorker> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxIoThreads);
                ThreadPool.GetAvailableThreads(out int freeWorkerThreads, out int freeIoThreads);
                ThreadPool.GetMinThreads(out int minWorkerThreads, out int minIoThreads);

                _logger.LogInformation("MinWorkerThreads: {MinWorkerThreads}", minWorkerThreads);
                _logger.LogInformation("MaxWorkerThreads: {MaxWorkerThreads}", maxWorkerThreads);
                _logger.LogInformation("FreeWorkerThreads: {FreeWorkerThreads}", freeWorkerThreads);

                _logger.LogInformation("MinIoThreads: {MinIoThreads}", minIoThreads);
                _logger.LogInformation("MaxIoThreads: {MaxIoThreads}", maxIoThreads);
                _logger.LogInformation("FreeIoThreads: {FreeIoThreads}", freeIoThreads);

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
