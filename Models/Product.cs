namespace Avaliacao2BimLp3.Models;

//id - codigo do produto
//name - nome do produto
//price - preço do produto
//active - produto ativo?

class Product 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public bool Active { get; set; }

    public Product () { }
    public Product (int Id, string Name, double Price, bool Active) 
    {
        Id = id;
        Name = name;
        Price = price;
        Active = active;
    }
}
