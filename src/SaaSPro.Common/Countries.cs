using System.Collections.Generic;
using System.Linq;

namespace SaaSPro.Common
{
    public class Country
    {
        // Variables    
        private List<Region> _regions;

        // Properties        
        public string CultureCode { get; set; }
        public string DisplayName { get; set; }
        public string IsoCode { get; set; }
        public string IsoAlpha3 { get; set; }
        public string IsoNumeric { get; set; }

        public IEnumerable<Region> Regions
        {
            get
            {
                if (_regions == null)
                {
                    LoadRegions();
                }
                return _regions;
            }
        }

        public string UsPostalServiceName { get; set; }

        public string PostalCodeValidationRegex { get; set; }

        public Country(string cultureCode, string displayName, string iso2, string iso3, string isoNumber, string postalcodeRegEx)
        {
            CultureCode = cultureCode;
            DisplayName = displayName;
            IsoCode = iso2;
            IsoAlpha3 = iso3;
            IsoNumeric = isoNumber;
            PostalCodeValidationRegex = postalcodeRegEx;
            UsPostalServiceName = DisplayName;
        }

        public Country(string cultureCode, string displayName, string iso2, string iso3, string isoNumber, string postalcodeRegEx, string usPostalServiceName)
        {
            CultureCode = cultureCode;
            DisplayName = displayName;
            IsoCode = iso2;
            IsoAlpha3 = iso3;
            IsoNumeric = isoNumber;
            PostalCodeValidationRegex = postalcodeRegEx;
            UsPostalServiceName = usPostalServiceName;
        }

        public static IEnumerable<Country> FindAll()
        {
            var result = new List<Country>
				{
					new Country("sq-AL", "Albania", "AL", "ALB", "008", "", "Albania"),
					new Country("ar-DZ", "Algeria", "DZ", "DZA", "012", "", "Algeria"),
					new Country("es-AR", "Argentina", "AR", "ARG", "032", "", "Argentina"),
					new Country("hy-AM", "Armenia", "AM", "ARM", "051", "", "Armenia"),
					new Country("en-AU", "Australia", "AU", "AUS", "036", "", "Australia"),
					new Country("de-AT", "Austria", "AT", "AUT", "040", "", "Austria"),
					new Country("az-AZ-Latn", "Azerbaijan", "AZ", "AZE", "031", "", "Azerbaijan"),
					new Country("ar-BH", "Bahrain", "BH", "BHR", "048", "", "Bahrain"),
					new Country("be-BY", "Belarus", "BY", "BLR", "112", "", "Belarus"),
					new Country("fr-BE", "Belgium", "BE", "BEL", "056", "", "Belgium"),
					new Country("en-BZ", "Belize", "BZ", "BLZ", "084", "", "Belize"),
					new Country("es-BO", "Bolivia", "BO", "BOL", "068", "", "Bolivia"),
					new Country("pt-BR", "Brazil", "BR", "BRA", "076", "", "Brazil"),
					new Country("ms-BN", "Brunei Darussalam", "BN", "BRN", "096", "", "Brunei Darussalam"),
					new Country("bg-BG", "Bulgaria", "BG", "BGR", "100", "", "Bulgaria"),
					new Country("en-CA", "Canada", "CA", "CAN", "124", "", "Canada"),
					new Country("en-CB", "Caribbean", "CB", "", "", ""),
					new Country("es-CL", "Chile", "CL", "CHL", "152", "", "Chile"),
					new Country("zh-CN", "China", "CN", "CHN", "156", "", "China"),
					new Country("es-CO", "Colombia", "CO", "COL", "170", "", "Colombia"),
					new Country("es-CR", "Costa Rica", "CR", "CRI", "188", "", "Costa Rica"),
					new Country("hr-HR", "Croatia", "HR", "HRV", "191", "", "Croatia"),
					new Country("cs-CZ", "Czech Republic", "CZ", "CZE", "203", "", "Czech Republic"),
					new Country("da-DK", "Denmark", "DK", "DNK", "208", "", "Denmark"),
					new Country("es-DO", "Dominican Republic", "DO", "DOM", "214", "", "Dominican Republic"),
					new Country("es-EC", "Ecuador", "EC", "ECU", "218", "", "Ecuador"),
					new Country("ar-EG", "Egypt", "EG", "EGY", "818", "", "Egypt"),
					new Country("es-SV", "El Salvador", "SV", "SLV", "222", "", "El Salvador"),
					new Country("et-EE", "Estonia", "EE", "EST", "233", "", "Estonia"),
					new Country("fo-FO", "Faroe Islands", "FO", "FRO", "234", "", "Faroe Islands"),
					new Country("fi-FI", "Finland", "FI", "FIN", "246", "", "Finland"),
					new Country("fr-FR", "France", "FR", "FRA", "250", "", "France"),
					new Country("ka-GE", "Georgia", "GE", "GEO", "268", "", "Georgia, Republic of"),
					new Country("de-DE", "Germany", "DE", "DEU", "276", "", "Germany"),
					new Country("el-GR", "Greece", "GR", "GRC", "300", "", "Greece"),
					new Country("es-GT", "Guatemala", "GT", "GTM", "320", "", "Guatemala"),
					new Country("es-HN", "Honduras", "HN", "HND", "340", "", "Honduras"),
					new Country("zh-HK", "Hong Kong", "HK", "HKG", "344", "", "Hong Kong"),
					new Country("hu-HU", "Hungary", "HU", "HUN", "348", "", "Hungary"),
					new Country("is-IS", "Iceland", "IS", "ISL", "352", "", "Iceland"),
					new Country("hi-IN", "India", "IN", "IND", "356", "", "India"),
					new Country("id-ID", "Indonesia", "ID", "IDN", "360", "", "Indonesia"),
					new Country("fa-IR", "Iran", "IR", "IRN", "364", "", "Iran"),
					new Country("ar-IQ", "Iraq", "IQ", "IRQ", "368", "", "Iraq"),
					new Country("en-IE", "Ireland", "IE", "IRL", "372", "", "Ireland"),
					new Country("he-IL", "Israel", "IL", "ISR", "376", "", "Israel"),
					new Country("it-IT", "Italy", "IT", "ITA", "380", "", "Italy"),
					new Country("en-JM", "Jamaica", "JM", "JAM", "388", "", "Jamaica"),
					new Country("ja-JP", "Japan", "JP", "JPN", "392", "", "Japan"),
					new Country("ar-JO", "Jordan", "JO", "JOR", "400", "", "Jordan"),
					new Country("kk-KZ", "Kazakhstan", "KZ", "KAZ", "398", "", "Kazakhstan"),
					new Country("sw-KE", "Kenya", "KE", "KEN", "404", "", "Kenya"),
					new Country("ar-KW", "Kuwait", "KW", "KWT", "414", "", "Kuwait"),
					new Country("lv-LV", "Latvia", "LV", "LVA", "428", "", "Latvia"),
					new Country("ar-LB", "Lebanon", "LB", "LBN", "422", "", "Lebanon"),
					new Country("ar-LY", "Libya", "LY", "LBY", "434", "", "Libya"),
					new Country("de-LI", "Liechtenstein", "LI", "LIE", "438", "", "Liechtenstein"),
					new Country("lt-LT", "Lithuania", "LT", "LTU", "440", "", "Lithuania"),
					new Country("fr-LU", "Luxembourg", "LU", "LUX", "442", "", "Luxembourg"),
					new Country("zh-MO", "Macau", "MO", "MAC", "446", "", "Macao"),
					new Country("mk-MK", "Macedonia", "MK", "MKD", "807", "", "Macedonia, Republic of"),
					new Country("ms-MY", "Malaysia", "MY", "MYS", "458", "", "Malaysia"),
					new Country("div-MV", "Maldives", "MV", "MDV", "462", "", "Maldives"),
					new Country("es-MX", "Mexico", "MX", "MEX", "484", "", "Mexico"),
					new Country("fr-MC", "Monaco", "MC", "MCO", "492", "", "Monaco (France)"),
					new Country("mn-MN", "Mongolia", "MN", "MNG", "496", "", "Mongolia"),
					new Country("ar-MA", "Morocco", "MA", "MAR", "504", "", "Morocco"),
					new Country("nl-NL", "Netherlands", "NL", "NLD", "528", "", "Netherlands"),
					new Country("en-NZ", "New Zealand", "NZ", "NZL", "554", "", "New Zealand"),
					new Country("es-NI", "Nicaragua", "NI", "NIC", "558", "", "Nicaragua"),
					new Country("en-US", "Niger", "NE", "NER", "562", "", "Niger"),
					new Country("ha-NG", "Nigeria", "NG", "NGA", "566", "", "Nigeria"),
					new Country("ko-KR", "North Korea", "KR", "KOR", "410", "", "North Korea (Korea, Democratic People’s Republic of)"),
					new Country("nb-NO", "Norway", "NO", "NOR", "578", "", "Norway"),
					new Country("ar-OM", "Oman", "OM", "OMN", "512", "", "Oman"),
					new Country("ur-PK", "Pakistan", "PK", "PAK", "586", "", "Pakistan"),
					new Country("es-PA", "Panama", "PA", "PAN", "591", "", "Panama"),
					new Country("es-PY", "Paraguay", "PY", "PRY", "600", "", "Paraguay"),
					new Country("es-PE", "Peru", "PE", "PER", "604", "", "Peru"),
					new Country("en-PH", "Philippines", "PH", "PHL", "608", "", "Philippines"),
					new Country("pl-PL", "Poland", "PL", "POL", "616", "", "Poland"),
					new Country("pt-PT", "Portugal", "PT", "PRT", "620", "", "Portugal"),
					new Country("es-PR", "Puerto Rico", "PR", "PRI", "630", ""),
					new Country("ar-QA", "Qatar", "QA", "QAT", "634", "", "Qatar"),
					new Country("ro-RO", "Romania", "RO", "ROU", "642", "", "Romania"),
					new Country("ru-RU", "Russia", "RU", "RUS", "643", "", "Russia"),
					new Country("ar-SA", "Saudi Arabia", "SA", "SAU", "682", "", "Saudi Arabia"),
					new Country("sr-SP-Latn", "Serbia", "RS", "SRB", "688", "", "Serbia, Republic of"),
					new Country("zh-SG", "Singapore", "SG", "SGP", "702", "", "Singapore"),
					new Country("sk-SK", "Slovakia", "SK", "SVK", "703", "", "Slovak Republic (Slovakia)"),
					new Country("sl-SI", "Slovenia", "SI", "SVN", "705", "", "Slovenia"),
					new Country("en-ZA", "South Africa", "ZA", "ZAF", "710", "", "South Africa"),
					new Country("ko-KR", "South Korea", "KR", "KOR", "410", "", "South Korea (Korea, Republic of)"),
					new Country("es-ES", "Spain", "ES", "ESP", "724", "", "Spain"),
					new Country("sv-SE", "Sweden", "SE", "SWE", "752", "", "Sweden"),
					new Country("fr-CH", "Switzerland", "CH", "CHE", "756", "", "Switzerland"),
					new Country("ar-SY", "Syrian Arab Republic", "SY", "SYR", "760", "", "Syrian Arab Republic (Syria)"),
					new Country("zh-TW", "Taiwan", "TW", "TWN", "158", "", "Taiwan"),
					new Country("th-TH", "Thailand", "TH", "THA", "764", "", "Thailand"),
					new Country("en-TT", "Trinidad and Tobago", "TT", "TTO", "780", "", "Trinidad and Tobago"),
					new Country("ar-TN", "Tunisia", "TN", "TUN", "788", "", "Tunisia"),
					new Country("tr-TR", "Turkey", "TR", "TUR", "792", "", "Turkey"),
					new Country("ar-AE", "U.A.E.", "AE", "ARE", "784", "", "United Arab Emirates"),
					new Country("uk-UA", "Ukraine", "UA", "UKR", "804", "", "Ukraine"),
					new Country("en-GB", "United Kingdom", "GB", "GBR", "826", "", "United Kingdom (Great Britain and Northern Ireland)"),
					new Country("en-US", "United States", "US", "USA", "840", "", "United States"),
					new Country("es-UY", "Uruguay", "UY", "URY", "858", "", "Uruguay"),
					new Country("uz-UZ-Latn", "Uzbekistan", "UZ", "UZB", "860", "", "Uzbekistan"),
					new Country("es-VE", "Venezuela", "VE", "VEN", "862", "", "Venezuela"),
					new Country("vi-VN", "Viet Nam", "VN", "VNM", "704", "", "Vietnam"),
					new Country("ar-YE", "Yemen", "YE", "YEM", "887", "", "Yemen"),
					new Country("en-ZW", "Zimbabwe", "ZW", "ZWE", "716", "", "Zimbabwe")
				};

            return result;
        }

