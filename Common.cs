using Entities.ExtendedModels;
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
        public static ApiCommonResponse<T> GetCommonApi<T>(string apiDomain, string apiEndPoint)
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

                ApiCommonResponse<T> apiCommonResponse = JsonConvert.DeserializeObject<ApiCommonResponse<T>>(response.Content);
                //Console.WriteLine(response.Content);                
                return apiCommonResponse;

                // Check if the request was successful
                //if (response.IsSuccessful)
                //{
                //}
                //else
                //{
                //    return new ApiCommonResponse<T>()
                //    {
                //        allowStatus = false,
                //        msg = "Error occurred while calling the API. Please try again later.",
                //        showMsg = true
                //    };
                //}
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
        public static ApiCommonResponse<T> PostCommonApi<T>(string apiDomain, string apiEndPoint, string jsonPayload)
        {
            try
            {
                // Create RestClient instance and specify the base URL of the API
                var client = new RestClient(apiDomain);

                // Create a new RestRequest with the endpoint you want to call
                var request = new RestRequest(apiEndPoint, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddStringBody(jsonPayload, DataFormat.Json);
                // (Optional) Add any required headers or parameters to the request
                //request.AddHeader("Authorization", "Bearer YOUR_ACCESS_TOKEN");
                //request.AddParameter("paramName", "paramValue");

                // Execute the request and get the response
                var response = client.ExecutePost(request);

                ApiCommonResponse<T> apiCommonResponse = JsonConvert.DeserializeObject<ApiCommonResponse<T>>(response.Content);
                //Console.WriteLine(response.Content);                
                return apiCommonResponse;

                // Check if the request was successful
                //if (response.IsSuccessful)
                //{
                //}
                //else
                //{
                //    return new ApiCommonResponse<T>()
                //    {
                //        allowStatus = false,
                //        msg = "Error occurred while calling the API. Please try again later.",
                //        showMsg = true
                //    };
                //}
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
        public static string getDeviceName()
        {
            return DeviceInfo.Name;
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
