#pragma checksum "C:\Users\Admin\Desktop\ASPNETCoreWithReact\Secovi\PortalPexIM\PortalPexIM\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64b06bbc08bd7a6a33816ea38bf62cb120587928"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
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
#line 1 "C:\Users\Admin\Desktop\ASPNETCoreWithReact\Secovi\PortalPexIM\PortalPexIM\Views\_ViewImports.cshtml"
using PortalPexIM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Desktop\ASPNETCoreWithReact\Secovi\PortalPexIM\PortalPexIM\Views\_ViewImports.cshtml"
using PortalPexIM.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64b06bbc08bd7a6a33816ea38bf62cb120587928", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e423e120777f1824f081df0a25f792a133825725", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Admin\Desktop\ASPNETCoreWithReact\Secovi\PortalPexIM\PortalPexIM\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 4 "C:\Users\Admin\Desktop\ASPNETCoreWithReact\Secovi\PortalPexIM\PortalPexIM\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<div class=""file-loading"">
    <input id=""input-pa"" name=""input-pa []"" type=""arquivo"" múltiplo>
</div>
<script>
$ ( ""# input-pa"" ). fileinput ({
    uploadUrl : ""/ file-upload-batch / 1"" ,
    uploadAsync : false ,
    minFileCount : 2 ,
    maxFileCount : 5 ,
    overwriteInitial : false ,
    initialPreview : [
        // DADOS DE IMAGEM
       'https://picsum.photos/id/700/1920/1080' ,
        // MARCAÇÃO DE IMAGEM EM BRUTO
        '<img src = ""https://picsum.photos/id/701/1920/1080"" class = ""kv-preview-data file-preview-image"">' ,
        // PDF DATA
        'https://kartik-v.github.io/bootstrap-fileinput-samples/samples/pdf-sample.pdf' ,
        // DADOS DE VÍDEO
        ""https://kartik-v.github.io/bootstrap-fileinput-samples/samples/small.mp4""
    ],
    initialPreviewAsData : true , // padroniza a marcação
    initialPreviewFileType : 'imagem' , // imagem é o padrão e pode ser substituído na configuração abaixo
    initialPreviewConfig : [
        { caption : ""Business-1");
            WriteLiteral(@".jpg"" , size : 762980 , url : ""/ site / file-delete"" , chave : 8 },
        { previewAsData : false , size : 823782 , caption : ""Business-2.jpg"" , url : ""/ site / file-delete"" , chave : 9 },
        { type : ""pdf"" , size : 8000 , caption : ""PDF-Sample.pdf"" , url : ""/ file-upload-batch / 2"" , chave : 10 },
        { type : ""video"" , size : 375000 , filetype : ""video / mp4"" , legenda : ""KrajeeSample.mp4"" , url : ""/ file-upload-batch / 2"" , chave : 11 }
    ],
    uploadExtraData : {
        img_key : ""1000"" ,
        img_keywords : ""feliz, natureza""
    }
}). on ( 'filesorted' , function ( e , params ) {
    console . log ( 'Arquivo classificado params' , params );
}). on ( 'fileuploaded' , função ( e , params ) {
    console . log ( 'Arquivo carregado params' , params );
});
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
