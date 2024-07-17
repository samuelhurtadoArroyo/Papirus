using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.UnitTests.Common;

[ExcludeFromCodeCoverage]
public class RepositoryTests
{
    private List<Case> _caseList = null!;

    private PropertyInfo _propertyInfoString = null!;

    private PropertyInfo _propertyInfoInt = null!;

    private PropertyInfo _propertyInfoDateTimeNullable = null!;

    [SetUp]
    public void SetUp()
    {
        _caseList = CaseMother.GetCaseList();
        _propertyInfoString = typeof(Case).GetProperty("Court")!;
        _propertyInfoInt = typeof(Case).GetProperty("ProcessId")!;
        _propertyInfoDateTimeNullable = typeof(Case).GetProperty("RegistrationDate")!;
    }

    [Ignore("Due date")]
    [TestCase(FilterOptions.StartsWith, "Court")]
    [TestCase(FilterOptions.EndsWith, "Court")]
    [TestCase(FilterOptions.Contains, "Court")]
    [TestCase(FilterOptions.DoesNotContain, "Invalid")]
    [TestCase(FilterOptions.IsEmpty, "")]
    [TestCase(FilterOptions.IsNotEmpty, "")]
    [TestCase(FilterOptions.IsEqualTo, "court")]
    [TestCase(FilterOptions.IsNotEqualTo, "court")]
    public void FilterData_WhenStringFilterOption_AppliesCorrectStringFilter(FilterOptions filterOption, string filterValue)
    {
        // Arrange
        var expected = ApplyStringFilter(_caseList, filterOption, filterValue);

        // Act
        var result = typeof(Repository<Case>)
            .GetMethod("FilterData", BindingFlags.NonPublic | BindingFlags.Static)
            ?.Invoke(null, [_caseList, _propertyInfoString, filterOption, filterValue]) as IEnumerable<Case>;

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Ignore("Due date")]
    [TestCase(FilterOptions.IsGreaterThan, "1")]
    [TestCase(FilterOptions.IsGreaterThanOrEqualTo, "1")]
    [TestCase(FilterOptions.IsLessThan, "2")]
    [TestCase(FilterOptions.IsLessThanOrEqualTo, "2")]
    [TestCase(FilterOptions.IsEqualTo, "1")]
    [TestCase(FilterOptions.IsNotEqualTo, "2")]
    public void FilterData_WhenIntFilterOption_AppliesCorrectIntFilter(FilterOptions filterOption, string filterValue)
    {
        // Arrange
        var expected = ApplyIntFilter(_caseList, filterOption, filterValue);

        // Act
        var result = typeof(Repository<Case>)
            .GetMethod("FilterData", BindingFlags.NonPublic | BindingFlags.Static)
            ?.Invoke(null, [_caseList, _propertyInfoInt, filterOption, filterValue]) as IEnumerable<Case>;

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Ignore("Due date")]
    [TestCase(FilterOptions.IsGreaterThan, "2023-01-01")]
    [TestCase(FilterOptions.IsGreaterThanOrEqualTo, "2023-01-01")]
    [TestCase(FilterOptions.IsLessThan, "2025-12-31")]
    [TestCase(FilterOptions.IsLessThanOrEqualTo, "2025-12-31")]
    [TestCase(FilterOptions.IsEqualTo, "2024-03-01")]
    [TestCase(FilterOptions.IsNotEqualTo, "2024-03-01")]
    public void FilterData_WhenDateTimeNullableFilterOption_AppliesCorrectDateTimeFilter(FilterOptions filterOption, string filterValue)
    {
        // Arrange
        var expected = ApplyDateTimeFilter(_caseList, filterOption, filterValue);

        // Act
        var result = typeof(Repository<Case>)
            .GetMethod("FilterData", BindingFlags.NonPublic | BindingFlags.Static)
            ?.Invoke(null, [_caseList, _propertyInfoDateTimeNullable, filterOption, filterValue]) as IEnumerable<Case>;

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Ignore("Due date")]
    [TestCase(FilterOptions.IsGreaterThan)]
    [TestCase(FilterOptions.IsGreaterThanOrEqualTo)]
    [TestCase(FilterOptions.IsLessThan)]
    [TestCase(FilterOptions.IsLessThanOrEqualTo)]
    [TestCase(FilterOptions.IsEqualTo)]
    [TestCase(FilterOptions.IsNotEqualTo)]
    public void FilterData_WhenInvalidDateTimeFormat_ReturnsAllItems(FilterOptions filterOption)
    {
        // Arrange
        var filterValue = "invalid-date";
        var expected = _caseList; // When parsing fails, no filtering should occur, so all items are returned

        // Act
        var result = typeof(Repository<Case>)
            .GetMethod("FilterData", BindingFlags.NonPublic | BindingFlags.Static)
            ?.Invoke(null, [_caseList, _propertyInfoDateTimeNullable, filterOption, filterValue]) as IEnumerable<Case>;

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    private static IEnumerable<Case> ApplyStringFilter(IEnumerable<Case> items, FilterOptions filterOption, string filterValue)
    {
        return filterOption switch
        {
            FilterOptions.StartsWith => items.Where(x => x.Court.StartsWith(filterValue, StringComparison.CurrentCultureIgnoreCase)),
            FilterOptions.EndsWith => items.Where(x => x.Court.ToLower().EndsWith(filterValue.ToLower())),
            FilterOptions.Contains => items.Where(x => x.Court.Contains(filterValue, StringComparison.CurrentCultureIgnoreCase)),
            FilterOptions.DoesNotContain => items.Where(x => !x.Court.Contains(filterValue, StringComparison.CurrentCultureIgnoreCase)),
            FilterOptions.IsEmpty => items.Where(x => string.IsNullOrEmpty(x.Court)),
            FilterOptions.IsNotEmpty => items.Where(x => !string.IsNullOrEmpty(x.Court)),
            FilterOptions.IsEqualTo => items.Where(x => x.Court.Equals(filterValue, StringComparison.CurrentCultureIgnoreCase)),
            FilterOptions.IsNotEqualTo => items.Where(x => !x.Court.Equals(filterValue, StringComparison.CurrentCultureIgnoreCase)),
            _ => throw new ArgumentOutOfRangeException(nameof(filterOption), filterOption, "Unsupported filter option"),
        };
    }

    private static IEnumerable<Case> ApplyIntFilter(IEnumerable<Case> items, FilterOptions filterOption, string filterValue)
    {
        int value = int.Parse(filterValue);

        return filterOption switch
        {
            FilterOptions.IsGreaterThan => items.Where(x => x.ProcessId > value),
            FilterOptions.IsGreaterThanOrEqualTo => items.Where(x => x.ProcessId >= value),
            FilterOptions.IsLessThan => items.Where(x => x.ProcessId < value),
            FilterOptions.IsLessThanOrEqualTo => items.Where(x => x.ProcessId <= value),
            FilterOptions.IsEqualTo => items.Where(x => x.ProcessId == value),
            FilterOptions.IsNotEqualTo => items.Where(x => x.ProcessId != value),
            _ => throw new ArgumentOutOfRangeException(nameof(filterOption), filterOption, "Unsupported filter option"),
        };
    }

    private static IEnumerable<Case> ApplyDateTimeFilter(IEnumerable<Case> items, FilterOptions filterOption, string filterValue)
    {
        if (!DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
        {
            return items;
        }

        return filterOption switch
        {
            FilterOptions.IsGreaterThan => items.Where(x => x.RegistrationDate > value),
            FilterOptions.IsGreaterThanOrEqualTo => items.Where(x => x.RegistrationDate >= value),
            FilterOptions.IsLessThan => items.Where(x => x.RegistrationDate < value),
            FilterOptions.IsLessThanOrEqualTo => items.Where(x => x.RegistrationDate <= value),
            FilterOptions.IsEqualTo => items.Where(x => x.RegistrationDate == value),
            FilterOptions.IsNotEqualTo => items.Where(x => x.RegistrationDate != value),
            _ => throw new ArgumentOutOfRangeException(nameof(filterOption), filterOption, "Unsupported filter option"),
        };
    }
}