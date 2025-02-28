using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class KullaniciRolController : BaseController<KullaniciRolTable>
    {
        public KullaniciRolController(IRepository<KullaniciRolTable> repository) : base(repository) { }
    }
}
