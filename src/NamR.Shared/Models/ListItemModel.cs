using System;

namespace NamR.Shared.Models
{
    public class ListItemModel
    {
        public Guid Id { get; set; }
        public Guid ListIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsGirl { get; set; }
    }
}
