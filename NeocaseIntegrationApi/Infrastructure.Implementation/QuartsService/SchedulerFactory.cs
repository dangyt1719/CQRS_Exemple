using Quartz;

namespace Infrastructure.Implementation.QuartsService
{
    public class SchedulerFactory : ISchedulerFactory
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public SchedulerFactory(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public Task<IReadOnlyList<IScheduler>> GetAllSchedulers(CancellationToken cancellationToken = default)
        {
            return _schedulerFactory.GetAllSchedulers(cancellationToken);
        }

        public Task<IScheduler> GetScheduler(CancellationToken cancellationToken = default)
        {
            return _schedulerFactory.GetScheduler(cancellationToken);
        }

        public Task<IScheduler> GetScheduler(string schedName, CancellationToken cancellationToken = default)
        {
            return _schedulerFactory.GetScheduler(schedName, cancellationToken);
        }
    }
}