using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class HizliSatisGrupController : BaseController<HizliSatisGrupTable>
    {
        public HizliSatisGrupController(IRepository<HizliSatisGrupTable> repository) : base(repository) { }
    }
}
