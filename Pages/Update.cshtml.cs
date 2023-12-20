using System.Threading.Tasks;
using AddressAPI.Data;
using AddressAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AddressAPI.Pages.Shared
{
    public class UpdateModel : PageModel
    {
        private readonly AddressesService _addressesService;

        public UpdateModel(AddressesService addressesService)
        {
            _addressesService = addressesService;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public Address AddressToUpdate { get; set; }

        public async Task OnGetAsync()
        {

            AddressToUpdate = await _addressesService.Get(Id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            await _addressesService.Update(Id, AddressToUpdate);

            return RedirectToPage("/Addresses");
        }
    }
}