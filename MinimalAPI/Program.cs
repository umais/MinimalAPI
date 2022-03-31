

var builder = WebApplication.CreateBuilder(args);
//builder.UseStartup<Startup>(); if you want to use the old startup class then you will have to create the stratup otherwise no need to

//We can map the services here using dependency injection
var config = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS API", Version = "v1" });
       
    }
);

builder.Services.AddSingleton(new DatabaseConfig { DatabaseName = config["DatabaseName"] });
builder.Services.AddSingleton<IDatabaseBootstrap,SQLiteDatabaseBootstrap>();
builder.Services.AddSingleton<IAccountRepository,AccountRepository>();

var app =builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","CMS API v1"));
//app.UseHttpsRedirection();
IDatabaseBootstrap? databaseBootstrap = app.Services.GetService<IDatabaseBootstrap>();
if(databaseBootstrap != null)
databaseBootstrap.SetUp();


//Will Add Swagger and ID4 Code to this lets see how that will work here . We need to also figure out other ways to secure this.

//These are the Minimal REST API Calls for Accounts Table 
app.MapGet("/api/Account/{id}", (string id,[FromServices]IAccountRepository accountRepository ) => accountRepository.GetAccount(id));
app.MapGet("/api/Account/", ( [FromServices] IAccountRepository accountRepository) => accountRepository.GetAllAccounts());
app.MapPost("/api/Account/", ([FromBody] Account account, [FromServices] IAccountRepository accountRepository) => accountRepository.AddAccount(account));
app.MapDelete("/api/Account/{id}", (string id, [FromServices] IAccountRepository accountRepository) => accountRepository.DeleteAccount(id));
app.MapPut("/api/Account/", ([FromBody] Account account, [FromServices] IAccountRepository accountRepository) => accountRepository.UpdateAccount(account));

//Running the API
 app.Run();
