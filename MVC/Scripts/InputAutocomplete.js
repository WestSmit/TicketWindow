﻿
function autocomplete(id, btnId, arr)
{
    var inp = document.getElementById(id);
    var currentFocus;
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(a);
        var n = 0;
        for (i = 0; i < arr.length; i++) {
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                b = document.createElement("DIV");
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                b.addEventListener("click", function (e) {
                    inp.value = this.getElementsByTagName("input")[0].value;
                    closeAllLists();
                    if (document.getElementById(btnId) != undefined) {
                        document.getElementById(btnId).disabled = false;
                    }
                    
                });
                n++;
                a.appendChild(b);
                if (n > 5) {
                    break;
                }
            }
            else {
                if (document.getElementById(btnId) != undefined) {
                    document.getElementById(btnId).disabled = true;
                }
            }
        }
    });
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            currentFocus++;
            addActive(x);
        } else if (e.keyCode == 38) { //up
            currentFocus--;
            addActive(x);
        } else if (e.keyCode == 13) {
            e.preventDefault();
            if (currentFocus > -1) {
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        if (!x) return false;
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}


var countries = ["Alchevsk",
    "Almazna",
    "Alupka",
    "Alushta",
    "Amvrosiivka",
    "Ananyiv",
    "Andrushivka",
    "Antratsyt",
    "Apostolove",
    "Armiansk",
    "Artemivsk",
    "Artemove",
    "Artsyz",
    "Avdiivka",
    "Bakhchisaray",
    "Bakhmach",
    "Bakhmut",
    "Balakliia",
    "Balta",
    "Bar",
    "Baranivka",
    "Barvinkove",
    "Bashtanka",
    "Baturyn",
    "Belz",
    "Berdiansk",
    "Berdychiv",
    "Berehove",
    "Berestechko",
    "Berezan",
    "Berezhany",
    "Berezivka",
    "Berezne",
    "Bershad",
    "Beryslav",
    "Bibrka",
    "BilaTserkva",
    "Bilhorod-Dnistrovskyi",
    "Biliaivka",
    "Bilohirsk",
    "Bilopillia",
    "Bilozerske",
    "Bilytske",
    "Blahovishchenske",
    "Bobrovytsia",
    "Bobrynets",
    "Bohodukhiv",
    "Bohuslav",
    "Boiarka",
    "Bolekhiv",
    "Bolhrad",
    "Borschiv",
    "Boryslav",
    "Boryspil",
    "Borzna",
    "Brianka",
    "Brody",
    "Broshniv-Osada",
    "Brovary",
    "Bucha",
    "Buchach",
    "Burshtyn",
    "Buryn",
    "Busk",
    "ChasivYar",
    "Cherkasy",
    "Chernihiv",
    "Chernivtsi",
    "Chernobyl",
    "Chervonohrad",
    "Chervonopartyzansk",
    "Chop",
    "Chornomorsk",
    "Chortkiv",
    "Chuhuiv",
    "Chyhyryn",
    "Chystiakove",
    "Debaltseve",
    "Derazhnia",
    "Derhachi",
    "Dnipro",
    "Dniprorudne",
    "Dobromyl",
    "Dobropillia",
    "Dokuchaievsk",
    "Dolyna",
    "Dolynska",
    "Donetsk",
    "Dovzhansk",
    "Drohobych",
    "Druzhba",
    "Druzhkivka",
    "Dubliany",
    "Dubno",
    "Dubrovytsia",
    "Dunaivtsi",
    "Dzhankoy",
    "Enerhodar",
    "Fastiv",
    "Feodosiya",
    "Hadiach",
    "Haisyn",
    "Haivoron",
    "Halych",
    "Henichesk",
    "Hertsa",
    "Hirnyk",
    "Hirske",
    "Hlobyne",
    "Hlukhiv",
    "Hlyniany",
    "Hnivan",
    "HolaPrystan",
    "Holubivka",
    "HorishniPlavni",
    "Horlivka",
    "Horodenka",
    "Horodnia",
    "Horodok",
    "Horodok",
    "Horodysche",
    "Horokhiv",
    "Hrebinka",
    "Huliaipole",
    "Ichnia",
    "Illintsi",
    "Ilovaisk",
    "Inkerman",
    "Irpin",
    "Irshava",
    "Ivano-Frankivsk",
    "Iziaslav",
    "Izium",
    "Izmail",
    "Kadiyivka",
    "Kaharlyk",
    "Kakhovka",
    "Kalush",
    "Kalynivka",
    "Kamianets-Podilskyi",
    "Kamianka",
    "Kamianka-Buzka",
    "Kamianka-Dniprovska",
    "Kamianske",
    "Kamin-Kashyrskyi",
    "Kaniv",
    "Karlivka",
    "Kerch",
    "Kharkiv",
    "Khartsyzk",
    "Kherson",
    "Khmelnytskyi",
    "Khmilnyk",
    "Khodoriv",
    "Khorol",
    "Khorostkiv",
    "Khotyn",
    "Khrestivka",
    "Khrustalnyi",
    "Khrystynivka",
    "Khust",
    "Khyriv",
    "Kilia",
    "Kitsman",
    "Kivertsi",
    "Kobeliaky",
    "Kodyma",
    "Kolomyia",
    "Komarno",
    "Komsomolske",
    "Konotop",
    "Kopychyntsi",
    "Korets",
    "Koriukivka",
    "Korosten",
    "Korostyshiv",
    "Korsun-Shevchenkivskyi",
    "Kosiv",
    "Kostiantynivka",
    "Kostopil",
    "Kovel",
    "Koziatyn",
    "Kramatorsk",
    "Krasnohorivka",
    "Krasnohrad",
    "Krasnoperekopsk",
    "Krasyliv",
    "Kremenchuk",
    "Kremenets",
    "Kreminna",
    "Krolevets",
    "Kropyvnytskyi",
    "KryvyiRih",
    "Kupiansk",
    "Kurakhove",
    "Kyiv",
    "Ladyzhyn",
    "Lanivtsi",
    "Lebedyn",
    "Liuboml",
    "Liubotyn",
    "Lokhvytsia",
    "Lozova",
    "Lubny",
    "Luhansk",
    "Lutsk",
    "Lutuhyne",
    "Lviv",
    "Lyman",
    "Lypovets",
    "Lysychansk",
    "Makiivka",
    "MalaVyska",
    "Malyn",
    "Marhanets",
    "Mariupol",
    "Maryinka",
    "Maydanets",
    "Melitopol",
    "Mena",
    "Merefa",
    "Miusynsk",
    "Mohyliv-Podilskyi",
    "Molochansk",
    "Molodohvardiysk",
    "Monastyrysche",
    "Monastyryska",
    "Morshyn",
    "Mospyne",
    "Mostyska",
    "Mukachevo",
    "Mykolaiv",
    "Mykolaiv",
    "Mykolaivka",
    "Myrhorod",
    "Myrnohrad",
    "Myronivka",
    "Nadvirna",
    "Nemyriv",
    "Netishyn",
    "Nikopol",
    "Nizhyn",
    "Nosivka",
    "NovaKakhovka",
    "NovaOdesa",
    "Novhorod-Siverskyi",
    "Novoazovsk",
    "Novodnistrovsk",
    "Novodruzhesk",
    "Novohrad-Volynskyi",
    "Novohrodivka",
    "Novoiavorivsk",
    "Novomoskovsk",
    "Novomyrhorod",
    "Novoselytsia",
    "Novoukrainka",
    "Novovolynsk",
    "NovyiBuh",
    "NovyiKalyniv",
    "NovyiRozdil",
    "Obukhiv",
    "Ochakiv",
    "Odessa",
    "Okhtyrka",
    "Oleksandriia",
    "Oleksandrivsk",
    "Oleshky",
    "Olevsk",
    "Orikhiv",
    "Oster",
    "Ostroh",
    "Ovruch",
    "Pavlohrad",
    "Perechyn",
    "Pereiaslav-Khmelnytskyi",
    "Peremyshliany",
    "Pereschepyne",
    "Perevalsk",
    "Pershotravensk",
    "Pervomaisk",
    "Pervomaisk",
    "Pervomaiskyi",
    "Petrovske",
    "Piatykhatky",
    "Pidhaitsi",
    "Pidhorodne",
    "Pivdenne",
    "Pochaiv",
    "Podilsk",
    "Pohrebysche",
    "Pokrov",
    "Pokrovsk",
    "Polohy",
    "Polonne",
    "Poltava",
    "Pomichna",
    "Popasna",
    "Pryluky",
    "Prymorsk",
    "Prypiat",
    "Pryvillia",
    "Pustomyty",
    "Putyvl",
    "Pyriatyn",
    "Radekhiv",
    "Radomyshl",
    "Radyvyliv",
    "Rakhiv",
    "Rava-Ruska",
    "Reni",
    "Rivne",
    "Rodynske",
    "Rohatyn",
    "Romny",
    "Rovenky",
    "Rozdilna",
    "Rozhysche",
    "Rubizhne",
    "Rudky",
    "Rzhyschiv",
    "Saky",
    "Sambir",
    "Sarny",
    "Schastia",
    "Selydove",
    "Semenivka",
    "Seredyna-Buda",
    "Sevastopol",
    "Shakhtarsk",
    "Sharhorod",
    "Shcholkine",
    "Shepetivka",
    "Shostka",
    "Shpola",
    "Shumsk",
    "Sieverodonetsk",
    "Simferopol",
    "Siversk",
    "Skadovsk",
    "Skalat",
    "Skole",
    "Skvyra",
    "Slavuta",
    "Slavutych",
    "Sloviansk",
    "Smila",
    "Sniatyn",
    "Snihurivka",
    "Snizhne",
    "Snovsk",
    "Sokal",
    "Sokyriany",
    "Soledar",
    "Sorokyne",
    "Sosnivka",
    "Starobilsk",
    "Starokostiantyniv",
    "StaryiKrym",
    "StaryiSambir",
    "Stebnyk",
    "Storozhynets",
    "Stryi",
    "Sudak",
    "SudovaVyshnia",
    "Sukhodilsk",
    "Sumy",
    "Svaliava",
    "Svarychiv",
    "Svatove",
    "Sviatohirsk",
    "Svitlodarsk",
    "Svitlovodsk",
    "Synelnykove",
    "Talne",
    "Tarascha",
    "Tatarbunary",
    "Tavriysk",
    "Teplodar",
    "Teplohirsk",
    "Terebovlia",
    "Ternivka",
    "Ternopil",
    "Tetiiv",
    "Tiachiv",
    "Tlumach",
    "Tokmak",
    "Toretsk",
    "Trostianets",
    "Truskavets",
    "Tulchyn",
    "Turka",
    "Tysmenytsia",
    "Uhniv",
    "Ukrainka",
    "Ukrainsk",
    "Uman",
    "Ustyluh",
    "Uzhhorod",
    "Uzyn",
    "Vakhrusheve",
    "Valky",
    "Varash",
    "Vashkivtsi",
    "Vasylivka",
    "Vasylkiv",
    "Vatutine",
    "VelykiMosty",
    "Verkhivtseve",
    "Verkhniodniprovsk",
    "Vilniansk",
    "Vilnohirsk",
    "Vinnytsia",
    "Volnovakha",
    "Volochysk",
    "Volodymyr-Volynskyi",
    "Vorozhba",
    "Vovchansk",
    "Voznesensk",
    "Vuhledar",
    "Vuhlehirsk",
    "Vylkove",
    "Vynnyky",
    "Vynohradiv",
    "Vyshhorod",
    "Vyshneve",
    "Vyzhnytsia",
    "Yahotyn",
    "Yalta",
    "Yampil",
    "Yaremcha",
    "Yasynuvata",
    "Yavoriv",
    "Yenakiieve",
    "Yevpatoria",
    "Yunokomunarivsk",
    "Yuzhne",
    "Yuzhnoukrainsk",
    "Zalischyky",
    "Zaporizhia",
    "Zastavna",
    "Zavodske",
    "Zbarazh",
    "Zboriv",
    "Zdolbuniv",
    "Zelenodolsk",
    "Zhashkiv",
    "Zhdanivka",
    "Zhmerynka",
    "Zhovkva",
    "ZhovtiVody",
    "Zhydachiv",
    "Zhytomyr",
    "Zinkiv",
    "Zmiiv",
    "Znamianka",
    "Zolochiv",
    "Zolote",
    "Zolotonosha",
    "Zorynsk",
    "Zuhres",
    "Zvenyhorodka",
    "Zymohiria"
];
