namespace MVM.LojaVirtual.IdentityCore;

public class IdentityAppSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresIn { get; set; }
}