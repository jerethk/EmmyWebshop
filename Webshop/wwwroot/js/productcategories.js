const productDisplay = document.getElementById("product_display");
const vegFruitLink = document.getElementById("veg");
const foodLink = document.getElementById("food");
const dairyLink = document.getElementById("dairy");
const bathroomLink = document.getElementById("bath");

vegFruitLink.onclick = () => fetchAndDisplayProducts("fruitveg");
foodLink.onclick = () => fetchAndDisplayProducts("food");
dairyLink.onclick = () => fetchAndDisplayProducts("dairy");
bathroomLink.onclick = () => fetchAndDisplayProducts("bathroom");


function fetchAndDisplayProducts(category) {
    var uri = "https://localhost:44372/api/products/" + category;

    fetch(uri)
        .then(response => {
            if (response.ok) {
                response.json()
                    .then(fetchedData => {
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

                            addToCartButton.onclick = () => {
                                fetch("./api/cart/", {
                                    method: "POST",
                                    headers: { 'Content-Type' : 'application/json' },
                                    body: `"${item.productCode}"`
                                })
                                    .then(response => {
                                        if (response.ok) {
                                            window.alert("Added to cart");
                                        }
                                    });
                        }

                            cell.insertAdjacentElement("afterbegin", addToCartButton);

                            //newRow.insertCell().innerHTML = `<a href="Index?productCategory=${category}&addToCart=${item.productCode}">Add to cart</a>`;
                        })
                    })
            }
        });
}
