using System.Collections.Generic;
using System.Threading.Tasks;
using AddressAPI.Data;
using AddressAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
}