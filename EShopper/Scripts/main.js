/*price range*/

/*$('#sl2').slider();*/

var RGBChange = function () {
    $('#RGB').css('background', 'rgb(' + r.getValue() + ',' + g.getValue() + ',' + b.getValue() + ')')
};

/*scroll to top*/

$(document).ready(function () {
    $(function () {
        $.scrollUp({
            scrollName: 'scrollUp', // Element ID
            scrollDistance: 300, // Distance from top/bottom before showing element (px)
            scrollFrom: 'top', // 'top' or 'bottom'
            scrollSpeed: 300, // Speed back to top (ms)
            easingType: 'linear', // Scroll to top easing (see http://easings.net/)
            animation: 'fade', // Fade, slide, none
            animationSpeed: 200, // Animation in speed (ms)
            scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
            //scrollTarget: false, // Set a custom target element for scrolling to the top
            scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
            scrollTitle: false, // Set a custom <a> title if required.
            scrollImg: false, // Set true to use image
            activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
            zIndex: 2147483647 // Z-Index for the overlay

        });
    });
});

function ValidControl(AInput) {
    var Input = AInput;
    if (Input.required != null) {
        if (Input.required) {
            if (Input.value === "" || Input.value === "-1") {
                Input.style.backgroundColor = "#FF6B6B";
            }
            else {
                Input.style.backgroundColor = "#F0F0E9";
            }
        }
    }
};

function AddToCart(productId) {
    var productID = productId;
    $.ajax({
        type: 'POST',
        url: '/Cart/AddCart',
        data: { productID }
        ,
        success: function () {
            alert("Urun sepete eklendi.");
        },
        error: function () {
            alert("Urun sepete eklenemedi");
        }
    });
};

function hesapla() {
    var urunler = document.getElementsByClassName("urun");
    var toplamTutar = 0;
    var kargoFiyati = 20;

    for (var i = 0; i < urunler.length; i++) {
        var urun = urunler[i];
        var fiyat = parseFloat(urun.getAttribute("data-fiyat"));
        var adetInput = urun.getElementsByClassName("cart_quantity_input")[0];
        var adet = parseInt(adetInput.value);
        if (adet > 5) {
            adet = 5;
            urun.getElementsByClassName("cart_quantity_input")[0].value = 5;
        }
        var urunTutari = fiyat * adet;
        toplamTutar += urunTutari;
    }

    var sepetTutariSpan = document.getElementById("sepetTutari");
    sepetTutariSpan.textContent = toplamTutar.toFixed(2);

    var toplamSepetTutariSpan = document.getElementById("toplamSepetTutari"); 
    var toplamSepTutar = toplamTutar + kargoFiyati;
    toplamSepetTutariSpan.textContent = toplamSepTutar.toFixed(2);
}
window.onload = hesapla;

function DeleteItemCart(productId) {
    var productID = productId;
    $.ajax({
        type: 'POST',
        url: '/Cart/DeleteItemCart',
        data: { productID }
        ,
        success: function () {
            alert("Urun sepetten silindi.");
        },
        error: function () {
            alert("Urun sepetten silinemedi.");
        }
    });
};

function checkedOnClick(el) {

    
    var checkboxesList = document.getElementsByClassName("checkoption");
    for (var i = 0; i < checkboxesList.length; i++) {
        checkboxesList.item(i).checked = false; 
    }

    el.checked = true; 
}

function AddOrder() {
    var subTotal = document.getElementById("toplamSepetTutari").innerHTML;
    var paymentType = 1;
    if (document.getElementById("pay2").checked) {
        paymentType = 2;
    }
    var address = document.getElementById("orderAddress").value;
    var description = document.getElementById("orderDescription").value;
    var phoneNumber = document.getElementById("orderPhone").value;

    $.ajax({
        type: 'POST',
        url: '/Order/AddOrder',
        data: { subTotal, paymentType, address, description, phoneNumber }
        ,
        success: function () {
            alert("Siparis Alindi...");
        },
        error: function () {
            alert("Siparis Alinamadi..");
        }
    });

    var paymentType = 1;
    var subTotal = document.getElementById("toplamSepetTutari").innerHTML = "0";
    var address = document.getElementById("orderAddress").value = "";
    var description = document.getElementById("orderDescription").value="";
    var phoneNumber = document.getElementById("orderPhone").value = "";
    var orderEmail = document.getElementById("orderEmail").value = "";
    var orderName = document.getElementById("orderName").value = "";
    var orderSurname = document.getElementById("orderSurname").value = "";
    var orderCartsTotal = document.getElementById("sepetTutari").innerHTML = "0";

};

