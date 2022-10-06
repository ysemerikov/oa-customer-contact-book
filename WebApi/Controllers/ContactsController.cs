using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    [HttpGet]
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

    [HttpGet("{contactId:long}")]
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

    [HttpPost]
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
    public Task<IActionResult> Delete(long contactId)
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpGet("{contactId:long}/Groups")]
    public Task<IActionResult> GetGroups(long contactId)
    {
        var result = GenerateFew(_ => Random.Shared.NextInt64());

        return Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpPut("{contactId:long}/Groups/{groupId:long}")]
    public Task<IActionResult> AddGroup(long contactId, long groupId)
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpDelete("{contactId:long}/Groups/{groupId:long}")]
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
