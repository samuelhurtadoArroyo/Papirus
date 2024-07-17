using FluentAssertions.Equivalency;
using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class FirmsControllerTests
{
    private FirmsController _firmsController = null!;

    private Mock<IFirmService> _mockFirmService = null!;

    private IMapper _mapper = null!;

    private static EquivalencyAssertionOptions<FirmDto> ExcludeProperties(EquivalencyAssertionOptions<FirmDto> options)
    {
        options.Excluding(t => t.GuidIdentifier);
        return options;
    }

    [SetUp]
    public void Setup()
    {
        _mockFirmService = new Mock<IFirmService>();
        _mapper = MapperCreator.CreateMapper();

        _firmsController = new FirmsController(_mockFirmService.Object, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfFirms()
    {
        // Arrange
        var firmsResponseExpected = FirmMother.GetFirmList();
        var firmsDtoExpected = FirmDtoMother.GetFirmList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockFirmService.Setup(x => x.GetAll()).ReturnsAsync(firmsResponseExpected);

        // Act
        var response = await _firmsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var firmsDtoResponse = response!.Value as List<FirmDto>;
        firmsDtoResponse.Should().NotBeNull();
        firmsDtoResponse.Should().BeEquivalentTo(firmsDtoExpected, ExcludeProperties);

        _mockFirmService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredFirms(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var firmListResponseExpected = FirmMother.GetFirmList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Firm>.Create(firmListResponseExpected, queryRequest);
        var firmsDtoResponseExpected = _mapper.Map<List<FirmDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockFirmService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _firmsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var firmsDtoResponse = response!.Value as List<FirmDto>;
        firmsDtoResponse.Should().NotBeNull();
        firmsDtoResponse.Should().BeEquivalentTo(firmsDtoResponseExpected);
        var paginationData = _firmsController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockFirmService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithFirm()
    {
        // Arrange
        var firmResponseExpected = FirmMother.DefaultFirm();
        var firmDtoResponseExpected = FirmDtoMother.GetDefaultFirm();
        var id = firmResponseExpected.Id;

        _mockFirmService.Setup(x => x.GetById(id)).ReturnsAsync(firmResponseExpected);

        // Act
        var response = await _firmsController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var firmDtoResponse = response!.Value as FirmDto;
        firmDtoResponse.Should().NotBeNull();
        firmDtoResponse.Should().BeEquivalentTo(firmDtoResponseExpected, ExcludeProperties);

        _mockFirmService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenFirmIsValid_ReturnsCreatedWithFirm()
    {
        // Arrange
        var firmDtoRequest = FirmDtoMother.GetDefaultFirm();
        firmDtoRequest.Id = 0;
        var firmDtoResponseExpected = FirmDtoMother.GetDefaultFirm();
        var firmResponseExpected = FirmMother.DefaultFirm();

        _mockFirmService.Setup(x => x.Create(It.IsAny<Firm>())).ReturnsAsync(firmResponseExpected);

        // Act
        var response = await _firmsController.Post(firmDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var firmDtoResponse = response!.Value as FirmDto;
        firmDtoResponse.Should().NotBeNull();
        firmDtoResponse.Should().BeEquivalentTo(firmDtoResponseExpected, ExcludeProperties);

        _mockFirmService.Verify(x => x.Create(It.IsAny<Firm>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenFirmIsValid_ReturnsOkWithUpdatedFirm()
    {
        // Arrange
        var firmDtoRequest = FirmDtoMother.GetDefaultFirm();
        var firmDtoResponseExpected = FirmDtoMother.GetDefaultFirm();
        var firmResponseExpected = FirmMother.DefaultFirm();

        _mockFirmService.Setup(x => x.Edit(It.IsAny<Firm>())).ReturnsAsync(firmResponseExpected);

        // Act
        var response = await _firmsController.Put(firmDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var firmDtoResponse = response!.Value as FirmDto;
        firmDtoResponse.Should().NotBeNull();
        firmDtoResponse.Should().BeEquivalentTo(firmDtoResponseExpected, ExcludeProperties);

        _mockFirmService.Verify(x => x.Edit(It.IsAny<Firm>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _firmsController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockFirmService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}