namespace Nexo_Pro_Integrator.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public CustomerType Typ { get; set; }
        public string ShortName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNip { get; set; }
    }

    public enum CustomerType
    {
        Person = 1,
        Company = 2
    }
}