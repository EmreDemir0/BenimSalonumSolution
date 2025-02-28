using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;

namespace BenimSalonumAPI.Controllers
{
    public class PersonelHareketController : BaseController<PersonelHareketTable>
    {
        public PersonelHareketController(IRepository<PersonelHareketTable> repository) : base(repository) { }
    }
}
