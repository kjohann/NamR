using Microsoft.EntityFrameworkCore;
using NamR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NamR.Server.Data
{

    public class ListContext : DbContext
    {
        public DbSet<ListItem> ListItems { get; set; }

        public ListContext(
            DbContextOptions<ListContext> options
        ) : base (options)
        {
        }

        public IEnumerable<ListItemModel> GetList(Guid listIdentifier) => GetAllListItems(listIdentifier).Select(l => l.ToModel());

        public IEnumerable<CompareListItemModel> GetListForComparing(Guid compareListIdentifier) => ListItems.Where(l => l.CompareListIdentifier == compareListIdentifier).Select(i => i.ToComparemodel());

        public IQueryable<ListItem> GetAllListItems(Guid listIdentifier) => ListItems.Where(l => l.ListIdentifier == listIdentifier);

        public async Task<ListItemModel> Add(NewListItemModel item)
        {
            var res = await AddItem(item.ToEntity());
            return res.ToModel();
        }

        public async Task<ListItem> AddItem(ListItem item)
        {
            if (ListItems.Any(i => i.ListIdentifier == item.ListIdentifier && i.Name == item.Name))
            {
                return ListItems.First(i => i.ListIdentifier == item.ListIdentifier && i.Name == item.Name);
            }
            item.CompareListIdentifier = await GetCompareIdentifier(item.ListIdentifier);
            var res = await ListItems.AddAsync(item);

            await SaveChangesAsync();

            return res.Entity;
        }

        public async Task Remove(Guid listId, Guid externalId)
        {
            var entity = await ListItems.SingleOrDefaultAsync(l => l.ListIdentifier == listId && l.ExternalId == externalId);

            if (entity == null)
            {
                return;
            }

            ListItems.Remove(entity);
            await SaveChangesAsync();
        }

        private async Task<Guid> GetCompareIdentifier(Guid listIdentifier) =>
            (await ListItems.FirstOrDefaultAsync(i => i.ListIdentifier == listIdentifier))?.CompareListIdentifier ?? Guid.NewGuid();
    }
}
