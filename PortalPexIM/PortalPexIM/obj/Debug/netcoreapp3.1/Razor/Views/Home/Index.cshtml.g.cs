#pragma checksum "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9becfc43d99e6d086af4702b48436bfe8b313220"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\_ViewImports.cshtml"
using PortalPexIM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\_ViewImports.cshtml"
using PortalPexIM.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9becfc43d99e6d086af4702b48436bfe8b313220", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e423e120777f1824f081df0a25f792a133825725", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<style>
    .highcharts-figure, .highcharts-data-table table {
        min-width: 310px;
        max-width: 800px;
        margin: 1em auto;
    }

    #container {
        height: 400px;
    }

    .highcharts-data-table table {
        font-family: Verdana, sans-serif;
        border-collapse: collapse;
        border: 1px solid #EBEBEB;
        margin: 10px auto;
        text-align: center;
        width: 100%;
        max-width: 500px;
    }

    .highcharts-data-table caption {
        padding: 1em 0;
        font-size: 1.2em;
        color: #555;
    }

    .highcharts-data-table th {
        font-weight: 600;
        padding: 0.5em;
    }

    .highcharts-data-table td, .highcharts-data-table th, .highcharts-data-table caption {
        padding: 0.5em;
    }

    .highcharts-data-table thead tr, .highcharts-data-table tr:nth-child(even) {
        background: #f8f8f8;
    }

    .highcharts-data-table tr:hover {
        background: #f1f7ff;
    }
    /*CSS FILT");
            WriteLiteral(@"RO*/
    .boxFiltro {
        position: absolute;
        width: 50px;
        height: 50px;
        background-color: lightgray;
        right: 0px;
        top: 40%;
        border-bottom-left-radius: 5px;
        border-top-left-radius: 5px;
        padding: 13px;
        color: #345675;
        z-index: 9;
        cursor:pointer;
    }
    .funcFiltroLista {
        position: absolute;
        display:none;
        width: 355px;
        z-index: 9;
        top: 27%;
        right: 45px;
        background-color: white;
        border-radius: 10px;
        border: 5px solid lightgray;
    }
    .fundoFiltroLista{
        position: fixed;
        width: 100%;
        height: 100%;
        z-index: 8;
    }
    .dropdown-menu.show {
        height: 200px;
    }
    /*FIM CSS FILTRO*/
</style>
<div class=""boxFiltro"">
    <i class=""material-icons"">
        filter_alt
    </i>
</div>
<div class=""fundoFiltroLista""></div>
<div class=""funcFiltroLista"">
    <div class=""co");
            WriteLiteral("l-md-12\">\r\n        <div class=\"form-group col-md-12\">\r\n            <select multiple id=\"cmbCidade\"  class=\"form-control selectpicker\" data-style=\"btn btn-link\" title=\"Selecione a Cidade\">\r\n");
