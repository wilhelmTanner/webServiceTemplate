
using CountryInfoService;

namespace Tanner.Template.Base.Models
{
    public class Country: tCountryInfo
    {
        public Country()
        {
        }

        public Country(tCountryCodeAndName countryInfo)
        { 
            this.sName = countryInfo.sName;
            this.sISOCode = countryInfo.sISOCode;
            this.ShortName = "FSN";
            this.IsoCode = "IsoCode";
        }

        public Country(tCountryInfo countryInfo)
        {
            this.sName = countryInfo.sName;
            this.sISOCode = countryInfo.sISOCode;
            this.ShortName = "FSN";
            this.IsoCode = "IsoCode";
        }

        public string? IsoCode { get; set; }
        public string? ShortName { get; set; }


    }
}
