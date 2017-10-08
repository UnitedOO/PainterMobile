using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using Android.Support.V7.Widget;

namespace MobilePaint.Droid
{
    [Activity(Label = "MobilePaint.Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private SupportToolbar bToolbar;
        private MenuToggler menuToggler;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ListView mRightDrawer;
        private Spinner mTab;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);

            bToolbar = FindViewById<SupportToolbar>(Resource.Id.lower_toolbar);
            bToolbar.InflateMenu(Resource.Menu.lower_menu);
            bToolbar.MenuItemClick += bToolbar_Click;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_menu);
            mRightDrawer = FindViewById<ListView>(Resource.Id.right_menu);
            mTab = FindViewById<Spinner>(Resource.Id.Tabs);

            mLeftDrawer.Tag = 0;
            mRightDrawer.Tag = 1;

            Dictionary<string, string[]> lmenu = new Dictionary<string, string[]>();
            lmenu.Add("File", new string[] { "Open", "Save", "Save in cloud", "Load from cloud" });
            lmenu.Add("Edit", new string[] { "Figure", "Text", "Image" });
            lmenu.Add("View", new string[] { "ToolBar", "ToolBox" });

            Dictionary<string, string[]> rmenu = new Dictionary<string, string[]>();
            rmenu.Add("Language", new string[] { "English", "Russian", "Ukrainian" });
            rmenu.Add("Skins", new string[] { "Skin1", "Skin2", "Skin3" });
            rmenu.Add("Help", new string[] { "FAQ", "About" });

            var adapterTab = ArrayAdapter.CreateFromResource(this, Resource.Array.tabs_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapterTab.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            mTab.Adapter = adapterTab;
           


            mLeftDrawer.Adapter = new CustomAdapter(this, lmenu);
            mRightDrawer.Adapter = new CustomAdapter(this, rmenu);

            menuToggler = new MenuToggler(
                this,                           //Host Activity
                mDrawerLayout,                  //DrawerLayout
                Resource.String.openDrawer,     //Opened Message
                Resource.String.closeDrawer     //Closed Message
            );

            mDrawerLayout.SetDrawerListener(menuToggler);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            menuToggler.SyncState();

        }

        private void bToolbar_Click(object sender, SupportToolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.action_simplfigure: Toast.MakeText(this, "SimpleFigure clicked", ToastLength.Short).Show(); break;
                case Resource.Id.action_figuretext: Toast.MakeText(this, "FigureText clicked", ToastLength.Short).Show(); break;
                case Resource.Id.action_figureimage: Toast.MakeText(this, "FigureImage clicked", ToastLength.Short).Show(); break;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                    menuToggler.OnOptionsItemSelected(item);
                    return true;
                case Resource.Id.action_settings:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }
                    else
                    {
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                    }

                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.settings_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            menuToggler.OnConfigurationChanged(newConfig);
        }
    }
}


