﻿using Snblog.Models;

namespace Snblog.Service.Extensions;

/// <summary>
/// 封装Select
/// 扩展EF Core以便在多个地方重复使用相同的代码
/// 使用AsNoTracking方法来禁用跟踪
/// </summary>
public static class SnippetExtensions
{
    public static IQueryable<SnippetDto> SelectSnippet(this IQueryable<Snippet> ret)
    {
        return ret.AsNoTracking().Select(e => new SnippetDto {
            Id = e.Id,
            Name = e.Name,
            Text = e.Text,
            UserId = e.UserId,
            User = new User {
                Id = e.User.Id,
                Name = e.User.Name,
                Nickname = e.User.Nickname
            },
            TypeId = e.TypeId,
            Type = new SnippetType {
                Id = e.Type.Id,
                Name = e.Type.Name
            },
            TypeSubId = e.TypeSubId,
            TypeSub = new SnippetTypeSub
            {
                Id = e.TypeSub.Id,
                Name = e.TypeSub.Name
            },
            TagId = e.TagId,
            Tag = new SnippetTag {
                Id = e.Tag.Id,
                Name = e.Tag.Name
            },
            SnippetVersionId = e.SnippetVersionId,
            TimeCreate = e.TimeCreate,
            TimeUpdate = e.TimeUpdate,
        });
    }
}