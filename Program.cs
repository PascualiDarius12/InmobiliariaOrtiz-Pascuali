var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//configurar servicio de autenticacion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>//el sitio web valida con cookie
    {
        options.LoginPath = "/Usuarios/Login";
        options.LogoutPath = "/Usuarios/Logout";
        options.AccessDeniedPath = "/Home/Restringido";
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//Tiempo de expiración
    });

//crear politicas de autorizacion

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    //no hace falta empleado por que si esta autenticado y no es administrados seria empleado, salvo que tengamos mas roles
    //options.AddPolicy("Empleado", policy => policy.RequireRole("Empleado"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles(); // archivos como fotos, css etc

app.UseRouting();

// Habilitar autenticación
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
