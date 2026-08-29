namespace InvoiceManagementMVC.Models;

public partial class InvoiceItem
{
    public decimal? ItemTotal()
    {
        return Quantity * Price;
    }
}

public partial class Invoice
{
    public decimal? Total()
    {
        decimal? total = 0;

        foreach (var item in InvoiceItems)
        {
            total += item.ItemTotal();
        }

        return total;
    }
}