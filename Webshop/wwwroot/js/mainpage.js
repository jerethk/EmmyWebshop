// JavaScript source code

const linkVeg = document.getElementById("veg");
const linkFruit = document.getElementById("fruit");
const linkBakery = document.getElementById("bakery");
const linkDairy = document.getElementById("dairy");
const linkFood = document.getElementById("food");
const linkBathroom = document.getElementById("bath");
const linkWashing = document.getElementById("wash");
const linkToys = document.getElementById("toys");
const prodDisp = document.getElementById("product_display");

const linkMenu = document.querySelector(".linkmenu");
linkMenu.onmouseover = () => { linkMenu.style = "cursor: pointer" };

let productList = null;

linkVeg.addEventListener("click", getVeges);
linkFruit.addEventListener("click", getFruit);
linkBakery.addEventListener("click", getBakery);
linkDairy.addEventListener("click", getDairy);
linkBathroom.addEventListener("click", getBathroom);
linkWashing.addEventListener("click", getWashing);
linkToys.addEventListener("click", getToys);
linkFood.addEventListener("click", getFood);

function updateProductDisplay(list) {
    prodDisp.innerHTML = "";

    for (i = 0; i < list.length; i++) {
        // now done using template literals!
        prodDisp.innerHTML += `<p style="font-weight: bold">${list[i].name}</p>`;
        prodDisp.innerHTML += `<img src="images/${list[i].pic}" height="120"> <br />`;
        prodDisp.innerHTML += `<p>Price: $ ${list[i].price} </p>`;
        prodDisp.innerHTML += `<p>Qty: ${list[i].qty} </p><br/>`;

        /*
        prodDisp.innerHTML += '<p style="font-weight: bold">' + list[i].name + "</p>";
        prodDisp.innerHTML += '<img src="images/' + list[i].pic + '" height="120"> <br />'
        prodDisp.innerHTML += '<p>Price: $' + list[i].price + '</p>';
        prodDisp.innerHTML += '<p>Qty: ' + list[i].qty + '</p><br/>';
        */
    }

}

function getVeges() {
    console.log("getting veges");

    let req = new XMLHttpRequest();
    req.open("GET", "veges.json");
    req.responseType = "json";
    req.send();
    
    req.onload = function() {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}

function getFruit() {
    let req = new XMLHttpRequest();
    req.open("GET", "fruit.json");
    req.responseType = "json";
    req.send();
    
    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}

function getBakery() {
    let req = new XMLHttpRequest();
    req.open("GET", "bakery.json");
    req.responseType = "json";
    req.send();

    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}

function getDairy() {
    let req = new XMLHttpRequest();
    req.open("GET", "dairy.json");
    req.responseType = "json";
    req.send();

    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}

function getWashing() {
    let req = new XMLHttpRequest();
    req.open("GET", "washing.json");
    req.responseType = "json";
    req.send();

    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}

function getBathroom() {
    let req = new XMLHttpRequest();
    req.open("GET", "bathroom.json");
    req.responseType = "json";
    req.send();

    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}

function getFood() {
    let req = new XMLHttpRequest();
    req.open("GET", "food.json");
    req.responseType = "json";
    req.send();

    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}
function getToys() {
    let req = new XMLHttpRequest();
    req.open("GET", "toys.json");
    req.responseType = "json";
    req.send();

    req.onload = function () {
        productList = req.response;
        updateProductDisplay(productList.products);
    }
}
