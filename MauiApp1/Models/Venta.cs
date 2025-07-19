using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Venta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Cliente { get; set; }
        public int Edad { get; set; }
        public string Tamano { get; set; }
        public bool QuesoExtra { get; set; }
        public bool Peperoni { get; set; }
        public string MetodoPago { get; set; }
    }
}
