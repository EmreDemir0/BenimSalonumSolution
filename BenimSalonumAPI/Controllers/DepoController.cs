using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class DepoController : BaseController<DepoTable>
    {
        public DepoController(IRepository<DepoTable> repository) : base(repository) { }
    }
}
