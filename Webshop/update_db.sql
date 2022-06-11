use myshop;
select * from products;
select * from customers;

select * from product_stocks;

select * from transactions;

select * from invoice_items;

#UPDATE products SET price=2.30 where product_code = "ban";

#insert into products values ("shm", "Shampoo", "400ml", 7.20, "shampoo.jpg", "bathroom");
#insert into product_stocks values ("shm", 25);

ALTER TABLE customers ADD password VARCHAR(30) DEFAULT "password"; 

ALTER TABLE invoice_items add sold_price decimal(5,2) DEFAULT 0;

#update invoice_items set sold_price = () where sold_price = 0;

select products.price from invoice_items inner join products 
on products.product_code = invoice_items.product;

update customers set email = "test@test" where customer_id = 7;

