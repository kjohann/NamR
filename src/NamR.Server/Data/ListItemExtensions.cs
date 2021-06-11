using NamR.Shared.Models;
using System;

namespace NamR.Server.Data
{
    public static class ListItemExtensions
    {
        public static ListItem ToEntity(this NewListItemModel item) => new ListItem
        {
            ExternalId = Guid.NewGuid(),
            ListIdentifier = item.ListIdentifier,
            Name = item.Name,
            IsGirl = item.IsGirl
        };

        public static ListItemModel ToModel(this ListItem item) => new ListItemModel
        {
            Id = item.ExternalId,
            ListIdentifier = item.ListIdentifier,
            Name = item.Name,
            IsGirl = item.IsGirl
        };
    }
}
