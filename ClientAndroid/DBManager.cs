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

        public async void RefreshDB()
        {
            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fileName = "db.db";
            var dbFullPath = Path.Combine(dbFolder, fileName);
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
    }
}