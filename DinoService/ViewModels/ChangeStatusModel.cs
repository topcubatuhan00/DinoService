namespace DinoService.ViewModels
{
    public class ChangeStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string? TeslimAlan { get; set; }
        public string? KargoAlici { get; set; }
    }
}
