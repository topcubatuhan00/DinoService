namespace DinoService.ViewModels;

public class HomeDataModel
{
    public int Delivered { get; set; } // teslim edilen
    public int Received { get; set; } // teslim alinan
    public int GuaranteedItems { get; set; } // garantide olanlar
    public int InProcess { get; set; } // islemde olanlar
    public int CetinGiden { get; set; } 
    public int CetinGelen { get; set; } 
    public int Total { get; set; } 
}
