using System;
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
        var result = await _contactService.Get(contactId);

        return result == default ? NotFound() : Ok(result);
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
    public Task<IActionResult> GetGroups(long contactId)
    {
        var result = GenerateFew(_ => Random.Shared.NextInt64());

        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpPut("{contactId:long}/Groups/{groupId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> AddGroup(long contactId, long groupId)
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpDelete("{contactId:long}/Groups/{groupId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> DeleteGroup(long contactId, long groupId)
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    private static IEnumerable<T> GenerateFew<T>(Func<int, T> generator)
    {
        var count = Random.Shared.Next(3, 10);
        return Enumerable.Range(0, count).Select(generator);
    }

    public ContactsController(ContactService contactService)
    {
        _contactService = contactService;
    }
}
