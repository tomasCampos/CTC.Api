using CTC.Api.Authentication;
using CTC.Application;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton(FirebaseApp.Create());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add project Services
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
