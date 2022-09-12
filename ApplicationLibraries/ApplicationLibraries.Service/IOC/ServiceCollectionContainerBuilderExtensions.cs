public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddRefit<TRefitClient, THandler>(this IServiceCollection services, string urlKey) where TRefitClient : class where THandler : DelegatingHandler
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        services.AddRefitClient<TRefitClient>(new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            })
        }).ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(configuration[urlKey]))
            .AddHttpMessageHandler<THandler>();

        return services;
    }
}