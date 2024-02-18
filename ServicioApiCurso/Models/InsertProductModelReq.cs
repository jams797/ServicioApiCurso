namespace ServicioApiCurso.Models
{
    public class InsertProductModelReq
    {
        public string NameProduct {  get; set; }
        public double Price { get; set; }
        public int Cant { get; set; }
        public int IdCategory { get; set; }
    }
}
