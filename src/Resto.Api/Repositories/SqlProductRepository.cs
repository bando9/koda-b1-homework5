using System.Data;
using Microsoft.Data.SqlClient;
using Resto.Api.Models;

namespace Resto.Api.Repositories;

public class SqlProductRepository : IProductRepository
{
    private readonly string _connectionString;
    private readonly ILogger<SqlProductRepository> _logger;
    

    public SqlProductRepository(IConfiguration configuration, ILogger<SqlProductRepository> logger)
    {
        _connectionString=configuration.GetConnectionString("resto_db") ?? throw new InvalidOperationException("Connection string tidak ditemukan di appsettings.");
        _logger=logger;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT p.Id, p.CategoryId, c.Name as CategoryName, p.Name, p.Price, p.Stock, p.IsActive, p.CreatedAt, p.UpdatedAt
            FROM dbo.Product AS p
            JOIN dbo.Category As c ON c.Id = p.CategoryId
            ORDER BY p.Id DESC;
        """;

        var products = new List<Product>();

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while(await reader.ReadAsync(cancellationToken))
        {
            products.Add(ReadProduct(reader));
        }
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {

        const string sql = """
            SELECT 
                p.Id, p.CategoryId, c.Name as CategoryName, p.Name, p.Price, p.Stock, p.IsActive, p.CreatedAt, p.UpdatedAt
            FROM dbo.Product AS p
            JOIN dbo.Category As c ON c.Id = p.CategoryId
            WHERE p.Id = @id;
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddId(command, id);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if(!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }
        return ReadProduct(reader);
    }

    public async Task<bool> TryAddAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.Product(CategoryId,Name, Price, Stock, IsActive) VALUES
            (@categoryId, @name, @price, @stock, @isActive);
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddProductParameters(command, product);

        try
        {
            var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);

            return affectedRows == 1;
        }
        catch (SqlException e) when (e.Number is 2601 or 2627)
        {
            return false;
        }
        catch (SqlException e)
        {
            System.Console.WriteLine("SQL Error tidak terduga saat menambah produk: {Number}", e.Number);
            return false;
        }
    }

    public async Task<bool> TryDeleteAsync(int id, CancellationToken cancellationToken)
    {
        const string sql = """
            DELETE FROM dbo.Product WHERE Id = @id;
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddId(command, id);
        
        var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);

        return affectedRows == 1;


    }

    public async Task<bool> TryUpdateAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE dbo.Product SET
                CategoryId = @categoryId,
                Name = @name,
                Price = @price,
                Stock = @stock,
                IsActive = @isActive,
                UpdatedAt = GETDATE()
            WHERE Id = @id;
            """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddProductParameters(command, product);
        
        var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);

        return affectedRows == 1;

    }


    // * ==================== METHOD ====================


    private void AddProductParameters(SqlCommand command, Product product)
    {
        command.Parameters.Add("@categoryId", SqlDbType.Int).Value = product.CategoryId;

        command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = product.Name;

        command.Parameters.Add("@stock", SqlDbType.Int).Value = product.Stock;

        command.Parameters.Add("@isActive", SqlDbType.Bit).Value = product.IsActive;

        var price = command.Parameters.Add("@price", SqlDbType.Decimal);
        price.Precision = 18;
        price.Scale = 2;
        price.Value = product.Price;
    }

    private static void AddId(SqlCommand command, int id)
    {
        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
    }

    private static Product ReadProduct(SqlDataReader reader)
    {
        return new Product
        {
            Id = reader.GetInt32(0),
        CategoryId = reader.GetInt32(1),
        Name = reader.GetString(2),
        Price = reader.GetDecimal(3),
        Stock = reader.GetInt32(4),
        IsActive = reader.GetBoolean(5),
        CreatedAt = reader.GetDateTime(6),
        UpdatedAt = reader.IsDBNull(7) ? null : reader.GetDateTime(7)
        };
    }
}