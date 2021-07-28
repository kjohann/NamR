using System;

namespace NamR.Shared.Models
{
    public class CompareListItemModel
    {
        public Guid Id { get; set; }
        public Guid CompareListIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsGirl { get; set; }
    }
}
