using SQLite;

namespace MauiApp1.Models
{
    public class Pizza
    {
        [PrimaryKey, AutoIncrement]
        public int idPizza { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string amount { get; set; }
        public double Price { get; set; }
    }
}
