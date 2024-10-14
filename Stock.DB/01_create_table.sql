drop table if exists table_asset_history;
drop table if exists stock_price_changes;
drop table if exists transactions;
drop table if exists deals;
drop table if exists stocks;
drop table if exists "user";

CREATE TABLE public.stocks (
	stock_id uuid NOT NULL,
	stock_code text NULL,
	stock_name text NULL,
	trading_floor text NULL,
	max_price numeric NULL,
	min_price numeric NULL,
	reference_price numeric NULL,
	matched_price numeric NULL,
	total_volume numeric NULL,
	total_assets numeric NULL,
	CONSTRAINT stocks_pkey PRIMARY KEY (stock_id)
);
CREATE TABLE public."user" (
	user_id uuid NOT NULL,
	user_name text NULL,
	"password" text NULL,
	total_net_assets numeric NULL,
	stock_value numeric NULL,
	cash_value numeric NULL,
	email text NULL,
	CONSTRAINT user_pkey PRIMARY KEY (user_id)
);

CREATE TABLE public.transactions (
	transactions_id uuid NOT NULL,
	stock_id uuid NULL,
	user_id uuid NULL,
	stock_code text NULL,
	transaction_type int4 NULL,
	created_at timestamp NULL,
	order_price numeric NULL,
	matched_price numeric NULL,
	volume int4 NULL,
	status int4 NULL,
	CONSTRAINT transactions_pkey PRIMARY KEY (transactions_id),
	CONSTRAINT pk_stocks_transactions FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id),
	CONSTRAINT pk_user_transactions FOREIGN KEY (user_id) REFERENCES public."user"(user_id)
);

CREATE TABLE public.deals (
	deal_id uuid NOT NULL,
	stock_id uuid NULL,
	user_id uuid NULL,
	stock_code text NULL,
	total_volume int4 NULL,
	total_tradeable_volume int4 NULL,
	matched_price numeric NULL,
	current_price numeric NULL,
	cost_value numeric NULL,
	market_value numeric NULL,
	profit_loss numeric NULL,
	profit_loss_by_percent numeric NULL,
	CONSTRAINT deals_pkey PRIMARY KEY (deal_id),
	CONSTRAINT pk_stocks_deals FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id),
	CONSTRAINT pk_user_deals FOREIGN KEY (user_id) REFERENCES public."user"(user_id)
);

CREATE TABLE public.stock_price_changes (
	stock_price_changes_id uuid NOT NULL,
	stock_id uuid NULL,
	current_price numeric NULL,
	change_price numeric NULL,
	change_price_by_percent numeric NULL,
	created_at timestamp NULL,
	modified_at timestamp NULL,
	CONSTRAINT stock_price_changes_pkey PRIMARY KEY (stock_price_changes_id),
	CONSTRAINT pk_stocks_stock_price_changes FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id)
);

CREATE TABLE public.table_asset_history (
	table_asset_history_id uuid NOT NULL,
	user_id uuid NULL,
	total_net_assets numeric NULL,
	stock_value numeric NULL,
	cash_value numeric NULL,
	created_at timestamp NULL,
	CONSTRAINT table_asset_history_pkey PRIMARY KEY (table_asset_history_id),
	CONSTRAINT pk_user_table_asset_history FOREIGN KEY (user_id) REFERENCES public."user"(user_id)
);