
using Bancard.API.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

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
                       
        }

        public async Task<object> SingleBuy(int shop_process_id, string currency, string amount, string iva_amount, string additional_data, string description, string return_url, string cancel_url)
        {
            string strToHash = privateKey + shop_process_id + amount.ToString() + currency;
            string token = Helper.CryptoService.GenerateHashToken(strToHash);
            
            var operation = new SingleBuyModel(){shop_process_id= shop_process_id, additional_data = additional_data, amount = amount, currency = currency, 
                                                 cancel_url = cancel_url ,return_url = return_url, description = description, token = token};
            
            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "single_buy", json);

            return rta;
        }
       
        public async Task<string> SingleBuyConfirmation(int shop_process_id)
        {
            string strToHash = privateKey + shop_process_id + "get_confirmation";
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new SingleBuyConfirmationModel()
            {
                shop_process_id = shop_process_id,
                token = token
            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "single_buy/confirmations", json);

            return rta;
        }
        public async Task<string> PreAuthorizations(int shop_process_id)
        {
            string strToHash = privateKey + shop_process_id + "pre-authorization-confirm";
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new PreauthorizationModel()
            {
                shop_process_id = shop_process_id,
                token = token
            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "preauthorizations/confirm", json);

            return rta;
        }
        public async Task<string> SingleBuyRollBack(int shop_process_id)
        {
            string strToHash = privateKey + shop_process_id  +"rollback" + "0.00";
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new SingleBuyRollBackModel()
            {
                shop_process_id = shop_process_id,
                token = token
            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "single_buy/rollback", json);

            return rta;
        }
        public async Task<string> PayWithToken(int shop_process_id, string amount, string iva_amount, string currency, int number_of_payments, string additional_data , string preauthorization, string aliasToken)
        {

            string strToHash = privateKey + shop_process_id + "charge" + amount + aliasToken;
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new TokenPayModel()
            {
                shop_process_id = shop_process_id,
                token = token,
                amount = amount,
                iva_amount = iva_amount,
                currency = currency,
                number_of_payments = number_of_payments,
                additional_data = additional_data,
                preauthorization = preauthorization,
                alias_token = aliasToken

            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "charge", json);

            return rta;
        }
        public async Task<string> CreditCardNew(int card_id, int user_id, string user_cell_phone, string user_mail, string return_url)
        {

            string strToHash = privateKey + card_id + user_id + "request_new_card";
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new CreditCardNewModel()
            {
                card_id = card_id,
                user_mail = user_mail,
                user_id = user_id,
                user_cell_phone = user_cell_phone,
                return_url = return_url,
                token = token
            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "cards/new ", json);

            return rta;
        }
        public async Task<string> GetCreditCards(string user_id, List<string> extra_response_attributes)
        {

            string strToHash = privateKey + user_id + "request_user_cards";
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new CreditCardGetModel()
            {
                token = token,
                extra_response_attributes = extra_response_attributes
            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await PostAsync("", Environment + BaseAddress, "users/user_id/cards", json);

            return rta;
        }
        public async Task<string> DeleteCreditCard(string user_id, string card_token, string alias_token)
        {

            string strToHash = privateKey + "delete_card" + user_id + card_token;
            string token = Helper.CryptoService.GenerateHashToken(strToHash);

            var operation = new CreditCardDeleteModel()
            {
                alias_token = alias_token,
                token = token
            };

            var model = new RequestRootModel() { operation = operation, public_key = publicKey };
            var json = JsonConvert.SerializeObject(model);
            var rta = await DeleteAsync("", Environment + BaseAddress, "users/user_id/cards", json);

            return rta;
        }

        private Task<string> PostAsync(string basicKey, string address, string uri, string jsonContent)
        {
            Task<string> salida;

            using (HttpClient client = new HttpClient())
            {
                SecurityProtocolType tipoProtocoloSeguridad = ServicePointManager.SecurityProtocol;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                address = ((address.EndsWith("/")) ? address.Substring(0, (address.Length - 1)) : address);
                uri = ((uri.StartsWith("/")) ? uri : "/" + uri);
                uri = ((uri.EndsWith("/")) ? uri.Substring(0, (address.Length - 1)) : uri);

                string finalAddress = address + uri;

                client.BaseAddress = new Uri(finalAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, finalAddress);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                //HTTP POST                        
                var responseTask = client.SendAsync(request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    salida = readTask;
                }
                else
                {
                    string estado = "{\"HttpResponseMessage\":{\"StatusCode\":\"" + result.StatusCode.ToString() + "\",\"ReasonPhrase\":\"" + result.ReasonPhrase + "\" }}";

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    salida = readTask;
                }

                ServicePointManager.SecurityProtocol = tipoProtocoloSeguridad;

            }
            return salida;
        }

        private Task<string> DeleteAsync(string basicKey, string address, string uri, string jsonContent)
        {
            Task<string> salida;

            using (HttpClient client = new HttpClient())
            {
                SecurityProtocolType tipoProtocoloSeguridad = ServicePointManager.SecurityProtocol;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                address = ((address.EndsWith("/")) ? address.Substring(0, (address.Length - 1)) : address);
                uri = ((uri.StartsWith("/")) ? uri : "/" + uri);
                uri = ((uri.EndsWith("/")) ? uri.Substring(0, (address.Length - 1)) : uri);

                string finalAddress = address + uri;

                client.BaseAddress = new Uri(finalAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, finalAddress);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                
                var responseTask = client.SendAsync(request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    salida = readTask;
                }
                else
                {
                    string estado = "{\"HttpResponseMessage\":{\"StatusCode\":\"" + result.StatusCode.ToString() + "\",\"ReasonPhrase\":\"" + result.ReasonPhrase + "\" }}";

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    salida = readTask;
                }

                ServicePointManager.SecurityProtocol = tipoProtocoloSeguridad;

            }
            return salida;
        }
    }
}
