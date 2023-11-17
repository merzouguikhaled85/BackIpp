using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Hub;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Helpers;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));



builder.Services.AddSignalR();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseOracle(builder.Configuration.GetConnectionString("dataMaintenanceConnection")));

builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});




builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});



builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7058",
            ValidAudience = "https://localhost:7058",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s59JXsM7wNh6rVGF"))
        };
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseCors("CORSPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/offers");
});

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
