drop function if exists public.buy_sell_stock();
CREATE OR REPLACE FUNCTION public.buy_sell_stock()
	RETURNS void 
	LANGUAGE plpgsql
AS $function$
    /**
     * Hàm thực hiện các lệnh mua bán chứng khoán của người dùng
     * @author nttue - 02.11.2024
     */      
    BEGIN   
    /**
     * Bước 1: Lấy ra các lệnh mua và các lệnh bán của chứng khoán trên sàn giao dịch của người dùng     
     */       
    drop table if exists v_buy;

    drop table if exists v_sell;

    create temp table v_buy as
    select 
        transactions_id,
        stock_id,
        user_id, 
        matched_price, 
        volume
    from transactions
    where transaction_type = 0 and "status" = 1
    order by created_at desc;



    create temp table v_sell as
    select 
        transactions_id,
        stock_id,
        user_id, 
        matched_price, 
        volume
    from transactions
    where transaction_type = 1 and "status" = 0
    order by created_at desc 
   ;
      

    /**
     * Tạo 1 bảng tạm để chứa kết quả phần match lệnh mua và lệnh bán
     */
    drop table if exists v_transaction;
    create temp table v_transaction(
        sell_transactions_id uuid,
        buy_transactions_id uuid,
        stock_id uuid,                        
        buy_user_id uuid,
        sell_user_id uuid,
        buy_price decimal(10, 2),
        sell_price decimal(10, 2),
        buy_volume int,
        sell_volume int        
    );
    /**
     * Bước 2: Thực hiện khớp các lệnh mua bán chứng khoán của người dùng
     */
    insert into v_transaction
    select 
        b.transactions_id as buy_transactions_id,
        s.transactions_id as sell_transactions_id,
        b.stock_id,
        b.user_id as buy_user_id,
        s.user_id as sell_user_id,
        b.matched_price as buy_price,
        s.matched_price as sell_price,
        b.volume as buy_volume,
        s.volume as sell_volume
    from 
        v_buy b
    join v_sell s
    on b.stock_id = s.stock_id and b.matched_price >= s.matched_price;    
    
    -- Update lệnh bán và lệnh mua  đã được hoàn thành
    UPDATE transactions
    SET "status" = CASE
        WHEN transactions_id IN (SELECT sell_transactions_id FROM v_transaction) THEN 1
        WHEN transactions_id IN (SELECT buy_transactions_id FROM v_transaction) THEN 1
        ELSE "status"
    END
    WHERE transactions_id IN (SELECT sell_transactions_id FROM v_transaction)
        OR transactions_id IN (SELECT buy_transactions_id FROM v_transaction);
    
    with v_temp as (
        select b.stock_id,
            b.buy_user_id,
            avg(b.buy_price) as avg_price,
            sum(b.buy_volume) as total_volume
        from v_transaction b
        group by b.stock_id, b.buy_user_id
    )
    insert into deals
    select 
        uuid_generate_v4(),
        t.stock_id,
        t.user_id,
        t.stock_code,
        t.total_volume,
        t.total_tradeable_volume,
        t.matched_price,
        t.current_price
    from (
        select 
            v1.stock_id,
            v1.buy_user_id as user_id,
            s.stock_code,
            v1.total_volume,
            s.tradable_volume as total_tradeable_volume,
            v1.avg_price as matched_price,
            s.matched_price as current_price
        from
            v_temp v1
        join stocks s on s.stock_id = v1.stock_id
    ) t;
    
    
    end;
$function$
                           