using Microsoft.AspNetCore.Components;
using NamR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NamR.Client.Components
{
    public partial class NameList<TListModel> where TListModel : IListItemModel
    {
        [Parameter]
        public bool IsGirl { get; set; }

        [Parameter]
        public string? ItemClass { get; set; }

        [Parameter]
        public List<TListModel> Items { get; set; } = new List<TListModel>();

        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public Func<TListModel, Task>? RemoveOnClick { get; set; }
        
        [Parameter]
        public Func<TListModel, string>? GetListIcon { get; set; }

        private bool HasItems() => Items.Any(i => i.IsGirl == IsGirl);

    }
}
