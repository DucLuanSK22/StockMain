drop function if exists public.update_cash_value_by_user(p_user_id uuid);
CREATE OR REPLACE FUNCTION public.update_cash_value_by_user(p_user_id uuid)
	RETURNS void 
	LANGUAGE plpgsql
AS $function$
    /**
     * Hàm cập nhật giá tiền mặt của người dùng khi người dùng nạp tiền vào tài khoản
     * @author nttue - 12.10.2024
     */
    BEGIN         
        update "user"
        set 
            cash_value = cash_value + 100000000, 
            total_net_assets = total_net_assets + 100000000 
        where user_id = p_user_id;

        INSERT INTO public.table_asset_history (
            table_asset_history_id,
            user_id, 
            total_net_assets, 
            stock_value, 
            cash_value, 
            created_at    
        ) 
        SELECT 
            uuid_generate_v4(),
            u.user_id, 
            u.total_net_assets, 
            u.stock_value, 
            u.cash_value,
            now()
        FROM 
            "user" u
        where u.user_id = p_user_id;
    end;
$function$
