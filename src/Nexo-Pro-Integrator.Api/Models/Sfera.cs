using System.Collections.Generic;
using System.Linq;
using InsERT.Moria.Klienci;
using InsERT.Moria.Sfera;
using InsERT.Mox.Product;

namespace Nexo_Pro_Integrator.Api.Models
{
    public class Sfera
    {
        public IEnumerable<string> GetCustomers()
        {
            using (var sfera = RunSfera())
            {
                var podmioty = sfera.Podmioty();
                var customers = podmioty.Dane.Wszystkie().ToList();

                return customers.Select(x => x.NazwaSkrocona);
            }
        }
        
        public void DodajPodmiot()
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
                        podmiotBO.PobierzKomunikatyBledow();
                        // podmiotBO.WypiszBledy();
                    }
                }
            }
        }
        
        private static Uchwyt RunSfera()
        {
            var danePolaczenia = DanePolaczenia.Jawne("(local)", "Nexo_Test", true);
            var mp = new MenedzerPolaczen();
            var sfera = mp.Polacz(danePolaczenia, ProductId.Subiekt);
            
            var a = sfera.ZalogujOperatora("Szef", "robocze");
            
            return sfera;
        }
    }
}