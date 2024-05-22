using System.Web.Mvc;

namespace WebCuoiKy.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminLogin",
                "Admin",
                new {Controller = "Admin" ,action = "DangNhap", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminTrangChu",
                "Admin/trang-chu",
                new { Controller = "Admin", action = "TrangChu", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminSlider",
                "Admin/Slider",
                new { Controller = "Admin", action = "Slider", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminThemSlider",
                "Admin/Them-Slider",
                new { Controller = "Admin", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminDanhMuc",
                "Admin/DanhMuc",
                new { Controller = "DanhMuc", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminThemDanhMuc",
                "Admin/Themdanhmuc",
                new { Controller = "DanhMuc", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminSanPham",
                "Admin/SanPham",
                new { Controller = "SanPham", action = "SanPham", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminThemSanPham",
                "Admin/Themsanpham",
                new { Controller = "SanPham", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminDonHang",
                "Admin/DonHang",
                new { Controller = "Admin", action = "DonHang", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminSuaDonHang",
                "Admin/SuaDonHang",
                new { Controller = "Admin", action = "SuaDonHang", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminSuaSanPham",
                "Admin/Suasanpham/{id}",
                new { Controller = "SanPham", action = "Edit", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminSuaDanhMuc",
                "Admin/Suadanhmuc/{id}",
                new { Controller = "DanhMuc", action = "Edit", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminSuaSlider",
                "Admin/Sua-Slider/{id}",
                new { Controller = "Admin", action = "Edit", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminChiTiet",
                "Chitietdonhang/{id}",
                new { Controller = "Admin", action = "Chitietdonhang", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}