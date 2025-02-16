namespace MVM.LojaVirtual.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}