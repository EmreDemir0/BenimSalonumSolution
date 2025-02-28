using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class StokHareketController : BaseController<StokHareketTable>
    {
        public StokHareketController(IRepository<StokHareketTable> repository) : base(repository) { }
    }
}
