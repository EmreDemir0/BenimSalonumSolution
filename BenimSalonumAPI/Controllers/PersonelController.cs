using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class PersonelController : BaseController<PersonelTable>
    {
        public PersonelController(IRepository<PersonelTable> repository) : base(repository) { }
    }
}
