namespace MVM.LojaVirtual.Identidade.API.Extensions;

public class AppSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresIn { get; set; }
}