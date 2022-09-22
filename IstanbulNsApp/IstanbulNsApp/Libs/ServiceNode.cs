
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Components;
using IstanbulNsApp.Repositories;
using System.Net.Http.Formatting;
namespace IstanbulNsApp.Libs
{
    public class ServiceNode<T,U> where T:class,new() 
    {
        public string BaseAdress { get; set; }

        IStringLocalizer<SharedResource> _localizer;

        HttpClient Client;

        public ServiceNode(HttpClient factory)
        {
            //Client = factory.CreateClient(name:"ApiRequests");
            Client = factory;
        }
        public ServiceNode(IStringLocalizer<SharedResource> localizer, HttpClient factory)
        {
            // Client = factory.CreateClient(name: "ApiRequests");
            Client = factory;
            _localizer = localizer;
        }
        public ReturnMessage<U> DeleteClient(string url, string token = null)
        {
            try
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = Client.DeleteAsync(url).Result;
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
                        case 11:
                            returnData.Message = _localizer["Əlaqə nömrəsi qeydiyyatla düzgün gəlmir"];
                            returnData.Code = 400;
                            break;
                        case 12:
                            returnData.Message = _localizer["Sıra kodu səhfdir"];
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
        public ReturnMessage<U> GetClient(string url, string token = null)
        {
            try
            
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = Client.GetAsync(url).Result;
                var responsecode = respraw.StatusCode;
                var response = respraw.Content.ReadAsAsync<ReturnMessage<U>>().Result;
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
        public ReturnMessage<U> PostClient(object data,string url, string token = null)
        {
            try
            {

                if (token != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var respraw = Client.PostAsJsonAsync(url, data).Result;

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
                        case 11:
                            returnData.Message = _localizer["Əlaqə nömrəsi qeydiyyatla düzgün gəlmir"];
                            returnData.Code = 400;
                            break;
                        case 12:
                            returnData.Message = _localizer["Sıra kodu səhfdir"];
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
                errorResp.Message = "Something wrong with server : "+x.Message;
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
                        case 11:
                            returnData.Message = _localizer["Əlaqə nömrəsi qeydiyyatla düzgün gəlmir"];
                            returnData.Code = 400;
                            break;
                        case 12:
                            returnData.Message = _localizer["Sıra kodu səhfdir"];
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
