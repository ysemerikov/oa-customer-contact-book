using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Services;

public class GroupService
{
    private readonly ContactService _contactService;

    public async Task<List<GroupModel>> GetAll(long? contactId = default)
    {
        var result = GenerateFew(_ => new GroupModel
        {
            Id = Random.Shared.NextInt64(),
            Name = Guid.NewGuid().ToString(),
            MemberCount = Random.Shared.Next(),
        });

        return result.ToList();
    }

    public async Task<GroupModel?> Get(long groupId)
    {
        var result = new GroupModel
        {
            Id = groupId,
            Name = Guid.NewGuid().ToString(),
            MemberCount = Random.Shared.Next(),
        };

        return result;
    }

    public async Task<GroupModel> Create([FromBody]GroupCreateModel model)
    {
        var result = new GroupModel
        {
            Id = Random.Shared.NextInt64(),
            Name = model.Name,
            MemberCount = 0,
        };

        return result;
    }

    public async Task<GroupModel?> Update(long groupId, [FromBody] GroupCreateModel model)
    {
        var result = new GroupModel
        {
            Id = groupId,
            Name = model.Name,
            MemberCount = Random.Shared.Next(),
        };

        return result;
    }

    public async Task<bool> Delete(long groupId)
    {
        return true;
    }

    public async Task<List<ContactModel>> GetContacts(long groupId)
    {
        var contactIds = GenerateFew(_ => Random.Shared.NextInt64());

        return await _contactService.GetByIds(contactIds.ToArray());
    }

    public async Task<bool> AddContactToGroup(long contactId, long groupId)
    {
        return true;
    }

    public async Task<bool> RemoveContactFromGroup(long contactId, long groupId)
    {
        return true;
    }

    private static IEnumerable<T> GenerateFew<T>(Func<int, T> generator)
    {
        var count = Random.Shared.Next(3, 10);
        return Enumerable.Range(0, count).Select(generator);
    }

    public GroupService(ContactService contactService)
    {
        _contactService = contactService;
    }
}
