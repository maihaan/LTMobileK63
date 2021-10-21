using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileK63Android
{
    [Activity(Label = "PhotoActivity")]
    public class PhotoActivity : Activity
    {
        Button btCamera;
        ImageView ivPhoto;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_photo);

            // Create your application here
            btCamera = (Button)FindViewById(Resource.Id.btCamera);
            ivPhoto = (ImageView)FindViewById(Resource.Id.ivPhoto);

            btCamera.Click += BtCamera_Click;
        }

        private void BtCamera_Click(object sender, EventArgs e)
        {
            Intent cameraIntent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(cameraIntent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            ivPhoto.SetImageBitmap((Android.Graphics.Bitmap)data.Extras.Get("data"));
        }
    }
}