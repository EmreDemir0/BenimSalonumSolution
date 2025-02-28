using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class CariController : BaseController<CariTable>
    {
        public CariController(IRepository<CariTable> repository) : base(repository) { }
    }
}
