namespace WebApi.ModelViewModels;
public class ReservationViewModel
{
    public int id { get; init; }
    public string reservation_date { get; set; } = "";
    public string initial_hour { get; set; } = "";
    public string finish_hour { get; set; } = "";
    public string protocol { get; set; } = "";
    public string status { get; set; } = "";
}