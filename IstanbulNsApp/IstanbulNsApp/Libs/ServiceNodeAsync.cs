
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using IstanbulNsApp.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace IstanbulNsApp.Libs
{
    public class ServiceNodeAsync<T, U> where T : class, new()
    {
        public static string BaseAdress { get; set; }

        static IStringLocalizer<SharedResource> _localizer;

        static HttpClient Client;

        public ServiceNodeAsync(HttpClient factory)
        {
            Client = factory;
        }
        public ServiceNodeAsync(IStringLocalizer<SharedResource> localizer, HttpClient factory)
        {
            Client = factory;
            _localizer = localizer;
        }
        public async Task<ReturnMessage<U>> DeleteClientAsync(string url, string token = null)
        {
            try
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = await Client.DeleteAsync(url);
                if (respraw.IsSuccessStatusCode)
                {
                    var response = await respraw.Content.ReadAsAsync<ReturnMessage<U>>();
                    return response;
                }
                else
                {
                    var response = await respraw.Content.ReadAsAsync<ReturnErrorMessage>();
                    ReturnMessage<U> returnData = new ReturnMessage<U>();
                    returnData.IsCatched = 1;
                    switch (response.ErrorType)
                    {

                        case 1:
                            returnData.Message = _localizer["There is problem with server, please try again"];
                            returnData.Code = 500;
                            break;
                        case 2:
                            returnData.Message = _localizer["Cannot find this type of data"];
                            returnData.Code = 400;
                            break;
                        case 3:
                            returnData.Message = _localizer["Wrong credentials"];
                            returnData.Code = 400;
                            break;
                        case 4:
                            returnData.Message = _localizer["This credentials are already exists, please try another one"];
                            returnData.Code = 400;
                            break;
                        case 5:
                            returnData.Message = _localizer["Password is wrong"];
                            returnData.Code = 400;
                            break;
                        case 6:
                            returnData.Message = _localizer["There is no such user with this credentials"];
                            returnData.Code = 400;
                            break;
                        case 7:
                            returnData.Message = _localizer["This time has taken by other user, please change time or date"];
                            returnData.Code = 400;
                            break;
                        case 8:
                            returnData.Message = _localizer["You have already unfinished query. Please wait until it will be finished. You will receive email when query finish"];
                            returnData.Code = 400;
                            break;
                        case 9:
                            returnData.Message = _localizer["Please confirm your email adress"];
                            returnData.Code = 400;
                            break;
                        default:
                            returnData.Message = "ERROR_TYPE_UNKNOWN";
                            returnData.Code = 500;
                            break;
                    }
                    return returnData;
                }


            }
            catch (Exception x)
            {
                var errorResp = new ReturnMessage<U>();
                errorResp.Code = 500;
                errorResp.IsCatched = 1;
                errorResp.Message = "Something wrong with server : " + x.Message;
                return errorResp;
            }

        }
        public  async Task<ReturnMessage<U>> GetClientAsync(string url, string token = null)
        {
            try
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = await Client.GetAsync(url);
                var response = await respraw.Content.ReadAsAsync<ReturnMessage<U>>();
                return response;
                 

            }
            catch (Exception x)
            {
                var errorResp = new ReturnMessage<U>();
                errorResp.Code = 500;
                errorResp.IsCatched = 1;
                errorResp.Message = "Something wrong with server : " + x.Message;
                return errorResp;
            }



        }
        public async Task<ReturnMessage<U>> PostClientAsync(object data, string url, string token = null)
        {
            try
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = await Client.PostAsJsonAsync(url, data);

                if (respraw.IsSuccessStatusCode)
                {
                    var response =await respraw.Content.ReadAsAsync<ReturnMessage<U>>();
                    return response;
                }
                else
                {
                    var response = await respraw.Content.ReadAsAsync<ReturnErrorMessage>();
                    ReturnMessage<U> returnData = new ReturnMessage<U>();
                    returnData.IsCatched = 1;
                    switch (response.ErrorType)
                    {

                        case 1:
                            returnData.Message = _localizer["Serverlə əlaqədə problem yarandı, yenidən cəhd edin"];
                            returnData.Code = 500;
                            break;
                        case 2:
                            returnData.Message = _localizer["Belə bir məlumat yoxdur"];
                            returnData.Code = 400;
                            break;
                        case 3:
                            returnData.Message = _localizer["Yalnış verilənlər"];
                            returnData.Code = 400;
                            break;
                        case 4:
                            returnData.Message = _localizer["Yazılan verilənlər doğru deyil"];
                            returnData.Code = 400;
                            break;
                        case 5:
                            returnData.Message = _localizer["Şifrə yalnışdır"];
                            returnData.Code = 400;
                            break;
                        case 6:
                            returnData.Message = _localizer["Belə bir user mövcud deyil"];
                            returnData.Code = 400;
                            break;
                        case 7:
                            returnData.Message = _localizer["Seçilən tarix başqa istifadəçi tərəfindən artıq tutulmuşdur,başqa tarixləri sınayın"];
                            returnData.Code = 400;
                            break;
                        case 8:
                            returnData.Message = _localizer["Bitməyən sıranız mövcuddur"];
                            returnData.Code = 400;
                            break;
                        case 9:
                            returnData.Message = _localizer["Emaili təsdiqləyin"];
                            returnData.Code = 400;
                            break;
                        case 10:
                            returnData.Message = _localizer["Seçdiyiniz vaxt Həkimin iş vaxtı ilə uyğun gəlmir,xaiş edirik vaxtı dəyişin və təkrardan sınayın"];
                            returnData.Code = 400;
                            break;
                        default:
                            returnData.Message = "ERROR_TYPE_UNKNOWN";
                            returnData.Code = 500;
                            break;
                    }
                    return returnData;
                }
            }
            catch (Exception x)
            {
                var errorResp = new ReturnMessage<U>();
                errorResp.Code = 500;
                errorResp.IsCatched = 1;
                errorResp.Message = "Something wrong with server : " + x.Message;
                return errorResp;
            }


        }
        public ReturnMessage<U> PutClient(object data, string url, string token = null)
        {
            try
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = Client.PutAsJsonAsync(url, data).Result;

                if (respraw.IsSuccessStatusCode)
                {
                    var response = respraw.Content.ReadAsAsync<ReturnMessage<U>>().Result;
                    return response;
                }
                else
                {
                    var response = respraw.Content.ReadAsAsync<ReturnErrorMessage>().Result;
                    ReturnMessage<U> returnData = new ReturnMessage<U>();
                    returnData.IsCatched = 1;
                    switch (response.ErrorType)
                    {

                        case 1:
                            returnData.Message = _localizer["There is problem with server, please try again"];
                            returnData.Code = 500;
                            break;
                        case 2:
                            returnData.Message = _localizer["Cannot find this type of data"];
                            returnData.Code = 400;
                            break;
                        case 3:
                            returnData.Message = _localizer["Wrong credentials"];
                            returnData.Code = 400;
                            break;
                        case 4:
                            returnData.Message = _localizer["This credentials are already exists, please try another one"];
                            returnData.Code = 400;
                            break;
                        case 5:
                            returnData.Message = _localizer["Password is wrong"];
                            returnData.Code = 400;
                            break;
                        case 6:
                            returnData.Message = _localizer["There is no such user with this credentials"];
                            returnData.Code = 400;
                            break;
                        case 7:
                            returnData.Message = _localizer["This time has taken by other user, please change time or date"];
                            returnData.Code = 400;
                            break;
                        case 8:
                            returnData.Message = _localizer["You have already unfinished query. Please wait until it will be finished. You will receive email when query finish"];
                            returnData.Code = 400;
                            break;
                        case 9:
                            returnData.Message = _localizer["Please confirm your email adress"];
                            returnData.Code = 400;
                            break;
                        default:
                            returnData.Message = "ERROR_TYPE_UNKNOWN";
                            returnData.Code = 500;
                            break;
                    }
                    return returnData;
                }
            }
            catch (Exception x)
            {
                var errorResp = new ReturnMessage<U>();
                errorResp.Code = 500;
                errorResp.IsCatched = 1;
                errorResp.Message = "Something wrong with server : " + x.Message;
                return errorResp;

            }

        }


    }
}

