﻿using Client.Services.Exceptions;
using DTO;
using System.Net.Http;
using System.Net;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace Client.Services.Foundations.SpecialisteAnalyseService
{
    public class SpecialisteAnalyseService : ISpecialisteAnalyseService
    {
        public HttpClient httpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public SpecialisteAnalyseService(HttpClient httpClient, ILocalStorageServices localStorageServices)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;
        }
        public async Task<InformationAnalyseResultDto> GetInformationAnalyse(string CodeQr)
        {
            CodeQr = CodeQr.Replace("/", "-");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/SpecialisteAnalyse/GetInformationAnalyse/{CodeQr}");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<InformationAnalyseResultDto>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Denied User Account");
            }
            else if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Error requesting the document");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task PostAnalyseResult(AnalyseResultToAdd analyseResultToAdd)
        {
            var ObjetAnalyseOnAddResult = new
            {
                IdLineAnalyse = analyseResultToAdd.IdLineAnalyse,
                FileUpload = Convert.ToBase64String(analyseResultToAdd.FileUpload)

            };
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/SpecialisteAnalyse/PostNewAnalyseResult");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var json = JsonConvert.SerializeObject(ObjetAnalyseOnAddResult);

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await this.httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Denied User Account");
            }
            else if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Data Has Been Canfirmed By Auther Radiology");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
