using System;

namespace NamR.Shared.Models
{
    public interface IListItemModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool IsGirl { get; set; }
    }
}
