drop table if exists table_asset_history;
drop table if exists stock_price_changes;
drop table if exists transactions;
drop table if exists deals;
drop table if exists stocks;
drop table if exists "user";

create table public."stocks" (
    stock_id uuid,
    stock_code text,
    stock_name text,
    trading_floor text,
    max_price numeric,
    min_price numeric,
    reference_price numeric,
    matched_price numeric,
    total_volume numeric,
    total_assets numeric,
    CONSTRAINT stocks_pkey PRIMARY KEY (stock_id)	
);
create table public."user" (
    user_id uuid,
    user_name text,
    password text,
    total_net_assets numeric,
    stock_value numeric,
    cash_value numeric,
    CONSTRAINT user_pkey primary key (user_id)    
);

create table public."transactions" (    
    transactions_id uuid,
    stock_id uuid,
    user_id uuid,
    stock_code text,
    transaction_type int,
    created_at timestamp,
    order_price numeric,
    matched_price numeric,
    volume int,
    status int,
    CONSTRAINT transactions_pkey primary key (transactions_id),
    CONSTRAINT pk_user_transactions FOREIGN KEY (user_id) REFERENCES public.user(user_id),
    CONSTRAINT pk_stocks_transactions FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id)
);

create table public."deals" (
    deal_id uuid,
    stock_id uuid,
    user_id uuid,
    stock_code text,
    total_volume int,
    total_tradeable_volume int,
    matched_price numeric,
    current_price numeric,
    cost_value numeric,
    market_value numeric,
    profit_loss numeric, 
    profit_loss_by_percent numeric,
    CONSTRAINT deals_pkey primary key (deal_id),
    CONSTRAINT pk_user_deals FOREIGN KEY (user_id) REFERENCES public.user(user_id),
    CONSTRAINT pk_stocks_deals FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id)
);


create table public."stock_price_changes" (
    stock_price_changes_id uuid,
    stock_id uuid,
    current_price numeric,
    change_price numeric,
    change_price_by_percent numeric,
    created_at timestamp,
    modified_at timestamp,
    CONSTRAINT stock_price_changes_pkey primary key (stock_price_changes_id),
    CONSTRAINT pk_stocks_stock_price_changes FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id)
);

create table public."table_asset_history" (
    table_asset_history_id uuid,    
    user_id uuid,
    total_net_assets numeric,
    stock_value numeric,
    cash_value numeric,
    created_at timestamp,
    CONSTRAINT table_asset_history_pkey primary key (table_asset_history_id),
    CONSTRAINT pk_user_table_asset_history FOREIGN KEY (user_id) REFERENCES public.user(user_id)
);