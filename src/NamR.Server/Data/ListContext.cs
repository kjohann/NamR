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

        public IQueryable<ListItem> GetAllListItems(Guid listIdentifier) => ListItems.Where(l => l.ListIdentifier == listIdentifier);

        public async Task<ListItemModel> Add(NewListItemModel item)
        {
            var res = await AddItem(item.ToEntity());
            return res.ToModel();
        }

        public async Task<ListItem> AddItem(ListItem item)
        {
            var res = await ListItems.AddAsync(item);

            await SaveChangesAsync();

            return res.Entity;
        }

        public async Task Remove(Guid externalId)
        {
            var entity = await ListItems.SingleOrDefaultAsync(l => l.ExternalId == externalId);

            if (entity == null)
            {
                return;
            }

            ListItems.Remove(entity);
            await SaveChangesAsync();
        }
    }
}
