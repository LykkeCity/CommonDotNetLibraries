using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Common
{
    public static class CountryManager
    {

        public static IReadOnlyDictionary<string, string> CisCountry()
        {
            var dict = new Dictionary<string, string>
            {

                {"RUS", "RUS"},
                {"ARM", "ARM"},
                {"BLR", "BLR"},
                {"KAZ", "KAZ"},
                {"KGZ", "KGZ"},
                {"MDA", "MDA"},
                {"TJK", "TJK"},
                {"TKM", "TKM"},
                {"UZB", "UZB"},
                {"UKR", "UKR"},
                {"LTU", "LTU"},
                {"LVA", "LVA"},
                {"EST", "EST"},
                {"GEO", "GEO"},

            };

            return new ReadOnlyDictionary<string, string>(dict);
        }

        public static IDictionary<string, string> ChineseCountry()
        {
            var dict =  new Dictionary<string, string>
            {

                {"CHN", "CHN"},


            };

            return new ReadOnlyDictionary<string, string>(dict);
        } 

        public static readonly IReadOnlyDictionary<string, string> CountryIso3ToIso2Links = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {

            #region ISO Codes3To2
            {"ABW", "AW"},
            {"AFG", "AF"},
            {"AGO", "AO"},
            {"AIA", "AI"},
            {"ALA", "AX"},
            {"ALB", "AL"},
            {"AND", "AD"},
            {"ARE", "AE"},
            {"ARG", "AR"},
            {"ARM", "AM"},
            {"ASM", "AS"},
            {"ATA", "AQ"},
            {"ATF", "TF"},
            {"ATG", "AG"},
            {"AUS", "AU"},
            {"AUT", "AT"},
            {"AZE", "AZ"},
            {"BDI", "BI"},
            {"BEL", "BE"},
            {"BEN", "BJ"},
            {"BES", "BQ"},
            {"BFA", "BF"},
            {"BGD", "BD"},
            {"BGR", "BG"},
            {"BHR", "BH"},
            {"BHS", "BS"},
            {"BIH", "BA"},
            {"BLM", "BL"},
            {"BLR", "BY"},
            {"BLZ", "BZ"},
            {"BMU", "BM"},
            {"BOL", "BO"},
            {"BRA", "BR"},
            {"BRB", "BB"},
            {"BRN", "BN"},
            {"BTN", "BT"},
            {"BVT", "BV"},
            {"BWA", "BW"},
            {"CAF", "CF"},
            {"CAN", "CA"},
            {"CCK", "CC"},
            {"CHE", "CH"},
            {"CHL", "CL"},
            {"CHN", "CN"},
            {"CIV", "CI"},
            {"CMR", "CM"},
            {"COD", "CD"},
            {"COG", "CG"},
            {"COK", "CK"},
            {"COL", "CO"},
            {"COM", "KM"},
            {"CPV", "CV"},
            {"CRI", "CR"},
            {"CUB", "CU"},
            {"CUW", "CW"},
            {"CXR", "CX"},
            {"CYM", "KY"},
            {"CYP", "CY"},
            {"CZE", "CZ"},
            {"DEU", "DE"},
            {"DJI", "DJ"},
            {"DMA", "DM"},
            {"DNK", "DK"},
            {"DOM", "DO"},
            {"DZA", "DZ"},
            {"ECU", "EC"},
            {"EGY", "EG"},
            {"ERI", "ER"},
            {"ESH", "EH"},
            {"ESP", "ES"},
            {"EST", "EE"},
            {"ETH", "ET"},
            {"FIN", "FI"},
            {"FJI", "FJ"},
            {"FLK", "FK"},
            {"FRA", "FR"},
            {"FRO", "FO"},
            {"FSM", "FM"},
            {"GAB", "GA"},
            {"GBR", "GB"},
            {"GEO", "GE"},
            {"GGY", "GG"},
            {"GHA", "GH"},
            {"GIB", "GI"},
            {"GIN", "GN"},
            {"GLP", "GP"},
            {"GMB", "GM"},
            {"GNB", "GW"},
            {"GNQ", "GQ"},
            {"GRC", "GR"},
            {"GRD", "GD"},
            {"GRL", "GL"},
            {"GTM", "GT"},
            {"GUF", "GF"},
            {"GUM", "GU"},
            {"GUY", "GY"},
            {"HKG", "HK"},
            {"HMD", "HM"},
            {"HND", "HN"},
            {"HRV", "HR"},
            {"HTI", "HT"},
            {"HUN", "HU"},
            {"IDN", "ID"},
            {"IMN", "IM"},
            {"IND", "IN"},
            {"IOT", "IO"},
            {"IRL", "IE"},
            {"IRN", "IR"},
            {"IRQ", "IQ"},
            {"ISL", "IS"},
            {"ISR", "IL"},
            {"ITA", "IT"},
            {"JAM", "JM"},
            {"JEY", "JE"},
            {"JOR", "JO"},
            {"JPN", "JP"},
            {"KAZ", "KZ"},
            {"KEN", "KE"},
            {"KGZ", "KG"},
            {"KHM", "KH"},
            {"KIR", "KI"},
            {"KNA", "KN"},
            {"KOR", "KR"},
            {"KWT", "KW"},
            {"LAO", "LA"},
            {"LBN", "LB"},
            {"LBR", "LR"},
            {"LBY", "LY"},
            {"LCA", "LC"},
            {"LIE", "LI"},
            {"LKA", "LK"},
            {"LSO", "LS"},
            {"LTU", "LT"},
            {"LUX", "LU"},
            {"LVA", "LV"},
            {"MAC", "MO"},
            {"MAF", "MF"},
            {"MAR", "MA"},
            {"MCO", "MC"},
            {"MDA", "MD"},
            {"MDG", "MG"},
            {"MDV", "MV"},
            {"MEX", "MX"},
            {"MHL", "MH"},
            {"MKD", "MK"},
            {"MLI", "ML"},
            {"MLT", "MT"},
            {"MMR", "MM"},
            {"MNE", "ME"},
            {"MNG", "MN"},
            {"MNP", "MP"},
            {"MOZ", "MZ"},
            {"MRT", "MR"},
            {"MSR", "MS"},
            {"MTQ", "MQ"},
            {"MUS", "MU"},
            {"MWI", "MW"},
            {"MYS", "MY"},
            {"MYT", "YT"},
            {"NAM", "NA"},
            {"NCL", "NC"},
            {"NER", "NE"},
            {"NFK", "NF"},
            {"NGA", "NG"},
            {"NIC", "NI"},
            {"NIU", "NU"},
            {"NLD", "NL"},
            {"NOR", "NO"},
            {"NPL", "NP"},
            {"NRU", "NR"},
            {"NZL", "NZ"},
            {"OMN", "OM"},
            {"PAK", "PK"},
            {"PAN", "PA"},
            {"PCN", "PN"},
            {"PER", "PE"},
            {"PHL", "PH"},
            {"PLW", "PW"},
            {"PNG", "PG"},
            {"POL", "PL"},
            {"PRI", "PR"},
            {"PRK", "KP"},
            {"PRT", "PT"},
            {"PRY", "PY"},
            {"PSE", "PS"},
            {"PYF", "PF"},
            {"QAT", "QA"},
            {"REU", "RE"},
            {"ROU", "RO"},
            {"RUS", "RU"},
            {"RWA", "RW"},
            {"SAU", "SA"},
            {"SDN", "SD"},
            {"SEN", "SN"},
            {"SGP", "SG"},
            {"SGS", "GS"},
            {"SHN", "SH"},
            {"SJM", "SJ"},
            {"SLB", "SB"},
            {"SLE", "SL"},
            {"SLV", "SV"},
            {"SMR", "SM"},
            {"SOM", "SO"},
            {"SPM", "PM"},
            {"SRB", "RS"},
            {"SSD", "SS"},
            {"STP", "ST"},
            {"SUR", "SR"},
            {"SVK", "SK"},
            {"SVN", "SI"},
            {"SWE", "SE"},
            {"SWZ", "SZ"},
            {"SXM", "SX"},
            {"SYC", "SC"},
            {"SYR", "SY"},
            {"TCA", "TC"},
            {"TCD", "TD"},
            {"TGO", "TG"},
            {"THA", "TH"},
            {"TJK", "TJ"},
            {"TKL", "TK"},
            {"TKM", "TM"},
            {"TLS", "TL"},
            {"TON", "TO"},
            {"TTO", "TT"},
            {"TUN", "TN"},
            {"TUR", "TR"},
            {"TUV", "TV"},
            {"TWN", "TW"},
            {"TZA", "TZ"},
            {"UGA", "UG"},
            {"UKR", "UA"},
            {"UMI", "UM"},
            {"URY", "UY"},
            {"USA", "US"},
            {"UZB", "UZ"},
            {"VAT", "VA"},
            {"VCT", "VC"},
            {"VEN", "VE"},
            {"VGB", "VG"},
            {"VIR", "VI"},
            {"VNM", "VN"},
            {"VUT", "VU"},
            {"WLF", "WF"},
            {"WSM", "WS"},
            {"YEM", "YE"},
            {"ZAF", "ZA"},
            {"ZMB", "ZM"},
            {"ZWE", "ZW"},

            #endregion

        });

        public static readonly IReadOnlyDictionary<string, string> CountryIso3ToNameLinks = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            #region Iso3ToCountryNames
            {"AFG", "Afghanistan"},
            {"ALA", "Aland Islands"},
            {"ALB", "Albania"},
            {"DZA", "Algeria"},
            {"ASM", "American Samoa"},
            {"AND", "Andorra"},
            {"AGO", "Angola"},
            {"AIA", "Anguilla"},
            {"ATA", "Antarctica"},
            {"ATG", "Antigua and Barbuda"},
            {"ARG", "Argentina"},
            {"ARM", "Armenia"},
            {"ABW", "Aruba"},
            {"AUS", "Australia"},
            {"AUT", "Austria"},
            {"AZE", "Azerbaijan"},
            {"BHS", "Bahamas"},
            {"BHR", "Bahrain"},
            {"BGD", "Bangladesh"},
            {"BRB", "Barbados"},
            {"BLR", "Belarus"},
            {"BEL", "Belgium"},
            {"BLZ", "Belize"},
            {"BEN", "Benin"},
            {"BMU", "Bermuda"},
            {"BTN", "Bhutan"},
            {"BOL", "Bolivia"},
            {"BES", "Bonaire, Saint Eustatius and Saba"},
            {"BIH", "Bosnia and Herzegovina"},
            {"BWA", "Botswana"},
            {"BVT", "Bouvet Island"},
            {"BRA", "Brazil"},
            {"IOT", "British Indian Ocean Territory"},
            {"VGB", "British Virgin Islands"},
            {"BRN", "Brunei"},
            {"BGR", "Bulgaria"},
            {"BFA", "Burkina Faso"},
            {"BDI", "Burundi"},
            {"KHM", "Cambodia"},
            {"CMR", "Cameroon"},
            {"CAN", "Canada"},
            {"CPV", "Cape Verde"},
            {"CYM", "Cayman Islands"},
            {"CAF", "Central African Republic"},
            {"TCD", "Chad"},
            {"CHL", "Chile"},
            {"CHN", "China"},
            {"CXR", "Christmas Island"},
            {"CCK", "Cocos Islands"},
            {"COL", "Colombia"},
            {"COM", "Comoros"},
            {"COK", "Cook Islands"},
            {"CRI", "Costa Rica"},
            {"HRV", "Croatia"},
            {"CUB", "Cuba"},
            {"CUW", "Curacao"},
            {"CYP", "Cyprus"},
            {"CZE", "Czech Republic"},
            {"COD", "Democratic Republic of the Congo"},
            {"DNK", "Denmark"},
            {"DJI", "Djibouti"},
            {"DMA", "Dominica"},
            {"DOM", "Dominican Republic"},
            {"TLS", "East Timor"},
            {"ECU", "Ecuador"},
            {"EGY", "Egypt"},
            {"SLV", "El Salvador"},
            {"GNQ", "Equatorial Guinea"},
            {"ERI", "Eritrea"},
            {"EST", "Estonia"},
            {"ETH", "Ethiopia"},
            {"FLK", "Falkland Islands"},
            {"FRO", "Faroe Islands"},
            {"FJI", "Fiji"},
            {"FIN", "Finland"},
            {"FRA", "France"},
            {"GUF", "French Guiana"},
            {"PYF", "French Polynesia"},
            {"ATF", "French Southern Territories"},
            {"GAB", "Gabon"},
            {"GMB", "Gambia"},
            {"GEO", "Georgia"},
            {"DEU", "Germany"},
            {"GHA", "Ghana"},
            {"GIB", "Gibraltar"},
            {"GRC", "Greece"},
            {"GRL", "Greenland"},
            {"GRD", "Grenada"},
            {"GLP", "Guadeloupe"},
            {"GUM", "Guam"},
            {"GTM", "Guatemala"},
            {"GGY", "Guernsey"},
            {"GIN", "Guinea"},
            {"GNB", "Guinea-Bissau"},
            {"GUY", "Guyana"},
            {"HTI", "Haiti"},
            {"HMD", "Heard Island and McDonald Islands"},
            {"HND", "Honduras"},
            {"HKG", "Hong Kong"},
            {"HUN", "Hungary"},
            {"ISL", "Iceland"},
            {"IND", "India"},
            {"IDN", "Indonesia"},
            {"IRN", "Iran"},
            {"IRQ", "Iraq"},
            {"IRL", "Ireland"},
            {"IMN", "Isle of Man"},
            {"ISR", "Israel"},
            {"ITA", "Italy"},
            {"CIV", "Ivory Coast"},
            {"JAM", "Jamaica"},
            {"JPN", "Japan"},
            {"JEY", "Jersey"},
            {"JOR", "Jordan"},
            {"KAZ", "Kazakhstan"},
            {"KEN", "Kenya"},
            {"KIR", "Kiribati"},
            {"XKX", "Kosovo"},
            {"KWT", "Kuwait"},
            {"KGZ", "Kyrgyzstan"},
            {"LAO", "Laos"},
            {"LVA", "Latvia"},
            {"LBN", "Lebanon"},
            {"LSO", "Lesotho"},
            {"LBR", "Liberia"},
            {"LBY", "Libya"},
            {"LIE", "Liechtenstein"},
            {"LTU", "Lithuania"},
            {"LUX", "Luxembourg"},
            {"MAC", "Macao"},
            {"MKD", "Macedonia"},
            {"MDG", "Madagascar"},
            {"MWI", "Malawi"},
            {"MYS", "Malaysia"},
            {"MDV", "Maldives"},
            {"MLI", "Mali"},
            {"MLT", "Malta"},
            {"MHL", "Marshall Islands"},
            {"MTQ", "Martinique"},
            {"MRT", "Mauritania"},
            {"MUS", "Mauritius"},
            {"MYT", "Mayotte"},
            {"MEX", "Mexico"},
            {"FSM", "Micronesia"},
            {"MDA", "Moldova"},
            {"MCO", "Monaco"},
            {"MNG", "Mongolia"},
            {"MNE", "Montenegro"},
            {"MSR", "Montserrat"},
            {"MAR", "Morocco"},
            {"MOZ", "Mozambique"},
            {"MMR", "Myanmar"},
            {"NAM", "Namibia"},
            {"NRU", "Nauru"},
            {"NPL", "Nepal"},
            {"NLD", "Netherlands"},
            {"ANT", "Netherlands Antilles"},
            {"NCL", "New Caledonia"},
            {"NZL", "New Zealand"},
            {"NIC", "Nicaragua"},
            {"NER", "Niger"},
            {"NGA", "Nigeria"},
            {"NIU", "Niue"},
            {"NFK", "Norfolk Island"},
            {"PRK", "North Korea"},
            {"MNP", "Northern Mariana Islands"},
            {"NOR", "Norway"},
            {"OMN", "Oman"},
            {"PAK", "Pakistan"},
            {"PLW", "Palau"},
            {"PSE", "Palestinian Territory"},
            {"PAN", "Panama"},
            {"PNG", "Papua New Guinea"},
            {"PRY", "Paraguay"},
            {"PER", "Peru"},
            {"PHL", "Philippines"},
            {"PCN", "Pitcairn"},
            {"POL", "Poland"},
            {"PRT", "Portugal"},
            {"PRI", "Puerto Rico"},
            {"QAT", "Qatar"},
            {"COG", "Republic of the Congo"},
            {"REU", "Reunion"},
            {"ROU", "Romania"},
            {"RUS", "Russia"},
            {"RWA", "Rwanda"},
            {"BLM", "Saint Barthelemy"},
            {"SHN", "Saint Helena"},
            {"KNA", "Saint Kitts and Nevis"},
            {"LCA", "Saint Lucia"},
            {"MAF", "Saint Martin"},
            {"SPM", "Saint Pierre and Miquelon"},
            {"VCT", "Saint Vincent and the Grenadines"},
            {"WSM", "Samoa"},
            {"SMR", "San Marino"},
            {"STP", "Sao Tome and Principe"},
            {"SAU", "Saudi Arabia"},
            {"SEN", "Senegal"},
            {"SRB", "Serbia"},
            {"SCG", "Serbia and Montenegro"},
            {"SYC", "Seychelles"},
            {"SLE", "Sierra Leone"},
            {"SGP", "Singapore"},
            {"SXM", "Sint Maarten"},
            {"SVK", "Slovakia"},
            {"SVN", "Slovenia"},
            {"SLB", "Solomon Islands"},
            {"SOM", "Somalia"},
            {"ZAF", "South Africa"},
            {"SGS", "South Georgia and the South Sandwich Islands"},
            {"KOR", "South Korea"},
            {"SSD", "South Sudan"},
            {"ESP", "Spain"},
            {"LKA", "Sri Lanka"},
            {"SDN", "Sudan"},
            {"SUR", "Suriname"},
            {"SJM", "Svalbard and Jan Mayen"},
            {"SWZ", "Swaziland"},
            {"SWE", "Sweden"},
            {"CHE", "Switzerland"},
            {"SYR", "Syria"},
            {"TWN", "Taiwan"},
            {"TJK", "Tajikistan"},
            {"TZA", "Tanzania"},
            {"THA", "Thailand"},
            {"TGO", "Togo"},
            {"TKL", "Tokelau"},
            {"TON", "Tonga"},
            {"TTO", "Trinidad and Tobago"},
            {"TUN", "Tunisia"},
            {"TUR", "Turkey"},
            {"TKM", "Turkmenistan"},
            {"TCA", "Turks and Caicos Islands"},
            {"TUV", "Tuvalu"},
            {"VIR", "U.S. Virgin Islands"},
            {"UGA", "Uganda"},
            {"UKR", "Ukraine"},
            {"ARE", "United Arab Emirates"},
            {"GBR", "United Kingdom"},
            {"USA", "United States"},
            {"UMI", "United States Minor Outlying Islands"},
            {"URY", "Uruguay"},
            {"UZB", "Uzbekistan"},
            {"VUT", "Vanuatu"},
            {"VAT", "Vatican"},
            {"VEN", "Venezuela"},
            {"VNM", "Vietnam"},
            {"WLF", "Wallis and Futuna"},
            {"ESH", "Western Sahara"},
            {"YEM", "Yemen"},
            {"ZMB", "Zambia"},
            {"ZWE", "Zimbabwe "},
            #endregion
        });

        public static readonly IReadOnlyDictionary<string, string> CountryIso2ToNameLinks = CountryIso3ToNameLinks
            .Select(x => new
            {
                Iso2 = Iso3ToIso2(x.Key),
                Name = x.Value
            })
            .Where(x => !string.IsNullOrWhiteSpace(x.Iso2))
            .ToDictionary(x => x.Iso2, x => x.Name);

        public static readonly IReadOnlyDictionary<string, string> CountryNameToIso3Links = CountryIso3ToNameLinks
            .ToDictionary(
                x => x.Value,
                x => x.Key);

        public static readonly IReadOnlyDictionary<string, string> CountryNameToIso2Links = CountryIso2ToNameLinks
            .ToDictionary(
                x => x.Value,
                x => x.Key);

        private static readonly Lazy<IReadOnlyDictionary<string, string>> DictCountryIso2ToIso3Links
            = new Lazy<IReadOnlyDictionary<string, string>>(() =>
            {
                var dict = CountryIso3ToIso2Links.ToDictionary(kpv => kpv.Value, kpv => kpv.Key);
                return new ReadOnlyDictionary<string, string>(dict);
            });
                
        public static IReadOnlyDictionary<string, string> CountryIso2ToIso3Links => DictCountryIso2ToIso3Links.Value;

        public static bool HasIso3(string iso3Id)
        {
            if (string.IsNullOrEmpty(iso3Id))
                return false;

            return CountryIso3ToIso2Links.ContainsKey(iso3Id);
        }

        public static bool HasIso2(string iso2Id)
        {
            if (string.IsNullOrEmpty(iso2Id))
                return false;

            return CountryIso2ToIso3Links.ContainsKey(iso2Id);
        }

        public static IEnumerable<string> AllAlpha3Codes()
        {
            return CountryIso3ToIso2Links.Keys;
        }

        public static IEnumerable<string> AllAlpha2Codes()
        {
            return CountryIso3ToIso2Links.Values;
        }

        public static string Iso3ToIso2(string iso3)
        {
            if (string.IsNullOrEmpty(iso3))
                return string.Empty;

            return CountryIso3ToIso2Links.TryGetValue(iso3, out var iso2) ? iso2 : string.Empty;
        }

        public static string Iso2ToIso3(string iso2)
        {
            if (string.IsNullOrEmpty(iso2))
                return string.Empty;

            return CountryIso2ToIso3Links.TryGetValue(iso2, out var iso3) ? iso3 : string.Empty;
        }

        /// <summary>
        /// Get country name by country <paramref name="iso3"/> code.
        /// </summary>
        /// <param name="iso3">Three capital letter country code.</param>
        /// <returns>
        /// <para>Country name for given code.</para>
        /// <para>Empty string if code is null, empty or invalid.</para>
        /// </returns>
        /// <example>
        /// <code>
        /// GetFullNameByCode("RUS");
        /// </code>
        /// </example>
        public static string GetCountryNameByIso3(string iso3)
        {
            if (string.IsNullOrEmpty(iso3))
                return string.Empty;

            return CountryIso3ToNameLinks.TryGetValue(iso3, out var name) ? name : string.Empty;
        }

        /// <summary>
        /// Get country name by country <paramref name="iso2"/> code.
        /// </summary>
        /// <param name="iso2">Two capital letter country code.</param>
        /// <returns>
        /// <para>Country name for given code.</para>
        /// <para>Empty string if code is null, empty or invalid.</para>
        /// </returns>
        /// <example>
        /// <code>
        /// GetFullNameByCode("RU");
        /// </code>
        /// </example>
        public static string GetCountryNameByIso2(string iso2)
        {
            if (string.IsNullOrEmpty(iso2))
                return string.Empty;

            return CountryIso2ToNameLinks.TryGetValue(iso2, out var name) ? name : string.Empty;
        }

        public static string GetIso2ByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            return CountryNameToIso2Links.TryGetValue(name, out var iso2) ? iso2 : string.Empty;
        }

        public static string GetIso3ByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            return CountryNameToIso3Links.TryGetValue(name, out var iso3) ? iso3 : string.Empty;
        }
    }
}
