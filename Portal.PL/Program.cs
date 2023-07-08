using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json.Serialization;
using Portal.Api.Custom_Middlewares;
using Portal.Application.Application_Extensions;
using Portal.Persistence.Persistence_Extensions;
using Portal.PL.Language;
using Portal.PL.PL_Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
                });




// Add services to the container.
// Permission To Reject or Accept Ticket
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AcceptRejectTicketPolicy", policy =>
        policy.RequireRole("SuperAdmin", "TicketAdmin"));
});



builder.Services.AddControllersWithViews();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApiErrorsServices();
builder.Services.AddApiServices();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

var supportedCultures = new[] {
    new CultureInfo(""),
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                   };

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    ///Ïå ÇáÖíİæáÊ ÈÊÇÚ ÇáÇÈáßíÔä ÈÊÇÚ ÈíÏÚã Çäåì áÛå
    DefaultRequestCulture = new RequestCulture("en-US"),
    // ÇáÌÒÁ Ïå ÚÔÇä ÇáßÇÔ æÇáßæßíÒ ÈÊÇÚÊí ÇáÈÑæÒÑ
    // æÏå ÈÊÈŞí İ ÇáÌÒÁ ÇáÎÇÕ Èí ÇáßáÇíäÊ æáíÓ Úáí
    // ÇáÓíÑİÑ ÚÔÇä ãíİÖáÔ íÚãá áæÏ Úáí ÇáÓíÑİÑ Ú ÇáİÇÖí æíÊŞá ÇáãÔÑæÚ
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});
app.UseSwaggerDocumentation();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

await app.DataBaseMigrateAsync<Program>();
app.Run();

