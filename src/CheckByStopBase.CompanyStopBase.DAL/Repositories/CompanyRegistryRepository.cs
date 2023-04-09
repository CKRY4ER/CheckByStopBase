using CheckByStopBase.CompanyStopBase.DAL.DataContext;
using CheckByStopBase.CompanyStopBase.Domain.Entities;

namespace CheckByStopBase.CompanyStopBase.DAL.Repositories;

public sealed class CompanyRegistryRepository : ICompanyRegistryRepository
{
    private readonly CompanyDbContext _context;

    public CompanyRegistryRepository(CompanyDbContext context)
        => _context = context;

    public IEnumerable<CompanyRegistry> GetByTaxNumber(IEnumerable<string> taxNumber)
        => _context.Registry.Where(r => taxNumber.Any(t => r.TaxNumber == t)).ToList();

    public async Task LoadNewRegistry(IEnumerable<CompanyRegistry> registry)
    {
        await _context.Registry.AddRangeAsync(registry);
        await _context.SaveChangesAsync();
    }
}

public interface ICompanyRegistryRepository
{
    /// <summary>
    /// Получить из списка присланных ИНН только те, которые находятся в реестре
    /// </summary>
    /// <param name="taxNumber"></param>
    /// <returns>
    /// Коллекция ИНН, которые были найдены в реестре или null
    /// </returns>
    IEnumerable<CompanyRegistry> GetByTaxNumber(IEnumerable<string> taxNumber);

    /// <summary>
    ///Загрузить новую версию реестра в БД
    /// </summary>
    /// <param name="registry"></param>
    /// <returns></returns>
    Task LoadNewRegistry(IEnumerable<CompanyRegistry> registry);
}