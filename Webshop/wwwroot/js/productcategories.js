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

            response.json()
                .then(fetchedData => {
                    productDisplay.innerHTML = '<table id="product_table"><thead><tr><th width="200px">Product description</th><th width="200px">Price</th><th width="200px"></th></tr></thead></table>';

                    const productTable = document.getElementById("product_table");

                    fetchedData.forEach((item) => {
                        var newRow = productTable.insertRow();
                        newRow.insertCell().innerHTML = `${item.description}`;
                        newRow.insertCell().innerHTML = `${item.price}`;
                        newRow.insertCell().innerHTML = `<img src="./images/${item.image}" width="80">`;
                        newRow.insertCell().innerHTML = `<a href="Index?productCategory=${category}&addToCart=${item.productCode}">Add to cart</a>`;
                    })
                })
        });
}
