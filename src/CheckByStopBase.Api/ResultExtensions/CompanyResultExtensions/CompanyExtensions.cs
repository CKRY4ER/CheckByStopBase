using CheckByStopBase.CompanyStopBase.Domain.Entities;
using static CheckByStopBase.Api.Controllers.CompanyController;

namespace CheckByStopBase.Api.ResultExtensions.CompanyResultExtensions;

public static class CompanyExtensions
{
    public static IEnumerable<CompanyResponseModel> CompanyRegistryToResultApi(this IEnumerable<CompanyRegistry> companies)
    {
        var result = new List<CompanyResponseModel>();

        foreach (var company in companies)
        {
            result.Add(new CompanyResponseModel()
            {
                CreateDate = company.CreateDate,
                AddDate = company.AddDate,
                TaxNumber = company.TaxNumber,
                CompanyType = company.CompanyType.ToString()
            });
        }

        return result;
    }
}