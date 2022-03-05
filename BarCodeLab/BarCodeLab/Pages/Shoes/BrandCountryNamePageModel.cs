using BarCodeLab.Data;
using BarCodeLab.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BarCodeLab.Pages.Shoes
{
    public class BrandCountryNamePageModel : PageModel
    {
        public SelectList BrandNameSL { get; set; }
        public SelectList CountryNameSL { get; set; }

        public void PopulateBrandsDropDownList(ShoesStoreContext _context,
            object selectedBrand = null)
        {
            var brandsQuery = from b in _context.Brand
                                   orderby b.Name // Sort by name.
                                   select b;

            BrandNameSL = new SelectList(brandsQuery.AsNoTracking(),
                        nameof(Brand.Id), nameof(Brand.Name), selectedBrand);
        }

        public void PopulateCountriesDropDownList(ShoesStoreContext _context,
            object selectedCountry = null)
        {
            var countriesQuery = from c in _context.Country
                              orderby c.Name // Sort by name.
                              select c;

            CountryNameSL = new SelectList(countriesQuery.AsNoTracking(),
                        nameof(Country.Id), nameof(Country.Name), selectedCountry);
        }
    }
}
