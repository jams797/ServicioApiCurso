namespace ServicioApiCurso.Helpers
{
    public static class MessageHelper
    {
        public static string ErrorCreateInvoice = "Error, no se puedo facturar el pedido";
        public static string ErrorCreateInvoiceProductNotFound = "Error, producto no existe o no fue encontrado";
        public static string ErrorCreateInvoiceProductExecendStock = "Error, uno o mas productos excede el stock existente";
    }
}
