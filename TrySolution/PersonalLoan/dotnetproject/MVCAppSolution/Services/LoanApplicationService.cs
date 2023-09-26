using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookStoreApp.Models;

namespace BookStoreApp.Services
{
    public interface ILoanApplicationService
    {
        bool AddLoanApplication(LoanApplication loanApplication);
        List<LoanApplication> GetAllLoanApplications();
        bool DeleteLoanApplication(int id);
    }
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly HttpClient _httpClient;
            public LoanApplicationService(HttpClient httpClient,IConfiguration configuration)
        {
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
_httpClient=new HttpClient(clientHandler);
         var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
        _httpClient.BaseAddress =new Uri(apiSettings.BaseUrl) ;
        }

        public bool AddLoanApplication(LoanApplication loanApplication)
        {
            try
            {
                var json = JsonConvert.SerializeObject(loanApplication);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress+$"/LoanApplication", content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public List<LoanApplication> GetAllLoanApplications()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress+"/LoanApplication").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<LoanApplication>>(data);
                }

                return new List<LoanApplication>();
            }
            catch (HttpRequestException)
            {
                return new List<LoanApplication>();
            }
        }

        public bool DeleteLoanApplication(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress+$"/LoanApplication/{id}").Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
