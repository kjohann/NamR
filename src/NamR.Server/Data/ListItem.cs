using System;

namespace NamR.Server.Data
{
    public class ListItem
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public Guid ListIdentifier { get; set; }
        public Guid CompareListIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsGirl { get; set; }
    }
}
