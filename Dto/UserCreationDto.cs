namespace VisualSoftAspCoreApi.Dto
{
    public class UserCreationDto
    {
        public string email {get; set;}
        public string pass {get; set;}
        public string? address {get; set;}
        public int? tel {get; set;}
        public bool activated {get; set;}
        public string role {get; set;}
       
    }
}