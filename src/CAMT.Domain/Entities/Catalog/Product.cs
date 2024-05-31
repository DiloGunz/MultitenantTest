using CAMT.Domain.Base;

namespace CAMT.Domain.Entities.Catalog;

public class Product : EntityBase
{
    /// <summary>
    /// product name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public decimal Price { get; set; }
}