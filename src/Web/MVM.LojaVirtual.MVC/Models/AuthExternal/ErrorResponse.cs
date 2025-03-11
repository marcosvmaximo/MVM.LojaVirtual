namespace MVM.LojaVirtual.MVC.Models.AuthExternal;

public class ErrorResponse
{
    public string Title { get; set; }
    public int Status { get; set; }
    public ErrorResponseMessages Errors { get; set; }
}


public class ErrorResponseMessages
{
    public List<string> Mensagens { get; set; }
}