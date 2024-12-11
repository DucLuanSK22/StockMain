drop function if exists public.update_deal(p_stock_id uuid);
CREATE OR REPLACE FUNCTION public.update_deal(p_stock_id uuid)
	RETURNS void 
	LANGUAGE plpgsql
AS $function$       
    BEGIN       
       -- Tạo 1 bảng tạm để tổng hợp các deal cùng mã của cùng 1 người 
       drop table if exists tmp_deal;
       create temp table tmp_deal AS
        select 
            stock_id,
            user_id,
            stock_code,
            sum(total_volume) as total_volume,
            total_tradeable_volume, 
            avg(matched_price) as matched_price,
            current_price
        from 
            deals
        where 
            stock_id = p_stock_id
        group by 
            stock_id,
            user_id,
            stock_code,
            total_tradeable_volume,
            current_price;

        -- Xóa hết dòng dữ liệu cũ 
        delete from deals d
        using tmp_deal t
        where 
            d.stock_id = t.stock_id
            and d.user_id = t.user_id;

        -- Thêm mới dữ liệu cho bảng deal
        insert into deals(
            deal_id,
            stock_id,
            user_id,
            stock_code,
            total_volume,
            total_tradeable_volume,
            matched_price,
            current_price
        )
        select 
            uuid_generate_v4(),
            stock_id,
            user_id,
            stock_code,
            total_volume,
            total_tradeable_volume, 
            matched_price,
            current_price
        from 
            tmp_deal;        
    end;    
$function$
                           