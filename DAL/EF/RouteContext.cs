using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
    public class RouteContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Carriage> Carriages { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        static RouteContext()
        {
            Database.SetInitializer<RouteContext>(new DbInitializer());
        }
        public RouteContext(string connectionString) : base(connectionString)
        {

        }

    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<RouteContext>
    {
        protected override void Seed(RouteContext db)
        {

            db.Carriages.Add(new Carriage { Id = 1, FreePlaces = 40, NumberOfPlaces = 60, Type = "2nd class sleeper",RouteId=1, Number = 1 });
            db.Carriages.Add(new Carriage { Id = 2, FreePlaces = 55, NumberOfPlaces = 60, Type = "1st class sleeper", RouteId = 1, Number = 2 });
            db.Carriages.Add(new Carriage { Id = 3, FreePlaces = 50, NumberOfPlaces = 60, Type = "3rd class sleeper", RouteId = 1, Number = 3 });
            db.Carriages.Add(new Carriage { Id = 4, FreePlaces = 43, NumberOfPlaces = 60, Type = "Seat:Standart" , RouteId = 2, Number = 1 });
            db.Carriages.Add(new Carriage { Id = 5, FreePlaces = 12, NumberOfPlaces = 60, Type = "Seat:Economic", RouteId=2, Number = 2 });
            db.Routes.Add(new Route
            {
                Id = 1,
                Name = "Kyiv - Kharkiv",
                Days = new string[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() },
                DaysDuration = 0,
                Stations = new int[] { 196, 235, 295, 149 },
                TimeOfStopPoints = new string[]
                {
                    new DateTime(2019, 1, 1, 12, 30, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 16, 35, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 17, 40, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 18, 40, 1).ToString("HH:mm")
                },
                CarriagesId = new int[] { 1, 2, 3 }
            });


            db.Routes.Add(new Route
            {
                Id = 2,
                Name = "Kyiv - Kharkiv (new)",
                Days = new string[] { DayOfWeek.Monday.ToString(), DayOfWeek.Tuesday.ToString() },
                DaysDuration = 0,
                Stations = new int[] { 196, 235, 295, 149 },
                TimeOfStopPoints = new string[]
                {
                    new DateTime(2019, 1, 1, 12, 30, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 16, 35, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 17, 40, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 18, 40, 1).ToString("HH:mm")
                },
                CarriagesId = new int[] { 4, 5 }
            });
            string[] Cities = new String[] {
                "Alchevsk", "Almazna", "Alupka", "Alushta", "Amvrosiivka", "Ananyiv",
                "Andrushivka", "Antratsyt", "Apostolove", "Armiansk", "Artemivsk", "Artemove",
                "Artsyz", "Avdiivka", "Bakhchisaray", "Bakhmach", "Bakhmut", "Balakliia",
                "Balta", "Bar", "Baranivka", "Barvinkove", "Bashtanka", "Baturyn",
                "Belz", "Berdiansk", "Berdychiv", "Berehove", "Berestechko", "Berezan",
                "Berezhany", "Berezivka", "Berezne", "Bershad", "Beryslav", "Bibrka",
                "BilaTserkva", "Bilhorod-Dnistrovskyi", "Biliaivka", "Bilohirsk", "Bilopillia", "Bilozerske",
                "Bilytske", "Blahovishchenske", "Bobrovytsia", "Bobrynets", "Bohodukhiv", "Bohuslav", "Boiarka", "Bolekhiv",
                "Bolhrad", "Borschiv", "Boryslav", "Boryspil", "Borzna", "Brianka", "Brody", "Broshniv-Osada", "Brovary",
                "Bucha", "Buchach", "Burshtyn", "Buryn", "Busk", "ChasivYar", "Cherkasy", "Chernihiv", "Chernivtsi",
                "Chernobyl", "Chervonohrad", "Chervonopartyzansk", "Chop", "Chornomorsk", "Chortkiv", "Chuhuiv", "Chyhyryn",
                "Chystiakove", "Debaltseve", "Derazhnia", "Derhachi", "Dnipro", "Dniprorudne", "Dobromyl", "Dobropillia",
                "Dokuchaievsk", "Dolyna", "Dolynska", "Donetsk", "Dovzhansk", "Drohobych", "Druzhba", "Druzhkivka", "Dubliany",
                "Dubno", "Dubrovytsia", "Dunaivtsi", "Dzhankoy", "Enerhodar", "Fastiv", "Feodosiya", "Hadiach", "Haisyn",
                "Haivoron", "Halych", "Henichesk", "Hertsa", "Hirnyk", "Hirske", "Hlobyne", "Hlukhiv", "Hlyniany",
                "Hnivan", "HolaPrystan", "Holubivka", "HorishniPlavni", "Horlivka", "Horodenka", "Horodnia", "Horodok",
                "Horodok", "Horodysche", "Horokhiv", "Hrebinka", "Huliaipole", "Ichnia", "Illintsi", "Ilovaisk", "Inkerman",
                "Irpin", "Irshava", "Ivano-Frankivsk", "Iziaslav", "Izium", "Izmail", "Kadiyivka", "Kaharlyk", "Kakhovka",
                "Kalush", "Kalynivka", "Kamianets-Podilskyi", "Kamianka", "Kamianka-Buzka", "Kamianka-Dniprovska", "Kamianske",
                "Kamin-Kashyrskyi", "Kaniv", "Karlivka", "Kerch", "Kharkiv", "Khartsyzk", "Kherson", "Khmelnytskyi", "Khmilnyk",
                "Khodoriv", "Khorol", "Khorostkiv", "Khotyn", "Khrestivka", "Khrustalnyi", "Khrystynivka", "Khust",
                "Khyriv", "Kilia", "Kitsman", "Kivertsi", "Kobeliaky", "Kodyma", "Kolomyia", "Komarno", "Komsomolske",
                "Konotop", "Kopychyntsi", "Korets", "Koriukivka", "Korosten", "Korostyshiv", "Korsun-Shevchenkivskyi", "Kosiv",
                "Kostiantynivka", "Kostopil", "Kovel", "Koziatyn", "Kramatorsk", "Krasnohorivka", "Krasnohrad", "Krasnoperekopsk",
                "Krasyliv", "Kremenchuk", "Kremenets", "Kreminna", "Krolevets", "Kropyvnytskyi", "KryvyiRih", "Kupiansk",
                "Kurakhove", "Kyiv", "Ladyzhyn", "Lanivtsi", "Lebedyn", "Liuboml", "Liubotyn", "Lokhvytsia", "Lozova",
                "Lubny", "Luhansk", "Lutsk", "Lutuhyne", "Lviv", "Lyman", "Lypovets", "Lysychansk", "Makiivka",
                "MalaVyska", "Malyn", "Marhanets", "Mariupol", "Maryinka", "Maydanets", "Melitopol", "Mena", "Merefa",
                "Miusynsk", "Mohyliv-Podilskyi", "Molochansk", "Molodohvardiysk", "Monastyrysche", "Monastyryska", "Morshyn",
                "Mospyne", "Mostyska", "Mukachevo", "Mykolaiv", "Mykolaiv", "Mykolaivka", "Myrhorod", "Myrnohrad", "Myronivka",
                "Nadvirna", "Nemyriv", "Netishyn", "Nikopol", "Nizhyn", "Nosivka", "NovaKakhovka", "NovaOdesa", "Novhorod-Siverskyi",
                "Novoazovsk", "Novodnistrovsk", "Novodruzhesk", "Novohrad-Volynskyi", "Novohrodivka", "Novoiavorivsk", "Novomoskovsk",
                "Novomyrhorod", "Novoselytsia", "Novoukrainka", "Novovolynsk", "NovyiBuh", "NovyiKalyniv", "NovyiRozdil",
                "Obukhiv", "Ochakiv", "Odessa", "Okhtyrka", "Oleksandriia", "Oleksandrivsk", "Oleshky",
                "Olevsk", "Orikhiv", "Oster", "Ostroh", "Ovruch", "Pavlohrad", "Perechyn", "Pereiaslav-Khmelnytskyi", "Peremyshliany",
                "Pereschepyne", "Perevalsk", "Pershotravensk", "Pervomaisk", "Pervomaisk", "Pervomaiskyi", "Petrovske", "Piatykhatky",
                "Pidhaitsi", "Pidhorodne", "Pivdenne", "Pochaiv", "Podilsk", "Pohrebysche", "Pokrov", "Pokrovsk", "Polohy",
                "Polonne", "Poltava", "Pomichna", "Popasna", "Pryluky", "Prymorsk", "Prypiat", "Pryvillia", "Pustomyty", "Putyvl",
                "Pyriatyn", "Radekhiv", "Radomyshl", "Radyvyliv", "Rakhiv", "Rava-Ruska", "Reni", "Rivne", "Rodynske", "Rohatyn",
                "Romny", "Rovenky", "Rozdilna", "Rozhysche", "Rubizhne", "Rudky", "Rzhyschiv", "Saky", "Sambir", "Sarny",
                "Schastia", "Selydove", "Semenivka", "Seredyna-Buda", "Sevastopol", "Shakhtarsk", "Sharhorod", "Shcholkine", "Shepetivka",
                "Shostka", "Shpola", "Shumsk", "Sieverodonetsk", "Simferopol", "Siversk", "Skadovsk", "Skalat", "Skole", "Skvyra", "Slavuta",
                "Slavutych", "Sloviansk", "Smila", "Sniatyn", "Snihurivka", "Snizhne", "Snovsk", "Sokal", "Sokyriany",
                "Soledar", "Sorokyne", "Sosnivka", "Starobilsk", "Starokostiantyniv", "StaryiKrym", "StaryiSambir", "Stebnyk", "Storozhynets", "Stryi",
                "Sudak", "SudovaVyshnia", "Sukhodilsk", "Sumy", "Svaliava", "Svarychiv", "Svatove", "Sviatohirsk",
                "Svitlodarsk", "Svitlovodsk", "Synelnykove", "Talne", "Tarascha", "Tatarbunary", "Tavriysk", "Teplodar",
                "Teplohirsk", "Terebovlia", "Ternivka", "Ternopil", "Tetiiv", "Tiachiv", "Tlumach", "Tokmak", "Toretsk",
                "Trostianets", "Truskavets", "Tulchyn", "Turka", "Tysmenytsia", "Uhniv", "Ukrainka", "Ukrainsk", "Uman",
                "Ustyluh", "Uzhhorod", "Uzyn", "Vakhrusheve", "Valky", "Varash", "Vashkivtsi", "Vasylivka", "Vasylkiv",
                "Vatutine", "VelykiMosty", "Verkhivtseve", "Verkhniodniprovsk", "Vilniansk", "Vilnohirsk", "Vinnytsia", "Volnovakha", "Volochysk", "Volodymyr-Volynskyi",
                "Vorozhba", "Vovchansk", "Voznesensk", "Vuhledar", "Vuhlehirsk", "Vylkove", "Vynnyky", "Vynohradiv",
                "Vyshhorod", "Vyshneve", "Vyzhnytsia", "Yahotyn", "Yalta", "Yampil", "Yaremcha", "Yasynuvata", "Yavoriv", "Yenakiieve", "Yevpatoria",
                "Yunokomunarivsk", "Yuzhne", "Yuzhnoukrainsk", "Zalischyky", "Zaporizhia", "Zastavna", "Zavodske", "Zbarazh", "Zboriv", "Zdolbuniv", "Zelenodolsk", "Zhashkiv",
                "Zhdanivka", "Zhmerynka", "Zhovkva", "ZhovtiVody", "Zhydachiv", "Zhytomyr", "Zinkiv", "Zmiiv",
                "Znamianka", "Zolochiv", "Zolote", "Zolotonosha", "Zorynsk", "Zuhres", "Zvenyhorodka", "Zymohiria"  };

            for(int i=0; i < Cities.Length; i++)
            {
                db.Stations.Add(new Station { Id = i, Name = Cities[i] });
            }
            db.SaveChanges();
        }
    }
}
