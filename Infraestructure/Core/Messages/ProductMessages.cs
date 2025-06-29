namespace Infrastructure.Core.Messages
{
    public static class ProductMessages
    {
        public static string ProductByIdNotFound()
              => "No existe un producto con ese identificador";

        public static string ProductNotFound()
            => "No hay productos en el inventario";

        public static string ProductByCategoryNotFound()
            => "No hay productos en el inventario con esa categoria";

        public static string ProductByNameNotFound()
            => "No hay productos en el inventario con ese nombre";
    }
}
