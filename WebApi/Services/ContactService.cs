using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services;

public class ContactService
{
    public async Task<List<ContactModel>> GetAll(string? firstName = default, string? lastName = default, string? phoneNumber = default)
    {
        var result = GenerateFew(_ => new ContactModel
        {
            Id = Random.Shared.NextInt64(),
            FirstName = firstName ?? "John",
            LastName = lastName ?? "Smith",
            PhoneNumber = phoneNumber ?? "+12345678900",
        });

        return result.ToList();
    }

    public async Task<List<ContactModel>> GetByIds(params long[] ids)
    {
        var result = ids.Select(id => new ContactModel
        {
            Id = id,
            FirstName = "John",
            LastName = "Smith",
            PhoneNumber = "+12345678900",
        });

        return result.ToList();
    }

    public async Task<ContactModel> Create(ContactCreateModel model)
    {
        var result = new ContactModel
        {
            Id = Random.Shared.NextInt64(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
        };

        return result;
    }

    public async Task<ContactModel?> Update(long contactId, ContactCreateModel model)
    {
        var result = new ContactModel
        {
            Id = contactId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
        };

        return result;
    }

    public async Task<bool> Delete(long contactId)
    {
        return true;
    }

    private static IEnumerable<T> GenerateFew<T>(Func<int, T> generator)
    {
        var count = Random.Shared.Next(3, 10);
        return Enumerable.Range(0, count).Select(generator);
    }
}
