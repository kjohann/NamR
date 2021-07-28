using System;

namespace NamR.Shared.Models
{
    public class ListItemModel : IListItemModel
    {
        public Guid Id { get; set; }
        public Guid CompareListIdentifier { get; set; }
        public Guid ListIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsGirl { get; set; }
    }
}
