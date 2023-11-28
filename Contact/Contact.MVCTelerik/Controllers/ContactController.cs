using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Application.CQRS.Core;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.MVC.Filters;
using Contact.MVC.Helpers;
using Contact.MVCTelerik.Extensions;
using Contact.MVCTelerik.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Contact.MVC.Controllers
{
    [Auth]
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<IActionResult> List(GetUserViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
                Request.Cookies.TryGetValue("Token", out string? token);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await client.GetAsync("users/contacts?" + model.Request.ToQueryString());

                string apiResponse = await response.Content.ReadAsStringAsync();

                var contactResponse = JsonConvert.DeserializeObject<ApiResult<GetUserContactResponse>>(apiResponse);
                
                model.Response = contactResponse.Response;

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? contactId)
        {
            if (contactId == null)
                return RedirectToAction("List", "Contact");
        
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
                Request.Cookies.TryGetValue("Token", out string? token);
        
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        
                var response = await client.GetAsync("users/contacts/" + contactId);
        
                string apiResponse = await response.Content.ReadAsStringAsync();
        
                var contactResponse = JsonConvert.DeserializeObject<ApiResult<GetUserContactDetailByIdResponse>>(apiResponse);
        
                if (contactResponse.StatusCode != 200)
                {
                    return RedirectToAction("List", "Contact");
                }

                return View(new ContactViewModel 
                { 
                    Response = contactResponse.Response, 
                    UpdateRequest = contactResponse.Response.AsUpdateUserContactRequest() 
                });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ContactViewModel model, int? contactId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
                Request.Cookies.TryGetValue("Token", out string? token);
        
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var content = new StringContent(JsonConvert.SerializeObject(model.UpdateRequest), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("users/contacts/" + contactId + "/update", content);
        
                string apiResponse = await response.Content.ReadAsStringAsync();
        
                var contactResponse = JsonConvert.DeserializeObject<ApiResult<UpdateUserContactResponse>>(apiResponse);
        
                if (contactResponse.StatusCode == 200)
                {
                    return RedirectToAction("List", "Contact");
                }
        
                return View();
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
                Request.Cookies.TryGetValue("Token", out string? token);
        
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        
                var content = new StringContent(JsonConvert.SerializeObject(model.CreateRequest), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("users/contacts", content);
        
                string apiResponse = await response.Content.ReadAsStringAsync();
        
                var contactResponse = JsonConvert.DeserializeObject<ApiResult<CreateUserContactResponse>>(apiResponse);
        
                if (contactResponse.StatusCode == 200)
                {
                    return RedirectToAction("List", "Contact");
                }
                
                return View(model);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? contactId)
        {

            if(contactId == null)
                return RedirectToAction("List","Contact");


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
                Request.Cookies.TryGetValue("Token", out string? token);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await client.DeleteAsync("users/contacts/" + contactId);

                string apiResponse = await response.Content.ReadAsStringAsync();

                var contactResponse = JsonConvert.DeserializeObject<ApiResult<GetUserContactResponse>>(apiResponse);

                if(contactResponse.StatusCode == 200)
                {
                    return RedirectToAction("List", "Contact");
                }

                return RedirectToAction("List", "Contact");

            }
        }
    }
}

