use myshop;
select * from products;
select * from customers;

select * from product_stocks;

select * from transactions;

select * from invoice_items;

#insert into products values ("zuc", "Zucchini", "300g", 1.70, "zucchini.jpg", "fruitveg");

#alter table customers add password varchar(30); 

#alter table invoice_items add sold_price decimal(5,2);

#update invoice_items set sold_price = () 
where sold_price = 0;

select products.price from invoice_items inner join products 
on products.product_code = invoice_items.product;

update customers set email = "test@test" where customer_id = 7;