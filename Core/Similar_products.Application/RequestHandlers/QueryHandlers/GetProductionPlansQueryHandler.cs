using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductionPlansQueryHandler : IRequestHandler<GetProductionPlansQuery, IEnumerable<ProductionPlanDto>>
{
	private readonly IProductionPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetProductionPlansQueryHandler(IProductionPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ProductionPlanDto>> Handle(GetProductionPlansQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ProductionPlanDto>>(await _repository.Get(trackChanges: false));
}
