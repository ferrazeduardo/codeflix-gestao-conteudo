using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput,CategoryModelOutput>
{ }