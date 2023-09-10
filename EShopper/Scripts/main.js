/*price range*/

/*$('#sl2').slider();*/

var RGBChange = function () {
    $('#RGB').css('background', 'rgb(' + r.getValue() + ',' + g.getValue() + ',' + b.getValue() + ')')
};


$(document).ready(function () {
    $(function () {
        $.scrollUp({
            scrollName: 'scrollYukarý', // Element ID
            scrollDistance: 300, // Elementin görünmeden önceki mesafe (px)
            scrollFrom: 'top', // 'top' veya 'bottom'
            scrollSpeed: 300, // Yukarý hareketin hýzý (ms)
            easingType: 'linear', // Yukarý hareket için geçiþ efekti
            animation: 'fade', // Fade, slide, none
            animationSpeed: 200, // Animasyon hýzý (ms)
            scrollTrigger: false, // Özel bir tetikleyici element belirleyin. Bir HTML dizesi veya jQuery nesnesi olabilir
            scrollText: '<i class="fa fa-angle-up"></i>', // Element için metin, HTML içerebilir
            scrollTitle: false, // Gerekirse özel bir <a> baþlýðý belirleyin
            scrollImg: false, // Resim kullanmak için true olarak ayarlayýn
            activeOverlay: false, // scrollUp aktif noktasýný göstermek için CSS rengi belirleyin, örn. '#00FFFF'
            zIndex: 2147483647 // Geçiþ katmaný için Z-indeksi


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
    handleCheckboxClick();
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
    var cardNumber = document.getElementById("orderCardNumber").value;
    var cardName = document.getElementById("orderCardName").value;
    var cardLastDate = document.getElementById("orderCardLastDate").value;
    var cardSecurityNumber = document.getElementById("orderCardSecurity").value;


    $.ajax({
        type: 'POST',
        url: '/Order/AddOrder',
        data: { subTotal, paymentType, address, description, phoneNumber, cardNumber, cardName, cardLastDate, cardSecurityNumber }
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
    var description = document.getElementById("orderDescription").value = "";
    var phoneNumber = document.getElementById("orderPhone").value = "";
    var orderEmail = document.getElementById("orderEmail").value = "";
    var orderName = document.getElementById("orderName").value = "";
    var orderSurname = document.getElementById("orderSurname").value = "";
    var orderCartsTotal = document.getElementById("sepetTutari").innerHTML = "0";
};

window.setTimeout(function () {
    $(".alert").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 4000);



function handleCheckboxClick() {
    var checkbox = document.getElementById("pay2");
    var orderCardNumber = document.getElementById("orderCardNumber");
    var orderCardName = document.getElementById("orderCardName");
    var orderCardLastDate = document.getElementById("orderCardLastDate");
    var orderCardSecurity = document.getElementById("orderCardSecurity");

    if (checkbox.checked) {
        orderCardNumber.disabled = false;
        orderCardName.disabled = false;
        orderCardLastDate.disabled = false;
        orderCardSecurity.disabled = false;
    }
    else {
        orderCardNumber.disabled = true;
        orderCardName.disabled = true;
        orderCardLastDate.disabled = true;
        orderCardSecurity.disabled = true;

        orderCardNumber.value = "";
        orderCardName.value = "";
        orderCardLastDate.value = "";
        orderCardSecurity.value = "";

    }
    var checkbox = document.getElementById("myCheckbox");
    checkbox.addEventListener("click", handleCheckboxClick);
}



window.addEventListener("load", handleCheckboxClick);
window.addEventListener("load", hesapla);
