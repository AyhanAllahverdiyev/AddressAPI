using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using AddressAPI.Service;
using AddressAPI.Data;

public class AddressesModel : PageModel
{
    private readonly AddressesService _addressesService;

    public AddressesModel(AddressesService addressesService)
    {
        _addressesService = addressesService;
    }

    public List<Address> Addresses { get; set; }

    public async Task OnGetAsync()
    {
        Addresses = await _addressesService.Get();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _addressesService.Remove(id);

        return RedirectToPage();
    }
    public async Task<IActionResult> OnPostUpdateAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return RedirectToPage("/Update", new { Id = id });
    }
    public async Task<IActionResult> OnPostCopyToClipboardAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Get the address details and store it in TempData
        var addressDetails = await GetAddressDetails(id);
        TempData["CopiedAddress"] = addressDetails;

        return RedirectToPage();
    }

    // Helper method to get address details
    private async Task<string> GetAddressDetails(string id)
    {
        // Retrieve the address details using id (modify this based on your service)
        var address = await _addressesService.Get(id);

        // Format the address details as a string
        return $"{address.Name}, {address.Location}, Residents: {string.Join(", ", address.Residents)}";
    }
}