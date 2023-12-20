using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BT_WebAPI.Controllers
{
    [EnableCors(origins: "https://localhost:44320", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        // Controller methods not shown...
    }
    public class CustomerController : ApiController
    {
        //httpGet: Dùng để lấy thông tin khách hàng
        //1. Dịch vụ lấy thông tin của toàn bộ khách hàng
        [HttpGet]
        public List<tblKhach> GetCustomerLists()
        {
            DataDataContext dbCustomer = new DataDataContext();
            return dbCustomer.tblKhaches.ToList();
        }
        //2. Dịch vụ lấy thông tin khách hàng với 1 mã nào đó

        [HttpGet]
        public tblKhach GetCustomer(string id)
        {
            DataDataContext dbCustomer = new DataDataContext();
            return dbCustomer.tblKhaches.FirstOrDefault(x => x.Makhach == id);
        }

        //3. httpPost, thêm mới một khách hàng
        [HttpPost]
        public bool InsertCustomer(string id, string name, string address, string phoneNumber)
        {
            try
            {
                DataDataContext dbCustomer = new DataDataContext();
                tblKhach customer = new tblKhach();
                customer.Makhach = id;
                customer.TenKhach = name;
                customer.DiaChi = address;
                customer.DienThoai = phoneNumber;
                dbCustomer.tblKhaches.InsertOnSubmit(customer);
                dbCustomer.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //4. httpPut để chỉnh sửa thông tin khách hàng

        [HttpPut]
        public bool UpdateCustomer(string id, string name, string address, string phoneNumber)
        {
            try
            {
                DataDataContext dbCustomer = new DataDataContext();
                //Lấy mã khách
                tblKhach customer = dbCustomer.tblKhaches.FirstOrDefault(x => x.Makhach == id);
                if(customer == null) return false;
                customer.Makhach = id;
                customer.TenKhach = name;
                customer.DiaChi = address;
                customer.DienThoai = phoneNumber;
                dbCustomer.SubmitChanges(); //Xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }

        //5. httpDelete để xóa 1 khách hàng
        [HttpDelete]
        public bool DeleteCustomer(string id)
        {
            try
            {
                DataDataContext dbCustomer = new DataDataContext();
                //Lấy mã khách hàng cần xóa
                tblKhach customer = dbCustomer.tblKhaches.FirstOrDefault(x => x.Makhach == id);
                if(customer == null) return false;
                dbCustomer.tblKhaches.DeleteOnSubmit(customer);
                dbCustomer.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
