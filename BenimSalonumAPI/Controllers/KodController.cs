using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class KodController : BaseController<KodTable>
    {
        public KodController(IRepository<KodTable> repository) : base(repository) { }
    }
}
