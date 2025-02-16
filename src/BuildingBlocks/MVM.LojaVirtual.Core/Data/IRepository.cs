using MVM.LojaVirtual.Core.Domain;

namespace MVM.LojaVirtual.Core.Data;

public interface IRepository<T> : IDisposable
    where T : Entity, IAggregateRoot
{
    
}