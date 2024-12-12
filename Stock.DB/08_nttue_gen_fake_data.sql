-- Tạo bảng tạm v_date
DROP TABLE IF EXISTS v_date;
CREATE TEMP TABLE v_date AS
SELECT generate_series(
    NOW()::date - INTERVAL '68 days',  -- Ngày bắt đầu (30 ngày trước)
    NOW()::date,                       -- Ngày kết thúc (ngày hôm nay)
    INTERVAL '1 day'                   -- Khoảng cách giữa các ngày
) AS "date";

ALTER TABLE stock_price_changes DROP COLUMN IF EXISTS rnk;
ALTER TABLE stock_price_changes ADD COLUMN rnk INTEGER;


WITH v_temp AS (
    SELECT 
        stock_id,
        created_at,
        dense_rank() OVER (PARTITION BY stock_id ORDER BY created_at DESC) AS rnk
    FROM 
        stock_price_changes
)
UPDATE stock_price_changes AS s
SET rnk = v.rnk
FROM v_temp AS v
WHERE s.stock_id = v.stock_id 
  AND s.created_at = v.created_at;

WITH t_date AS (
    SELECT 
        "date",
        dense_rank() OVER (ORDER BY "date") AS rnk
    FROM 
        v_date
)
 UPDATE stock_price_changes AS s
 SET created_at = t."date"
FROM t_date AS t 
where s.rnk = t.rnk
;
select * from public.calculate_stock_change();