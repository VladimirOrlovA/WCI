using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCI.DAL;

namespace WCI.BLL
{
    public class CurrencyDToBuilder : ClassBuilder
    {
        public CurrencyDToBuilder(string resourceAddress) : base(resourceAddress) { }

        public override ModelDTO Create()
        {
            string message;
            Currency currency = DataLoad.GetCurrencyFromXML(ResourceAddress, out message);

            CurrencyDTO currencyDTO = new CurrencyDTO();

            currencyDTO.Title = currencyDTO.Title;
            currencyDTO.Link = currencyDTO.Link;
            
            foreach(var item in currency.items)
            {
                CurrencyDTO.Item itemDTO = new CurrencyDTO.Item();

                itemDTO.Title = item.Title;
                itemDTO.Description = item.Description;
                itemDTO.Change = item.Change;
                itemDTO.PubDate = item.PubDate;

                currencyDTO.items.Add(itemDTO);
            }

            currencyDTO.Title = currencyDTO.Title;

            return currencyDTO;
        }
    }
}
