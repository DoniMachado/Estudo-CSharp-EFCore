namespace EFCore.Domain.Common.ValueObject;

public record ResultPaginationVO(int Count, int TotalPages, int PageIndex, int PageSize, dynamic Itens)
{
    public bool HasPrevious => PageIndex > 1;
    public bool HasNext => PageIndex < TotalPages;

}
