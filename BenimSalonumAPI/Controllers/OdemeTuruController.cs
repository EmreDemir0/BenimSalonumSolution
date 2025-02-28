using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class OdemeTuruController : BaseController<OdemeTuruTable>
    {
        public OdemeTuruController(IRepository<OdemeTuruTable> repository) : base(repository) { }
    }
}
