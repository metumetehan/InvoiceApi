namespace InvoiceApi
{
    public class Tax
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Rate { get; set; }
        public bool Deducted { get; set; }
    }

}

