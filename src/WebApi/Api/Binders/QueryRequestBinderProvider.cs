namespace Papirus.WebApi.Api.Binders;

[ExcludeFromCodeCoverage]
public class QueryRequestBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(context));

        if (context.Metadata.ModelType == typeof(QueryRequest))
        {
            return new BinderTypeModelBinder(typeof(QueryRequestModelBinder));
        }

        return null;
    }
}