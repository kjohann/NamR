using System;

namespace NamR.Shared.Models
{
    public record NewListItemModel
    {
        public Guid ListIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsGirl => Gender == Gender.Girl;
        public Gender Gender { get; set; }
    }
}
