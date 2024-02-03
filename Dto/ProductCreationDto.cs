namespace VisualSoftAspCoreApi.Dto
{
    public class ProductCreationDto
    {
        public string model {get; set;}
        public string? details {get; set;}
        public int? advancePyment {get; set;}
        public int? monthlyInstallment {get; set;}
        public int? financeDuration {get; set;}
        public string? imageURL {get; set;}
        
    }
}