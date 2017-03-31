using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using instore.database;

namespace Instore.database
{
   public class dbrepository
    {
        String dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "instore.db");
        public string createdb()
        {
            var output = "";
            var db = new SQLiteConnection(dbpath);
            return output;
        }

        public string createtb()
        {
            try
            {
                var db = new SQLiteConnection(dbpath);
                db.CreateTable<dbclass>();
                string result = "";
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }

        }

        public string insertdb(int id, string tit, string photo)
        {
            try
            {
                var output = "";
                var db = new SQLiteConnection(dbpath);
                dbclass item = new dbclass();
                item.ID = id;
                item.title = tit;
                item.image= photo;
                db.Insert(item);
                return output;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public string getallrecords()
        {
            try
            {
                var output = "";
                var db = new SQLiteConnection(dbpath);
                var table = db.Table<dbclass>();
                foreach (var item in table)
                {
                    output += "\n" + item.ID + item.title + item.image;
                }
                return output;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }


    }
}