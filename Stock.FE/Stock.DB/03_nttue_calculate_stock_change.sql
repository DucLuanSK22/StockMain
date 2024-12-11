drop function if exists public.calculate_stock_change();
CREATE OR REPLACE FUNCTION public.calculate_stock_change()
	RETURNS void 
	LANGUAGE plpgsql
AS $function$
    /**
     * Hàm cập nhật giá trị của mã chứng khoán, tài sản chứng khoán của người dùng
     * và thực hiện insert dữ liệu với bảng lịch sử thay đổi giá chứng khoán
     * @author nttue - 12.10.2024
     */
    BEGIN         
        -- Tạo 1 bảng tạm chứa giá trị thay đổi và phép toán tăng giảm tửng mã chứng khoán
        drop table if exists temp_stock_change;

        create table temp_stock_change(
            stock_id uuid,
            price_change decimal(10, 2),
            operation int            
        );

        insert into temp_stock_change
        select stock_id, (random() * 5),  round(random())
        from stocks; 

        -- Update bảng stock
        update stocks s
        set
            max_price = case 
                when t.operation = 1 then 
                    s.max_price + t.price_change 
                else 
                    case 
                        when s.max_price - t.price_change < 0 then s.max_price
                        else s.max_price - t.price_change 
                    end
                end,
            min_price = case 
                when t.operation = 1 then 
                    s.min_price + t.price_change 
                else 
                    case 
                        when s.min_price - t.price_change < 0 then s.min_price
                        else s.min_price - t.price_change 
                    end
                 end,
            reference_price = case 
                when t.operation = 1 then 
                    s.reference_price + t.price_change 
                else 
                    case 
                        when s.reference_price - t.price_change < 0 then s.reference_price
                        else s.reference_price - t.price_change 
                    end
                end,
            matched_price = case 
                when t.operation = 1 then 
                    s.matched_price + t.price_change 
                else 
                    case 
                        when s.matched_price - t.price_change < 0 then s.matched_price
                        else s.matched_price - t.price_change 
                    end
                end,
            total_assets = s.matched_price * s.total_volume,
            difference = case 
                            when t.price_change > 0 and t.operation = 1 then 0
                            when t.price_change > 0 and t.operation = 0 then 1
                            else 2 
                        end,
            change_price = t.price_change
        from 
            temp_stock_change t
        where 
            s.stock_id = t.stock_id;
        
        -- Insert dữ liệu vào bảng lịch sử thay đổi giá chứng khoán
        INSERT INTO public.stock_price_changes (
            stock_price_changes_id,
            stock_id,
            current_price, 
            change_price, 
            change_price_by_percent, 
            created_at, 
            modified_at
        )
            SELECT 
                uuid_generate_v4(),
                t.stock_id,
                s.matched_price + 
                    (CASE WHEN t.operation = 1 THEN t.price_change ELSE 0 END),
                t.price_change,
                t.price_change / s.matched_price,
                now(),
                now()
            FROM 
                temp_stock_change t
            JOIN 
                stocks s ON s.stock_id = t.stock_id
            ;
        
        /** 
         * Sau khi update và insert dữ liệu chứng khoán xong tiến hành
         * cập nhật lại giá trị tài sản chứng khoản của người dùng và thực hiện insert dữ liệu và 
         * thực hiện insert vào bảng log tài sản người dùng phục vụ vẽ biểu đồ
         */
        drop table  if exists tmp_user;
        
        create table tmp_user (
            user_id uuid,
            total_change decimal(10, 2)
        );

        insert into tmp_user
        SELECT 
            d.user_id,
            SUM((s.matched_price - d.matched_price) * d.total_volume) OVER 
                (PARTITION BY d.stock_id)
            AS total_change
        FROM 
            deals d
        JOIN 
            stocks s ON s.stock_id = d.stock_id;
        
        UPDATE "user" u
        SET 
            stock_value = u.stock_value + v.total_change,
            total_net_assets = u.total_net_assets + v.total_change
        FROM 
            tmp_user v
        WHERE 
            u.user_id = v.user_id;

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
            u.total_net_assets + v.total_change, 
            u.stock_value + v.total_change, 
            u.cash_value,
            now()
        FROM 
            tmp_user v
        JOIN 
            "user" u ON u.user_id = v.user_id;
    end;
$function$
