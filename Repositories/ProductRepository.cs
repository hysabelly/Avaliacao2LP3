using Avaliacao2BimLp3.Models;
using Avaliacao2BimLp3.Database;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Avaliacao2BimLp3.Repositories;

class ProductRepository 
{
    private DatabaseConfig databaseConfig;
    public ProductRepository (DatabaseConfig databaseConfig) 
    {
        this.databaseConfig = databaseConfig;
    }

    // insere um produto na tabela
    public Product Save(Product product) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Product VALUES(@Id, @Name, @Price, @Active)", product);
        return product;
    }

    // deleta um produto na tabela
    public void Delete(int id) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Product WHERE id = @Id", new {Id = id});
    }

    // verificação dos produtos
    public bool existsById (int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<bool>("SELECT count(id) FROM Products WHERE id = @Id", new {Id= id});
        return result;
    }

    // habilita um produto
     public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET  active = @Active  WHERE id = @Id ", new { Id = id, Active = true });
    }

    // desabilita um produto
    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET  active = @Active  WHERE id = @Id ", new { Id = id, Active = false });
    }

    // retorna todos os produtos
    public List<Product> GetAll() 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var product = connection.Query<Product>("SELECT * FROM Products");
        return product.ToList();
    }

    // retorna os produtos dentro de um intervalo de preço
    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice) 
    {
        
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT  * FROM Products WHERE price BETWEEN @InitialPrice AND @EndPrice", new { InitialPrice = initialPrice, EndPrice = endPrice });
        return products.ToList();
    }

    // retorna os produtos com preço acima de um preço especificado
    public List<Product> GetAllWithPriceHigherThan(double price) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price  > @Price", new { Price = price });
        return products.ToList();
    }
    
    // retorna os produtos com preço abaixo de um preço especificado
    public List<Product> GetAllWithPriceLowerThan(double price) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price  < @Price", new { Price = price });
        return products.ToList();
    }
    
    // retorna a média dos preços dos produtos
    public double GetAveragePrice() 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);                          
        connection.Open();

        double avarage = connection.ExecuteScalar<double>("SELECT AVG(price) FROM products");
        return avarage;
    }
}
