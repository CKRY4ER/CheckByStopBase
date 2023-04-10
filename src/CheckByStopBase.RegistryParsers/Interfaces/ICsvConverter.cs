using CheckByStopBase.Domain.Entities;

namespace CheckByStopBase.RegistryParsers.Interfaces;

public interface ICsvConverter<TEntity> : IConverter
    where TEntity : Entity
{
    public IEnumerable<TEntity> Convert(MemoryStream stream);
}