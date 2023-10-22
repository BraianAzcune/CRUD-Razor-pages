using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TutorialEU.Data;
using TutorialEU.Models;

namespace TutorialEU.Pages.Productos {
    public class IndexModel : PageModel {
        private readonly TutorialUEContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(TutorialUEContext context, ILogger<IndexModel> logger) {
            _context = context;
            _logger = logger;
        }

        public List<Producto> Productos { get; set; } = new List<Producto>();


        public async Task OnGetAsync() {
            this.Productos = await this._context.Productos.AsNoTracking().OrderBy(p => p.Id).Skip(0).Take(10).ToListAsync();
        }
    }
}
