using System;
using System.Data;
using SQLite;

namespace instore.database
{
    [Table("dbclass")]
    public class dbclass
    {
        [PrimaryKey, Column("ID")]
        public int ID { get; set; }
        public string title { get; set; }
        public string image { get; set; }
    }
}