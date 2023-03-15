using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( 
    c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; //pede para que o swagger leia de um arquivo xml
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); //mostra o diretório que se encontra o swagger, que é o diretório da aplicação
        c.IncludeXmlComments(xmlPath); // inclui nossos comentários no swagger
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
