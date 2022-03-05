namespace BarCodeLab.Pages.Shoes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using BarCodeLab.Models;

    public class DeleteModel : PageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public DeleteModel(BarCodeLab.Data.ShoesStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shoes Shoes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shoes = await _context.Shoes.FirstOrDefaultAsync(m => m.Id == id);

            if (Shoes == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shoes = await _context.Shoes.FindAsync(id);

            if (Shoes != null)
            {
                _context.Shoes.Remove(Shoes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
