using AddressAPI.Data;
using AddressAPI.Service;
using AddressAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<UsersDatabaseSettings>(builder.Configuration.GetSection("UsersDatabaseSettings"));
 
builder.Services.AddRazorPages();
//////////
builder.Services.Configure<AddressesDatabaseSettings>(builder.Configuration.GetSection("AddressesDatabaseSettings"));
builder.Services.AddSingleton<AddressesService>();
/////////
builder.Services.AddSingleton<UserService>();
 var app = builder.Build();

app.MapGet("/", () => "Address API!");

/// <summary>
/// Get all addresses
/// </summary>
app.MapGet("/api/Address", async (AddressesService addressesService) => await addressesService.Get());

/// <summary>
/// Get an address by id
/// </summary>
app.MapGet("/api/Address/{id}", async (AddressesService addressesService, string id) =>
{
    var address = await addressesService.Get(id);
    return address is null ? Results.NotFound() : Results.Ok(address);
});

/// <summary>
/// Create a new address
/// </summary>
app.MapPost("/api/Address", async (AddressesService addressesService, Address address) =>
{
    await addressesService.Create(address);
    return Results.Ok();
});

/// <summary>
/// Update an address
/// </summary>
app.MapPut("/api/Address/{id}", async (AddressesService addressesService, string id, Address updatedAddress) =>
{
    var address = await addressesService.Get(id);
    if (address is null) return Results.NotFound();

    updatedAddress.Id = address.Id;
    await addressesService.Update(id, updatedAddress);

    return Results.NoContent();
});

/// <summary>
/// Delete an address
/// </summary>
app.MapDelete("/api/Address/{id}", async (AddressesService addressesService, string id) =>
{
    var address = await addressesService.Get(id);
    if (address is null) return Results.NotFound();

    await addressesService.Remove(address.Id);

    return Results.NoContent();
});

////////////////////////////////////////////////////////////////////////////////////////

// Kullanıcı Endpointleri
app.MapGet("/api/User", async (UserService userService) => await userService.GetUsers());
app.MapGet("/api/User/{id}", async (UserService userService, string id) =>
{
    var user = await userService.GetUser(id);
    return user is null ? Results.NotFound() : Results.Ok(user);
});
app.MapPost("/api/User", async (UserService userService, User newUser) =>
{
    await userService.CreateUser(newUser,newUser.Password);
    return Results.Ok();
});
app.MapPut("/api/User/{id}", async (UserService userService, string id, User updatedUser) =>
{
    var user = await userService.GetUser(id);
    if (user is null) return Results.NotFound();

    updatedUser.Id = user.Id;
    await userService.UpdateUser(id, updatedUser);

    return Results.NoContent();
});
app.MapDelete("/api/User/{id}", async (UserService userService, string id) =>
{
    var user = await userService.GetUser(id);
    if (user is null) return Results.NotFound();

    await userService.RemoveUser(user.Id);

    return Results.NoContent();
});
 


app.MapRazorPages();

app.Run();
