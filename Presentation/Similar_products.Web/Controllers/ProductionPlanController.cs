using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Web.Controllers;

[Route("api/productionPlans")]
[ApiController]
public class ProductionPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductionPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var productionPlans = await _mediator.Send(new GetProductionPlansQuery());

        return Ok(productionPlans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var productionPlan = await _mediator.Send(new GetProductionPlanByIdQuery(id));

        if (productionPlan is null)
        {
            return NotFound($"ProductionPlan with id {id} is not found.");
        }
        
        return Ok(productionPlan);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductionPlanForCreationDto? productionPlan)
    {
        if (productionPlan is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateProductionPlanCommand(productionPlan));

        return CreatedAtAction(nameof(Create), productionPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductionPlanForUpdateDto? productionPlan)
    {
        if (productionPlan is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateProductionPlanCommand(productionPlan));

        if (!isEntityFound)
        {
            return NotFound($"ProductionPlan with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteProductionPlanCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ProductionPlan with id {id} is not found.");
        }

        return NoContent();
    }
}
