using NamR.Shared.Models;
using System;

namespace NamR.Server.Data
{
    public static class ListItemExtensions
    {
        public static ListItem ToEntity(this NewListItemModel item) => new()
        {
            ExternalId = Guid.NewGuid(),
            ListIdentifier = item.ListIdentifier,
            Name = item.Name,
            IsGirl = item.IsGirl
        };

        public static ListItemModel ToModel(this ListItem item) => new()
        {
            Id = item.ExternalId,
            CompareListIdentifier = item.CompareListIdentifier,
            ListIdentifier = item.ListIdentifier,
            Name = item.Name,
            IsGirl = item.IsGirl
        };

        public static CompareListItemModel ToComparemodel(this ListItem item) => new()
        {
            Id = item.ExternalId,
            CompareListIdentifier = item.CompareListIdentifier,
            IsGirl = item.IsGirl,
            Name = item.Name
        };
    }
}
