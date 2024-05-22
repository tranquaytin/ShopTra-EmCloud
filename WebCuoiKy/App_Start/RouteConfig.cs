using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCuoiKy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Liên Hệ",
                url: "lien-he",
                defaults: new { controller = "Home", action = "LienHe", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Giỏ Hàng",
                url: "gio-hang",
                defaults: new { controller = "GioHang", action = "GioHang", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Đăng Nhập",
                url: "dang-nhap",
                defaults: new { controller = "KhachHang", action = "DangNhap", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Đăng Ký",
                url: "dang-ky",
                defaults: new { controller = "KhachHang", action = "DangKy", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Đặt hàng",
                url: "dat-hang",
                defaults: new { controller = "GioHang", action = "DatHang", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Xác Nhận Đặt hàng",
                url: "xac-nhan-dat-hang",
                defaults: new { controller = "GioHang", action = "XacNhanDatHang", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "Tìm Kiếm",
                url: "tim-kiem",
                defaults: new { controller = "TimKiem", action = "KQTimKiem", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );


            routes.MapRoute(
                name: "San Pham",
                url: "chi-tiet-san-pham/{id}",
                defaults: new { controller = "Home", action = "ChiTietSanPham", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );
            routes.MapRoute(
                name: "San Pham Theo Danh Mục",
                url: "san-pham/{id}",
                defaults: new { controller = "Home", action = "SanPhamTheoDanhMuc", id = UrlParameter.Optional }
                , namespaces: new[] { "WebCuoiKy.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebCuoiKy.Controllers" }
            );
        }
    }
}
