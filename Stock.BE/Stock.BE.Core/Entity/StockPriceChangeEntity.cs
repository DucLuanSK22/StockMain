using System.ComponentModel.DataAnnotations.Schema;
namespace Stock.BE.Core.Entity
{
    [Table("stocks")]
    public class StockPriceChangeEntity
    {
        [Column("stock_price_changes_id")]
        public Guid stock_price_changes_id { get; set; }

        [Column("stock_id")]
        public Guid stock_id { get; set; }

        [Column("current_price")]
        public decimal current_price { get; set; }

        [Column("change_price")]
        public decimal change_price { get; set; }

        [Column("change_price_by_percent")]
        public decimal change_price_by_percent { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("modified_at")]
        public DateTime modified_at { get; set; }
    }
}