        public static Country FindByIsoCode(string isoCode)
        {
            return FindAll().SingleOrDefault(y => y.IsoCode.Trim().ToLower() == isoCode.Trim().ToLower() || y.IsoAlpha3.Trim().ToLower() == isoCode.Trim().ToLower() || y.IsoNumeric.Trim().ToLower() == isoCode.Trim().ToLower());
        }

        private void LoadRegions()
        {
            _regions = new List<Region>();

            switch (IsoCode)
            {
                // United States
                case "US":
                    _regions.Add(new Region("Alabama", "AL"));
                    _regions.Add(new Region("Alaska", "AK"));
                    _regions.Add(new Region("Arizona", "AZ"));
                    _regions.Add(new Region("Arkansas", "AR"));
                    _regions.Add(new Region("Armed Forces Africa", "AE"));
                    _regions.Add(new Region("Armed Forces Americas", "AA"));
                    _regions.Add(new Region("Armed Forces Canada", "AE"));
                    _regions.Add(new Region("Armed Forces Europe", "AE"));
                    _regions.Add(new Region("Armed Forces Middle", "AE"));
                    _regions.Add(new Region("Armed Forces Pacific", "AP"));
                    _regions.Add(new Region("California", "CA"));
                    _regions.Add(new Region("Colorado", "CO"));
                    _regions.Add(new Region("Connecticut", "CT"));
                    _regions.Add(new Region("Delaware", "DE"));
                    _regions.Add(new Region("District Of Columbia", "DC"));
                    _regions.Add(new Region("Florida", "FL"));
                    _regions.Add(new Region("Georgia", "GA"));
                    _regions.Add(new Region("Hawaii", "HI"));
                    _regions.Add(new Region("Idaho", "ID"));
                    _regions.Add(new Region("Illinois", "IL"));
                    _regions.Add(new Region("Indiana", "IN"));
                    _regions.Add(new Region("Iowa", "IA"));
                    _regions.Add(new Region("Kansas", "KS"));
                    _regions.Add(new Region("Kentucky", "KY"));
                    _regions.Add(new Region("Louisiana", "LA"));
                    _regions.Add(new Region("Maine", "ME"));
                    _regions.Add(new Region("Maryland", "MD"));
                    _regions.Add(new Region("Massachusetts", "MA"));
                    _regions.Add(new Region("Michigan", "MI"));
                    _regions.Add(new Region("Minnesota", "MN"));
                    _regions.Add(new Region("Mississippi", "MS"));
                    _regions.Add(new Region("Missouri", "MO"));
                    _regions.Add(new Region("Montana", "MT"));
                    _regions.Add(new Region("Nebraska", "NE"));
                    _regions.Add(new Region("Nevada", "NV"));
                    _regions.Add(new Region("New Hampshire", "NH"));
                    _regions.Add(new Region("New Jersey", "NJ"));
                    _regions.Add(new Region("New Mexico", "NM"));
                    _regions.Add(new Region("New York", "NY"));
                    _regions.Add(new Region("North Carolina", "NC"));
                    _regions.Add(new Region("North Dakota", "ND"));
                    _regions.Add(new Region("Ohio", "OH"));
                    _regions.Add(new Region("Oklahoma", "OK"));
                    _regions.Add(new Region("Oregon", "OR"));
                    _regions.Add(new Region("Pennsylvania", "PA"));
                    _regions.Add(new Region("Rhode Island", "RI"));
                    _regions.Add(new Region("South Carolina", "SC"));
                    _regions.Add(new Region("South Dakota", "SD"));
                    _regions.Add(new Region("Tennessee", "TN"));
                    _regions.Add(new Region("Texas", "TX"));
                    _regions.Add(new Region("U.S. Virgin Islands", "VI"));
                    _regions.Add(new Region("Utah", "UT"));
                    _regions.Add(new Region("Vermont", "VT"));
                    _regions.Add(new Region("Virginia", "VA"));
                    _regions.Add(new Region("Washington", "WA"));
                    _regions.Add(new Region("West Virginia", "WV"));
                    _regions.Add(new Region("Wisconsin", "WI"));
                    _regions.Add(new Region("Wyoming", "WY"));
                    break;

                case "CA":
                    // CANADA
                    _regions.Add(new Region("Alberta", "AB"));
                    _regions.Add(new Region("British Columbia", "BC"));
                    _regions.Add(new Region("Manitoba", "MB"));
                    _regions.Add(new Region("New Brunswick", "NB"));
                    _regions.Add(new Region("Newfoundland", "NL"));
                    _regions.Add(new Region("Northwest Territories", "NT"));
                    _regions.Add(new Region("Nova Scotia", "NS"));
                    _regions.Add(new Region("Nunavut", "NU"));
                    _regions.Add(new Region("Ontario", "ON"));
                    _regions.Add(new Region("Prince Edward Island", "PE"));
                    _regions.Add(new Region("Quebec", "QC"));
                    _regions.Add(new Region("Saskatchewan", "SK"));
                    _regions.Add(new Region("Yukon Territory", "YT"));
                    break;
                case "AU":
                    // Australia
                    _regions.Add(new Region("Austrailian Capital Territory", "ACT"));
                    _regions.Add(new Region("New South Wales", "NSW"));
                    _regions.Add(new Region("Northern Territory", "NT"));
                    _regions.Add(new Region("Queensland", "QLD"));
                    _regions.Add(new Region("South Australia", "SA"));
                    _regions.Add(new Region("Tasmania", "TAS"));
                    _regions.Add(new Region("Victoria", "VIC"));
                    _regions.Add(new Region("Western Australia", "WA"));
                    _regions.Add(new Region("Cocos (Keeling) Islands", "CC"));
                    _regions.Add(new Region("Christmas Island", "CX"));
                    _regions.Add(new Region("Heard Island", "HM"));
                    _regions.Add(new Region("Norfolk Island", "NF"));
                    break;
                case "CN":
                    // China
                    _regions.Add(new Region("Beijing (北京)", "CN-11"));
                    _regions.Add(new Region("Chongqing (重庆)", "CN-50"));
                    _regions.Add(new Region("Shanghai (上海)", "CN-31"));
                    _regions.Add(new Region("Tianjin (天津)", "CN-12"));
                    _regions.Add(new Region("Anhui (安徽)", "CN-34"));
                    _regions.Add(new Region("Fujian (福建)", "CN-35"));
                    _regions.Add(new Region("Gansu (甘肃)", "CN-62"));
                    _regions.Add(new Region("Guangdong (广东)", "CN-44"));
                    _regions.Add(new Region("Guizhou (贵州)", "CN-52"));
                    _regions.Add(new Region("Hainan (海南)", "CN-46"));
                    _regions.Add(new Region("Hebei (河北)", "CN-13"));
                    _regions.Add(new Region("Heilongjiang (黑龙江)", "CN-23"));
                    _regions.Add(new Region("Henan (河南)", "CN-41"));
                    _regions.Add(new Region("Hubei (湖北)", "CN-42"));
                    _regions.Add(new Region("Hunan (湖南)", "CN-43"));
                    _regions.Add(new Region("Jiangsu (江苏)", "CN-32"));
                    _regions.Add(new Region("Jiangxi (江西)", "CN-36"));
                    _regions.Add(new Region("Jilin (吉林)", "CN-22"));
                    _regions.Add(new Region("Liaoning (吉林)", "CN-21"));
                    _regions.Add(new Region("Qinghai (青海)", "CN-63"));
                    _regions.Add(new Region("Shaanxi (陕西)", "CN-61"));
                    _regions.Add(new Region("Shandong (山东)", "CN-37"));
                    _regions.Add(new Region("Shanxi (山西)", "CN-14"));
                    _regions.Add(new Region("Sichuan (四川)", "CN-51"));
                    _regions.Add(new Region("Taiwan", "CN-71"));
                    _regions.Add(new Region("Yunnan (云南)", "CN-53"));
                    _regions.Add(new Region("Zhejiang (浙江)", "CN-33"));
                    _regions.Add(new Region("Guangxi (广西壮族)", "CN-45"));
                    _regions.Add(new Region("Nei Mongol (内蒙古)", "CN-15"));
                    _regions.Add(new Region("Ningxia (宁夏回族)", "CN-64"));
                    _regions.Add(new Region("Xinjiang (新疆维吾尔族)", "CN-65"));
                    _regions.Add(new Region("Xizang", "CN-54"));
                    _regions.Add(new Region("Hong Kong (香港)", "CN-91"));
                    _regions.Add(new Region("Macau (澳门)", "CN-92"));
                    break;
                case "FR":
                    // France
                    _regions.Add(new Region("Ain", "01"));
                    _regions.Add(new Region("Aisne", "02"));
                    _regions.Add(new Region("Allier", "03"));
                    _regions.Add(new Region("Alpes-de-Haute-Provence", "04"));
                    _regions.Add(new Region("Hautes-Alpes", "05"));
                    _regions.Add(new Region("Alpes-Maritimes", "06"));
                    _regions.Add(new Region("Ardèche", "07"));
                    _regions.Add(new Region("Ardennes", "08"));
                    _regions.Add(new Region("Ariège", "09"));
                    _regions.Add(new Region("Aube", "10"));
                    _regions.Add(new Region("Aude", "11"));
                    _regions.Add(new Region("Aveyron", "12"));
                    _regions.Add(new Region("Bouches-du-Rhône", "13"));
                    _regions.Add(new Region("Calvados", "14"));
                    _regions.Add(new Region("Cantal", "15"));
                    _regions.Add(new Region("Charente", "16"));
                    _regions.Add(new Region("Charente-Maritime", "17"));
                    _regions.Add(new Region("Cher", "18"));
                    _regions.Add(new Region("Corrèze", "19"));
                    _regions.Add(new Region("Corse-du-Sud", "2A"));
                    _regions.Add(new Region("Haute-Corse", "2B"));
                    _regions.Add(new Region("Côte-d'Or", "21"));
                    _regions.Add(new Region("Côtes-d'Armor", "22"));
                    _regions.Add(new Region("Creuse", "23"));
                    _regions.Add(new Region("Dordogne", "24"));
                    _regions.Add(new Region("Doubs", "25"));
                    _regions.Add(new Region("Drôme", "26"));
                    _regions.Add(new Region("Eure", "27"));
                    _regions.Add(new Region("Eure-et-Loir", "28"));
                    _regions.Add(new Region("Finistère", "29"));
                    _regions.Add(new Region("Gard", "30"));
                    _regions.Add(new Region("Haute-Garonne", "31"));
                    _regions.Add(new Region("Gers", "32"));
                    _regions.Add(new Region("Gironde", "33"));
                    _regions.Add(new Region("Hérault", "34"));
                    _regions.Add(new Region("Ille-et-Vilaine", "35"));
                    _regions.Add(new Region("Indre", "36"));
                    _regions.Add(new Region("Indre-et-Loire", "37"));
                    _regions.Add(new Region("Isère", "38"));
                    _regions.Add(new Region("Jura", "39"));
                    _regions.Add(new Region("Landes", "40"));
                    _regions.Add(new Region("Loir-et-Cher", "41"));
                    _regions.Add(new Region("Loire", "42"));
                    _regions.Add(new Region("Haute-Loire", "43"));
                    _regions.Add(new Region("Loire-Atlantique", "44"));
                    _regions.Add(new Region("Loiret", "45"));
                    _regions.Add(new Region("Lot", "46"));
                    _regions.Add(new Region("Lot-et-Garonne", "47"));
                    _regions.Add(new Region("Lozère", "48"));
                    _regions.Add(new Region("Maine-et-Loire", "49"));
                    _regions.Add(new Region("Manche", "50"));
                    _regions.Add(new Region("Marne", "51"));
                    _regions.Add(new Region("Haute-Marne", "52"));
                    _regions.Add(new Region("Mayenne", "53"));
                    _regions.Add(new Region("Meurthe-et-Moselle", "54"));
                    _regions.Add(new Region("Meuse", "55"));
                    _regions.Add(new Region("Morbihan", "56"));
                    _regions.Add(new Region("Moselle", "57"));
                    _regions.Add(new Region("Nièvre", "58"));
                    _regions.Add(new Region("Nord", "59"));
                    _regions.Add(new Region("Oise", "60"));
                    _regions.Add(new Region("Orne", "61"));
                    _regions.Add(new Region("Pas-de-Calais", "62"));
                    _regions.Add(new Region("Puy-de-Dôme", "63"));
                    _regions.Add(new Region("Pyrénées-Atlantiques", "64"));
                    _regions.Add(new Region("Hautes-Pyrénées", "65"));
                    _regions.Add(new Region("Pyrénées-Orientales", "66"));
                    _regions.Add(new Region("Bas-Rhin", "67"));
                    _regions.Add(new Region("Haut-Rhin", "68"));
                    _regions.Add(new Region("Rhône", "69"));
                    _regions.Add(new Region("Haute-Saône", "70"));
                    _regions.Add(new Region("Saône-et-Loire", "71"));
                    _regions.Add(new Region("Sarthe", "72"));
                    _regions.Add(new Region("Savoie", "73"));
                    _regions.Add(new Region("Haute-Savoie", "74"));
                    _regions.Add(new Region("Paris", "75"));
                    _regions.Add(new Region("Seine-Maritime", "76"));
                    _regions.Add(new Region("Seine-et-Marne", "77"));
                    _regions.Add(new Region("Yvelines", "78"));
                    _regions.Add(new Region("Deux-Sèvres", "79"));
                    _regions.Add(new Region("Somme", "80"));
                    _regions.Add(new Region("Tarn", "81"));
                    _regions.Add(new Region("Tarn-et-Garonne", "82"));
                    _regions.Add(new Region("Var", "83"));
                    _regions.Add(new Region("Vaucluse", "84"));
                    _regions.Add(new Region("Vendée", "85"));
                    _regions.Add(new Region("Vienne", "86"));
                    _regions.Add(new Region("Haute-Vienne", "87"));
                    _regions.Add(new Region("Vosges", "88"));
                    _regions.Add(new Region("Yonne", "89"));
                    _regions.Add(new Region("Territoire de Belfort", "90"));
                    _regions.Add(new Region("Essonne", "91"));
                    _regions.Add(new Region("Hauts-de-Seine", "92"));
                    _regions.Add(new Region("Seine-Saint-Denis", "93"));
                    _regions.Add(new Region("Val-de-Marne", "94"));
                    _regions.Add(new Region("Val-d'Oise", "95"));
                    _regions.Add(new Region("Clipperton Island", "CP"));
                    _regions.Add(new Region("Saint Barthélemy", "BL"));
                    _regions.Add(new Region("Saint Martin (French part)", "MF"));
                    _regions.Add(new Region("New Caledonia", "NC"));
                    _regions.Add(new Region("French Polynesia", "PF"));
                    _regions.Add(new Region("Saint-Pierre and Miquelon", "PM"));
                    _regions.Add(new Region("French Southern Territories", "TF"));
                    _regions.Add(new Region("Wallis and Futuna", "WF"));
                    _regions.Add(new Region("Mayotte", "YT"));
                    break;
                case "IL":
                    // Israel
                    _regions.Add(new Region("Center District מחוז המרכז ", "M"));
                    _regions.Add(new Region("Haifa District מחוז חיפה ", "HA"));
                    _regions.Add(new Region("Jerusalem District מחוז ירושלים ", "JM"));
                    _regions.Add(new Region("North District מחוז הצפון ", "Z"));
                    _regions.Add(new Region("South District מחוז דרום ", "D"));
                    _regions.Add(new Region("Tel Aviv", "TA"));
                    break;
                case "JP":
                    // Japan
                    _regions.Add(new Region("Aiti (Aichi) (愛知)", "23"));
                    _regions.Add(new Region("Akita (秋田)", "05"));
                    _regions.Add(new Region("Aomori (青森)", "02"));
                    _regions.Add(new Region("Ehime (愛媛)", "38"));
                    _regions.Add(new Region("Gihu (Gifu) (岐阜)", "21"));
                    _regions.Add(new Region("Gunma (群馬)", "10"));
                    _regions.Add(new Region("Hirosima (Hiroshima) (広島)", "34"));
                    _regions.Add(new Region("Hokkaidô (Hokkaidō) (北海道)", "01"));
                    _regions.Add(new Region("Hukui (Fukui) (福井)", "18"));
                    _regions.Add(new Region("Hukuoka (Fukuoka) (福岡)", "40"));
                    _regions.Add(new Region("Hukusima (Fukushima) (福島)", "07"));
                    _regions.Add(new Region("Hyôgo (Hyōgo) (兵庫)", "28"));
                    _regions.Add(new Region("Ibaraki (茨城)", "08"));
                    _regions.Add(new Region("Isikawa (Ishikawa) (石川)", "17"));
                    _regions.Add(new Region("Iwate (岩手)", "03"));
                    _regions.Add(new Region("Kagawa (香川)", "37"));
                    _regions.Add(new Region("Kagosima (Kagoshima) (鹿児島)", "46"));
                    _regions.Add(new Region("Kanagawa (神奈川)", "14"));
                    _regions.Add(new Region("Kumamoto (熊本)", "43"));
                    _regions.Add(new Region("Kyôto (Kyōto) (京都)", "26"));
                    _regions.Add(new Region("Kôti (Kōchi) (高知)", "39"));
                    _regions.Add(new Region("Mie (三重)", "24"));
                    _regions.Add(new Region("Miyagi (宮城)", "04"));
                    _regions.Add(new Region("Miyazaki (宮崎)", "45"));
                    _regions.Add(new Region("Nagano (長野)", "20"));
                    _regions.Add(new Region("Nagasaki (長崎)", "42"));
                    _regions.Add(new Region("Nara (奈良)", "29"));
                    _regions.Add(new Region("Niigata (新潟)", "15"));
                    _regions.Add(new Region("Okayama (岡山)", "33"));
                    _regions.Add(new Region("Okinawa (沖縄)", "47"));
                    _regions.Add(new Region("Saga (佐賀)", "41"));
                    _regions.Add(new Region("Saitama (埼玉)", "11"));
                    _regions.Add(new Region("Siga (Shiga) (滋賀)", "25"));
                    _regions.Add(new Region("Simane (Shimane) (島根)", "32"));
                    _regions.Add(new Region("Sizuoka (Shizuoka) (静岡)", "22"));
                    _regions.Add(new Region("Tiba (Chiba) (千葉)", "12"));
                    _regions.Add(new Region("Tokusima (Tokushima) (徳島)", "36"));
                    _regions.Add(new Region("Totigi (Tochigi) (栃木)", "09"));
                    _regions.Add(new Region("Tottori (鳥取)", "31"));
                    _regions.Add(new Region("Toyama (富山)", "16"));
                    _regions.Add(new Region("Tôkyô (Tōkyō) (東京)", "13"));
                    _regions.Add(new Region("Wakayama (和歌山)", "30"));
                    _regions.Add(new Region("Yamagata (山形)", "06"));
                    _regions.Add(new Region("Yamaguti (Yamaguchi) (山口)", "35"));
                    _regions.Add(new Region("Yamanasi (Yamanashi) (山梨)", "19"));
                    _regions.Add(new Region("Ôita (Ōita) (大分)", "44"));
                    _regions.Add(new Region("Ôsaka (Ōsaka) (大阪)", "27"));
                    break;
                case "MX":
                    // Mexico
                    _regions.Add(new Region("Aguascalientes", "AGU"));
                    _regions.Add(new Region("Baja California", "BCN"));
                    _regions.Add(new Region("Baja California Sur", "BCS"));
                    _regions.Add(new Region("Campeche", "CAM"));
                    _regions.Add(new Region("Chiapas", "CHP"));
                    _regions.Add(new Region("Chihuahua", "CHH"));
                    _regions.Add(new Region("Coahuila", "COA"));
                    _regions.Add(new Region("Colima", "COL"));
                    _regions.Add(new Region("Federal District", "DIF"));
                    _regions.Add(new Region("Durango", "DUR"));
                    _regions.Add(new Region("Guanajuato", "GUA"));
                    _regions.Add(new Region("Guerrero", "GRO"));
                    _regions.Add(new Region("Hidalgo", "HID"));
                    _regions.Add(new Region("Jalisco", "JAL"));
                    _regions.Add(new Region("Mexico State", "MEX"));
                    _regions.Add(new Region("Michoacán", "MIC"));
                    _regions.Add(new Region("Morelos", "MOR"));
                    _regions.Add(new Region("Nayarit", "NAY"));
                    _regions.Add(new Region("Nuevo León", "NLE"));
                    _regions.Add(new Region("Oaxaca", "OAX"));
                    _regions.Add(new Region("Puebla", "PUE"));
                    _regions.Add(new Region("Querétaro", "QUE"));
                    _regions.Add(new Region("Quintana Roo", "ROO"));
                    _regions.Add(new Region("San Luis Potosí", "SLP"));
                    _regions.Add(new Region("Sinaloa", "SIN"));
                    _regions.Add(new Region("Sonora", "SON"));
                    _regions.Add(new Region("Tabasco", "TAB"));
                    _regions.Add(new Region("Tamaulipas", "TAM"));
                    _regions.Add(new Region("Tlaxcala", "TLA"));
                    _regions.Add(new Region("Veracruz", "VER"));
                    _regions.Add(new Region("Yucatán", "YUC"));
                    _regions.Add(new Region("Zacatecas", "ZAC"));
                    break;
                case "ES":
                    // Spain
                    _regions.Add(new Region("Andalucía", "AN"));
                    _regions.Add(new Region("Aragón", "AR"));
                    _regions.Add(new Region("Asturias", "O"));
                    _regions.Add(new Region("Balearic Islands", "IB"));
                    _regions.Add(new Region("Basque Country", "PV"));
                    _regions.Add(new Region("Canary Islands", "CN"));
                    _regions.Add(new Region("Cantabria", "S"));
                    _regions.Add(new Region("Castilla-La Mancha", "CM"));
                    _regions.Add(new Region("Castilla y León", "CL"));
                    _regions.Add(new Region("Catalonia", "CT"));
                    _regions.Add(new Region("Extremadura", "EX"));
                    _regions.Add(new Region("Galicia", "GA"));
                    _regions.Add(new Region("La Rioja", "LO"));
                    _regions.Add(new Region("Madrid", "M"));
                    _regions.Add(new Region("Murcia", "MU"));
                    _regions.Add(new Region("Navarre", "NA"));
                    _regions.Add(new Region("Valencia", "VC"));
                    _regions.Add(new Region("A Coruña", "C"));
                    _regions.Add(new Region("Álava", "VI"));
                    _regions.Add(new Region("Albacete", "AB"));
                    _regions.Add(new Region("Alicante", "A"));
                    _regions.Add(new Region("Almería", "AL"));
                    _regions.Add(new Region("Asturias", "O"));
                    _regions.Add(new Region("Ávila", "AV"));
                    _regions.Add(new Region("Badajoz", "BA"));
                    _regions.Add(new Region("Baleares", "PM"));
                    _regions.Add(new Region("Barcelona", "B"));
                    _regions.Add(new Region("Biscay", "BI"));
                    _regions.Add(new Region("Burgos", "BU"));
                    _regions.Add(new Region("Cáceres", "CC"));
                    _regions.Add(new Region("Cádiz", "CA"));
                    _regions.Add(new Region("Cantabria", "S"));
                    _regions.Add(new Region("Castellón", "CS"));
                    _regions.Add(new Region("Ciudad Real", "CR"));
                    _regions.Add(new Region("Córdoba", "CO"));
                    _regions.Add(new Region("Cuenca", "CU"));
                    _regions.Add(new Region("Girona", "GI"));
                    _regions.Add(new Region("Granada", "GR"));
                    _regions.Add(new Region("Guadalajara", "GU"));
                    _regions.Add(new Region("Guipúzcoa", "SS"));
                    _regions.Add(new Region("Huelva", "H"));
                    _regions.Add(new Region("Huesca", "HU"));
                    _regions.Add(new Region("Jaén", "J"));
                    _regions.Add(new Region("La Rioja", "LO"));
                    _regions.Add(new Region("Las Palmas", "GC"));
                    _regions.Add(new Region("León", "LE"));
                    _regions.Add(new Region("Lleida", "L"));
                    _regions.Add(new Region("Lugo", "LU"));
                    _regions.Add(new Region("Madrid", "M"));
                    _regions.Add(new Region("Málaga", "MA"));
                    _regions.Add(new Region("Murcia", "MU"));
                    _regions.Add(new Region("Navarre", "NA"));
                    _regions.Add(new Region("Ourense", "OR"));
                    _regions.Add(new Region("Palencia", "P"));
                    _regions.Add(new Region("Pontevedra", "PO"));
                    _regions.Add(new Region("Salamanca", "SA"));
                    _regions.Add(new Region("Santa Cruz De Tenerife", "TF"));
                    _regions.Add(new Region("Segovia", "SG"));
                    _regions.Add(new Region("Seville", "SE"));
                    _regions.Add(new Region("Soria", "SO"));
                    _regions.Add(new Region("Tarragona", "T"));
                    _regions.Add(new Region("Teruel", "TE"));
                    _regions.Add(new Region("Toledo", "TO"));
                    _regions.Add(new Region("Valencia", "V"));
                    _regions.Add(new Region("Valladolid", "VA"));
                    _regions.Add(new Region("Zamora", "ZA"));
                    _regions.Add(new Region("Zaragoza", "Z"));
                    _regions.Add(new Region("Ceuta", "CE"));
                    _regions.Add(new Region("Melilla", "ML"));
                    break;
                case "GB":
                    // United Kingdom
                    _regions.Add(new Region("Aberdeenshire", "ABD"));
                    _regions.Add(new Region("Aberdeen", "ABE"));
                    _regions.Add(new Region("Argyll and Bute", "AGB"));
                    _regions.Add(new Region("Isle of Anglesey", "AGY"));
                    _regions.Add(new Region("Angus", "ANS"));
                    _regions.Add(new Region("Antrim", "ANT"));
                    _regions.Add(new Region("Ards", "ARD"));
                    _regions.Add(new Region("Armagh", "ARM"));
                    _regions.Add(new Region("Bath and North East Somerset", "BAS"));
                    _regions.Add(new Region("Blackburn with Darwen", "BBD"));
                    _regions.Add(new Region("Bedfordshire", "BDF"));
                    _regions.Add(new Region("Barking and Dagenham", "BDG"));
                    _regions.Add(new Region("Brent", "BEN"));
                    _regions.Add(new Region("Bexley", "BEX"));
                    _regions.Add(new Region("Belfast", "BFS"));
                    _regions.Add(new Region("Bridgend", "BGE"));
                    _regions.Add(new Region("Blaenau Gwent", "BGW"));
                    _regions.Add(new Region("Birmingham", "BIR"));
                    _regions.Add(new Region("Buckinghamshire", "BKM"));
                    _regions.Add(new Region("Ballymena", "BLA"));
                    _regions.Add(new Region("Ballymoney", "BLY"));
                    _regions.Add(new Region("Bournemouth", "BMH"));
                    _regions.Add(new Region("Banbridge", "BNB"));
                    _regions.Add(new Region("Barnet", "BNE"));
                    _regions.Add(new Region("Brighton and Hove", "BNH"));
                    _regions.Add(new Region("Barnsley", "BNS"));
                    _regions.Add(new Region("Bolton", "BOL"));
                    _regions.Add(new Region("Blackpool", "BPL"));
                    _regions.Add(new Region("Bracknell Forest", "BRC"));
                    _regions.Add(new Region("Bradford", "BRD"));
                    _regions.Add(new Region("Bromley", "BRY"));
                    _regions.Add(new Region("Bristol", "BST"));
                    _regions.Add(new Region("Bury", "BUR"));
                    _regions.Add(new Region("Cambridgeshire", "CAM"));
                    _regions.Add(new Region("Caerphilly", "CAY"));
                    _regions.Add(new Region("Ceredigion", "CGN"));
                    _regions.Add(new Region("Craigavon", "CGV"));
                    _regions.Add(new Region("Cheshire", "CHS"));
                    _regions.Add(new Region("Carrickfergus", "CKF"));
                    _regions.Add(new Region("Cookstown", "CKT"));
                    _regions.Add(new Region("Calderdale", "CLD"));
                    _regions.Add(new Region("Clackmannanshire", "CLK"));
                    _regions.Add(new Region("Coleraine", "CLR"));
                    _regions.Add(new Region("Cumbria", "CMA"));
                    _regions.Add(new Region("Camden", "CMD"));
                    _regions.Add(new Region("Carmarthenshire", "CMN"));
                    _regions.Add(new Region("Cornwall", "CON"));
                    _regions.Add(new Region("Coventry", "COV"));
                    _regions.Add(new Region("Cardiff", "CRF"));
                    _regions.Add(new Region("Croydon", "CRY"));
                    _regions.Add(new Region("Castlereagh", "CSR"));
                    _regions.Add(new Region("Conwy", "CWY"));
                    _regions.Add(new Region("Darlington", "DAL"));
                    _regions.Add(new Region("Derbyshire", "DBY"));
                    _regions.Add(new Region("Denbighshire", "DEN"));
                    _regions.Add(new Region("Derby", "DER"));
                    _regions.Add(new Region("Devon", "DEV"));
                    _regions.Add(new Region("Dungannon and South Tyrone", "DGN"));
                    _regions.Add(new Region("Dumfries and Galloway", "DGY"));
                    _regions.Add(new Region("Doncaster", "DNC"));
                    _regions.Add(new Region("Dundee", "DND"));
                    _regions.Add(new Region("Dorset", "DOR"));
                    _regions.Add(new Region("Down", "DOW"));
                    _regions.Add(new Region("Derry", "DRY"));
                    _regions.Add(new Region("Dudley", "DUD"));
                    _regions.Add(new Region("Durham", "DUR"));
                    _regions.Add(new Region("Ealing", "EAL"));
                    _regions.Add(new Region("East Ayrshire", "EAY"));
                    _regions.Add(new Region("Edinburgh", "EDH"));
                    _regions.Add(new Region("East Dunbartonshire", "EDU"));
                    _regions.Add(new Region("East Lothian", "ELN"));
                    _regions.Add(new Region("Eilean Siar", "ELS"));
                    _regions.Add(new Region("Enfield", "ENF"));
                    _regions.Add(new Region("East Renfrewshire", "ERW"));
                    _regions.Add(new Region("East Riding of Yorkshire", "ERY"));
                    _regions.Add(new Region("Essex", "ESS"));
                    _regions.Add(new Region("East Sussex", "ESX"));
                    _regions.Add(new Region("Falkirk", "FAL"));
                    _regions.Add(new Region("Fermanagh", "FER"));
                    _regions.Add(new Region("Fife", "FIF"));
                    _regions.Add(new Region("Flintshire", "FLN"));
                    _regions.Add(new Region("Gateshead", "GAT"));
                    _regions.Add(new Region("Glasgow", "GLG"));
                    _regions.Add(new Region("Gloucestershire", "GLS"));
                    _regions.Add(new Region("Greenwich", "GRE"));
                    _regions.Add(new Region("Guernsey", "GSY"));
                    _regions.Add(new Region("Gwynedd", "GWN"));
                    _regions.Add(new Region("Halton", "HAL"));
                    _regions.Add(new Region("Hampshire", "HAM"));
                    _regions.Add(new Region("Havering", "HAV"));
                    _regions.Add(new Region("Hackney", "HCK "));
                    _regions.Add(new Region("Herefordshire County", "HEF"));
                    _regions.Add(new Region("Hillingdon", "HIL"));
                    _regions.Add(new Region("Highland", "HLD"));
                    _regions.Add(new Region("Hammersmith and Fulham", "HMF"));
                    _regions.Add(new Region("Hounslow", "HNS"));
                    _regions.Add(new Region("Hartlepool", "HPL"));
                    _regions.Add(new Region("Hertfordshire", "HRT"));
                    _regions.Add(new Region("Harrow", "HRW"));
                    _regions.Add(new Region("Haringey", "HRY"));
                    _regions.Add(new Region("Isles of Scilly", "IOS"));
                    _regions.Add(new Region("Isle of Wight", "IOW"));
                    _regions.Add(new Region("Islington", "ISL"));
                    _regions.Add(new Region("Inverclyde", "IVC"));
                    _regions.Add(new Region("Jersey", "JSY"));
                    _regions.Add(new Region("Kensington and Chelsea", "KEC"));
                    _regions.Add(new Region("Kent", "KEN"));
                    _regions.Add(new Region("Kingston upon Hull", "KHL"));
                    _regions.Add(new Region("Kirklees", "KIR"));
                    _regions.Add(new Region("Kingston upon Thames", "KTT"));
                    _regions.Add(new Region("Knowsley", "KWL"));
                    _regions.Add(new Region("Lancashire", "LAN"));
                    _regions.Add(new Region("Lambeth", "LBH"));
                    _regions.Add(new Region("Leicester", "LCE"));
                    _regions.Add(new Region("Leeds", "LDS"));
                    _regions.Add(new Region("Leicestershire", "LEC"));
                    _regions.Add(new Region("Lewisham", "LEW"));
                    _regions.Add(new Region("Lincolnshire", "LIN"));
                    _regions.Add(new Region("Liverpool", "LIV"));
                    _regions.Add(new Region("Limavady", "LMV"));
                    _regions.Add(new Region("London", "LND"));
                    _regions.Add(new Region("Larne", "LRN"));
                    _regions.Add(new Region("Lisburn", "LSB"));
                    _regions.Add(new Region("Luton", "LUT"));
                    _regions.Add(new Region("Manchester", "MAN"));
                    _regions.Add(new Region("Middlesbrough", "MDB"));
                    _regions.Add(new Region("Medway", "MDW"));
                    _regions.Add(new Region("Magherafelt", "MFT"));
                    _regions.Add(new Region("Milton Keynes", "MIK"));
                    _regions.Add(new Region("Midlothian", "MLN"));
                    _regions.Add(new Region("Monmouthshire", "MON"));
                    _regions.Add(new Region("Merton", "MRT"));
                    _regions.Add(new Region("Moray", "MRY"));
                    _regions.Add(new Region("Merthyr Tydfil", "MTY"));
                    _regions.Add(new Region("Moyle", "MYL"));
                    _regions.Add(new Region("North Ayrshire", "NAY"));
                    _regions.Add(new Region("Northumberland", "NBL"));
                    _regions.Add(new Region("North Down", "NDN"));
                    _regions.Add(new Region("North East Lincolnshire", "NEL"));
                    _regions.Add(new Region("Newcastle upon Tyne", "NET"));
                    _regions.Add(new Region("Norfolk", "NFK"));
                    _regions.Add(new Region("Nottingham", "NGM"));
                    _regions.Add(new Region("North Lanarkshire", "NLK"));
                    _regions.Add(new Region("North Lincolnshire", "NLN"));
                    _regions.Add(new Region("North Somerset", "NSM"));
                    _regions.Add(new Region("Newtownabbey", "NTA"));
                    _regions.Add(new Region("Northamptonshire", "NTH"));
                    _regions.Add(new Region("Neath Port Talbot", "NTL"));
                    _regions.Add(new Region("Nottinghamshire", "NTT"));
                    _regions.Add(new Region("North Tyneside", "NTY"));
                    _regions.Add(new Region("Newham", "NWM"));
                    _regions.Add(new Region("Newport", "NWP"));
                    _regions.Add(new Region("North Yorkshire", "NYK"));
                    _regions.Add(new Region("Newry and Mourne", "NYM"));
                    _regions.Add(new Region("Oldham", "OLD"));
                    _regions.Add(new Region("Omagh", "OMH"));
                    _regions.Add(new Region("Orkney Islands", "ORK"));
                    _regions.Add(new Region("Oxfordshire", "OXF"));
                    _regions.Add(new Region("Pembrokeshire", "PEM"));
                    _regions.Add(new Region("Perth and Kinross", "PKN"));
                    _regions.Add(new Region("Plymouth", "PLY"));
                    _regions.Add(new Region("Poole", "POL"));
                    _regions.Add(new Region("Portsmouth", "POR"));
                    _regions.Add(new Region("Powys", "POW"));
                    _regions.Add(new Region("Peterborough", "PTE"));
                    _regions.Add(new Region("Redcar and Cleveland", "RCC"));
                    _regions.Add(new Region("Rochdale", "RCH"));
                    _regions.Add(new Region("Rhondda Cynon Taff", "RCT"));
                    _regions.Add(new Region("Redbridge", "RDB"));
                    _regions.Add(new Region("Reading", "RDG"));
                    _regions.Add(new Region("Renfrewshire", "RFW"));
                    _regions.Add(new Region("Richmond upon Thames", "RIC"));
                    _regions.Add(new Region("Rotherham", "ROT"));
                    _regions.Add(new Region("Rutland", "RUT"));
                    _regions.Add(new Region("Sandwell", "SAW"));
                    _regions.Add(new Region("South Ayrshire", "SAY"));
                    _regions.Add(new Region("Scottish Borders, The", "SCB"));
                    _regions.Add(new Region("Suffolk", "SFK"));
                    _regions.Add(new Region("Sefton", "SFT"));
                    _regions.Add(new Region("South Gloucestershire", "SGC"));
                    _regions.Add(new Region("Sheffield", "SHF"));
                    _regions.Add(new Region("St Helens", "SHN"));
                    _regions.Add(new Region("Shropshire", "SHR"));
                    _regions.Add(new Region("Stockport", "SKP"));
                    _regions.Add(new Region("Salford", "SLF"));
                    _regions.Add(new Region("Slough", "SLG"));
                    _regions.Add(new Region("South Lanarkshire", "SLK"));
                    _regions.Add(new Region("Sunderland", "SND"));
                    _regions.Add(new Region("Solihull", "SOL"));
                    _regions.Add(new Region("Somerset", "SOM"));
                    _regions.Add(new Region("Southend-on-Sea", "SOS"));
                    _regions.Add(new Region("Surrey", "SRY"));
                    _regions.Add(new Region("Strabane", "STB"));
                    _regions.Add(new Region("Stoke-on-Trent", "STE"));
                    _regions.Add(new Region("Stirling", "STG"));
                    _regions.Add(new Region("Southampton", "STH"));
                    _regions.Add(new Region("Sutton", "STN"));
                    _regions.Add(new Region("Staffordshire", "STS"));
                    _regions.Add(new Region("Stockton-on-Tees", "STT"));
                    _regions.Add(new Region("South Tyneside", "STY"));
                    _regions.Add(new Region("Swansea", "SWA"));
                    _regions.Add(new Region("Swindon", "SWD"));
                    _regions.Add(new Region("Southwark", "SWK"));
                    _regions.Add(new Region("Tameside", "TAM"));
                    _regions.Add(new Region("Telford and Wrekin", "TFW"));
                    _regions.Add(new Region("Thurrock", "THR"));
                    _regions.Add(new Region("Torbay", "TOB"));
                    _regions.Add(new Region("Torfaen", "TOF"));
                    _regions.Add(new Region("Trafford", "TRF"));
                    _regions.Add(new Region("Tower Hamlets", "TWH"));
                    _regions.Add(new Region("Vale of Glamorgan", "VGL"));
                    _regions.Add(new Region("Warwickshire", "WAR"));
                    _regions.Add(new Region("West Berkshire", "WBK"));
                    _regions.Add(new Region("West Dunbartonshire", "WDU"));
                    _regions.Add(new Region("Waltham Forest", "WFT"));
                    _regions.Add(new Region("Wigan", "WGN"));
                    _regions.Add(new Region("Wiltshire", "WIL"));
                    _regions.Add(new Region("Wakefield", "WKF"));
                    _regions.Add(new Region("Walsall", "WLL"));
                    _regions.Add(new Region("West Lothian", "WLN"));
                    _regions.Add(new Region("Wolverhampton", "WLV"));
                    _regions.Add(new Region("Wandsworth", "WND"));
                    _regions.Add(new Region("Windsor and Maidenhead", "WNM"));
                    _regions.Add(new Region("Wokingham", "WOK"));
                    _regions.Add(new Region("Worcestershire", "WOR"));
                    _regions.Add(new Region("Wirral", "WRL"));
                    _regions.Add(new Region("Warrington", "WRT"));
                    _regions.Add(new Region("Wrexham", "WRX"));
                    _regions.Add(new Region("Westminster", "WSM"));
                    _regions.Add(new Region("West Sussex", "WSX"));
                    _regions.Add(new Region("York", "YOR"));
                    _regions.Add(new Region("Shetland Islands", "ZET"));
                    break;
                case "AL":
                    // Albania
                    _regions.Add(new Region("Berat", "BR"));
                    _regions.Add(new Region("Diber", "DI"));
                    _regions.Add(new Region("Durres", "DR"));
                    _regions.Add(new Region("Elbasan", "EL"));
                    _regions.Add(new Region("Fier", "FR"));
                    _regions.Add(new Region("Gjirokaster", "GJ"));
                    _regions.Add(new Region("Gramsh", "GR"));
                    _regions.Add(new Region("Kolonje", "ER"));
                    _regions.Add(new Region("Korce", "KO"));
                    _regions.Add(new Region("Kruje", "KR"));
                    _regions.Add(new Region("Kukes", "KU"));
                    _regions.Add(new Region("Lezhe", "LE"));
                    _regions.Add(new Region("Librazhd", "LB"));
                    _regions.Add(new Region("Lushnje", "LU"));
                    _regions.Add(new Region("Mat", "MT"));
                    _regions.Add(new Region("Mirdite", "MR"));
                    _regions.Add(new Region("Permet", "PR"));
                    _regions.Add(new Region("Pogradec", "PG"));
                    _regions.Add(new Region("Puke", "PU"));
                    _regions.Add(new Region("Sarande", "SR"));
                    _regions.Add(new Region("Shkoder", "SH"));
                    _regions.Add(new Region("Skrapar", "SK"));
                    _regions.Add(new Region("Tepelene", "TE"));
                    _regions.Add(new Region("Tropoje", "TP"));
                    _regions.Add(new Region("Vlore", "VL"));
                    _regions.Add(new Region("Tiran", "TI"));
                    _regions.Add(new Region("Bulqize", "BU"));
                    _regions.Add(new Region("Delvine", "DL"));
                    _regions.Add(new Region("Devoll", "DV"));
                    _regions.Add(new Region("Has", "HA"));
                    _regions.Add(new Region("Kavaje", "KA"));
                    _regions.Add(new Region("Kucove", "KC"));
                    _regions.Add(new Region("Kurbin", "KB"));
                    _regions.Add(new Region("Malesi e Madhe", "MM"));
                    _regions.Add(new Region("Mallakaster", "MK"));
                    _regions.Add(new Region("Peqin", "PQ"));
                    _regions.Add(new Region("Tirane", "TR"));

                    break;
                case "DZ":
                    // Algeria
                    _regions.Add(new Region("Alger", "AL"));
                    _regions.Add(new Region("Batna", "BT"));
                    _regions.Add(new Region("Constantine", "CO"));
                    _regions.Add(new Region("Medea", "MD"));
                    _regions.Add(new Region("Mostaganem", "MG"));
                    _regions.Add(new Region("Oran", "OR"));
                    _regions.Add(new Region("Saida", "SD"));
                    _regions.Add(new Region("Setif", "SF"));
                    _regions.Add(new Region("Tiaret", "TR"));
                    _regions.Add(new Region("Tizi Ouzou", "TO"));
                    _regions.Add(new Region("Tlemcen", "TL"));
                    _regions.Add(new Region("Bejaia", "BJ"));
                    _regions.Add(new Region("Biskra", "BS"));
                    _regions.Add(new Region("Blida", "BL"));
                    _regions.Add(new Region("Bouira", "BU"));
                    _regions.Add(new Region("Djelfa", "DJ"));
                    _regions.Add(new Region("Guelma", "GL"));
                    _regions.Add(new Region("Jijel", "JJ"));
                    _regions.Add(new Region("Laghouat", "LG"));
                    _regions.Add(new Region("Mascara", "MC"));
                    _regions.Add(new Region("M'Sila", "MS"));
                    _regions.Add(new Region("Oum el Bouaghi", "OB"));
                    _regions.Add(new Region("Sidi Bel Abbes", "SB"));
                    _regions.Add(new Region("Skikda", "SK"));
                    _regions.Add(new Region("Tebessa", "TB"));
                    _regions.Add(new Region("Adrar", "AR"));
                    _regions.Add(new Region("Ain Defla", "AD"));
                    _regions.Add(new Region("Ain Temouchent", "AT"));
                    _regions.Add(new Region("Annaba", "AN"));
                    _regions.Add(new Region("Bechar", "BC"));
                    _regions.Add(new Region("Bordj Bou Arreridj", "BB"));
                    _regions.Add(new Region("Boumerdes", "BM"));
                    _regions.Add(new Region("Chlef", "CH"));
                    _regions.Add(new Region("El Bayadh", "EB"));
                    _regions.Add(new Region("El Oued", "EO"));
                    _regions.Add(new Region("El Tarf", "ET"));
                    _regions.Add(new Region("Ghardaia", "GR"));
                    _regions.Add(new Region("Illizi", "IL"));
                    _regions.Add(new Region("Khenchela", "KH"));
                    _regions.Add(new Region("Mila", "ML"));
                    _regions.Add(new Region("Naama", "NA"));
                    _regions.Add(new Region("Ouargla", "OG"));
                    _regions.Add(new Region("Relizane", "RE"));
                    _regions.Add(new Region("Souk Ahras", "SA"));
                    _regions.Add(new Region("Tamanghasset", "TM"));
                    _regions.Add(new Region("Tindouf", "TN"));
                    _regions.Add(new Region("Tipaza", "TP"));
                    _regions.Add(new Region("Tissemsilt", "TS"));

                    break;
                case "AR": // Argentia
                    _regions.Add(new Region("Buenos Aires", "BA"));
                    _regions.Add(new Region("Catamarca", "CT"));
                    _regions.Add(new Region("Chaco", "CC"));
                    _regions.Add(new Region("Chubut", "CH"));
                    _regions.Add(new Region("Cordoba", "CB"));
                    _regions.Add(new Region("Corrientes", "CN"));
                    _regions.Add(new Region("Distrito Federal", "DF"));
                    _regions.Add(new Region("Entre Rios", "ER"));
                    _regions.Add(new Region("Formosa", "FM"));
                    _regions.Add(new Region("Jujuy", "JY"));
                    _regions.Add(new Region("La Pampa", "LP"));
                    _regions.Add(new Region("La Rioja", "LR"));
                    _regions.Add(new Region("Mendoza", "MZ"));
                    _regions.Add(new Region("Misiones", "MN"));
                    _regions.Add(new Region("Neuquen", "NQ"));
                    _regions.Add(new Region("Rio Negro", "RN"));
                    _regions.Add(new Region("Salta", "SA"));
                    _regions.Add(new Region("San Juan", "SJ"));
                    _regions.Add(new Region("San Luis", "SL"));
                    _regions.Add(new Region("Santa Cruz", "SC"));
                    _regions.Add(new Region("Santa Fe", "SF"));
                    _regions.Add(new Region("Santiago del Estero", "SE"));
                    _regions.Add(new Region("Tierra del Fuego", "TF"));
                    _regions.Add(new Region("Tucuman", "TM"));
                    break;
                case "AM": // Armenia
                    _regions.Add(new Region("Aragatsotn", "AG"));
                    _regions.Add(new Region("Ararat", "AR"));
                    _regions.Add(new Region("Armavir", "AV"));
                    _regions.Add(new Region("Geghark'unik'", "GR"));
                    _regions.Add(new Region("Kotayk'", "KT"));
                    _regions.Add(new Region("Lorri", "LO"));
                    _regions.Add(new Region("Shirak", "SH"));
                    _regions.Add(new Region("Syunik'", "SU"));
                    _regions.Add(new Region("Tavush", "TV"));
                    _regions.Add(new Region("Vayots' Dzor", "VD"));
                    _regions.Add(new Region("Yerevan", "ER"));
                    break;
                case "AT": // Austria
                    _regions.Add(new Region("Burgenland", "BU"));
                    _regions.Add(new Region("Karnten", "KA"));
                    _regions.Add(new Region("Niederosterreich", "NO"));
                    _regions.Add(new Region("Oberosterreich", "OO"));
                    _regions.Add(new Region("Salzburg", "SZ"));
                    _regions.Add(new Region("Steiermark", "ST"));
                    _regions.Add(new Region("Tirol", "TR"));
                    _regions.Add(new Region("Vorarlberg", "VO"));
                    _regions.Add(new Region("Wien", "WI"));
                    break;
                case "BR": // Brazil
                    _regions.Add(new Region("Acre", "AC"));
                    _regions.Add(new Region("Amapá", "AP"));
                    _regions.Add(new Region("Bahia", "BA"));
                    _regions.Add(new Region("Goiás", "GO"));
                    _regions.Add(new Region("Piauí", "PI"));
                    _regions.Add(new Region("Ceará", "CE"));
                    _regions.Add(new Region("Paraná", "PR"));
                    _regions.Add(new Region("Alagoas", "AL"));
                    _regions.Add(new Region("Paraíba", "PB"));
                    _regions.Add(new Region("Roraima", "RR"));
                    _regions.Add(new Region("Sergipe", "SE"));
                    _regions.Add(new Region("Amazonas", "AM"));
                    _regions.Add(new Region("Maranhão", "MA"));
                    _regions.Add(new Region("Rondônia", "RO"));
                    _regions.Add(new Region("São Paulo", "SP"));
                    _regions.Add(new Region("Tocantins", "TO"));
                    _regions.Add(new Region("Mato Grosso", "MT"));
                    _regions.Add(new Region("Minas Gerais", "MG"));
                    _regions.Add(new Region("Espírito Santo", "ES"));
                    _regions.Add(new Region("Rio de Janeiro", "RJ"));
                    _regions.Add(new Region("Santa Catarina", "SC"));
                    _regions.Add(new Region("Rio Grande do Sul", "RS"));
                    _regions.Add(new Region("Mato Grosso do Sul", "MS"));
                    _regions.Add(new Region("Rio Grande do Norte", "RN"));
                    _regions.Add(new Region("Distrito Federal", "DF"));
                    _regions.Add(new Region("Paro", "PA"));
                    _regions.Add(new Region("Pernambuco", "PE"));
                    break;
            }
        }
    }

    public class Region
    {
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public Region(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}
