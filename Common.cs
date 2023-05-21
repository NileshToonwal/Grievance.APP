using Grievance.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Grievance
{
    public class Common
    {
        public  ApiCommonResponse<T> GetCommonApi<T>(string apiUrl)
        {
            try
            {
                var options = new RestClientOptions()
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(apiUrl, Method.Get);
                RestResponse response =  client.Execute(request);
                ApiCommonResponse<T> apiCommonResponse = JsonConvert.DeserializeObject<ApiCommonResponse<T>>(response.Content);
                Console.WriteLine(response.Content);
                return apiCommonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while calling API: {ex}");
                return new ApiCommonResponse<T>()
                {
                    allowStatus = false,
                    msg = "An error occurred while calling the API. Please try again later.",
                    showMsg = true
                };
            }
        }

        //public async Task ApiCommonResponse<T> getCommonApi<T>(string apiUrl)
        //{
        //    try
        //    {
        //        var options = new RestClientOptions()
        //        {
        //            MaxTimeout = -1,
        //        };
        //        var client = new RestClient(options);
        //        var request = new RestRequest(apiUrl, Method.Get);
        //        RestResponse response = await client.ExecuteAsync(request);
        //        ApiCommonResponse<T> apiCommonResponse = JsonConvert.DeserializeObject<ApiCommonResponse<T>>(response.Content);
        //        Console.WriteLine(response.Content);
        //        return apiCommonResponse;
               

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiCommonResponse<T>() { allowStatus = false, msg = "Due to technical issue, Kindly check later.", showMsg = true };
        //    }
        //}
    }
}
