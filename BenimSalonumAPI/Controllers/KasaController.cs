using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class KasaController : BaseController<KasaTable>
    {
        public KasaController(IRepository<KasaTable> repository) : base(repository) { }
    }
}
