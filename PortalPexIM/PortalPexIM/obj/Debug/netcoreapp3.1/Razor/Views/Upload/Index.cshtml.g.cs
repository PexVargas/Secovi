#pragma checksum "C:\Users\vinic\OneDrive\Documentos\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Upload\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68937b91285cfef5bbafebaa3523074f3eff305b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Upload_Index), @"mvc.1.0.view", @"/Views/Upload/Index.cshtml")]
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
#line 1 "C:\Users\vinic\OneDrive\Documentos\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\_ViewImports.cshtml"
using PortalPexIM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\vinic\OneDrive\Documentos\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\_ViewImports.cshtml"
using PortalPexIM.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68937b91285cfef5bbafebaa3523074f3eff305b", @"/Views/Upload/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e423e120777f1824f081df0a25f792a133825725", @"/Views/_ViewImports.cshtml")]
    public class Views_Upload_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<PortalPexIM.ViewModel.FiltroEstados>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<style>\r\n</style>\r\n\r\n\r\n<div class=\"row \">\r\n    <div class=\"col-md-12 quadro\">\r\n        <h3 id=\"p1\">Upload</h3>\r\n        <br />\r\n    </div>\r\n</div>\r\n\r\n");
#nullable restore
#line 15 "C:\Users\vinic\OneDrive\Documentos\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Upload\Index.cshtml"
 using (Html.BeginForm("Importar", "upload", FormMethod.Post, new { enctype = "multipart/form-data", id = "myform" }))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""modal-content"">
        <div class=""modal-header"">
            <h5 class=""modal-title"" id=""exampleModalLabel"">Importdador de bases</h5>

            <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                <span aria-hidden=""true"">&times;</span>
            </button>
        </div>
        <div class=""modal-body"">
            <div class=""form-group"">
                <label class=""label-control"">Datetime Picker</label>
                <input type=""text"" class=""form-control datetimepicker"" id=""periodo"" name=""periodo""");
            BeginWriteAttribute("value", " value=\"", 910, "\"", 918, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
            </div>
            <input type=""file"" id=""arquivo"" name=""arquivo"">
        </div>
        <div class=""modal-footer"">
            <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Cancelar</button>
            <button type=""submit"" class=""btn btn-primary"">Salvar</button>
        </div>
    </div>
");
#nullable restore
#line 37 "C:\Users\vinic\OneDrive\Documentos\GitHub\Secovi\PortalPexIM\PortalPexIM\Views\Upload\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<script>

    $(document).ready(function () {

        $('.datetimepicker').datetimepicker({
            format: 'MM/YYYY',
            icons: {
                time: ""fa fa-clock-o"",
                date: ""fa fa-calendar"",
                up: ""fa fa-chevron-up"",
                down: ""fa fa-chevron-down"",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });
    })
   
</script>







");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<PortalPexIM.ViewModel.FiltroEstados>> Html { get; private set; }
    }
}
#pragma warning restore 1591
