using DatabaseFactory.Data;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandServices.Points
{
    public class CreatePointForVisitingSiteService : CreatePointService
    {
        public CreatePointForVisitingSiteService(
            ILogger<CreatePointForVisitingSiteService> logger,
            Database database) : base(logger, database)
        {
        }

        protected override string ProcedureName => "Blog.CreatePointForVisitingSite";
    }
}
