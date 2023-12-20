

using System.Threading.Tasks;
using AddressAPI.Data;
using AddressAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AddressAPI.Pages.Shared
{
    // CreateAddressModel.cs
    public class AddAddressModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public string Location { get; set; }

        [BindProperty]
        public List<string> Residents { get; set; }

        public async Task<IActionResult> OnPostAsync([FromServices] AddressesService addressesService)
        {
            if (!ModelState.IsValid)
            {
                // errors
                return Page();
            }

            var newAddress = new Address
            {
                Name = Name,
                Year = Year,
                Location = Location,
                Residents = Residents ?? new List<string>()
            };

            await addressesService.Create(newAddress);

            return RedirectToPage("/Addresses"); // Redirect 
        }
    }
}
