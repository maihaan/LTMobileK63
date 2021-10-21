package crc64891c1cae58e8e202;


public class ActivityDSNguoiDung
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MobileK63Android.ActivityDSNguoiDung, MobileK63Android", ActivityDSNguoiDung.class, __md_methods);
	}


	public ActivityDSNguoiDung ()
	{
		super ();
		if (getClass () == ActivityDSNguoiDung.class)
			mono.android.TypeManager.Activate ("MobileK63Android.ActivityDSNguoiDung, MobileK63Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
