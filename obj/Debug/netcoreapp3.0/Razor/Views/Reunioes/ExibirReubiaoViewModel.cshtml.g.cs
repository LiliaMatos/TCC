#pragma checksum "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dba4644a7a744c97ba648f6a0e049ad9f73b71a1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reunioes_ExibirReubiaoViewModel), @"mvc.1.0.view", @"/Views/Reunioes/ExibirReubiaoViewModel.cshtml")]
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
#line 1 "C:\TCC\Views\_ViewImports.cshtml"
using PortariaInteligente;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\TCC\Views\_ViewImports.cshtml"
using PortariaInteligente.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dba4644a7a744c97ba648f6a0e049ad9f73b71a1", @"/Views/Reunioes/ExibirReubiaoViewModel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99dc8ae49a97660bf45662d5195dbbf89785e134", @"/Views/_ViewImports.cshtml")]
    public class Views_Reunioes_ExibirReubiaoViewModel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortariaInteligente.ViewModels.ReuniaoViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml"
  
    ViewData["Title"] = "Exibir";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 7 "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml"
Write(Model.Reuniao.ReuniaoNome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n<ul>\r\n");
#nullable restore
#line 9 "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml"
     foreach (var Visitante in Model.listaVisitantes)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>");
#nullable restore
#line 11 "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml"
       Write(Visitante.VisitanteNome);

#line default
#line hidden
#nullable disable
            WriteLiteral("  - ");
#nullable restore
#line 11 "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml"
                                   Write(Visitante.VisitanteEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </li>\r\n");
#nullable restore
#line 12 "C:\TCC\Views\Reunioes\ExibirReubiaoViewModel.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortariaInteligente.ViewModels.ReuniaoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591