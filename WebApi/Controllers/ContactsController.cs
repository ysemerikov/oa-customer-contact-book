using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly ContactService _contactService;
    private readonly GroupService _groupService;

    /// <summary>
    /// Returns all exising contacts, filters can be applied.
    /// </summary>
    /// <param name="firstName">Filter by first name.</param>
    /// <param name="lastName">Filter by first name.</param>
    /// <param name="phoneNumber">Filter by first name.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(string? firstName = default, string? lastName = default, string? phoneNumber = default)
    {
        var result = await _contactService.GetAll(firstName, lastName, phoneNumber);

        return Ok(result);
    }

    /// <summary>
    /// Get contact by id.
    /// </summary>
    /// <param name="contactId">Contact id.</param>
    [HttpGet("{contactId:long}")]
    [ProducesResponseType(typeof(ContactModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long contactId)
    {
        var result = await _contactService.GetByIds(contactId);

        return result.Count == 0 ? NotFound() : Ok(result[0]);
    }

    /// <summary>
    /// Create a new contact.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ContactModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody]ContactCreateModel model)
    {
        var result = await _contactService.Create(model);

        return Ok(result);
    }

    [HttpPut("{contactId:long}")]
    [ProducesResponseType(typeof(ContactModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(long contactId, [FromBody] ContactCreateModel model)
    {
        var result = await _contactService.Update(contactId, model);

        return result == default ? NotFound() : Ok(result);
    }

    [HttpDelete("{contactId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long contactId)
    {
        var result = await _contactService.Delete(contactId);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{contactId:long}/Groups")]
    [ProducesResponseType(typeof(IEnumerable<long>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGroups(long contactId)
    {
        var contactGroups = await _groupService.GetAll(contactId);

        return Ok(contactGroups.Select(x => x.Id));
    }

    [HttpPut("{contactId:long}/Groups/{groupId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddGroup(long contactId, long groupId)
    {
        return await _groupService.AddContactToGroup(contactId, groupId)
            ? Ok()
            : NotFound();
    }

    [HttpDelete("{contactId:long}/Groups/{groupId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGroup(long contactId, long groupId)
    {
        return await _groupService.RemoveContactFromGroup(contactId, groupId)
            ? Ok()
            : NotFound();
    }

    public ContactsController(ContactService contactService, GroupService groupService)
    {
        _contactService = contactService;
        _groupService = groupService;
    }
}
