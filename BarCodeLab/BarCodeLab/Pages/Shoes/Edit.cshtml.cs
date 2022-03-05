namespace BarCodeLab.Pages.Shoes
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using BarCodeLab.Models;

    public class EditModel : BrandCountryNamePageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public EditModel(BarCodeLab.Data.ShoesStoreContext context)
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

            Shoes = await _context.Shoes
                .Include(s => s.Brand).Include(s => s.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Shoes == null)
            {
                return NotFound();
            }
            PopulateBrandsDropDownList(_context, Shoes.BrandId);
            PopulateCountriesDropDownList(_context, Shoes.ManufacturerId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoesToUpdate = await _context.Shoes.FindAsync(id);

            if (shoesToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                 shoesToUpdate,
                 "shoes",   // Prefix for form value.
                   s => s.Id, s => s.BrandId, s => s.ManufacturerId,
                 s => s.Name, s => s.DeliveryDate, s => s.Season, s => s.Gender))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateCountriesDropDownList(_context, shoesToUpdate.ManufacturerId);
            PopulateBrandsDropDownList(_context, shoesToUpdate.BrandId);
            return Page();
        }

        private bool ShoesExists(int id)
        {
            return _context.Shoes.Any(e => e.Id == id);
        }
    }
}
