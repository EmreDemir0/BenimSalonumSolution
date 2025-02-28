using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class KullaniciController : BaseController<KullaniciTable>
    {
        public KullaniciController(IRepository<KullaniciTable> repository) : base(repository) { }
    }
}
