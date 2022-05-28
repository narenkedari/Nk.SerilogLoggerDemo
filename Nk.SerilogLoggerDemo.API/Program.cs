using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
//NK start
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));



//var columnOptions = new ColumnOptions
//{
//    AdditionalColumns = new Collection<SqlColumn>
//    {
//        new SqlColumn
//            {ColumnName = "correlationId", PropertyName = "correlationId", DataType = SqlDbType.NVarChar, DataLength = 64}
//    }
//};
//NK end

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
//NK start
app.UseSerilogRequestLogging();
//NK end



app.UseAuthorization();

app.MapControllers();

app.Run();
