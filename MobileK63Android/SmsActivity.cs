using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileK63Android
{
    [Activity(Label = "SmsActivity")]
    public class SmsActivity : Activity
    {
        EditText etPhone, etContent;
        Button btSend, btCall;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_sms);

            etPhone = (EditText)FindViewById(Resource.Id.etPhone);
            etContent = (EditText)FindViewById(Resource.Id.etContent);
            btSend = (Button)FindViewById(Resource.Id.btSend);            
            btSend.Click += BtSend_Click;
            btCall = (Button)FindViewById(Resource.Id.btCall);
            btCall.Click += BtCall_Click;
        }

        private void BtCall_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(etPhone.Text))
            {
                Toast.MakeText(this, "Bạn phải nhập số điện thoại cần gọi", ToastLength.Short).Show();
                return;
            }

            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) == Android.Content.PM.Permission.Granted)
            {
                var uri = Android.Net.Uri.Parse("tel:" + etPhone.Text);
                Intent callIntent = new Intent(Intent.ActionDial, uri);
                Intent[] ds = new Intent[1];
                ds[0] = callIntent;
                StartActivities(ds);
            }
            else
                Toast.MakeText(this, "Bạn phải cấp quyền gọi điện cho ứng dụng", ToastLength.Short).Show();
        }

        private void BtSend_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(etPhone.Text))
            {
                Toast.MakeText(this, "Bạn phải nhập số điện thoại nhận tin nhắn", ToastLength.Short).Show();
                return;
            }

            if (String.IsNullOrEmpty(etContent.Text))
            {
                Toast.MakeText(this, "Bạn phải nhập nội dung tin nhắn", ToastLength.Short).Show();
                return;
            }

            // kiem tra quyen gui tin nhan
            if(ActivityCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) == Android.Content.PM.Permission.Granted)
            {
                SmsManager.Default.SendTextMessage(etPhone.Text, null, etContent.Text, null, null);
            }    
            else
                Toast.MakeText(this, "Bạn phải cấp quyền gửi tin nhắn SMS cho ứng dụng", ToastLength.Short).Show();
        }
    }
}