using BootSecovi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BootSecovi
{
    class Zap
    {
        PexIMContext dbx = new PexIMContext();
        public void Coletar(string siglaEstado, string estado, int tipoImovel)
        {

            List<FiltroZap> lstTiposSubTipos = new List<FiltroZap>();

            //FiltroZap f1 = new FiltroZap
            //{
            //    Tipo = "APARTMENT",
            //    TipoV3 = "KITNET",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "KITNET"
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "APARTMENT",
            //    TipoV3 = "",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "STUDIO"
            //};





            //FiltroZap f1 = new FiltroZap
            //{
            //    Tipo = "HOME",
            //    TipoV3 = "HOME",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = ""
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "HOME",
            //    TipoV3 = "VILLAGE_HOUSE",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "VILLAGE_HOUSE"
            //};


            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "HOME",
            //    TipoV3 = "CONDOMINIUM",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "CONDOMINIUM"
            //};




            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "APARTMENT",
            //    TipoV3 = "FLAT",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "FLAT"
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "APARTMENT",
            //    TipoV3 = "LOFT",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "LOFT"
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "FARM",
            //    TipoV3 = "FARM",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = ""
            //};


            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "ALLOTMENT_LAND",
            //    TipoV3 = "RESIDENTIAL_ALLOTMENT_LAND",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = ""
            //};



            //COMERCIAL
            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "BUSINESS",
            //    TipoV3 = "BUSINESS",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};


            FiltroZap f2 = new FiltroZap
            {
                Tipo = "OFFICE",
                TipoV3 = "OFFICE",
                Perfil = "COMMERCIAL",
                SubTipo = ""
            };



            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "HOME",
            //    TipoV3 = "COMMERCIAL_PROPERTY",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};


            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "HOTEL",
            //    TipoV3 = "HOTEL",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "OFFICE",
            //    TipoV3 = "FLOOR",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "BUILDING",
            //    TipoV3 = "COMMERCIAL_BUILDING",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};


            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "ALLOTMENT_LAND",
            //    TipoV3 = "COMMERCIAL_ALLOTMENT_LAND",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "SHED_DEPOSIT_WAREHOUSE",
            //    TipoV3 = "SHED_DEPOSIT_WAREHOUSE",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};


            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "PARKING_SPACE",
            //    TipoV3 = "PARKING_SPACE",
            //    Perfil = "COMMERCIAL",
            //    SubTipo = ""
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "APARTMENT",
            //    TipoV3 = "PENTHOUSE",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = "PENTHOUSE"
            //};

            //FiltroZap f2 = new FiltroZap
            //{
            //    Tipo = "APARTMENT",
            //    TipoV3 = "APARTMENT",
            //    Perfil = "RESIDENTIAL",
            //    SubTipo = ""
            //};

            lstTiposSubTipos.Add(f2);

            foreach (var tipo in lstTiposSubTipos)
            {
                var total = GetTotal(siglaEstado, estado, tipoImovel, "", "", tipo.Tipo, tipo.TipoV3, tipo.Perfil, tipo.SubTipo);

                Dictionary<string, string> faixas = new Dictionary<string, string>();

                Faixas f = new Faixas();


                if (tipoImovel == 1)
                    faixas = f.GetFaixaVenda();
                else
                    faixas = f.GetfaixaAluguel();

                foreach (var faixa in faixas)
                {
                    var coletaRestanteSemLimite = false;
                    var valorMinimo = faixa.Key == "0" ? "" : faixa.Key;
                    var valorMaximo = faixa.Value == "0" ? "" : faixa.Value;

                    Console.WriteLine(string.Format("Faixa {0} a {1}", valorMinimo, valorMaximo));

                    var totalFaixa = GetTotal(siglaEstado, estado, tipoImovel, valorMinimo, valorMaximo, tipo.Tipo, tipo.TipoV3, tipo.Perfil, tipo.SubTipo);
                    var totalFaixaSemLimite = GetTotal(siglaEstado, estado, tipoImovel, valorMinimo, "", tipo.Tipo, tipo.TipoV3, tipo.Perfil, tipo.SubTipo);


                    if (totalFaixaSemLimite < 5000)
                    {
                        Console.WriteLine(string.Format("Faixa {0} a {1}", valorMinimo, " sem limite"));

                        totalFaixa = totalFaixaSemLimite;
                        coletaRestanteSemLimite = true;
                        valorMaximo = "";
                    }

                    var paginas = (totalFaixa / 24) + 1;

                    Console.WriteLine(string.Format("Total de paginas {0}", paginas));

                    int from = 0;
                    var options = new ParallelOptions { MaxDegreeOfParallelism = 5 };



                    for (int i = 0; i < paginas; i++)
                    {
                        Console.WriteLine(string.Format("Pagina {0}  -  from:{1}", i, from));
                        ColetarFaixa(siglaEstado, estado, tipoImovel, valorMinimo, valorMaximo, tipo.Tipo, from.ToString(), i.ToString(), tipo.TipoV3, tipo.Perfil, tipo.SubTipo);
                        from += 24;
                    }


                    Console.WriteLine("");

                    if (coletaRestanteSemLimite)
                        break;
                }

            }
        }




        public int GetTotal(string siglaEstado, string estado, int tipoImovel, string valorMin, string valorMax, string tipo, string tipov3, string usageTypes, string subTipo)
        {
            string strTipoImovel = "";

            if (tipoImovel == 1)
                strTipoImovel = "SALE";
            else
                strTipoImovel = "RENTAL";

            try
            {

                var url = "https://glue-api.zapimoveis.com.br/v2/listings?unitSubTypes=" + subTipo + "&unitTypes=" + tipo + "&usageTypes=" + usageTypes + "&unitTypesV3=" + tipov3 + "&priceMax=" + valorMax + "&priceMin=" + valorMin + "&business=" + strTipoImovel + "&categoryPage=RESULT&parentId=null&listingType=USED&size=24&from=0&includeFields=search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount)%2Cexpansion(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cnearby(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cpage%2CfullUriFragments%2Cdevelopments(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2CsuperPremium(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cowners(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))&cityWiseStreet=1&developmentsSize=3&superPremiumSize=3&addressCountry=&addressState=" + estado + "&addressCity=&addressZone=&addressNeighborhood=&addressStreet=&addressAccounts=&addressType=&addressPointLat=&addressPointLon=&__zt=ama%3A";

                // HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://glue-api.zapimoveis.com.br/v2/listings?business=" + strTipoImovel + "&unitTypes=" + tipo + "&unitTypesV3=" + tipov3+ "&priceMax=" + valorMax + "&priceMin=" + valorMin + "&categoryPage=RESULT&parentId=null&listingType=USED&portal=ZAP&size=18&from=0&includeFields=search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount)%2Cexpansion(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cnearby(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cpage%2CfullUriFragments%2Cdevelopments(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2CsuperPremium(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cowners(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))&cityWiseStreet=1&developmentsSize=3&superPremiumSize=3&addressCountry=&addressState=" + estado + "&__zt=smb%3Ac%2Cama%3Ab");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // WebProxy myproxy = new WebProxy("83.149.70.159:13012");
                WebProxy myproxy = new WebProxy("163.172.48.109:15001");

                myproxy.BypassProxyOnLocal = false;
                request.Proxy = myproxy;
                request.Timeout = 30000;
                request.Headers.Add("postman-token", "71c850b8-7d01-1ab7-8ef8-8dcda8e27d6f");
                request.Headers.Add("cache-control", "no-cache");
                request.Headers.Add("x-domain", "www.zapimoveis.com.br");
                request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
                request.Headers.Add("sec-ch-ua-mobile", "?0");
                request.Headers.Add("accept", "application/json, text/plain, */*");
                request.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \";Not A Brand\";v=\"99\"");

                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                WebHeaderCollection header = response.Headers;

                var encoding = UTF8Encoding.UTF8;

                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    try
                    {

                        int totalDevelopments = 0;
                        int totalSuperPremium = 0;

                        string responseText = reader.ReadToEnd();

                        JObject o = JObject.Parse(responseText);

                        var imoveisSearch = o["search"]["result"]["listings"];

                        var totalSearch = Convert.ToString(o["search"]["totalCount"]);


                        if (o["developments"] != null)
                            totalDevelopments = Convert.ToInt32(Convert.ToString(o["developments"]["search"]["totalCount"]));

                        if (o["superPremium"] != null)
                            totalSuperPremium = Convert.ToInt32(Convert.ToString(o["superPremium"]["search"]["totalCount"]));


                        if (totalSearch != "")
                            return Convert.ToInt32(totalSearch) + totalDevelopments + totalSuperPremium; ;
                    }
                    catch (Exception)
                    {

                        Console.WriteLine(string.Format("OBS: Faixa não coletada Tipo {0},  Faixa {1} a {2}", tipo, valorMin, valorMax));
                    }

                }

            }
            catch (Exception)
            {

                return GetTotal(siglaEstado, estado, tipoImovel, valorMin, valorMax, tipo, tipov3, usageTypes, subTipo);

            }

            return 0;
        }


        public void ColetarFaixa(string siglaEstado, string estado, int tipoImovel, string valorMin, string valorMax, string tipo, string from, string pagina, string tipov3, string usageTypes, string subTipo)
        {
            using (var db = new PexIMContext())
            {

                try
                {
                    string strTipoImovel = "";

                    if (tipoImovel == 1)
                        strTipoImovel = "SALE";
                    else
                        strTipoImovel = "RENTAL";

                    var url = "https://glue-api.zapimoveis.com.br/v2/listings?unitSubTypes=" + subTipo + "&unitTypes=" + tipo + "&usageTypes=" + usageTypes + "&unitTypesV3=" + tipov3 + "&priceMax=" + valorMax + "&priceMin=" + valorMin + "&business=" + strTipoImovel + "&categoryPage=RESULT&parentId=null&listingType=USED&size=24&from=" + from + "&page=" + pagina + "&includeFields=search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount)%2Cexpansion(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cnearby(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cpage%2CfullUriFragments%2Cdevelopments(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2CsuperPremium(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))%2Cowners(search(result(listings(listing(displayAddressType%2Camenities%2CusableAreas%2CconstructionStatus%2ClistingType%2Cdescription%2Ctitle%2CcreatedAt%2Cfloors%2CunitTypes%2CnonActivationReason%2CproviderId%2CpropertyType%2CunitSubTypes%2CunitsOnTheFloor%2ClegacyId%2Cid%2Cportal%2CunitFloor%2CparkingSpaces%2CupdatedAt%2Caddress%2Csuites%2CpublicationType%2CexternalId%2Cbathrooms%2CusageTypes%2CtotalAreas%2CadvertiserId%2CadvertiserContact%2CwhatsappNumber%2Cbedrooms%2CacceptExchange%2CpricingInfos%2CshowPrice%2Cresale%2Cbuildings%2CcapacityLimit%2Cstatus)%2Caccount(id%2Cname%2ClogoUrl%2ClicenseNumber%2CshowAddress%2ClegacyVivarealId%2ClegacyZapId%2Cminisite)%2Cmedias%2CaccountLink%2Clink))%2CtotalCount))&cityWiseStreet=1&developmentsSize=3&superPremiumSize=3&addressCountry=&addressState=" + estado + "&addressCity=&addressZone=&addressNeighborhood=&addressStreet=&addressAccounts=&addressType=&addressPointLat=&addressPointLon=&__zt=ama%3A";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    WebProxy myproxy = new WebProxy("163.172.48.109:15001");

                    myproxy.BypassProxyOnLocal = false;
                    request.Proxy = myproxy;
                    request.Timeout = 30000;
                    request.Headers.Add("postman-token", "71c850b8-7d01-1ab7-8ef8-8dcda8e27d6f");
                    request.Headers.Add("cache-control", "no-cache");
                    request.Headers.Add("x-domain", "www.zapimoveis.com.br");
                    request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
                    request.Headers.Add("sec-ch-ua-mobile", "?0");
                    request.Headers.Add("accept", "application/json, text/plain, */*");
                    request.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \";Not A Brand\";v=\"99\"");

                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    WebHeaderCollection header = response.Headers;

                    var encoding = UTF8Encoding.UTF8;

                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {

                        try
                        {
                            string responseText = reader.ReadToEnd();

                            JObject o = JObject.Parse(responseText);


                            if (o["developments"] != null)
                            {
                                var imoveisDevelopments = o["developments"]["search"]["result"]["listings"];
                                foreach (var item in imoveisDevelopments)
                                {
                                    try
                                    {
                                        ImoveisCapturados imovelCapturado = new ImoveisCapturados();
                                        imovelCapturado.Finalidade = usageTypes;
                                        var imovel = item["listing"];
                                        var medias = item["medias"];
                                        var link = item["link"];
                                        var account = item["account"];
                                        imovelCapturado.Anunciante = Convert.ToString(account["name"]).Replace("[", "").Replace("]", "");


                                        imovelCapturado.SiglaEstado = siglaEstado;
                                        imovelCapturado.TipoImovel = tipoImovel;

                                        imovelCapturado.Url = "https://www.zapimoveis.com.br" + Convert.ToString(link["href"]);
                                        var streetNumber = Convert.ToString(link["data"]["streetNumber"]);

                                        var urlImg = "";
                                        foreach (var media in medias)
                                        {
                                            urlImg += media["url"] + ";";
                                        }

                                        urlImg = urlImg.Replace("{action}/{width}x{height}", "fit-in/800x360");

                                        imovelCapturado.Imagens = urlImg;

                                        var descricao = Convert.ToString(imovel["description"]);
                                        var id = Convert.ToString(imovel["id"]);


                                        imovelCapturado.AreaPrivativa = Convert.ToString(imovel["usableArea"]);
                                        imovelCapturado.Descricao = descricao;

                                        imovelCapturado.Quartos = Convert.ToString(imovel["bedrooms"]).Replace("[", "").Replace("]", "");
                                        imovelCapturado.Banheiros = Convert.ToString(imovel["bathrooms"]).Replace("[", "").Replace("]", "");

                                        imovelCapturado.Garagens = Convert.ToString(imovel["parkingSpaces"]).Replace("[", "").Replace("]", "");
                                        imovelCapturado.AreaTotal = Convert.ToString(imovel["totalAreas"]).Replace("[", "").Replace("]", "").Replace("\"", "");

                                        var subTipoInterno = Convert.ToString(imovel["unitSubTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                        imovelCapturado.Tipo = Convert.ToString(imovel["unitTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                        if (subTipoInterno != "")
                                            imovelCapturado.Tipo = subTipoInterno;

                                        imovelCapturado.CodImobiliaria = 1;

                                        var address = imovel["address"];

                                        imovelCapturado.Cidade = Convert.ToString(address["city"]);
                                        imovelCapturado.Bairro = Convert.ToString(address["neighborhood"]);
                                        imovelCapturado.Cep = Convert.ToString(address["zipCode"]);
                                        imovelCapturado.Rua = Convert.ToString(address["street"]);


                                        imovelCapturado.Suites = Convert.ToString(imovel["suites"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                        var pricingInfos = imovel["pricingInfos"];

                                        foreach (var pricing in pricingInfos)
                                        {
                                            imovelCapturado.Iptu = Convert.ToString(pricing["yearlyIptu"]);
                                            imovelCapturado.Valor = Convert.ToString(pricing["price"]);
                                            imovelCapturado.Condominio = Convert.ToString(pricing["monthlyCondoFee"]);
                                        }




                                        db.ImoveisCapturados.Add(imovelCapturado);

                                    }
                                    catch (Exception)
                                    {

                                        Console.WriteLine("Erro");
                                        ColetarFaixa(siglaEstado, estado, tipoImovel, valorMin, valorMax, tipo, from, pagina, tipov3, usageTypes, subTipo);
                                    }
                                }
                            }

                            if (o["superPremium"] != null)
                            {
                                var imoveisSuperPremium = o["superPremium"]["search"]["result"]["listings"];
                                foreach (var item in imoveisSuperPremium)
                                {
                                    try
                                    {
                                        ImoveisCapturados imovelCapturado = new ImoveisCapturados();
                                        imovelCapturado.Finalidade = usageTypes;
                                        var imovel = item["listing"];
                                        var medias = item["medias"];
                                        var link = item["link"];
                                        var account = item["account"];
                                        imovelCapturado.Anunciante = Convert.ToString(account["name"]).Replace("[", "").Replace("]", "");


                                        imovelCapturado.SiglaEstado = siglaEstado;
                                        imovelCapturado.TipoImovel = tipoImovel;

                                        imovelCapturado.Url = "https://www.zapimoveis.com.br" + Convert.ToString(link["href"]);
                                        var streetNumber = Convert.ToString(link["data"]["streetNumber"]);

                                        var urlImg = "";
                                        foreach (var media in medias)
                                        {
                                            urlImg += media["url"] + ";";
                                        }

                                        urlImg = urlImg.Replace("{action}/{width}x{height}", "fit-in/800x360");

                                        imovelCapturado.Imagens = urlImg;

                                        var descricao = Convert.ToString(imovel["description"]);
                                        var id = Convert.ToString(imovel["id"]);


                                        imovelCapturado.AreaPrivativa = Convert.ToString(imovel["usableArea"]);
                                        imovelCapturado.Descricao = descricao;

                                        imovelCapturado.Quartos = Convert.ToString(imovel["bedrooms"]).Replace("[", "").Replace("]", "");
                                        imovelCapturado.Banheiros = Convert.ToString(imovel["bathrooms"]).Replace("[", "").Replace("]", "");

                                        imovelCapturado.Garagens = Convert.ToString(imovel["parkingSpaces"]).Replace("[", "").Replace("]", "");

                                        imovelCapturado.AreaTotal = Convert.ToString(imovel["totalAreas"]).Replace("[", "").Replace("]", "").Replace("\"", "");

                                        var subTipoInterno = Convert.ToString(imovel["unitSubTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                        imovelCapturado.Tipo = Convert.ToString(imovel["unitTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                        if (subTipoInterno != "")
                                            imovelCapturado.Tipo = subTipoInterno;

                                        imovelCapturado.CodImobiliaria = 1;

                                        var address = imovel["address"];

                                        imovelCapturado.Cidade = Convert.ToString(address["city"]);
                                        imovelCapturado.Bairro = Convert.ToString(address["neighborhood"]);
                                        imovelCapturado.Cep = Convert.ToString(address["zipCode"]);
                                        imovelCapturado.Rua = Convert.ToString(address["street"]);


                                        imovelCapturado.Suites = Convert.ToString(imovel["suites"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                        var pricingInfos = imovel["pricingInfos"];

                                        foreach (var pricing in pricingInfos)
                                        {
                                            imovelCapturado.Iptu = Convert.ToString(pricing["yearlyIptu"]);
                                            imovelCapturado.Valor = Convert.ToString(pricing["price"]);
                                            imovelCapturado.Condominio = Convert.ToString(pricing["monthlyCondoFee"]);
                                        }




                                        db.ImoveisCapturados.Add(imovelCapturado);

                                    }
                                    catch (Exception)
                                    {

                                        Console.WriteLine("Erro");
                                        ColetarFaixa(siglaEstado, estado, tipoImovel, valorMin, valorMax, tipo, from, pagina, tipov3, usageTypes, subTipo);
                                    }
                                }

                            }

                            var imoveis = o["search"]["result"]["listings"];

                            foreach (var item in imoveis)
                            {
                                try
                                {
                                    ImoveisCapturados imovelCapturado = new ImoveisCapturados();
                                    imovelCapturado.Finalidade = usageTypes;
                                    var imovel = item["listing"];
                                    var medias = item["medias"];
                                    var link = item["link"];
                                    var account = item["account"];
                                    imovelCapturado.Anunciante = Convert.ToString(account["name"]).Replace("[", "").Replace("]", "");


                                    imovelCapturado.SiglaEstado = siglaEstado;
                                    imovelCapturado.TipoImovel = tipoImovel;

                                    imovelCapturado.Url = "https://www.zapimoveis.com.br" + Convert.ToString(link["href"]);
                                    var streetNumber = Convert.ToString(link["data"]["streetNumber"]);

                                    var urlImg = "";
                                    foreach (var media in medias)
                                    {
                                        urlImg += media["url"] + ";";
                                    }

                                    urlImg = urlImg.Replace("{action}/{width}x{height}", "fit-in/800x360");

                                    imovelCapturado.Imagens = urlImg;

                                    var descricao = Convert.ToString(imovel["description"]);
                                    var id = Convert.ToString(imovel["id"]);


                                    imovelCapturado.AreaPrivativa = Convert.ToString(imovel["usableAreas"]);
                                    imovelCapturado.Descricao = descricao;

                                    imovelCapturado.Quartos = Convert.ToString(imovel["bedrooms"]).Replace("[", "").Replace("]", "");
                                    imovelCapturado.Banheiros = Convert.ToString(imovel["bathrooms"]).Replace("[", "").Replace("]", "");

                                    imovelCapturado.Garagens = Convert.ToString(imovel["parkingSpaces"]).Replace("[", "").Replace("]", "");
                                    imovelCapturado.AreaTotal = Convert.ToString(imovel["totalAreas"]).Replace("[", "").Replace("]", "").Replace("\"", "");

                                    var subTipoInterno = Convert.ToString(imovel["unitSubTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                    imovelCapturado.Tipo = Convert.ToString(imovel["unitTypes"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                    if (subTipoInterno != "")
                                        imovelCapturado.Tipo = subTipoInterno;

                                    imovelCapturado.CodImobiliaria = 1;

                                    var address = imovel["address"];

                                    imovelCapturado.Cidade = Convert.ToString(address["city"]);
                                    imovelCapturado.Bairro = Convert.ToString(address["neighborhood"]);
                                    imovelCapturado.Cep = Convert.ToString(address["zipCode"]);
                                    imovelCapturado.Rua = Convert.ToString(address["street"]);
                                    imovelCapturado.Localidade = Convert.ToString(address["zone"]);


                                    imovelCapturado.Suites = Convert.ToString(imovel["suites"]).Replace("[", "").Replace("]", "").Replace("\"", "").Trim();

                                    var pricingInfos = imovel["pricingInfos"];

                                    foreach (var pricing in pricingInfos)
                                    {
                                        imovelCapturado.Iptu = Convert.ToString(pricing["yearlyIptu"]);
                                        imovelCapturado.Valor = Convert.ToString(pricing["price"]);
                                        imovelCapturado.Condominio = Convert.ToString(pricing["monthlyCondoFee"]);
                                    }




                                    db.ImoveisCapturados.Add(imovelCapturado);

                                }
                                catch (Exception)
                                {

                                    Console.WriteLine("Erro");
                                    ColetarFaixa(siglaEstado, estado, tipoImovel, valorMin, valorMax, tipo, from, pagina, tipov3, usageTypes, subTipo);
                                }
                            }

                            db.SaveChanges();
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(string.Format("OBS: Faixa não coletada Tipo {0},  Faixa {1} a {2}", tipo, valorMin, valorMax));
                            ColetarFaixa(siglaEstado, estado, tipoImovel, valorMin, valorMax, tipo, from, pagina, tipov3, usageTypes, subTipo);
                        }
                    }
                }
                catch (Exception ex2)
                {
                    ColetarFaixa(siglaEstado, estado, tipoImovel, valorMin, valorMax, tipo, from, pagina, tipov3, usageTypes, subTipo);

                }
            }
        }
    }
}
