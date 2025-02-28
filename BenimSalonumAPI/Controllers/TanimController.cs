using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class TanimController : BaseController<TanimlarTable>
    {
        public TanimController(IRepository<TanimlarTable> repository) : base(repository) { }
    }
}
