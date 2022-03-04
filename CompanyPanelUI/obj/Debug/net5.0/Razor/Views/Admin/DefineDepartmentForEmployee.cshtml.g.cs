#pragma checksum "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ba7b0c59b4cfe461bd9ca0ec7ffa8191d34b401"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_DefineDepartmentForEmployee), @"mvc.1.0.view", @"/Views/Admin/DefineDepartmentForEmployee.cshtml")]
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
#line 1 "D:\CompanyPanel\CompanyPanelUI\Views\_ViewImports.cshtml"
using CompanyPanelUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\CompanyPanel\CompanyPanelUI\Views\_ViewImports.cshtml"
using CompanyPanelUI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ba7b0c59b4cfe461bd9ca0ec7ffa8191d34b401", @"/Views/Admin/DefineDepartmentForEmployee.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4b964cc9676e9dd9414a58e3cd42d993044ca2d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_DefineDepartmentForEmployee : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<EntityLayer.Concrete.CustomUser>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
  
    ViewBag.Title = "title";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 8 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
 if(Model.Any())
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""card card-primary"">
              <div class=""card-header"">
                <h3 class=""card-title"">Bölüm ve Şirket Ataması Yapılmamış Personeller</h3>
              </div>
              
              <br/>
              <div class=""card"">
                        <div class=""card-header"">
                        <h3 class=""card-title"">Personeller</h3>
                        </div>
 
                        <div class=""card-body"">
                        <table class=""table table-bordered"">
                        <thead>
                        <tr>
                        <th style=""width: 10px"">#</th>
                        <th>Personelin Adı Soyadı</th>
                        <th>Personelin Maili</th>
                        <th>Personelin Telefonu</th>
                        <th>Personelin Firm Kaydı</th>
                        
                        </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 34 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                             foreach(var item  in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                           \r\n                        <td>");
#nullable restore
#line 38 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                       Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 39 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                       Write(item.NameSurname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 40 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                       Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 41 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                           Write(item.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 42 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                             if (item.FirmId == null)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td class=\"bg-danger\">Personelin firması yoktur. Lütfen personele firma tanımlayınız.</td>\r\n");
#nullable restore
#line 45 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td class=\"bg-success\">");
#nullable restore
#line 48 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                                                  Write(item.FirmId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 49 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        \r\n                        <td>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 1957, "\"", 2017, 2);
            WriteAttributeValue("", 1964, "/Admin/DepartmentRegisterForEmployee/?userId=", 1964, 45, true);
#nullable restore
#line 52 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
WriteAttributeValue("", 2009, item.Id, 2009, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning btn-sm\"><i class=\"fas fa-edit\"></i></a>\r\n                              \r\n                        </td>\r\n                       \r\n                        </tr>\r\n");
#nullable restore
#line 57 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"
                         }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                        </table>\r\n                        </div>\r\n\r\n\r\n                        </div>\r\n                        </div>\r\n");
#nullable restore
#line 65 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DefineDepartmentForEmployee.cshtml"

                      
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<EntityLayer.Concrete.CustomUser>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591