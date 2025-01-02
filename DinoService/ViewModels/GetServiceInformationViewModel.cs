using System.ComponentModel.DataAnnotations;

namespace DinoService.ViewModels;

public class GetServiceInformationViewModel
{
    [Required(ErrorMessage = "Lütfen ürün seri numarasını giriniz.")]
    [Display(Name = "İletişim Numarası")]
    //[StringLength(12, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 12)]
    public string ServiceNumber { get; set; }
}
