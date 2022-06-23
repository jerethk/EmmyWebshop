# Emmy's shop

This is a web app which simulates an online shopping experience.
You browse through products, add them to a shopping cart, then check out.
It also has account creation and user login features.

I created this application to gain experience in developing with ASP.NET Core, and specifically Razor 
pages. The solution also includes a simple RESTful API which enables the main application to retrieve 
and update product data with AJAX calls (using the Javascript fetch() API) without a full page reload.

The shopping cart function uses session state, and fetch() with HTTP POST to update the cart without
reloading the page.
