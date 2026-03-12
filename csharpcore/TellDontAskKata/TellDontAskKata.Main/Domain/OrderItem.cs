namespace TellDontAskKata.Main.Domain;

public class OrderItem
{
    public Product Product { get; }
    public int Quantity { get; }
    public decimal TaxedAmount { get; }
    public decimal Tax { get; }

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;

        var unitaryTax = Round((product.Price / 100m) * product.Category.TaxPercentage);
        var taxAmount = Round(unitaryTax * quantity);
        Tax = taxAmount;

        var unitaryTaxedAmount = Round(product.Price + unitaryTax);
        var taxedAmount = Round(unitaryTaxedAmount * quantity);
        TaxedAmount = taxedAmount;
    }

    private static decimal Round(decimal amount) =>
        decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
}