#nullable restore
#line 91 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                 foreach(var itens in Model.Cidades) 
                { 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9becfc43d99e6d086af4702b48436bfe8b3132205715", async() => {
#nullable restore
#line 93 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                                      Write(itens);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 93 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                       WriteLiteral(itens);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 94 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                \r\n            </select>\r\n        </div>\r\n        <div class=\"form-group col-md-12\">\r\n            <select multiple id=\"cmbBairro\" class=\"form-control selectpicker\" data-style=\"btn btn-link\" title=\"Selecione o Bairro\">\r\n");
#nullable restore
#line 100 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                 foreach (var itens in Model.Bairros)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9becfc43d99e6d086af4702b48436bfe8b3132208282", async() => {
#nullable restore
#line 102 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                                      Write(itens);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 102 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                       WriteLiteral(itens);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 103 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </div>\r\n        <div class=\"form-group col-md-12\">\r\n            <select multiple id=\"cmbTipo\" class=\"form-control selectpicker\" data-style=\"btn btn-link\" title=\"Selecione o Tipo\">\r\n");
#nullable restore
#line 108 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                 foreach(var itens in Model.Tipos) 
                { 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9becfc43d99e6d086af4702b48436bfe8b31322010827", async() => {
#nullable restore
#line 110 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                                      Write(itens);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 110 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                       WriteLiteral(itens);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 111 "C:\Users\WINDOWS\Documents\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </select>
        </div>
        <div class=""col-md-6"" style=""float:left;"">
            <button type=""button"" class=""btn btn-default"" onclick=""limpaFiltro()"">Limpar</button>
        </div>
        <div class=""col-md-6"" style=""float:left;"">
            <button type=""button"" class=""btn btn-primary"" onclick=""preencheFiltro()"">Pesquisar</button>
        </div>
    </div>
</div>

<div class=""row"">
    <div class=""col-md-12"">
        <div class=""card"">
            <div class=""card-header card-header-info  card-header-icon"">
                <div class=""card-icon"">
                    <i class=""material-icons"">
                        trending_up
                    </i>
                    
                </div>
            </div>
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-md-9"">
                        <h4 class=""card-title"">Quantidade de ofertas</h4>
                    </div>
                    <div class=""col-");
            WriteLiteral(@"md-3"">
                        <div class=""togglebutton radio"">
                            <label>
                                <span style=""font-weight:bold""></span>Venda
                                <input type=""checkbox"" id=""cmbAbsoluto"">
                                <span class=""toggle""></span>Locação
                            </label>
                        </div>
                    </div>
                </div>
                <div class=""row"" id=""graficoEvolutivo"">

                </div>
            </div>
        </div>
    </div>
</div>

<div class=""row"" id=""boxGraficos"">

    <div class=""col-md-6"">
        <div class=""card"">
            <div class=""card-header card-header-icon card-header-info"">
                <div class=""card-icon"">
                    <i class=""material-icons"">room</i>
                </div>
            </div>
            <div class=""card-body"">
                <figure class=""highcharts-figure"">
                    <div id=""graficoCidad");
            WriteLiteral(@"es""></div>
                </figure>
            </div>
        </div>
    </div>

    <div class=""col-md-6"">
        <div class=""card"">
            <div class=""card-header card-header-icon card-header-info"">
                <div class=""card-icon"">
                    <i class=""material-icons"">south_east</i>
                </div>

            </div>
            <div class=""card-body"">
                <figure class=""highcharts-figure"">
                    <div id=""graficoBairros""></div>
                </figure>
            </div>
        </div>
    </div>

    <div class=""col-md-6"">
        <div class=""card"">
            <div class=""card-header card-header-icon card-header-info"">
                <div class=""card-icon"">
                    <i class=""material-icons"">language</i>
                </div>
            </div>
            <div class=""card-body"">
                <figure class=""highcharts-figure"">
                    <div id=""graficoTipos""></div>
                </figure");
            WriteLiteral(@">
            </div>
        </div>
    </div>

    <div class=""col-md-6"">
        <div class=""card"">
            <div class=""card-header card-header-icon card-header-info"">
                <div class=""card-icon"">
                    <i class=""material-icons"">garage</i>
                </div>
            </div>
            <div class=""card-body"">
                <figure class=""highcharts-figure"">
                    <div id=""graficoGaragens""></div>
                </figure>
            </div>
        </div>
    </div>


</div>




<script src=""https://code.highcharts.com/highcharts.js""></script>
<script src=""https://code.highcharts.com/modules/data.js""></script>
<script src=""https://code.highcharts.com/modules/drilldown.js""></script>
<script src=""https://code.highcharts.com/modules/exporting.js""></script>
<script src=""https://code.highcharts.com/modules/export-data.js""></script>
<script src=""https://code.highcharts.com/modules/accessibility.js""></script>


<script>

    $(d");
            WriteLiteral(@"ocument).ready(function () {       
        Pesquisar();
    });  

    var cidades = [];
    var bairros = [];
    var tipos = [];
    var tipoimovel = 2;

    $('.boxFiltro').on('click', function () {
        $('.fundoFiltroLista').show();
        $('.funcFiltroLista').toggle();
    });
    $('.fundoFiltroLista').on('click', function () {
        $('.fundoFiltroLista').hide();
        $('.funcFiltroLista').hide();
    });
    $('#cmbCidade').on('change', function () {
        cidades = $('#cmbCidade').val();
        $.ajax({
            type: ""POST"",
            contentType: ""application/json; charset=utf-8"",
            url: ""/Home/Bairros"",
            data: JSON.stringify({ cidades }),

            dataType: ""json"",
            success: function (result) {
                console.log(result);
            }
        });
    });
    
    function limpaFiltro() {

        cidades = [];
        bairros = [];
        tipos = [];

        $('#cmbCidade').val('');
        $");
            WriteLiteral(@"('#cmbCidade').selectpicker(""refresh"");
        $('#cmbBairro').val('');
        $('#cmbBairro').selectpicker(""refresh"");
        $('#cmbTipo').val('');
        $('#cmbTipo').selectpicker(""refresh"");

        Pesquisar();
    }

    function preencheFiltro() {
        $('.fundoFiltroLista').hide();
        $('.funcFiltroLista').hide();

        cidades = $('#cmbCidade').val();
        bairros = $('#cmbBairro').val();
        tipos = $('#cmbTipo').val();

        Pesquisar();
    }
    

    function Pesquisar() {
        if($(""#cmbAbsoluto"").is(':checked'))
            tipoimovel = 2;
        else
            tipoimovel = 1;

        cidades = $('#cmbCidade').val();
        bairros = $('#cmbBairro').val();
        tipos = $('#cmbTipo').val();

        CarregarGraficoMeses();
        CarregarGraficoTipos();
        CarregarGraficoCidades();
        CarregarGraficoBairros();
        CarregarGraficoGaragens();
    }


    function CarregarGraficoMeses() {
     

        ");
            WriteLiteral(@"var processed_json = new Array();
        $.ajax({
            type: ""POST"",
            contentType: ""application/json; charset=utf-8"",
            url: ""/Home/GetEvolutivo"",
            data: JSON.stringify({ cidades: cidades, Bairros: bairros, Tipos: tipos, TipoImovel: tipoimovel }),

            dataType: ""json"",
            success: function (result) {

                var processed_json = [];

                for (i = 0; i < result.length; i++) {
                    processed_json.push([result[i].key, result[i].key, result[i].value]);
                }

                var dadosPivot = getPivotArray(processed_json, 0, 1, 2);

                var categories = dadosPivot;
                categories[0].shift();

                var dadosGrafico = [];

                for (i = 1; i < dadosPivot.length; i++) {

                    dadosGrafico.push({
                        name: dadosPivot[i].shift(),
                        data: dadosPivot[i]
                    });
          ");
            WriteLiteral(@"      }

                CarregarGrafico(""graficoEvolutivo"", categories[0], dadosGrafico, ""Evolutivo de ofertas"", ""column"", ""12"");

            },
            error: function (err) {
            }
        });

    }

    function CarregarGraficoCidades() {
        var processed_json = new Array();

        var processed_json = new Array();
        $.ajax({
            type: ""POST"",
            contentType: ""application/json; charset=utf-8"",
            url: ""/Home/GetCidades"",
            data: JSON.stringify({ cidades, bairros, tipos, tipoimovel }),

            dataType: ""json"",
            success: function (result) {

                var processed_json = [];

                var value = 0;
                for (i = 0; i < result.length; i++) {
                    var key = result[i].key;

                    if (i >= 7) {
                        var key = ""OUTRAS"";
                        value += result[i].value;
                        processed_json.push([key, key, value");
            WriteLiteral(@"]);
                    }
                    else
                        processed_json.push([result[i].key, result[i].key, result[i].value]);
                }

                var dadosPivot = getPivotArray(processed_json, 0, 1, 2);

                var categories = dadosPivot;
                categories[0].shift();

                var dadosGrafico = [];

                for (i = 1; i < dadosPivot.length; i++) {

                    dadosGrafico.push({
                        name: dadosPivot[i].shift(),
                        data: dadosPivot[i]
                    });
                }

                CarregarGrafico(""graficoCidades"", categories[0], dadosGrafico, ""Oferta por cidades"", ""bar"", ""6"");
            },
            error: function (err) {
            }
        });
    }

    function CarregarGraficoBairros() {
        var processed_json = new Array();

        $.ajax({
            type: ""POST"",
            contentType: ""application/json; charset=utf-8"",
    ");
            WriteLiteral(@"        url: ""/Home/GetBairros"",
            data: JSON.stringify({ cidades, bairros, tipos, tipoimovel }),

            dataType: ""json"",
            success: function (result) {

                var processed_json = [];

                var value = 0;
                for (i = 0; i < result.length; i++) {
                    var key = result[i].key;

                    if (i >= 7) {
                        var key = ""OUTROS"";
                        value += result[i].value;
                        processed_json.push([key, key, value]);
                    }
                    else
                        processed_json.push([result[i].key, result[i].key, result[i].value]);
                }

                var dadosPivot = getPivotArray(processed_json, 0, 1, 2);

                var categories = dadosPivot;
                categories[0].shift();

                var dadosGrafico = [];

                for (i = 1; i < dadosPivot.length; i++) {

                    dadosGrafi");
            WriteLiteral(@"co.push({
                        name: dadosPivot[i].shift(),
                        data: dadosPivot[i]
                    });
                }

                CarregarGrafico(""graficoBairros"", categories[0], dadosGrafico, ""Oferta por bairros"", ""bar"", ""6"");
            },
            error: function (err) {
            }
        });
    }

    function CarregarGraficoTipos() {
        var processed_json = new Array();

        var processed_json = new Array();
        $.ajax({
            type: ""POST"",
            contentType: ""application/json; charset=utf-8"",
            url: ""/Home/GetTipos"",
            data: JSON.stringify({ cidades, bairros, tipos, tipoimovel }),

            dataType: ""json"",
            success: function (result) {

                var processed_json = [];

                var value = 0;
                for (i = 0; i < result.length; i++) {
                    var key = result[i].key;

                    if (i >= 7) {
                        var");
            WriteLiteral(@" key = ""OUTROS"";
                        value += result[i].value;
                        processed_json.push([key, key, value]);
                    }
                    else
                        processed_json.push([result[i].key, result[i].key, result[i].value]);
                }


                var dadosPivot = getPivotArray(processed_json, 0, 1, 2);

                var categories = dadosPivot;
                categories[0].shift();

                var dadosGrafico = [];

                for (i = 1; i < dadosPivot.length; i++) {

                    dadosGrafico.push({
                        name: dadosPivot[i].shift(),
                        data: dadosPivot[i]
                    });
                }

                CarregarGrafico(""graficoTipos"", categories[0], dadosGrafico, ""Tipo de imóvel"", ""bar"", ""6"");
            },
            error: function (err) {
            }
        });
    }

    function CarregarGraficoGaragens() {
     
        var processed_");
            WriteLiteral(@"json = new Array();

        $.ajax({
            type: ""POST"",
            contentType: ""application/json; charset=utf-8"",
            url: ""/Home/GetGaragens"",
            data: JSON.stringify({ cidades, bairros, tipos, tipoimovel }),

            dataType: ""json"",
            success: function (result) {

                CarregarGraficoPizza(""graficoGaragens"", ""Garagens"", result);
            },
            error: function (err) {
            }
        });
    }

    function CarregarGrafico(id, categories, dadosGrafico, titulo, tipo, colmd) {


        Highcharts.chart(id, {
            chart: {
                type: tipo,
                height: 400
            },
            title: {
                text: titulo
            },
            legend: {
                itemStyle: {
                    fontSize: '10px'
                },
                enabled: false
            },
            yAxis: {
                title: {
                    text: ''
               ");
            WriteLiteral(@" }
            },
            //yAxis: {
            //   // min: 10,
            //    startOnTick: false
            //},
            plotOptions: {
                series: {
                    pointWidth: 35
                }
            },

            xAxis: {
                categories: categories
            },
            series: dadosGrafico
        });
    }

    function CarregarGraficoPizza(id, titulo, dados) {

    

        var dadosProcessados = [];

        $.each(dados, function (i, obj) {
            var agregador = { name: obj.key, y: obj.value }

            var resultObject = search(obj.key, dadosProcessados);

            if (!resultObject) {
                dadosProcessados.push(agregador)
            }
            else {
                resultObject.y += (obj.value)
            }

        })

        Highcharts.chart(id, {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                pl");
            WriteLiteral(@"otShadow: false,
                type: 'pie'
            },
            title: {
                text: titulo
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                    }
                }
            },
            series: [{
                name: '',
                colorByPoint: true,
                data: dadosProcessados
            }]
        });
    }


    function getPivotArray(dataArray, rowIndex, colIndex, dataIndex) {
        //Code from https://techbrij.com
        var result = {}, ret = [];
        var newCols = [];
        for (var i = 0; i < dataArray.length; i++) {

            if (!result");
            WriteLiteral(@"[dataArray[i][rowIndex]]) {
                result[dataArray[i][rowIndex]] = {};
            }
            result[dataArray[i][rowIndex]][dataArray[i][colIndex]] = dataArray[i][dataIndex];

            //To get column names
            if (newCols.indexOf(dataArray[i][colIndex]) == -1) {
                newCols.push(dataArray[i][colIndex]);
            }
        }

        // newCols.sort();
        var item = [];

        //Add Header Row
        item.push('Item');
        item.push.apply(item, newCols);
        ret.push(item);

        //Add content
        for (var key in result) {
            item = [];
            item.push(key);
            for (var i = 0; i < newCols.length; i++) {
                item.push(result[key][newCols[i]] || 0);
            }
            ret.push(item);
        }
        return ret;
    }

    function search(nameKey, myArray) {
        for (var i = 0; i < myArray.length; i++) {
            if (myArray[i].name === nameKey) {
                re");
            WriteLiteral("turn myArray[i];\r\n            }\r\n        }\r\n    }\r\n</script>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
