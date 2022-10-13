using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly GroupService _groupService;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GroupModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _groupService.GetAll();

        return Ok(result);
    }

    [HttpGet("{groupId:long}")]
    [ProducesResponseType(typeof(GroupModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long groupId)
    {
        var result = await _groupService.Get(groupId);
        return result == default ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GroupModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody]GroupCreateModel model)
    {
        var result = await _groupService.Create(model);

        return Ok(result);
    }

    [HttpPut("{groupId:long}")]
    [ProducesResponseType(typeof(GroupModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(long groupId, [FromBody] GroupCreateModel model)
    {
        var result = await _groupService.Update(groupId, model);

        return result == default ? NotFound() : Ok(result);
    }

    [HttpDelete("{groupId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long groupId)
    {
        return await _groupService.Delete(groupId) ? Ok() : NotFound();
    }

    [HttpGet("{groupId:long}/Contacts")]
    [ProducesResponseType(typeof(IEnumerable<long>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContacts(long groupId)
    {
        var result = await _groupService.GetContacts(groupId);

        return Ok(result);
    }

    public GroupsController(GroupService groupService)
    {
        _groupService = groupService;
    }
}
