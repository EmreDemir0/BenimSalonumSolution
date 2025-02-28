using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class IndirimController : BaseController<IndirimTable>
    {
        public IndirimController(IRepository<IndirimTable> repository) : base(repository) { }
    }
}
