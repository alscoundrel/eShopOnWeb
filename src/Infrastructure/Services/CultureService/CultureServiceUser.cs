

using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public static class CultureServiceUser
    {
        //private readonly IConfiguration _configuration;

        //public static  CultureServiceUser(IConfiguration configuration){
        //    _configuration = configuration;
        //}

        public static Currency FindCurrency(Currency? default_price_unit){
            Currency currency;
            if(Enum.TryParse(RegionInfo.CurrentRegion.ISOCurrencySymbol, true, out currency)){ return currency;}
            return default_price_unit.HasValue?default_price_unit.Value:Currency.USD;// TODO: Get from configuration
        }
    }
}