using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.CreateCategory;
//opcional
public interface ICreateCategory : IRequestHandler<CreateCategoryInput,CategoryModelOutput>
{ }