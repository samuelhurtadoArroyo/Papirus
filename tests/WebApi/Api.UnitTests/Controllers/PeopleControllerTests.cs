using FluentAssertions.Equivalency;
using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class PeopleControllerTests
{
    private PeopleController _peopleController = null!;

    private Mock<IPersonService> _mockPersonService = null!;

    private IMapper _mapper = null!;

    private static EquivalencyAssertionOptions<PersonDto> ExcludeProperties(EquivalencyAssertionOptions<PersonDto> options)
    {
        options.Excluding(t => t.GuidIdentifier);
        return options;
    }

    [SetUp]
    public void Setup()
    {
        _mockPersonService = new Mock<IPersonService>();
        _mapper = MapperCreator.CreateMapper();

        _peopleController = new PeopleController(_mockPersonService.Object, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfPeople()
    {
        // Arrange
        var peopleResponseExpected = PersonMother.GetPersonList();
        var peopleDtoExpected = PersonDtoMother.GetPersonList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockPersonService.Setup(x => x.GetAll()).ReturnsAsync(peopleResponseExpected);

        // Act
        var response = await _peopleController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var peopleDtoResponse = response!.Value as List<PersonDto>;
        peopleDtoResponse.Should().NotBeNull();
        peopleDtoResponse.Should().BeEquivalentTo(peopleDtoExpected, ExcludeProperties);

        _mockPersonService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredPeople(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var personListResponseExpected = PersonMother.GetPersonList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Person>.Create(personListResponseExpected, queryRequest);
        var peopleDtoResponseExpected = _mapper.Map<List<PersonDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockPersonService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _peopleController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var peopleDtoResponse = response!.Value as List<PersonDto>;
        peopleDtoResponse.Should().NotBeNull();
        peopleDtoResponse.Should().BeEquivalentTo(peopleDtoResponseExpected);
        var paginationData = _peopleController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockPersonService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithPeople()
    {
        // Arrange
        var personResponseExpected = PersonMother.NaturalCCPerson();
        var personDtoResponseExpected = PersonDtoMother.NaturalCCPerson();
        var id = personResponseExpected.Id;

        _mockPersonService.Setup(x => x.GetById(id)).ReturnsAsync(personResponseExpected);

        // Act
        var response = await _peopleController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var personDtoResponse = response!.Value as PersonDto;
        personDtoResponse.Should().NotBeNull();
        personDtoResponse.Should().BeEquivalentTo(personDtoResponseExpected, ExcludeProperties);

        _mockPersonService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenPeopleIsValid_ReturnsCreatedWithPeople()
    {
        // Arrange
        var personDtoRequest = PersonDtoMother.NaturalCCPerson();
        personDtoRequest.Id = 0;
        var personDtoResponseExpected = PersonDtoMother.NaturalCCPerson();
        var personResponseExpected = PersonMother.NaturalCCPerson();

        _mockPersonService.Setup(x => x.Create(It.IsAny<Person>())).ReturnsAsync(personResponseExpected);

        // Act
        var response = await _peopleController.Post(personDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var personDtoResponse = response!.Value as PersonDto;
        personDtoResponse.Should().NotBeNull();
        personDtoResponse.Should().BeEquivalentTo(personDtoResponseExpected, ExcludeProperties);

        _mockPersonService.Verify(x => x.Create(It.IsAny<Person>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenPeopleIsValid_ReturnsOkWithUpdatedPeople()
    {
        // Arrange
        var personDtoRequest = PersonDtoMother.NaturalCCPerson();
        var personDtoResponseExpected = PersonDtoMother.NaturalCCPerson();
        var personResponseExpected = PersonMother.NaturalCCPerson();

        _mockPersonService.Setup(x => x.Edit(It.IsAny<Person>())).ReturnsAsync(personResponseExpected);

        // Act
        var response = await _peopleController.Put(personDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var personDtoResponse = response!.Value as PersonDto;
        personDtoResponse.Should().NotBeNull();
        personDtoResponse.Should().BeEquivalentTo(personDtoResponseExpected, ExcludeProperties);

        _mockPersonService.Verify(x => x.Edit(It.IsAny<Person>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _peopleController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockPersonService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}