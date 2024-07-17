using FluentAssertions.Equivalency;
using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class CasesControllerTests
{
    private CasesController _caseController = null!;

    private Mock<ICaseService> _mockCaseService = null!;

    private Mock<ICaseDocumentFieldValueService> _mockCaseDocumentFieldValueService = null!;

    private Mock<IMapper> _mockMapper = null!;

    private static EquivalencyAssertionOptions<CaseDto> ExcludeProperties(EquivalencyAssertionOptions<CaseDto> options)
    {
        options.Excluding(t => t.GuidIdentifier);
        return options;
    }

    [SetUp]
    public void Setup()
    {
        _mockCaseService = new Mock<ICaseService>();
        _mockCaseDocumentFieldValueService = new Mock<ICaseDocumentFieldValueService>();
        _mockMapper = new Mock<IMapper>();

        _caseController = new CasesController(_mockCaseService.Object, _mockCaseDocumentFieldValueService.Object, _mockMapper.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfCases()
    {
        // Arrange
        var casesResponseExpected = CaseMother.GetCaseList();
        var casesDtoExpected = CaseDtoMother.GetCaseList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockMapper.Setup(x => x.Map<List<CaseDto>>(It.IsAny<List<Case>>())).Returns(casesDtoExpected);
        _mockCaseService.Setup(x => x.GetAll()).ReturnsAsync(casesResponseExpected);

        // Act
        var response = await _caseController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var caseDtoResponse = response!.Value as List<CaseDto>;
        caseDtoResponse.Should().NotBeNull();
        caseDtoResponse.Should().BeEquivalentTo(casesDtoExpected, ExcludeProperties);

        _mockCaseService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredCases(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var caseListResponseExpected = CaseMother.GetCaseList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Case>.Create(caseListResponseExpected, queryRequest);
        var caseDtoResponseExpected = CaseDtoMother.GetCaseList();
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockMapper.Setup(x => x.Map<List<CaseDto>>(It.IsAny<List<Case>>())).Returns(caseDtoResponseExpected);
        _mockCaseService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _caseController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var casesDtoResponse = response!.Value as List<CaseDto>;
        casesDtoResponse.Should().NotBeNull();
        casesDtoResponse.Should().BeEquivalentTo(caseDtoResponseExpected);
        var paginationData = _caseController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockCaseService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithCase()
    {
        // Arrange
        var caseResponseExpected = CaseMother.DemandCase();
        var caseDtoResponseExpected = CaseDtoMother.DemandCase();
        var id = caseResponseExpected.Id;

        _mockMapper.Setup(x => x.Map<CaseDto>(It.IsAny<Case>())).Returns(caseDtoResponseExpected);
        _mockCaseService.Setup(x => x.GetById(id)).ReturnsAsync(caseResponseExpected);

        // Act
        var response = await _caseController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var personDtoResponse = response!.Value as CaseDto;
        personDtoResponse.Should().NotBeNull();
        personDtoResponse.Should().BeEquivalentTo(caseDtoResponseExpected, ExcludeProperties);

        _mockCaseService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenCaseIsValid_ReturnsCreatedWithCase()
    {
        // Arrange
        var caseDtoRequest = CaseDtoMother.DemandCase();
        caseDtoRequest.Id = 0;
        var caseDtoResponseExpected = CaseDtoMother.DemandCase();
        var caseResponseExpected = CaseMother.DemandCase();

        _mockMapper.Setup(x => x.Map<CaseDto>(It.IsAny<Case>())).Returns(caseDtoResponseExpected);
        _mockCaseService.Setup(x => x.Create(It.IsAny<Case>())).ReturnsAsync(caseResponseExpected);

        // Act
        var response = await _caseController.Post(caseDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var caseDtoResponse = response!.Value as CaseDto;
        caseDtoResponse.Should().NotBeNull();
        caseDtoResponse.Should().BeEquivalentTo(caseDtoResponseExpected, ExcludeProperties);

        _mockCaseService.Verify(x => x.Create(It.IsAny<Case>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenCaseIsValid_ReturnsOkWithUpdatedCase()
    {
        // Arrange
        var caseDtoRequest = CaseDtoMother.DemandCase();
        var caseDtoResponseExpected = CaseDtoMother.DemandCase();
        var caseResponseExpected = CaseMother.DemandCase;

        _mockMapper.Setup(x => x.Map<CaseDto>(It.IsAny<Case>())).Returns(caseDtoResponseExpected);
        _mockCaseService.Setup(x => x.Edit(It.IsAny<Case>())).ReturnsAsync(caseResponseExpected);

        // Act
        var response = await _caseController.Put(caseDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var personDtoResponse = response!.Value as CaseDto;
        personDtoResponse.Should().NotBeNull();
        personDtoResponse.Should().BeEquivalentTo(caseDtoResponseExpected, ExcludeProperties);

        _mockCaseService.Verify(x => x.Edit(It.IsAny<Case>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _caseController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockCaseService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task GetEmailPreview_WhenIdIsValid_ReturnsOkWithCase()
    {
        // Arrange
        var caseExpected = CaseMother.DemandCase();
        var caseDto = CaseDtoMother.DemandCase();
        EmailPreviewDto emailPreviewDtoExpected = new()
        {
            Case = caseDto,
            CaseDocuments = new List<CaseDocumentFieldValueDto>
        {
            new() { Id = 1, CaseId = caseDto.Id }
        }
        };

        _mockMapper.Setup(x => x.Map<CaseDto>(It.IsAny<Case>())).Returns(caseDto);
        _mockCaseService.Setup(x => x.GetById(caseExpected.Id)).ReturnsAsync(caseExpected);
        _mockCaseDocumentFieldValueService.Setup(x => x.GetByCaseIdAsync(caseExpected.Id)).ReturnsAsync(emailPreviewDtoExpected.CaseDocuments);

        // Act
        var response = await _caseController.GetEmailPreview(caseExpected.Id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var dtoResponse = response!.Value as EmailPreviewDto;
        dtoResponse.Should().NotBeNull();
        dtoResponse.Should().BeEquivalentTo(emailPreviewDtoExpected);

        _mockCaseService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task UpdateBusinessLine_WhenCaseIsValid_ReturnsCase()
    {
        // Arrange
        const int businessLineId = 2;
        var caseDtoRequest = CaseDtoMother.DemandCase();
        caseDtoRequest.Id = 0;
        var caseDtoResponseExpected = CaseDtoMother.DemandCase();
        caseDtoResponseExpected.BusinessLineId = businessLineId;
        var caseResponseExpected = CaseMother.DemandCase();
        caseResponseExpected.BusinessLineId = businessLineId;

        _mockMapper.Setup(x => x.Map<CaseDto>(It.IsAny<Case>())).Returns(caseDtoResponseExpected);
        _mockCaseService.Setup(x => x.UpdateBusinessLineAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(caseResponseExpected);

        // Act
        var response = await _caseController.UpdateBusinessLine(new UpdateBusinessLineInputDto { Id = caseDtoRequest.Id, BusinessLineId = businessLineId }) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var caseDtoResponse = response!.Value as CaseDto;
        caseDtoResponse.Should().NotBeNull();
        caseDtoResponse.Should().BeEquivalentTo(caseDtoResponseExpected, ExcludeProperties);
    }
}