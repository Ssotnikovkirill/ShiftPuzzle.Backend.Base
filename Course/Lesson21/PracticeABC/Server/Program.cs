using PracticeABC;
using System.Data.SQLite; // Добавляем пространство имен для работы с SQLite

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
builder.Services.AddControllers();

// Добавляем поддержку Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем ProductRepository
builder.Services.AddSingleton<IProductRepository>(provider =>
{
    // Создаем базу данных и передаем путь к ней
    string connectPath = "Data Source=DataBase.db"; 

    IProductRepository productRepository = new SqlLiteProductRepository(connectPath);
    return productRepository; // Путь к файлу базы данных SQLite
});

<<<<<<< HEAD
builder.Services.AddSingleton<IProductRepository>(provider =>
{
    // Создаем базу данных и передаем путь к ней
    string connectPath = "Data Source=DataBase.db"; 
    // Создаем экземпляр репозитория и передаем путь к базе данных SQLite которая пишет и вывод в верхнем регистре
    IProductRepository productRepository = new SQLLiteUpperCaseRepository(connectPath);
    return productRepository; // Путь к файлу базы данных SQLite
});

=======
>>>>>>> 182a80b7a8d696519f8dd40fbf8b4bd873dc5367
var app = builder.Build();

// Настраиваем конвейер обработки HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

 