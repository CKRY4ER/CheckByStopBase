using CheckByStopBase.Api.ResultExtensions.CompanyResultExtensions;
using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;
using CheckByStopBase.ServiceLayer.CompanyServices.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CheckByStopBase.Api.Controllers;

[ApiController]
[Route("api/v1/company")]
public class CompanyController : ControllerBase
{
    [HttpPost("check-company")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckCompany([FromServices] IGetCompanyService service,
        [FromServices] ILogger<CompanyController> logger,
        [FromBody] CompanyRequestModel requestModel)
    {
        IEnumerable<CompanyRegistry> result;
        try
        {
            result = await service.GetByTaxNumbers(requestModel.TaxNumbers, requestModel.Partner);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Internal Service Error!");
            return StatusCode(500, "Внутренняя ошибка сервиса!");
        }

        return Ok(result.CompanyRegistryToResultApi());
    }

    #region Models

    public class CompanyResponseModel
    {
        public DateTime CreateDate { get; set; }

        public DateTime AddDate { get; set; }

        public string TaxNumber { get; set; } = null!;

        public string CompanyType { get; set; } = null!;
    }

    public class CompanyRequestModel
    {
        public string Partner { get; set; }

        public List<string> TaxNumbers { get; set; } = null!;

        public sealed class Validator : AbstractValidator<CompanyRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.TaxNumbers)
                    .NotEmpty()
                    .WithMessage("Must be providet!");
                RuleFor(x => x.Partner)
                    .Must(p => IsEnum(p))
                    .WithMessage("Invalid partner!");
            }

            private bool IsEnum(string partner)
                => Enum.TryParse(partner, out PartnerEnum enumPartner);
        }
    }

    #endregion Models
}