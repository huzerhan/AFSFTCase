using AFSFTCase.Data;
using AFSFTCase.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AFSFTCase.Controllers
{
    public class TranslatorController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        private readonly AFSFTDbContext _db;

        public TranslatorController(AFSFTDbContext db)
        {
            _db = db;
        }

        //public TranslatorController()
        //{
        //    _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert,chain, sslPolicyErrors) => { return true; };
        //}


        public async Task<List<string>> GetTranslation(string text, string translator)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"https://api.funtranslations.com/translate/");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{translator}.json?text={text}");

            var result = await client.SendAsync(request);
            var returnString = await result.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<TranslationObjectDTO>(returnString);
            var translationResult = deserializedResponse.Contents.TranslatedString;
            var translatorResult = deserializedResponse.Contents.Translator;
            var inputString = deserializedResponse.Contents.InputString;
            List<string> apiResult = new List<string>() { translationResult, translatorResult, inputString };
            //var response = await httpClient.GetAsync("http://api.funtranslations.com/translate/leetspeak.json?text="+$"{inputString}");

            //var apiResponse = await response.Content.ReadAsStringAsync();
            //var deserializedTranslation = JsonConvert.DeserializeObject<TranslationObjectDTO>(apiResponse);
            //var translationResult = deserializedTranslation.Contents.TranslatedString;

            return apiResult;
        }

       
        [HttpGet]
        public async Task<ActionResult> GetInput(DataModelEntity _entity)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                try
                {
                    List<string> translationResult = await GetTranslation(_entity.InputString, _entity.Translator);
                    if (translationResult[0] != null)
                    {
                        //_entity.Translator = translator;

                        _entity.TranslatedString = translationResult[0];
                        _entity.Translator = translationResult[1];
                        _entity.InputString = translationResult[2];
                        //_entity.InputString =
                        result = _entity.TranslatedString;
                        _db.DataModelEntities.Add(_entity);
                        _db.SaveChanges();
                        //ViewBag.Result = result;
                    }
                }
                catch (NullReferenceException)
                {

                    throw;
                }


                return View("Index", _entity);

            }
            return View("Index", _entity);

        }


        public IActionResult Index()
        {
            return View();
        }
        
    }
}
