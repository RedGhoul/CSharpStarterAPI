using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Validation.Helpers
{
    public class EventValidationHelpers
    {
        private readonly ApplicationDbContext _Context;
        public EventValidationHelpers(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<bool> BeValidEventTypeId(int Id, CancellationToken arg)
        {
            return await _Context.EventTypes.AnyAsync(x => x.Id == Id);
        }

    }
}
