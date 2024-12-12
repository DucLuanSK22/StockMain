drop function if exists public.buy_stock(p_transactions jsonb);
CREATE OR REPLACE FUNCTION public.buy_stock(p_transactions jsonb)
	RETURNS void 
	LANGUAGE plpgsql
AS $function$
    /**
     * Hàm thực hiện đặt lệnh mua chứng khoán
     * @author nttue - 02.11.2024
     */      
    BEGIN       
        create temp table v_transactions as 
            SELECT *
            FROM jsonb_to_record(p_transactions) AS x (
                transactions_id uuid,
                stock_id uuid,
                user_id uuid,
                stock_code text,
                transaction_type integer,
                created_at timestamp,
                order_price numeric,
                matched_price numeric,
                volume integer,
                status integer
            );

        insert into transactions
            (transactions_id, stock_id, user_id, stock_code, transaction_type, created_at, order_price, matched_price, volume, status)            
            SELECT *
            FROM v_transactions;
    
        update "user" u
        set cash_value = u.cash_value - (v.matched_price * v.volume) * 1000,
            total_net_assets = u.total_net_assets - (v.matched_price * v.volume) * 1000
        from v_transactions v
        where u.user_id = v.user_id;
        
        insert into table_asset_history
        select    uuid_generate_v4(),
                v.user_id,
                u.total_net_assets,
                u.cash_value,
                u.stock_value,
                now()
        from 
            v_transactions v
        join 
            "user" u on u.user_id = v.user_id;
    end;    
$function$
                           