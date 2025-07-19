using SQLite;

namespace MauiApp1.Models
{
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, MaxLength(100)]
        public string Nombre { get; set; }
        [NotNull]
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
    }
}
