using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async () =>
{
    var todo = await FetchTodoAsync();
    return todo is not null
        ? Results.Ok(todo)
        : Results.Problem("Could not fetch todo.");
});

app.Run();

static async Task<Todo?> FetchTodoAsync()
{
    using HttpClient client = new()
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
    };

    return await client.GetFromJsonAsync<Todo>("/todos/1");
}

record Todo(int UserId, int Id, string Title, bool Completed);
