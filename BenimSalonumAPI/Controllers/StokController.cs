using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class StokController : BaseController<StokTable>
    {
        public StokController(IRepository<StokTable> repository) : base(repository) { }
    }
}
