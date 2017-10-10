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
using Android.Graphics;
using Android.Support.V7.Widget;

namespace MobilePaint.Droid
{
    [Activity(Label = "MobilePaint.Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        XData data = new XData();

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

            FindViewById<PDrawView>(Resource.Id.drawFiheld).data = data;
            SetSpinnersListeners();

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
            lmenu.Add("File", new string[] { });
            lmenu.Add(" Open", new string[] { });
            lmenu.Add(" Save", new string[] { });
            lmenu.Add(" Save in cloud", new string[] { });
            lmenu.Add(" Load from cloud", new string[] { });

            lmenu.Add("Edit", new string[] { });
            lmenu.Add(" Figure", new string[] { });
            lmenu.Add(" Text", new string[] { });
            lmenu.Add(" Image", new string[] { });
            lmenu.Add("  Open Image", new string[] { });

            lmenu.Add("View", new string[] { });
            lmenu.Add(" ToolBar", new string[] { });
            lmenu.Add(" ToolBox", new string[] { });

            Dictionary<string, string[]> rmenu = new Dictionary<string, string[]>();
            rmenu.Add("Language", new string[] { });
            rmenu.Add(" English", new string[] { });
            rmenu.Add(" Russian", new string[] { });
            rmenu.Add(" Ukrainian", new string[] { });

            rmenu.Add("Skins", new string[] { });
            rmenu.Add(" Skin1", new string[] { });
            rmenu.Add(" Skin2", new string[] { });
            rmenu.Add(" Skin3", new string[] { });

            rmenu.Add("Help", new string[] { });
            rmenu.Add(" FAQ", new string[] { });
            rmenu.Add(" About", new string[] { });

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

        private void SetSpinnersListeners()
        {
            Spinner spColor = FindViewById<Spinner>(Resource.Id.spColor);
            Spinner spWidth = FindViewById<Spinner>(Resource.Id.spWidth);
            Spinner spType = FindViewById<Spinner>(Resource.Id.spType);

            var adapterC = ArrayAdapter.CreateFromResource(this, Resource.Array.color_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapterC.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spColor.Adapter = adapterC;

            var adapterW = ArrayAdapter.CreateFromResource(this, Resource.Array.width_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapterW.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spWidth.Adapter = adapterW;

            var adapterT = ArrayAdapter.CreateFromResource(this, Resource.Array.type_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapterT.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spType.Adapter = adapterT;

            spColor.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spWidth.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            if (spinner.Id == Resource.Id.spColor)
            {
                switch (spinner.SelectedItem.ToString())
                {
                    case "Red": data.Color = Color.Red; break;
                    case "Blue": data.Color = Color.Blue; break;
                    case "Green": data.Color = Color.Green; break;
                }
            }
            else if (spinner.Id == Resource.Id.spWidth)
            {
                data.Width = Convert.ToInt32(spinner.SelectedItem.ToString());
            }
            else if (spinner.Id == Resource.Id.spType)
            {
                switch (spinner.SelectedItem.ToString())
                {
                    case "Curve": data.Type = Figure.FType.Curve; break;
                    case "Line": data.Type = Figure.FType.Line; break;
                    case "Rect": data.Type = Figure.FType.Rect; break;
                    case "Ellipse": data.Type = Figure.FType.Ellipse; break;
                   
                }
            }
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


