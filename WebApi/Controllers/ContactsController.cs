using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    /// <summary>
    /// Returns all exising contacts, filters can be applied.
    /// </summary>
    /// <param name="firstName">Filter by first name.</param>
    /// <param name="lastName">Filter by first name.</param>
    /// <param name="phoneNumber">Filter by first name.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactModel>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll(string? firstName = default, string? lastName = default, string? phoneNumber = default)
    {
        var result = GenerateFew(_ => new ContactModel
        {
            Id = Random.Shared.NextInt64(),
            FirstName = firstName ?? "John",
            LastName = lastName ?? "Smith",
            PhoneNumber = phoneNumber ?? "+12345678900",
        });

        return Task.FromResult<IActionResult>(Ok(result));
    }

    /// <summary>
    /// Get contact by id.
    /// </summary>
    /// <param name="contactId">Contact id.</param>
    [HttpGet("{contactId:long}")]
    [ProducesResponseType(typeof(ContactModel), StatusCodes.Status200OK)]
    public Task<IActionResult> Get(long contactId)
    {
        var result = new ContactModel
        {
            Id = Random.Shared.NextInt64(),
            FirstName = "John",
            LastName = "Smith",
            PhoneNumber = "+12345678900",
        };

        return Task.FromResult<IActionResult>(Ok(result));
    }

    /// <summary>
    /// Create a new contact.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ContactModel), StatusCodes.Status200OK)]
    public Task<IActionResult> Post([FromBody]ContactCreateModel model)
    {
        var result = new ContactModel
        {
            Id = Random.Shared.NextInt64(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
        };

        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpPut("{contactId:long}")]
    [ProducesResponseType(typeof(ContactModel), StatusCodes.Status200OK)]
    public Task<IActionResult> Put(long contactId, [FromBody] ContactCreateModel model)
    {
        var result = new ContactModel
        {
            Id = contactId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
        };

        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpDelete("{contactId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> Delete(long contactId)
    {
        return Task.FromResult<IActionResult>(Ok());
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
}
