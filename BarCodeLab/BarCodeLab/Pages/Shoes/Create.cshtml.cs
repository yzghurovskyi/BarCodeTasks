namespace BarCodeLab.Pages.Shoes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using BarCodeLab.Models;

    public class CreateModel : BrandCountryNamePageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public CreateModel(BarCodeLab.Data.ShoesStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateBrandsDropDownList(_context);
            PopulateCountriesDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Shoes Shoes { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyShoes = new Shoes();

            if (await TryUpdateModelAsync(
                 emptyShoes,
                 "shoes",   // Prefix for form value.
                 s => s.Id, s => s.BrandId, s => s.ManufacturerId, 
                 s => s.Name, s => s.DeliveryDate, s => s.Season, s => s.Gender))
            {
                _context.Shoes.Add(emptyShoes);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateBrandsDropDownList(_context, emptyShoes.BrandId);
            PopulateCountriesDropDownList(_context, emptyShoes.ManufacturerId);
            return Page();
        }
    }
}
