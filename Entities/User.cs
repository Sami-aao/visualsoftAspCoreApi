namespace VisualSoftAspCoreApi.Entities
{
    public class User
    {
        public int Id {get; set;}
        public string email {get; set;}
        public string pass {get; set;}
        public string address {get; set;}
        public int tel {get; set;}
        public bool activated {get; set;}
        public string role {get; set;}
        public List<Product> products { get; set;} = new List<Product>();

    }
}