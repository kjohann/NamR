using System;

namespace NamR.Shared.Models
{
    public class NewListItemModel
    {
        public Guid ListIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsGirl { get; set; }
    }
}
