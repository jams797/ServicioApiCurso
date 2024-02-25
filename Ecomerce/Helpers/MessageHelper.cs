namespace ServicioApiCurso.Helpers
{
    public static class MessageHelper
    {
        public static string ErrorCreateInvoice = "Error, no se puedo facturar el pedido";
        public static string ErrorCreateInvoiceProductNotFound = "Error, producto no existe o no fue encontrado";
        public static string ErrorCreateInvoiceProductExecendStock = "Error, uno o mas productos excede el stock existente";

        public static string LoginErrorUserName = "Usuario incorrecto";
        public static string LoginErrorPassword = "Contraseña incorrecta";
        public static string LoginErrorNotActived = "Usuario deshabilitado";

        public static string TokenSesionErrorNotParams = "No se encontro un token sesión";
        public static string TokenSesionErrorValidate = "La sesión no es válida";
        public static string TokenSesionErrorExpired = "La sesión ha caducado";
    }
}
