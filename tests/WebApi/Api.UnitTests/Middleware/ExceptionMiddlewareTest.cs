using Papirus.Tests.Common.Define;

namespace Papirus.WebApi.Api.UnitTests.Middleware;

[ExcludeFromCodeCoverage]
public class ExceptionMiddlewareTest
{
    private DefaultHttpContext? defaultHttpContext;

    private ExceptionMiddleware exceptionMiddleware = null!;

    #region Setup

    public static IEnumerable<object[]> BusinessExceptionData =>
    [
        [new BusinessException(), 500, "Internal Server Error", "BusinessException"],
        [new BadRequestException("Bad Request"), 400, "Bad Request", "Bad Request"],
        [new NotFoundException("Not Found"), 404, "Not Found", "Not Found"],
        [new InternalServerErrorException("Internal Server Error"), 500, "Internal Server Error", "Internal Server Error"],
    ];

    [SetUp]
    public void Setup()
    {
        defaultHttpContext = new DefaultHttpContext();
    }

    #endregion Setup

    [Test, TestCaseSource("BusinessExceptionData")]
    public async Task InvokeAsync_WhenExceptionIsThrown_ReturnsExpectedErrorResponse(Exception ex, int statusCode, string errorType, string errorMessage)
    {
        // Arrange
        exceptionMiddleware = new ExceptionMiddleware(_ => throw ex);
        defaultHttpContext!.Response.Body = new MemoryStream();

        // Act
        await exceptionMiddleware.InvokeAsync(defaultHttpContext);
        var response = GetCustomErrorResponse(defaultHttpContext);

        // Asserts
        response.Should().NotBeNull();
        defaultHttpContext.Response.StatusCode.Should().Be(statusCode);
        response.ErrorType.Should().Be(errorType);
        response.Errors.Count.Should().Be(1);
        response.Errors[0].Should().Contain(errorMessage);
        var (keyContent, valueContent) = defaultHttpContext.HttpContext.Response.Headers.ElementAt(0);
        keyContent.Should().Be(CommonConst.ContentType);
        valueContent.Should().BeEquivalentTo(CommonConst.ApplicationJson);
        ex.Should().NotBeNull();
        ex.Message.Should().Contain(errorMessage);
        var baseException = ex.GetBaseException();
        baseException.GetType().Should().Be(ex.GetType());
    }

    [Test]
    public async Task InvokeAsync_WhenInnerExceptionIsThrown_ReturnsExpectedErrorResponse()
    {
        // Arrange
        const string errorMessage = "Business Exception";
        const string internalErrorMessage = "Internal Exception";
        const int statusCode = 500;
        const string errorType = "Internal Server Error";

        ApplicationException innerException = new(internalErrorMessage);
        BusinessException businessException = new(errorMessage, innerException);

        exceptionMiddleware = new ExceptionMiddleware(_ => throw businessException);
        defaultHttpContext!.Response.Body = new MemoryStream();

        // Act
        await exceptionMiddleware.InvokeAsync(defaultHttpContext);
        var response = GetCustomErrorResponse(defaultHttpContext);

        // Asserts
        response.Should().NotBeNull();
        defaultHttpContext.Response.StatusCode.Should().Be(statusCode);
        response.ErrorType.Should().Be(errorType);
        response.Errors.Count.Should().Be(2);
        response.Errors[0].Should().Contain(errorMessage);
        response.Errors[1].Should().Contain(internalErrorMessage);
        var (keyContent, valueContent) = defaultHttpContext.HttpContext.Response.Headers.ElementAt(0);
        keyContent.Should().Be(CommonConst.ContentType);
        valueContent.Should().BeEquivalentTo(CommonConst.ApplicationJson);
    }

    [Test]
    public async Task InvokeAsync_WhenHttpContextIsNull_ReturnsStatusOkAndNoResponseStarted()
    {
        // Arrange
        var next = new Mock<RequestDelegate>();
        exceptionMiddleware = new ExceptionMiddleware(next.Object);
        defaultHttpContext!.Response.Body = new MemoryStream();

        // Act
        await exceptionMiddleware.InvokeAsync(null!);

        // Asserts
        defaultHttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        defaultHttpContext.Response.HasStarted.Should().BeFalse();
    }

    [Test]
    public async Task InvokeAsync_WhenNoExceptionIsThrown_ReturnsStatusOkAndNoResponseStarted()
    {
        // Arrange
        var next = new Mock<RequestDelegate>();
        exceptionMiddleware = new ExceptionMiddleware(next.Object);
        defaultHttpContext!.Response.Body = new MemoryStream();

        //Act
        await exceptionMiddleware.InvokeAsync(defaultHttpContext);

        //Assert
        defaultHttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        defaultHttpContext.Response.HasStarted.Should().BeFalse();
    }

    [Test]
    public void UseExceptionMiddleware_WhenCalled_RegistersMiddleware()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        // Act
        app.UseExceptionMiddleware();

        // Asserts
        app.Should().NotBeNull();
    }

    private static ErrorDetails GetCustomErrorResponse(DefaultHttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var streamText = reader.ReadToEnd();
        var response = JsonConvert.DeserializeObject<ErrorDetails>(streamText);
        return response!;
    }
}