using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resto.Front.Api.StatusOrderChange.Infrastructure.NalunshService.Models
{
    /// <summary>
    /// Модель получения ответа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
    {
        public T Result { get; set; }
    }
}
