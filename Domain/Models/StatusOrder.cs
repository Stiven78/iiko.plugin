namespace Resto.Front.Api.StatusOrderChange.Domain.Models
{
    /// <summary>
    /// Модель статуса заказа
    /// </summary>
    public class StatusOrder
    {
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
    }
}
