
using Answers.Modal;
using Newtonsoft.Json;

namespace Answers.Models.Model
{
    public partial class Products
    {
        [JsonProperty("products")]
        public Product[] products { get; set; }

        [JsonProperty("specials")]
        public Special[] specials { get; set; }

        [JsonProperty("quantities")]
        public Quantity[] quantities { get; set; }
    }

    //public partial class Product
    //{
    //    [JsonProperty("name")]
    //    public string name { get; set; }

    //    [JsonProperty("price")]
    //    public long price { get; set; }
        
    //}

    public partial class Quantity
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("quantity")]
        public long quantity { get; set; }
    }

    public partial class Special
    {
        [JsonProperty("quantities")]
        public Quantity[] quantities { get; set; }

        [JsonProperty("total")]
        public long total { get; set; }
    }
}
