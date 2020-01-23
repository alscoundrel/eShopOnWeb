

using System;
using System.Globalization;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public static class CultureServiceUser
    {
        public static Currency FindCurrency(){
            Currency currency;
            if(Enum.TryParse(RegionInfo.CurrentRegion.ISOCurrencySymbol, true, out currency)){ return currency;}
            return Currency.USD;// TODO: Get from configuration
        }
    }
}