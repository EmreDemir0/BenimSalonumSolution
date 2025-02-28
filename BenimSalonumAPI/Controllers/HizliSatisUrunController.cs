using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class HizliSatisUrunController : BaseController<HizliSatisUrunTable>
    {
        public HizliSatisUrunController(IRepository<HizliSatisUrunTable> repository) : base(repository) { }
    }
}
