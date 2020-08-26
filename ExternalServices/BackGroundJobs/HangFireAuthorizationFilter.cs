using Hangfire.Dashboard;

namespace ExternalServices.BackGroundJobs
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
