namespace MVM.LojaVirtual.MVC.Models.AuthExternal;

public class ErrorResponse
{
    public string Title { get; set; }
    public int Status { get; set; }
    public ResponseErrorMessages Errors { get; set; }
}


public class ResponseErrorMessages
{
    public List<string> Mensagens { get; set; }
}