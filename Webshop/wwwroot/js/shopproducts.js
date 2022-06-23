const productDisplay = document.getElementById("product_display");
const vegFruitLink = document.getElementById("veg");
const foodLink = document.getElementById("food");
const dairyLink = document.getElementById("dairy");
const bathroomLink = document.getElementById("bath");
const cartCount = document.getElementById("cart_count");

vegFruitLink.onclick = () => fetchAndDisplayProducts("fruitveg");
foodLink.onclick = () => fetchAndDisplayProducts("food");
dairyLink.onclick = () => fetchAndDisplayProducts("dairy");
bathroomLink.onclick = () => fetchAndDisplayProducts("bathroom");

// Fetch products via Web API, and display them in table
function fetchAndDisplayProducts(category) {
    var uri = "https://localhost:44372/api/products/" + category;

    fetch(uri)
        .then(response => {
            if (response.ok) {
                response.json()
                    .then(fetchedData => {
                        // Create table
                        productDisplay.innerHTML = '<table id="product_table"><tr><th width="200px">Product description</th><th width="100px">Price</th><th width="200px"></th></tr></table>';

                        const productTable = document.getElementById("product_table");

                        fetchedData.forEach((item) => {
                            var newRow = productTable.insertRow();
                            newRow.insertCell().innerHTML = `${item.description}`;
                            newRow.insertCell().innerHTML = `${item.price}`;
                            newRow.insertCell().innerHTML = `<img src="./images/${item.image}" width="80">`;

                            var cell = newRow.insertCell();
                            var addToCartButton = document.createElement("input");
                            addToCartButton.setAttribute("type", "button");
                            addToCartButton.setAttribute("value", "Add to cart");

                            // This is the "add to cart" functionality for each product item
                            addToCartButton.onclick = () => {
                                fetch("./api/cart/", {
                                    method: "POST",
                                    headers: { 'Content-Type' : 'application/json' },
                                    body: `"${item.productCode}"`
                                })
                                    .then(response => {
                                        if (response.ok) {
                                            window.alert(`${item.description} Added to cart`);

                                            // update the cart count; the API call returns the cart count
                                            response.text().then(count => {
                                                cartCount.innerHTML = `(${count})`;
                                            });
                                            
                                        }
                                    });
                            }

                            cell.insertAdjacentElement("afterbegin", addToCartButton);
                        })
                    })
            }
        });
}
