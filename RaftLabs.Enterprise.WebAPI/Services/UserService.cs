using AutoMapper;
using System.Text.Json;
using RaftLabs.Enterprise.Configuration;
using RaftLabs.Enterprise.Domain.DTOs;
using RaftLabs.Enterprise.Domain.Models;
using RaftLabs.Enterprise.WebAPI.Interfaces.Services;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;

namespace RaftLabs.Enterprise.WebAPI.Services
{
    internal class UserService : BaseService<User>, IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IMemoryCache memoryCache;
        private readonly string apiBaseUrl;
        private readonly IHttpClientFactory clientFactory;
        public UserService(IConfiguration configuration, IHttpClientFactory clientFactory, ISettings settings, IMapper mapper, IMemoryCache memoryCache) : base(settings)
        {
            this.configuration = configuration;
            this.clientFactory = clientFactory;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            apiBaseUrl = configuration.GetValue<string>("AppSettings:ApiBaseUrl");
        }
        async Task<UserDTO> IUserService.GetUserByIdAsync(string userId)
        {
            if (memoryCache.TryGetValue($"User_{userId}", out UserDTO cachedUser))
            {
                return cachedUser;
            }

            UserDTO userDTO = new();
            using (var client = clientFactory.CreateClient("RetryOnFailure"))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = null;

                try
                {
                    response = await client.GetAsync(apiBaseUrl + "users/" + userId);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine($"Unauthorized");
                        throw new UnauthorizedAccessException("Unauthorized!");
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        GetUserApiResponse apiResponse = JsonSerializer.Deserialize<GetUserApiResponse>(result);

                        //AutoMapper Not working check later
                        //userDTO = mapper.Map<UserDTO>(apiResponse.data);

                        userDTO = new()
                        {
                            Id = apiResponse.data.id,
                            Email = apiResponse.data.email,
                            Firstname = apiResponse.data.first_name,
                            Lastname = apiResponse.data.last_name,
                            Avatar = apiResponse.data.avatar,
                        };
                        memoryCache.Set($"User_{userId}", userDTO, TimeSpan.FromMinutes(configuration.GetValue<int>("AppSettings:CacheExpiryInMinutes")));
                        return userDTO;
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Socket Exception: {ex.Message}");
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                        response.StatusCode = System.Net.HttpStatusCode.RequestTimeout;
                }
                catch (IOException ex)
                {
                    response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                }
                catch
                {
                    response.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                }
                return userDTO;
            }
        }
        async Task<IEnumerable<UserDTO>> IUserService.GetAllUsersAsync(string pageNumber)
        {
            if (memoryCache.TryGetValue($"UserList_{pageNumber}", out List<UserDTO> cachedUsers))
            {
                return cachedUsers;
            }
            List<UserDTO> userDTOs = new();
            UserDTO userDTO = new();
            using (var client = clientFactory.CreateClient("RetryOnFailure"))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = null;

                try
                {
                    response = await client.GetAsync(apiBaseUrl + "users?page=" + pageNumber);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        GetAllUserApiResponse apiResponse = JsonSerializer.Deserialize<GetAllUserApiResponse>(result);

                        //AutoMapper Not working check later
                        //userDTOs = mapper.Map<List<UserDTO>>(apiResponse.data);

                        foreach (var user in apiResponse.data)
                        {
                            userDTO = new()
                            {
                                Id = user.id,
                                Email = user.email,
                                Firstname = user.first_name,
                                Lastname = user.last_name,
                                Avatar = user.avatar,
                            };
                            userDTOs.Add(userDTO);
                        }
                        memoryCache.Set($"UserList_{pageNumber}", userDTOs, TimeSpan.FromMinutes(configuration.GetValue<int>("AppSettings:CacheExpiryInMinutes")));
                        return userDTOs;
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Socket Exception: {ex.Message}");
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                        response.StatusCode = System.Net.HttpStatusCode.RequestTimeout;
                }
                catch (IOException ex)
                {
                    response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                }
                catch
                {
                    response.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                }
                return userDTOs;
            }
        }

    }
}
