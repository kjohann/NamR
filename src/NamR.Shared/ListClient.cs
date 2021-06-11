﻿using NamR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NamR.Shared
{
    public class ListClient
    {
        private readonly HttpClient _client;

        public ListClient(
            HttpClient client
        )
        {
            _client = client;
        }

        public async Task<IEnumerable<ListItemModel>> GetList(Guid listId)
        {
            var res = await _client.GetFromJsonAsync<IEnumerable<ListItemModel>>($"api/list/{listId}");
            return res;
        }

        public async Task<ListItemModel> AddItem(NewListItemModel item)
        {
            var res = await _client.PostAsJsonAsync("api/list/item", item);

            // TODO: have fun
            res.EnsureSuccessStatusCode();

            return await res.Content.ReadFromJsonAsync<ListItemModel>();
        }

        public async Task RemoveItem(Guid listItemId)
        {
            var res = await _client.DeleteAsync($"api/list/item/{listItemId}");
            // TODO: have fun
            res.EnsureSuccessStatusCode();
        }
    }
}
