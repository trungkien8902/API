using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BT_WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public List<tblHang> GetSanPhams()
        {
            DataDataContext db = new DataDataContext();
            return db.tblHangs.ToList();
        }
        [HttpGet]
        public tblHang GetProduct(string id)
        {
            DataDataContext db = new DataDataContext();
            return db.tblHangs.FirstOrDefault(n => n.Mahang == id);
        }
        [Route("Search/{name}")]
        [HttpGet]
        public List<tblHang> GetSanPham(string name)
        {
            DataDataContext db = new DataDataContext();
            return db.tblHangs.Where(n => n.TenHang.Contains(name)).ToList();
        }
        [Route("TonKho")]
        [HttpGet]
        public List<tblHang> GetTonKho()
        {
            DataDataContext db = new DataDataContext();
            return db.tblHangs.Where(n => n.SoLuong > 0).ToList();
        }
        [HttpPost]
        public bool InsertNewProduct(string id, string name, string description, int PriceIn, int PriceOut, int number)
        {
            try
            {
                DataDataContext dBProduct = new DataDataContext();
                tblHang product = new tblHang();
                product.Mahang = id;
                product.TenHang = name;
                product.GhiChu = description;
                product.DonGiaNhap = PriceIn;
                product.DonGiaBan = PriceOut;
                product.SoLuong = number;
                dBProduct.tblHangs.InsertOnSubmit(product);
                dBProduct.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateProduct(string id, string name, string description, int PriceIn, int PriceOut, int number)
        {
            try
            {
                DataDataContext dBProduct = new DataDataContext();
                tblHang product = dBProduct.tblHangs.FirstOrDefault(n => n.Mahang == id);
                if (product == null) { return false; }
                product.Mahang = id;
                product.TenHang = name;
                product.GhiChu = description;
                product.DonGiaNhap = PriceIn;
                product.DonGiaBan = PriceOut;
                product.SoLuong = number;
                dBProduct.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteProduct(string id)
        {
            try
            {
                DataDataContext dBProduct = new DataDataContext();
                tblHang product = dBProduct.tblHangs.FirstOrDefault(n => n.Mahang == id);
                if (product == null) { return false; }
                dBProduct.tblHangs.DeleteOnSubmit(product);
                dBProduct.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
