using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileK63Android.Adapters;
using MobileK63Android.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MobileK63Android
{
    [Activity(Label = "Quản lý người dùng", MainLauncher =true)]
    public class ActivityDSNguoiDung : Activity
    {
        EditText etEmail, etPassword;
        RadioButton rbMo, rbDong;
        Button btThem, btLuu;
        ListView lvDanhSach;

        List<String> dsIDNguoiDung, dsEmail, dsActive, dsPassword;
        String khoaBaoMat = "sfhjkshjwqhiurwqhrhqkjwbrhjsfd,sdnfjksdhuimfweuhrewjjkfbndsbfmndsb";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_nguoi_dung);

            etEmail = (EditText)FindViewById(Resource.Id.etEmail);
            etPassword = (EditText)FindViewById(Resource.Id.etPassword);
            rbMo = (RadioButton)FindViewById(Resource.Id.rbMo);
            rbDong = (RadioButton)FindViewById(Resource.Id.rbDong);
            btThem = (Button)FindViewById(Resource.Id.btThem);
            btLuu = (Button)FindViewById(Resource.Id.btLuu);
            lvDanhSach = (ListView)FindViewById(Resource.Id.lvDanhSach);

            btThem.Click += BtThem_Click;
            btLuu.Click += BtLuu_Click;
            lvDanhSach.ItemSelected += LvDanhSach_ItemSelected;

            LayDSNguoiDung();
        }

        private void LvDanhSach_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            etEmail.Text = dsEmail[lvDanhSach.SelectedItemPosition];
            etPassword.Text = dsPassword[lvDanhSach.SelectedItemPosition];
            String active = dsActive[lvDanhSach.SelectedItemPosition];
            if (active.ToLower().Equals("open"))
            {
                rbMo.Checked = true;
                rbDong.Checked = false;
            }
            else
            {
                rbMo.Checked = false;
                rbDong.Checked = true;
            }    
        }

        private void BtLuu_Click(object sender, EventArgs e)
        {
            try
            {
                String active = "open";
                if (rbDong.Checked)
                    active = "close";
                String url = "https://k63.vnanews.net/API/K63/CapNhatNguoiDung?khoa=" + khoaBaoMat
                    + "&id=" + dsIDNguoiDung[lvDanhSach.SelectedItemPosition] 
                    + "&email=" + etEmail.Text.Trim()
                    + "&password=" + etPassword.Text.Trim()
                    + "&active=" + active;
                using (HttpClient client = new HttpClient())
                {
                    var ketQua = client.GetStringAsync(url);
                    if (ketQua != null)
                    {
                        String msg = ketQua.Result;
                        Toast.MakeText(this, msg, ToastLength.Long).Show();
                        LayDSNguoiDung();
                        etEmail.Text = "";
                        etPassword.Text = "";
                        return;
                    }
                }
            }
            catch
            {
                Toast.MakeText(this, "Kiểm tra kết nối Internet của bạn!", ToastLength.Long).Show();
                return;
            }
        }

        private void BtThem_Click(object sender, EventArgs e)
        {
            try
            {
                String active = "open";
                if (rbDong.Checked)
                    active = "close";
                String url = "https://k63.vnanews.net/API/K63/ThemNguoiDung?khoa=" + khoaBaoMat                    
                    + "&email=" + etEmail.Text.Trim()
                    + "&password=" + etPassword.Text.Trim()
                    + "&active=" + active;
                using (HttpClient client = new HttpClient())
                {
                    var ketQua = client.GetStringAsync(url);
                    if (ketQua != null)
                    {
                        String msg = ketQua.Result;
                        Toast.MakeText(this, msg, ToastLength.Long).Show();
                        LayDSNguoiDung();
                        etEmail.Text = "";
                        etPassword.Text = "";
                        return;
                    }
                }
            }
            catch
            {
                Toast.MakeText(this, "Kiểm tra kết nối Internet của bạn!", ToastLength.Long).Show();
                return;
            }
        }

        private void LayDSNguoiDung()
        {
            try
            {
                String url = "https://k63.vnanews.net/API/K63/DSNguoiDung?khoa=" + khoaBaoMat;
                using(HttpClient client = new HttpClient())
                {
                    var ketQua = client.GetStringAsync(url);
                    if(ketQua != null )
                    {
                        List<ModelNguoiDung> ds = JsonConvert.DeserializeObject<List<ModelNguoiDung>>(ketQua.Result);
                        if(ds != null && ds.Count > 0)
                        {
                            dsIDNguoiDung = new List<string>();
                            dsEmail = new List<string>();
                            dsActive = new List<string>();
                            dsPassword = new List<string>();
                            foreach(ModelNguoiDung m in ds)
                            {
                                dsIDNguoiDung.Add(m.ID);
                                dsEmail.Add(m.Email);
                                dsActive.Add(m.Active);
                                dsPassword.Add(m.Password);
                            }    

                            // Hien thi du lieu len listview
                            var adapter = new NguoiDungAdapter(this, dsEmail, dsActive);
                            lvDanhSach.Adapter = adapter;
                        }    
                    }    
                }    
            }
            catch(Exception e)
            {
                Toast.MakeText(this, "Kiểm tra kết nối Internet của bạn!" + e.Message, ToastLength.Long).Show();
                return;
            }
        }
    }
}