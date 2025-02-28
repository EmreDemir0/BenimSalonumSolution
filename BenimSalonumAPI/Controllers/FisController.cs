using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class FisController : BaseController<FisTable>
    {
        public FisController(IRepository<FisTable> repository) : base(repository) { }
    }
}
