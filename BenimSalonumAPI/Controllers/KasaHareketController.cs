using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class KasaHareketController : BaseController<KasaHareketTable>
    {
        public KasaHareketController(IRepository<KasaHareketTable> repository) : base(repository) { }
    }
}
