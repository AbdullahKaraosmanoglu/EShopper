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
                Input.style.backgroundColor = "#FF4D4D";
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
            alert("�r�n sepete eklendi.");
        },
        error: function () {
            alert("sepete eklenemedi");
        }
    });
};

function Dumenden() {


    var price = document.getElementsByClassName("ticket_price")[n].innerHTML;
    var noTickets = document.getElementsByClassName("num")[n].value;
    var total = parseFloat(price) * noTickets;
    if (!isNaN(total))
        document.getElementsByClassName("total")[n].innerHTML = total;


            alert("d�menden giri� ba�ar�l�");
      
};

