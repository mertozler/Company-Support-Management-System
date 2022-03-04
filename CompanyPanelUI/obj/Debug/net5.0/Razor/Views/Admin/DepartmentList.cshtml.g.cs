#pragma checksum "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "325931eb88f99ae9150c4b84f59559fda6626428"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_DepartmentList), @"mvc.1.0.view", @"/Views/Admin/DepartmentList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"325931eb88f99ae9150c4b84f59559fda6626428", @"/Views/Admin/DepartmentList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4b964cc9676e9dd9414a58e3cd42d993044ca2d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_DepartmentList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<EntityLayer.Concrete.Department>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
  
    ViewBag.Title = "DepartmentList";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<br/>\r\n");
#nullable restore
#line 8 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
 if (Model.Count != 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""card card-primary"">
        <div class=""card-header"">
            <h3 class=""card-title"">Kayıtlı Bölümler</h3>
        </div>

        <br />
        <div class=""card"">
            
            <div class=""card-body"">
                <table class=""table table-bordered"">
                    <thead>
                        <tr>
                            <th style=""width: 10px"">#</th>
                            <th>Bölüm Adı</th>
                            <th>Bölüm Açıklaması</th>
                            <th>Bölüm Varsayılan Durumu</th>
                            <th>Bölüm Düzenle</th>
                            <th>Bölümü Sil</th>
                            <th>Bölümü Varsayılan Yap</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 32 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                         foreach (var item in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n\r\n                                <td>");
#nullable restore
#line 36 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                               Write(item.DepartmentId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 37 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                               Write(item.DepartmentName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 38 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                               Write(item.DepartmentAbout);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 39 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                                 if (item.DepartmentisDefault == false)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td class=\"bg-danger\">Departman varsayılan değil. (Eğer varsayılan yapmak istiyorsanız sağ taraftaki + simgesine basınız)</td>\r\n");
#nullable restore
#line 42 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td class=\"bg-success\">Departman varsayılan durumda</td>\r\n");
#nullable restore
#line 46 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 1897, "\"", 1944, 2);
            WriteAttributeValue("", 1904, "/Admin/EditDepartment/", 1904, 22, true);
#nullable restore
#line 48 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
WriteAttributeValue("", 1926, item.DepartmentId, 1926, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning btn-sm\"><i class=\"fas fa-edit\"></i></a>\r\n                                </td>\r\n                                <td>\r\n                                    \r\n                                                                    <a");
            BeginWriteAttribute("href", " href=\"", 2195, "\"", 2244, 2);
            WriteAttributeValue("", 2202, "/Admin/DeleteDepartment/", 2202, 24, true);
#nullable restore
#line 52 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
WriteAttributeValue("", 2226, item.DepartmentId, 2226, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning btn-sm\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2276, "\"", 2463, 20);
            WriteAttributeValue("", 2286, "return", 2286, 6, true);
            WriteAttributeValue(" ", 2292, "confirm(\'", 2293, 10, true);
#nullable restore
#line 52 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
WriteAttributeValue("", 2302, item.DepartmentName, 2302, 20, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2322, "isimli", 2323, 7, true);
            WriteAttributeValue(" ", 2329, "bölümü", 2330, 7, true);
            WriteAttributeValue(" ", 2336, "silmek", 2337, 7, true);
            WriteAttributeValue(" ", 2343, "istediğinden", 2344, 13, true);
            WriteAttributeValue(" ", 2356, "emin", 2357, 5, true);
            WriteAttributeValue(" ", 2361, "misin?", 2362, 7, true);
            WriteAttributeValue(" ", 2368, "Bu", 2369, 3, true);
            WriteAttributeValue(" ", 2371, "bölüme", 2372, 7, true);
            WriteAttributeValue(" ", 2378, "kayıtlı", 2379, 8, true);
            WriteAttributeValue(" ", 2386, "personellerin", 2387, 14, true);
            WriteAttributeValue(" ", 2400, "hesapları", 2401, 10, true);
            WriteAttributeValue(" ", 2410, "silinecektir.", 2411, 14, true);
            WriteAttributeValue(" ", 2424, "Bu", 2425, 3, true);
            WriteAttributeValue(" ", 2427, "sorumluluğu", 2428, 12, true);
            WriteAttributeValue(" ", 2439, "kabul", 2440, 6, true);
            WriteAttributeValue(" ", 2445, "ediyor", 2446, 7, true);
            WriteAttributeValue(" ", 2452, "musunuz?\')", 2453, 11, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-trash\"></i></a>\r\n                                </td>\r\n                                <td>\r\n                                     <a");
            BeginWriteAttribute("href", " href=\"", 2615, "\"", 2673, 2);
            WriteAttributeValue("", 2622, "/Admin/SetDepartmentDefaultValue/", 2622, 33, true);
#nullable restore
#line 55 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
WriteAttributeValue("", 2655, item.DepartmentId, 2655, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning btn-sm\"><i class=\"fas fa-plus\"></i></a>\r\n                                </td>\r\n                                \r\n\r\n                            </tr>\r\n");
#nullable restore
#line 60 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n\r\n\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 68 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"


}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>Departman kaydı bulunmamaktadır.</p>\r\n");
#nullable restore
#line 74 "D:\CompanyPanel\CompanyPanelUI\Views\Admin\DepartmentList.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<EntityLayer.Concrete.Department>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591