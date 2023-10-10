
using Bancard.API.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bancard.API
{
    public class BancardService
    {
        HttpClient client = new HttpClient();
        const string BaseAddress = "vpos/api/0.3/";
        const string Environment = "https://vpos.infonet.com.py:8888/";
        const string publicKey = "1UFiKuPqgccfTi3XX9iAA6Vt9Oa4dD63";
        const string privateKey = "0gRRUhowsFtSZJnSBdL3F+dvEq1k96mAbR0XppX.";

        public BancardService()
        {
            /*
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            */
            
        }

        public async Task<object> SingleBuy(int shop_process_id, string currency, string amount, string iva_amount, string additional_data, string description, string return_url, string cancel_url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");

            string strToHash = privateKey + shop_process_id + amount.ToString() + currency;
            string token = Helper.CryptoService.GenerateHashToken(strToHash);
            
            var operation = new SingleBuyModel() {shop_process_id= shop_process_id, additional_data = additional_data, amount = amount, currency = currency, cancel_url = cancel_url ,
                return_url = return_url, description = description, token = token};
            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync(Environment + BaseAddress + "single_buy", content);
            response.EnsureSuccessStatusCode();

            return response.Content.ReadFromJsonAsync<object>();

        }
        public async Task SingleBuyConfirmation(SingleBuyModel model)
        {
            var response = await client.PostAsJsonAsync(Environment + BaseAddress + "single_buy", model);
            response.EnsureSuccessStatusCode();
        }
        public async Task PreAuthorizations(SingleBuyModel model)
        {
            var response = await client.PostAsJsonAsync(Environment + BaseAddress + "single_buy", model);
            response.EnsureSuccessStatusCode();
        }
        public async Task SingleBuyRollBack(SingleBuyModel model)
        {
            var response = await client.PostAsJsonAsync(Environment + BaseAddress + "single_buy", model);
            response.EnsureSuccessStatusCode();
        }
        public async Task PayWithToken(SingleBuyModel model)
        {
            var response = await client.PostAsJsonAsync(Environment + BaseAddress + "single_buy", model);
            response.EnsureSuccessStatusCode();
        }
        public async Task CreditCardNew(SingleBuyModel model)
        {
            var response = await client.PostAsJsonAsync(Environment + BaseAddress+ "single_buy", model);
            response.EnsureSuccessStatusCode();
        }
        public async Task GetCreditCards(SingleBuyModel model)
        {
            var response = await client.PostAsJsonAsync(BaseAddress + "single_buy", model);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteCreditCard(SingleBuyModel model)
        {
            var response = await client.DeleteAsync(Environment + BaseAddress + "single_buy");
            response.EnsureSuccessStatusCode();
        }

    }
}
