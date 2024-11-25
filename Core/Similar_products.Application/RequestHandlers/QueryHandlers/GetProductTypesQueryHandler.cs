using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductTypesQueryHandler : IRequestHandler<GetProductTypesQuery, IEnumerable<ProductTypeDto>>
{
	private readonly IProductTypeRepository _repository;
	private readonly IMapper _mapper;

	public GetProductTypesQueryHandler(IProductTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ProductTypeDto>> Handle(GetProductTypesQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ProductTypeDto>>(await _repository.Get(trackChanges: false));
}
