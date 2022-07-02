using Microsoft.Data.Sqlite;
using Avaliacao2BimLp3.Database;
using Avaliacao2BimLp3.Repositories;
using Avaliacao2BimLp3.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product") {
    var productRepository = new ProductRepository(databaseConfig);

    if(modelAction == "List") {
        Console.WriteLine("Product List");
        foreach (var product in productRepository.GetAll()) {
            Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
        }
    }

    if(modelAction == "New") {
        Console.WriteLine("Product New");

        int id = Convert.ToInt32(args[2]);
        var name = args[3];
        var price = Convert.ToDouble(args[4]);
        var active = Convert.ToBoolean(args[5]);
        
        var product = new Product(id, name, price, active);

        if(productRepository.ExitsById(id)) {
            Console.WriteLine($"O produto com Id {id} já existe.");
        } 
        else {
            productRepository.Save(product);
            Console.WriteLine($"O produto com Nome {name} foi cadastrado com sucesso!");            
        } 
    }

        if(modelAction == "Delete") {
        var id = Convert.ToInt32(args[2]);

        if(productRepository.ExitsById(id)) {
            productRepository.Delete(id);
            Console.WriteLine($"O produto com Id {id} foi removido com sucesso.");
        }
        else {
            Console.WriteLine($"O produto com Id {id} não foi encontrado!");
        }        
    }

    if(modelAction == "Enable") {
        var id = Convert.ToInt32(args[2]);
        if(productRepository.ExitsById(id)) {
            productRepository.Enable(id);
            Console.WriteLine($"O produto com Id {id} foi habilitado com sucesso.");
        }
        else {
            Console.WriteLine($"O produto com Id {id} não foi encontrado!");
        }
    }

    if(modelAction == "Disable") {
        var id = Convert.ToInt32(args[2]);
        if(productRepository.ExitsById(id)) {
            productRepository.Disable(id);
            Console.WriteLine($"O produto com Id {id} foi desabilitado com sucesso.");
        } 
        else {
            Console.WriteLine($"O produto com Id {id} não foi encontrado!");
        }
    }

    if (modelAction == "PriceBetween") {
        double initialPrice = Convert.ToDouble(args[2]);
        double endPrice = Convert.ToDouble(args[3]);

        if (productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Any()) {
            foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice)) {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        } 
        else {
            Console.WriteLine($"Nenhum produto foi encontrado no intervalo de preço entre R$ {initialPrice} e R$ {endPrice}.");
        }
    }

    if (modelAction == "PriceHigherThan") {
        double price = Convert.ToDouble(args[2]);

        if (productRepository.GetAllWithPriceHigherThan(price).Any()) {
            foreach (var product in productRepository.GetAllWithPriceHigherThan(price)) {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        }
        else {
            Console.WriteLine($"Nenhum produto foi encontrado com o preço maior que R$ {price}!");
        }
    }

    if (modelAction == "PriceLowerThan") {
        double price = Convert.ToDouble(args[2]);

        if (productRepository.GetAllWithPriceLowerThan(price).Any()) {
            foreach (var product in productRepository.GetAllWithPriceLowerThan(price)) {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        }
        else {
            Console.WriteLine($"Nenhum produto foi encontrado com o preço menor que R$ {price}!");
        }
    }

    if (modelAction == "AveragePrice") {
        if (productRepository.GetAll().Any()) {
            Console.WriteLine($"A média final de todos os preços é de: R$ {productRepository.GetAveragePrice()}.");
        }
        else {
            Console.WriteLine("Não há nenhum produto cadastrado!");
        }
    }
}
