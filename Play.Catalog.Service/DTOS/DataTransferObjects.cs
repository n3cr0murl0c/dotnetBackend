

namespace Play.Catalog.Service.DTOS{
    public record ItemDto( Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedAt);

    public record CreateItemDto( string Name, string Description, decimal Price);

    public record UpdateItemDto(string Name, string Description, decimal Price);
}