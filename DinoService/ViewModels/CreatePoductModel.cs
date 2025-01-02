using System.Text.Json.Serialization;

namespace DinoService.ViewModels
{
    public class CreatePoductModel
    {
        public string? ServiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerCompanyName { get; set; }
        public string? Status { get; set; }
        public DateTime? ServiceRegistirationDate { get; set; }
        public string ProductName { get; set; }
        public int? ProductAmount { get; set; }
        public string? OtherItems { get; set; }
        public string ProductProblem { get; set; }
        public decimal? Price { get; set; }
    }
}
