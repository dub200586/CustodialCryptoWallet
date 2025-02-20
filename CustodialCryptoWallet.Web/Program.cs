using CustodialCryptoWallet.Web.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitDbContext(builder.Configuration, builder.Environment);
builder.Services.InitRepositories();
builder.Services.InitServices();
builder.Services.InitMapper();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandlerMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
