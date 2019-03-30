using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.EntityFrameworkCore;

using ClientCommon;

namespace ClientAndroid
{
    public class DBManager
    {
        public static DBManager Instance { get; } = new DBManager();

        private DBManager()
        {
        }

        public async void RefreshDB(Context applicationContext)
        {
            ContextWrapper cw = new ContextWrapper(applicationContext);
            var dbFolder = cw.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);

            var fileName = "db.db";
            var dbFullPath = Path.Combine(dbFolder.AbsolutePath, fileName);
            try
            {
                using (var db = new ClientDBContext(dbFullPath))
                {
                    await db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        internal void SaveTestResults(TestResult tr)
        {
            try
            {
                using (var db = new ClientDBContext())
                {
                    db.Add(tr);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}