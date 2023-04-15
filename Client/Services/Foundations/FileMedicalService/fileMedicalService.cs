﻿using Client.Services.Exceptions;
using DTO;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http.Json;

namespace Client.Services.Foundations.FileMedicalService
{
    public class fileMedicalService : IfileMedicalService
    {
        public HttpClient httpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public fileMedicalService(HttpClient httpClient, ILocalStorageServices localStorageServices)
        {
            httpClient = httpClient;
            this.localStorageServices = localStorageServices;
        }
        public async Task<FileMedicalPatientDto> PostFileMedicalPatientAsync(FileMedicalToAddDto fileMedicalToAdd)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/FileMedical/PostNewFileMedical");
            var keysReservation = JsonSerializer.Serialize(fileMedicalToAdd);

            request.Content = new StringContent(keysReservation, Encoding.UTF8, "application/json");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<FileMedicalPatientDto>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
         
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Condition User denied");
            }
           
            else
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}