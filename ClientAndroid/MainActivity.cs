using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using ClientCommon;
using Android.Widget;

namespace ClientAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ContextWrapper cw = new ContextWrapper(ApplicationContext);
            var dbFolder = cw.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
            DBController.Instance.RefreshDB(dbFolder.AbsolutePath);

            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            RefreshTests();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            StartActivityForResult(typeof(TestActivity), 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            RefreshTests();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {
                StartActivity(typeof(StatisticActivity));
            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public void RefreshTests()
        {
            IEnumerable<Test> tests = DBController.Instance.GetTests();
            if(tests != null)
            {
                TestListAdapter tla = new TestListAdapter(this, tests.ToArray());
                ListView lvTests = FindViewById<ListView>(Resource.Id.lvTests);
                lvTests.Adapter = tla;
                lvTests.ItemSelected += LvTests_ItemSelected;
                lvTests.ItemClick += LvTests_ItemClick;
            }
        }

        private void LvTests_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int testId = Convert.ToInt32(e.Id);
            Intent intent = new Intent(this, typeof(TestResultActivity));
            intent.PutExtra("TEST_ID", testId);
            StartActivityForResult(intent, testId);
        }

        private void LvTests_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            /*Intent intent = new Intent(this, typeof(TestResultActivity));
            intent.PutExtra("TEST_ID", e.Id);
            StartActivityForResult(intent, Convert.ToInt32(e.Id));*/
        }
    }
}