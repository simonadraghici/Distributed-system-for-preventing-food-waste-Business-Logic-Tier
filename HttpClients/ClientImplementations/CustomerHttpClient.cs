﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;


namespace HttpClients.ClientImplementations;

public class CustomerHttpClient : ICustomerService
{
    private readonly HttpClient _client;
    private readonly IAuthService _authService;

    public CustomerHttpClient(HttpClient client, IAuthService authService)
    {
        _client = client;
        _authService = authService;
        

    }

    public async Task CreateAsync(CustomerCreationDTO dto)
    {
        HttpResponseMessage message = await _client.PostAsJsonAsync("/Customers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateAsync(CustomerUpdateDTO dto)
    {
        //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.);
        HttpResponseMessage message = await _client.PatchAsJsonAsync("/Customers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteAsync(int accountId)
    {
       // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Jwt);
        HttpResponseMessage response = await _client.DeleteAsync($"/Customers/{accountId}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}