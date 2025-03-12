using Resto.Front.Api.StatusOrderChange.Domain.Models;
using Resto.Front.Api.StatusOrderChange.Infrastructure.NalunshService.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System;

namespace Resto.Front.Api.StatusOrderChange.Infrastructure.NalunshService
{
    /// <summary>
    /// Обращение в апи внешнего сервиса
    /// </summary>
    public class NalunshService
    {
        private string url = "https://api.nalunch.me/";

        /// <summary>
        /// Получение объекта сервиса
        /// </summary>
        /// <returns></returns>
        public ResponseModel<StatusOrder> GetStatus() 
        {
            return MakeRequest<ResponseModel<StatusOrder>>(url + "test/orders/1/status");
        }

        /// <summary>
        /// Отправка запроса в систему,в дальнейшем можно расширить дополнительными параметрами
        /// </summary>
        /// <typeparam name="T">Получаемый объект</typeparam>
        /// <param name="urlAction">Юрл к которому идет обращение</param>
        /// <returns></returns>
        public static T MakeRequest<T>(string urlAction)
        {
            try
            {
                var request = WebRequest.Create(urlAction) as HttpWebRequest;
                var responseContent = "";
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                            using (var reader = new StreamReader(stream))
                            {
                                responseContent = reader.ReadToEnd();
                            }
                    }
                }
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (WebException ex)
            {
                using (var eResponse = ex.Response)
                {
                    if (eResponse != null)
                    {
                        using (var eStream = eResponse.GetResponseStream())
                        {
                            if (eStream == null) throw ex;

                            using (var reader = new StreamReader(eStream))
                            {
                                var error = reader.ReadToEnd();
                                var wRespStatusCode = ((HttpWebResponse)ex.Response).StatusCode;
                                PluginContext.Log.Error("Request error " + wRespStatusCode);
                                throw new Exception(error);
                            }
                        }
                    }
                    throw ex;
                }
            }
        }
    }
}
