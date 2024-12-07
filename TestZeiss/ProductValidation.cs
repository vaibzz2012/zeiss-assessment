namespace ZeissAssessment
{
    public static class ProductValidation
    {
        public static bool ValidateStockDecrement(Product product, int quantity, out string errorMessage)
        {
            if (product == null)
            {
                errorMessage = "Product not found.";
                return false;
            }

            if (quantity <= 0)
            {
                errorMessage = "Quantity must be greater than zero.";
                return false;
            }

            if (product.AvailableQuantity < quantity)
            {
                errorMessage = "Insufficient stock available.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public static bool ValidateStockIncrement(int quantity, out string errorMessage)
        {
            if (quantity <= 0)
            {
                errorMessage = "Quantity must be greater than zero.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
