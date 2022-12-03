using TodoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApi.Services;

public class TodoService
{
    protected readonly IMongoCollection<TodoItem> _DbCollection;

    public TodoService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _DbCollection = mongoDatabase.GetCollection<TodoItem>(
            databaseSettings.Value.CollectionName);
    }
    public virtual async Task<List<TodoItem>> GetAsync() =>
        await _DbCollection.Find(_ => true).ToListAsync();

    public async Task<TodoItem?> GetAsync(long id) =>
        await _DbCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TodoItem newBook) =>
        await _DbCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(long id, TodoItem updatedBook) =>
        await _DbCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(long id) =>
        await _DbCollection.DeleteOneAsync(x => x.Id == id);
}