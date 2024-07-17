namespace Papirus.WebApi.Api.Binders;

[ExcludeFromCodeCoverage]
public class QueryRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var valueproviders = bindingContext.ValueProvider;

        var queryRequest = new QueryRequest();

        if (valueproviders.GetValue("PageNumber").Length > 0)
            queryRequest.PageNumber = Convert.ToInt32(valueproviders.GetValue("PageNumber").FirstValue);
        if (valueproviders.GetValue("PageSize").Length > 0)
            queryRequest.PageSize = Convert.ToInt32(valueproviders.GetValue("PageSize").FirstValue);
        if (valueproviders.GetValue("SearchString").Length > 0)
            queryRequest.SearchString = valueproviders.GetValue("SearchString").FirstValue;
        if (valueproviders.GetValue("FilterParams").Length > 0)
        {
            var filterParamslist = new List<FilterParams>();
            foreach (var item in valueproviders.GetValue("FilterParams").ToList())
            {
                var filter = JsonConvert.DeserializeObject<FilterParams>(item.Replace("\n", ""));
                filterParamslist.Add(filter!);
            }
            queryRequest.FilterParams = filterParamslist;
        }
        if (valueproviders.GetValue("SortingParams").Length > 0)
        {
            var sortingParamslist = new List<SortingParams>();
            foreach (var item in valueproviders.GetValue("SortingParams").ToList())
            {
                try
                {
                    var sorter = JsonConvert.DeserializeObject<SortingParams>(item.Replace("\n", ""));
                    sortingParamslist.Add(sorter!);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
            }
            queryRequest.SortingParams = sortingParamslist;
        }

        bindingContext.Result = ModelBindingResult.Success(queryRequest);
        return Task.CompletedTask;
    }
}