using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace K63apiWeb.API
{
    public class K63Controller : ApiController
    {
        String khoaBaoMat = "sfhjkshjwqhiurwqhrhqkjwbrhjsfd,sdnfjksdhuimfweuhrewjjkfbndsbfmndsb";

        // GET api/<controller>
        public String Get(String v1, String v2)
        {
            return v1 + " AND " + v2;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value " + id;
        }

        [HttpGet]
        public ModelNguoiDung DangNhap(String email, string password)
        {
            String connectionString = "Data Source=.\\MSSQL2014; initial catalog=LTMK63DB; user id=K63Mobile; password=12345678;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * From tbNguoiDung Where Email=N'" + email + "' And Password=N'" + password + "'", con);
            DataTable tb = new DataTable();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                tb.Load(dr, LoadOption.OverwriteChanges);
                con.Close();
            }
            catch
            { }
            if(tb != null && tb.Rows.Count > 0)
            {
                ModelNguoiDung m = new ModelNguoiDung();
                m.Email = tb.Rows[0]["Email"].ToString();
                m.Password = tb.Rows[0]["Password"].ToString();
                m.Active = tb.Rows[0]["Active"].ToString();
                m.ID = tb.Rows[0]["ID"].ToString();
                return m;
            }
            return null;
        }

        [HttpGet]
        public IEnumerable<ModelNguoiDung> DSNguoiDung(String khoa)
        {
            if (!khoa.Equals(khoaBaoMat))
                return null;

            String connectionString = "Data Source=.\\MSSQL2014; initial catalog=LTMK63DB; user id=K63Mobile; password=12345678;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * From tbNguoiDung", con);
            DataTable tb = new DataTable();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                tb.Load(dr, LoadOption.OverwriteChanges);
                con.Close();
            }
            catch
            { }
            if (tb != null && tb.Rows.Count > 0)
            {
                List<ModelNguoiDung> ds = new List<ModelNguoiDung>();
                foreach (DataRow r in tb.Rows)
                {
                    ModelNguoiDung m = new ModelNguoiDung();
                    m.Email = r["Email"].ToString();
                    m.Password = r["Password"].ToString();
                    m.Active = r["Active"].ToString();
                    m.ID = r["ID"].ToString();
                    ds.Add(m);
                }
                return ds;
            }
            return null;
        }

        [HttpGet]
        public string[] DSNguoiDung1(int dem)
        {
            List<String> ds = new List<string>();
            for (int i = 0; i < dem; i++)
            {
                ds.Add(i.ToString());
            }
            return ds.ToArray();
        }

        [HttpGet]
        public String ThemNguoiDung(String email, string password, String active, string khoa)
        {
            if (!khoa.Equals(khoaBaoMat))
                return "Khóa không hợp lệ";

            String connectionString = "Data Source=.\\MSSQL2014; initial catalog=LTMK63DB; user id=K63Mobile; password=12345678;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Insert Into tbNguoiDung(Email, Password, Active) Values(N'" +
                email + "', N'" + password + "', N'" + active + "')", con);
            try
            {
                con.Open();
                int dem = cmd.ExecuteNonQuery();                
                con.Close();
                if (dem == 1)
                    return "Thêm mới thành công";
                else
                    return "Thêm dữ liệu lỗi";
            }
            catch
            {
                return "Lỗi";
            }
        }

        [HttpGet]
        public String CapNhatNguoiDung(String id, String email, string password, String active, string khoa)
        {
            if (!khoa.Equals(khoaBaoMat))
                return "Khóa không hợp lệ";

            String connectionString = "Data Source=.\\MSSQL2014; initial catalog=LTMK63DB; user id=K63Mobile; password=12345678;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Update tbNguoiDung Set Email=N'" +
                email + "', Password=N'" + password + "', Active=N'" + active + "' Where ID=" + id, con);
            try
            {
                con.Open();
                int dem = cmd.ExecuteNonQuery();
                con.Close();
                if (dem == 1)
                    return "Cập nhật thành công";
                else
                    return "Cập nhật liệu lỗi";
            }
            catch
            {
                return "Lỗi";
            }
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}