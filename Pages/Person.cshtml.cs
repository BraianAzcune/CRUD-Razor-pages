using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TutorialEU.Data;
using TutorialEU.Models;

namespace TutorialEU.Pages {
    public class PersonModel : PageModel {
        public List<Person> personas { get; set; } = new List<Person>();
        // ! INYECCCION DE DEPENDENCIAS, no me gusta poner logica de DB en la pagina, cambiar
        private readonly TutorialUEContext _context;
        public PersonModel(TutorialUEContext context) {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int SizePage { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public bool MostrarInactivos { get; set; } = true;

        public async Task OnGetAsync() {
            CurrentPage = CurrentPage >= 0 ? CurrentPage : 0;
            SizePage = SizePage >= 0 ? SizePage : 10;

            var query = this._context.Person
                .AsNoTracking()
                .OrderBy(p => p.CreatedAt)
                .Skip(CurrentPage * SizePage)
                .Take(SizePage);

            if (this.MostrarInactivos) {
                this.personas = await query.ToListAsync();
            } else {
                this.personas = await query.Where(p => p.IsEnabled == true).ToListAsync();
            }
        }
    }
}
