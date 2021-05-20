using gamebook.Exceptions;
using gamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Services
{
    public class PlaceMover : IPlaceMover
    {
        private readonly List<Location> _locations = new()
        {
            new Location { Title = "Byt", Description = "Nacházíš se ve svém bytě, který už jsi neuklízel roky.", Pic = "interier.jpg" },
            new Location { Title = "KFC", Description = "Tvůj džob...pokaždé co sem přijdeš, cítíš ten oder přepáleného tuku.", Pic="kfc.png" },
            new Location { Title = "FGN", Description = "FUJ! Takovej libereckej chánov!", Pic = "fgn.jpg" },
            new Location { Title = "Trafika", Description = "Zde prodávaj nejlepší cíga ve městě.", Sound = new List<string> { "trafika1sound.mp3", "trafika2sound.mp3"}, Pic = "trafika.png" },
            new Location { Title = "LIDL", Description = "Pokladna 4 VOLNÁÁÁ", Pic="lidl.png",Sound = new List<string> { "lidl1sound.mp3", "lidl2sound.mp3", "lidl3sound.mp3" } },
            new Location { Title = "Náměstí", Description = "Tady na námku dělaj ten nejlepší kebab.", Pic = "namesti.jpg" },
            new Location { Title = "Army Shop", Description = "Jak moje máma vždycky říkala, nábojů není nikdy dost!", Pic="army_shop.png", Sound = new List<string> { "armyshopsound.mp3"} },
            new Location { Title = "Večerka", Description = "Velká pán, více platit!",Pic="vecerka.png", Sound = new List<string> { "vecerka1sound.mp3", "vecerka2sound.mp3"} },
            new Location { Title = "Benzínka", Description = "Není lepší párek v rohlíku, než z benzínky.", Pic= "benzinka.png" },
            new Location { Title = "Varna", Description = "Tady vyrábíš to nejčistší piko ve městě!", Pic = "varna.jpg"},
            new Location { Title = "Letiště", Description = "Tvoje cesta pryč z tohohle MORDORU!", Pic="letiste.jpg" },
            new Location { Title = "Mapa", Description = "Mapa tohohle prohnilýho města.", Pic ="mapa.JPG" },
            new Location { Title = "KFC", Description="Manažer prodejny: Zase pozdě a uplně vožralej, ale tentokrát už ti to nedaruju. Sbal si všechny svoje věci a vypadni.", Pic="kfc.png", Sound = new List<string> { "kfcdialog_veci.mp3" } },
            new Location { Title= "KFC",Description = "Manažer prodejny: Nepros a vypadni, vypadni!!!",Pic="kfc.png", Sound = new List<string> { "kfcdialog_vypadni.mp3" } },//DIAL1_1
            new Location { Title="KFC", Description = "Manažer prodejny: Co jsem řekl to platí, takových co přišli jen jednou a víckrát se neukázali mám dost.", Pic="kfc.png", Sound = new List<string> { "kfcdialog_plati.mp3" } },//DIAL_1_1_1
            new Location { Title = "Zakoutí Fugnerky", Description = "Místo kde se nejlépe dealuji piko", Pic = "sellpiko.png", Sound= new List<string>{"pernik1sound.mp3", "pernik2sound.mp3", "pernik3sound.mp3" } },
            new Location { Title= "Vězení", Description = "Byl jsi chycen při páchání trestné činnosti a byli ti sebrány všechny věci", Pic = "vezeni.png"},
            new Location { Title = "Bazén", Description = "Místo kde ti stačí jen ždibet štěstí aby jsi se dostal k penězům", Pic = "skrinky.jpg" },
            new Location { Title = "Boj", Description = "Dej mu co proto!!!", Pic="fandafight.PNG" }

        };

        private readonly List<Connection> _map = new()
        {
            new Connection { From = Places.Byt, NextPlace = Places.Mapa, Description = "<p>JÍT OBKOUKNOUT MĚSTO</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.KFC, Description = "<p>JÍT DO TVÉ VYSNĚNÉ PRÁCE V KFC</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Fgn, Description = "<p>JÍT OČÍHNOUT FUGNERKU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Vecerka, Description = "<p>JÍT NAKOUPIT NĚJAKÝ TO ZBOŽÍ DO VEČERKY</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.LIDL, Description = "<p>JÍT MAKAT DO LIDLU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Bazen, Description = "<p>JÍT SE POROZHLÉDNOUT PO PENĚZÍCH K BAZÉNU</p>", SpecialPlace = "Bazen" },
            new Connection { From = Places.Mapa, NextPlace = Places.Benzinka, Description = "<p>PODÍVAT NA BENZÍNKU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.ArmyShop, Description = "<p> KOUKNOUT SE CO TEN STAREJ DĚDEK DNESKA PRODÁVÁ V ARMYSHOPU </p>", SpecialPlace= "ArmyShop" },
            new Connection { From = Places.Mapa, NextPlace = Places.Namesti, Description = "<p>POBHLÍDNOUT SE PO NÁMĚSTÍ</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Byt, Description = "<p>ZPĚT DO BYTU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Letiste, Description = "<p>CESTA ZA LEPŠÍM ŽIVOTEM</p>", SpecialPlace = "Letiste" },
            new Connection { From = Places.KFC, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Fgn, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Vecerka, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Benzinka, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.ArmyShop, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Namesti, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Letiste, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Fgn, NextPlace = Places.Trafika, Description = "<p>JÍT SI KOUPIT NĚJAKÝ CÍGA</p>" },
            new Connection { From = Places.Trafika, NextPlace = Places.Fgn, Description = "<p>ZPĚT NA FUGNERKU</p>" },
            new Connection { From = Places.Byt, NextPlace = Places.Varna, Description = "<p>UVAŘIT NĚJAKOU SPECIALTKU</p>", SpecialPlace = "Varna" },
            new Connection { From = Places.Vecerka, NextPlace = Places.Vecerka, MoneyPlace = "Vecerka", Moneydesc = "<p>OKRÁST (+250)</p>", MoneyGet = 250 },
            new Connection { From = Places.LIDL, NextPlace = Places.LIDL, MoneyPlace = "LIDL", Moneydesc = "<p>PRACOVAT (+80)</p>", MoneyGet = 80 },
            new Connection { From = Places.LIDL,NextPlace = Places.Mapa, Description="<p>ZPĚT NA MAPU</p>"},
            new Connection { From = Places.Benzinka, NextPlace = Places.Benzinka, MoneyPlace = "Benzinka", Moneydesc = "<p>OKRÁST (+500)</p>", MoneyGet = 500 },
            new Connection { From = Places.Varna, NextPlace = Places.Byt, Moneydesc = "<p>UVAŘIT PERNÍK (PERNÍK +1)</p>" },
            new Connection { From = Places.Varna, NextPlace = Places.Byt, Description = "<p>ZPĚT DO BYTU</p>" },
            new Connection { From = Places.Fgn, NextPlace = Places.Zakouti, Description = "<p>JÍT PRODAT TVŮJ ORGINÁLNÍ VÝROBEK</p>", SpecialPlace = "Prodavani" },
            new Connection { From = Places.Zakouti, NextPlace = Places.Fgn, Description = "<p>ZPĚT NA FGN</p>" },
            new Connection { From = Places.Zakouti, NextPlace = Places.Fgn, MoneyPlace = "Zakoutí Fgn", Moneydesc = "PRODAT TVŮJ VÝROBEK(+250)", MoneyGet = 250 },
            new Connection { From = Places.Vezeni, NextPlace = Places.Byt, Description = "<p>ODĚJÍT Z VĚZENÍ</p>" },
            new Connection { From = Places.Bazen, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Bazen, NextPlace = Places.Bazen, MoneyPlace = "Bazénové skříňky", Moneydesc = "VYKRÁST SKŘÍŇKU", SpecialPlace = "Bazen" },
            new Connection { From = Places.Trafika, NextPlace = Places.Trafika, BuyingPlace = "Nákup Cíg", HpUp = 1, Cost = 200, Description="<p>KOUPIT CÍGA (HP+1 CASH -200)</p>" },
            new Connection { From = Places.Benzinka, NextPlace = Places.Benzinka, BuyingPlace = "Nákup párku v rohlíku", HpUp = 1, Cost = 100, Description = "<p>KOUPIT NEJLUXUSNĚJŠÍ PÁREK V ROHLÍKU V OKOLOÍ (HP+1 CASH -100)</p>" },
            new Connection { From = Places.ArmyShop, NextPlace = Places.ArmyShop,Description="<p>KOUPIT GLOCK (-10000)</p>", SpecialPlace="ArmyShop"},
            new Connection { From = Places.Namesti, NextPlace = Places.BojNaNamesti, Description = "<p>ZBÍT NĚKOHO KVŮLI PENĚZŮM</p>", SpecialPlace = "namestiBoj"},
            new Connection { From = Places.BojNaNamesti, NextPlace = Places.Namesti, Description = "<p>ZPĚT NA NÁMĚSTÍ</p>" },
            new Connection { From = Places.BojNaNamesti, NextPlace = Places.BojNaNamesti, MoneyPlace = "Boj Na Náměstí",Moneydesc="DÁT RÁNU", SpecialPlace = "BojNaNamesti"},


            //---------------------
            //dialog sekvence kfc
            new Connection { From = Places.KFC, NextPlace = Places.KFC_DIAL_1, Description="<p>DOBREJ ŠÉFE</p>"},
            new Connection { From = Places.KFC_DIAL_1, NextPlace = Places.Mapa, Description = "<p>UDAV SE TY ŠPÍNO</p>" },
            new Connection { From = Places.KFC_DIAL_1, NextPlace = Places.KFC_DIAL_1_1, Description = "<p>SLIBUJU ŽE UŽ SE TO NESTANE</p>" },
            new Connection { From = Places.KFC_DIAL_1_1, NextPlace = Places.Mapa, Description = "<p>BĚŽ SE BODNOUT</p>" },
            new Connection { From = Places.KFC_DIAL_1_1, NextPlace = Places.KFC_DIAL_1_1_1, Description = "<p>PROSÍÍM SLIBUJU ŽE UŽ SE TO VÍCKRÁT NESTANE.</p>" },
            new Connection { From = Places.KFC_DIAL_1_1_1, NextPlace = Places.Mapa, Description = "<p>BĚŽ NĚKAM</p>" }



        };
        public bool ExistLocation(Places id)
        {
            return (_locations.Count > (int)id);
        }

        public List<Connection> GetConnectionsFrom(Places id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.From == id).ToList();
            }
            throw new InvalidLocationException();
        }

        public List<Connection> GetConnectionsTo(Places id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.NextPlace == id).ToList();
            }
            throw new InvalidLocationException();
        }

        public Location GetLocation(Places id)
        {
            if (ExistLocation(id))
            {
                return _locations[(int)id];
            }
            throw new InvalidLocationException();
        }
    }
}
