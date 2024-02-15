namespace ServicioApiCurso.Models
{
    public class GenericResponse<T>
    {
        public int statusCode { get; set; }
        public T data { get; set; }
        public string? message { get; set; }
    }
}
