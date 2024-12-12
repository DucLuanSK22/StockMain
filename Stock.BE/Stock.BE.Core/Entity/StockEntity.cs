using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.BE.Core.Entity
{
    [Table("stocks")]
    public class StockEntity
    {
        public Guid stock_id { get; set; }
        public string stock_code { get; set; }
        public string stock_name { get; set; }
        public string trading_floor { get; set; }
        public decimal max_price { get; set; }
        public decimal min_price { get; set; }
        public decimal reference_price { get; set; }
        public decimal matched_price { get; set; }
        public decimal total_volume { get; set; }
        public decimal total_assets { get; set; }
        public int difference { get; set; }
        public decimal change_price{ get; set; }
        public int tradable_volume { get; set; }
    }
}
