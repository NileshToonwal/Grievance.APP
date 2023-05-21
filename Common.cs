using Entities.ExtendedModels;
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
        public ApiCommonResponse<T> GetCommonApi<T>(string apiDomain, string apiEndPoint)
        {
            try
            {
                // Create RestClient instance and specify the base URL of the API
                var client = new RestClient(apiDomain);

                // Create a new RestRequest with the endpoint you want to call
                var request = new RestRequest(apiEndPoint, Method.Get);

                // (Optional) Add any required headers or parameters to the request
                //request.AddHeader("Authorization", "Bearer YOUR_ACCESS_TOKEN");
                //request.AddParameter("paramName", "paramValue");

                // Execute the request and get the response
                var response = client.Execute(request);

                // Check if the request was successful
                if (response.IsSuccessful)
                {
                    ApiCommonResponse<T> apiCommonResponse = JsonConvert.DeserializeObject<ApiCommonResponse<T>>(response.Content);
                    //Console.WriteLine(response.Content);                
                    return apiCommonResponse;
                }
                else
                {                   
                    return new ApiCommonResponse<T>()
                    {
                        allowStatus = false,
                        msg = response.ErrorMessage,
                        showMsg = true
                    };                   
                }
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
