var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



// app.UseHttpsRedirection();
app.UseDefaultFiles(); 



// adding page name redirect code below (6/17/23 12:03PM):

app.Use(async (context, next) =>
{
    var url = context.Request.Path.Value ?? "";
    url = url.ToLower();

    if (url.EndsWith("/sheetmusic") || url.EndsWith("/discussion") || url.EndsWith("/payments"))
    {
        context.Request.Path = "/index.html";
    }

    await next();
});



app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();



