using System.Collections.Generic;
using System.Linq;
using InsERT.Moria.Sfera;
using InsERT.Mox.Product;

namespace Nexo_Pro_Integrator.Api.Models
{
    public class Sfera
    {
        public IEnumerable<Customer> GetCustomers()
        {
            using (var sfera = RunSfera())
            {
                var customersRef = sfera.Podmioty();
                var customers = customersRef.Dane.Wszystkie().ToList();

                return customers.Select(x => new Customer
                {
                    Id = x.Id,
                    Typ = (CustomerType)x.Typ,
                    ShortName = x.NazwaSkrocona,
                    CompanyName = x.Firma?.Nazwa,
                    CompanyNip = x.NIP
                });
            }
        }
        
        public IEnumerable<Product> GetProducts()
        {
            using (var sfera = RunSfera())
            {
                var productsRef = sfera.Asortymenty();
                var products = productsRef.Dane.Wszystkie().ToList();

                return products.Select(x => new Product
                {
                    Id = x.Id,
                    Symbol = x.Symbol,
                    Name = x.Nazwa
                });
            }
        }
        
        public IEnumerable<Order> GetOrders()
        {
            using (var sfera = RunSfera())
            {
                var ordersRef = sfera.ZamowieniaOdKlientow();
                var orders = ordersRef.Dane.Wszystkie().ToList();

                return orders.Select(x => new Order
                {
                    Id = x.Id,
                    Symbol = x.Symbol,
                    Number = x.NumerReferencyjny,
                });
            }
        }
        
        public void AddCustomer()
        {
            using (var sfera = RunSfera())
            {
                var podmioty = sfera.Podmioty();

                using (var podmiotBO = podmioty.UtworzFirme())
                {
                    var adres = podmiotBO.DodajAdres();
                    adres.Szczegoly.Ulica = "ul. Wiązowa";
                    adres.Szczegoly.NrDomu = "22";
                    adres.Szczegoly.KodPocztowy = "55-100";
                    adres.Szczegoly.Miejscowosc = "Trzebnica";

                    int id = podmioty.Dane.Wszystkie().Select(a => a.Id).Max() + 1;
                    podmiotBO.Dane.Firma.Nazwa = "Klient " + id.ToString();
                    podmiotBO.Dane.NazwaSkrocona = "Klient " + id.ToString();

                    if (!podmiotBO.Zapisz())
                    {
                        var errors = podmiotBO.PobierzKomunikatyBledow();
                    }
                }
            }
        }
        
        private static Uchwyt RunSfera()
        {
            var connectionData = DanePolaczenia.Jawne("(local)", "Nexo_Test", true);
            var connectionManager = new MenedzerPolaczen();
            
            var sfera = connectionManager.Polacz(connectionData, ProductId.Subiekt);
            
            sfera.ZalogujOperatora("Szef", "robocze");
            
            return sfera;
        }
    }
}