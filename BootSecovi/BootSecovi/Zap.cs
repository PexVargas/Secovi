using BootSecovi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BootSecovi
{
    class Zap
    {
        PexIMContext db = new PexIMContext();

        public void Coletar(string siglaEstado,string estado, int tipoImovel)
        {

            var tipos = "VILLAGE_HOUSE".Split(',');

            foreach (var tipo in tipos)
            {
                int min = 0;
                int max = 100;
                int from = 0;

                var total = GetTotal(siglaEstado, estado, tipoImovel, "", "", tipo);

                if (total < 5000)
                {
                    var paginas = total / 18;
                    paginas = paginas + 20;

                    for (int i = 0; i < paginas; i++)
                    {
                        //Console.WriteLine(string.Format("Faixa de {0} a {1}", min, max));
                        //ColetarFaixa(siglaEstado, estado, tipoImovel, min.ToString(), max.ToString(), "HOME", from.ToString(), i.ToString());
                        ColetarFaixa(siglaEstado, estado, tipoImovel, "", "", tipo, from.ToString(), i.ToString());

                        from += 18;
                        //min = (max + 1);
                        //max += 100;
                    }
                }
                else
                {


                }
            }

    
        }

        public int GetTotal(string siglaEstado,string estado, int tipoImovel, string valorMin,  string valorMax, string tipo)
        {
            string strTipoImovel = "";

            if (tipoImovel == 1)
                strTipoImovel = "SALES";
            else
                strTipoImovel = "RENTAL";
            //var client = new RestClient("https://glue-api.zapimoveis.com.br/v2/listings?business=" + strTipoImovel + "&priceMax=" + valorMax + "&priceMin=" + valorMin + "&categoryPage=RESULT&parentId=null&listingType=USED&portal=ZAP&size=24&from=0&includeFields=search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount)%2Cexpansion(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cnearby(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cpage%2CfullUriFragments%2Cdevelopments(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2CsuperPremium(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cowners(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))&cityWiseStreet=1&developmentsSize=3&superPremiumSize=3&addressCountry=&addressState=Rio%20de%20Janeiro&addressCity=Rio%20de%20Janeiro&addressZone=&addressNeighborhood=&addressStreet=&addressAccounts=&addressType=city&addressLocationId=BR%3ERio%20de%20Janeiro%3ENULL%3ERio%20de%20Janeiro&addressPointLat=-22.288726&addressPointLon=-42.53408&__zt=smb%3Ac%2Cama%3Ab");
            var client = new RestClient("https://glue-api.zapimoveis.com.br/v2/listings?business="+ strTipoImovel +"&unitTypes="+tipo+"&priceMax="+valorMax+"&priceMin="+valorMin+"&categoryPage=RESULT&parentId=null&listingType=USED&portal=ZAP&size=18&from=0&includeFields=search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount)%2Cexpansion(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cnearby(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cpage%2CfullUriFragments%2Cdevelopments(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2CsuperPremium(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cowners(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))&cityWiseStreet=1&developmentsSize=3&superPremiumSize=3&addressCountry=&addressState="+estado+"&__zt=smb%3Ac%2Cama%3Ab");


            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "ccbda95b-aa10-50f0-1ff6-bb7456d2714b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-domain", "www.zapimoveis.com.br");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("accept", "application/json, text/plain, */*");
            request.AddHeader("sec-ch-ua", "\"Chromium\";v=\"88\", \"Google Chrome\";v=\"88\", \";Not A Brand\";v=\"99\"");
            IRestResponse response = client.Execute(request);


            JObject o = JObject.Parse(response.Content);

            var imoveis = o["search"]["result"]["listings"];

            var total = Convert.ToString(o["search"]["totalCount"]);

            if (total != "")
                return Convert.ToInt32(total);

            return 0;
        }
       
        
        public void ColetarFaixa(string siglaEstado, string estado, int tipoImovel, string valorMin, string valorMax, string tipo,string from,  string pagina)
        {
            string strTipoImovel = "";

            if (tipoImovel == 1)
                strTipoImovel = "SALES";
            else
                strTipoImovel = "RENTAL";

            var client = new RestClient("https://glue-api.zapimoveis.com.br/v2/listings?business="+strTipoImovel+ "&unitTypes=" + tipo + "&priceMax="+valorMax+"&priceMin="+valorMin+ "&categoryPage=RESULT&parentId=null&listingType=USED&portal=ZAP&size=24&from="+from+"&page="+pagina+"&includeFields=search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount)%2Cexpansion(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cnearby(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cpage%2CfullUriFragments%2Cdevelopments(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2CsuperPremium(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cowners(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))&cityWiseStreet=1&developmentsSize=3&superPremiumSize=3&addressCountry=&addressState="+estado+"&__zt=smb%3Ac%2Cama%3Ab");



            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "ccbda95b-aa10-50f0-1ff6-bb7456d2714b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-domain", "www.zapimoveis.com.br");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("accept", "application/json, text/plain, */*");
            request.AddHeader("sec-ch-ua", "\"Chromium\";v=\"88\", \"Google Chrome\";v=\"88\", \";Not A Brand\";v=\"99\"");
            IRestResponse response = client.Execute(request);


            JObject o = JObject.Parse(response.Content);

            var imoveis = o["search"]["result"]["listings"];

            var TOTAL = Convert.ToString( o["search"]["totalCount"]);
           


            foreach (var item in imoveis)
            {
                try
                {
                    ImoveisCapturados imovelCapturado = new ImoveisCapturados();
                    var imovel = item["listing"];
                    var medias = item["medias"];
                    var link = item["link"];
                    var account = item["account"];
                    imovelCapturado.Anunciante = Convert.ToString(account["name"]);



                    imovelCapturado.SiglaEstado = siglaEstado;
                    imovelCapturado.TipoImovel = tipoImovel;

                    imovelCapturado.Url = "https://www.zapimoveis.com.br"+Convert.ToString(link["href"]);
                    var streetNumber  = Convert.ToString(link["data"]["streetNumber"]);

                    var urlImg = "";
                    foreach (var media in medias)
                    {
                        urlImg += media["url"] + ";";
                    }

                    urlImg = urlImg.Replace("{action}/{width}x{height}", "fit-in/800x360");

                    imovelCapturado.Imagens = urlImg;

                    var descricao = Convert.ToString(imovel["description"]);
                    var id = Convert.ToString(imovel["id"]);
                   // imovelCapturado.Url = "https://www.zapimoveis.com.br/imovel/" + id;

                    imovelCapturado.AreaPrivativa = Convert.ToString(imovel["usableArea"]);
                    imovelCapturado.Descricao = descricao;

                    imovelCapturado.Quartos = Convert.ToString(imovel["bedrooms"]).Replace("[", "").Replace("]", "");
                    imovelCapturado.Banheiros = Convert.ToString(imovel["bathrooms"]).Replace("[", "").Replace("]", "");
                    imovelCapturado.Finalidade = Convert.ToString(imovel["usageTypes"]).Replace("[", "").Replace("]", "");

                    imovelCapturado.Garagens = Convert.ToString(imovel["parkingSpaces"]).Replace("[", "").Replace("]", "");
                    imovelCapturado.AreaTotal = Convert.ToString(imovel["totalAreas"]).Replace("[", "").Replace("]", "").Replace("\"", "");
                    imovelCapturado.Tipo = Convert.ToString(imovel["unitTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
                    imovelCapturado.CodImobiliaria = 1;

                    var address = imovel["address"];

                    imovelCapturado.Cidade = Convert.ToString(address["city"]);
                    imovelCapturado.Bairro = Convert.ToString(address["neighborhood"]);
                    imovelCapturado.Cep = Convert.ToString(address["zipCode"]);
                    imovelCapturado.Rua = Convert.ToString(address["street"]);
                    imovelCapturado.Localidade = Convert.ToString(address["zone"]);


                    var pricingInfos = imovel["pricingInfos"];

                    foreach (var pricing in pricingInfos)
                    {
                        imovelCapturado.Iptu = Convert.ToString(pricing["yearlyIptu"]);
                        imovelCapturado.Valor = Convert.ToString(pricing["price"]);
                        imovelCapturado.Condominio = Convert.ToString(pricing["monthlyCondoFee"]);
                    }


                    Console.WriteLine(imovelCapturado.Url);

                    db.ImoveisCapturados.Add(imovelCapturado);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    Console.WriteLine("Erro");
                }
            }
        }
    }
}
