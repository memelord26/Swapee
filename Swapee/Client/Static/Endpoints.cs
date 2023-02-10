using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swapee.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string BuyersEndpoint = $"{Prefix}/buyers";
        public static readonly string CategoriesEndpoint = $"{Prefix}/categories";
        public static readonly string OrdersEndpoint = $"{Prefix}/orders";
        public static readonly string PaymentsEndpoint = $"{Prefix}/payments";
        public static readonly string ProductsEndpoint = $"{Prefix}/products";
        public static readonly string SellersEndpoint = $"{Prefix}/sellers";
    }
}
