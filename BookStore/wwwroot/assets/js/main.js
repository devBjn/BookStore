//Edit header
const header = document.getElementById("header");
window.addEventListener("scroll", () => {
    var sclY = this.scrollY;
    if (sclY > 0) {
        header.style.height = "125px";
        header.style.backgroundColor = `white`;
        header.style.boxShadow = `0 0 50px 0 rgb(0 0 0 / 30%)`;
        header.style.transition = `all 0.5s`;
        header.style.zIndex = `10`;
        header.style.position = `fixed`;
        header.style.paddingTop = "25px";
    } else {
        header.style.height = "130px";
        header.style.background = `transparent`;
        header.style.transition = `all 0.5s`;
        header.style.zIndex = `1`;
        header.style.position = `absolute`;
        header.style.boxShadow = `none`;
        header.style.paddingTop = "20px";
    }
})


//Back to top
var btn = $('#button');

$(window).scroll(function () {
    if ($(window).scrollTop() > 300) {
        btn.addClass('show');
    } else {
        btn.removeClass('show');
    }
});

btn.on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, '300');
});


//load cart count
loadCartCount();
async function loadCartCount() {
    var usernameEl = document.getElementById("username");
    var cartIcon = document.getElementById("cartIcon");
    if (usernameEl == null) {
        cartIcon.style.display = "none"
    } else {
        cartIcon.style.display = "block"
    }
    try {
        var response = await fetch(`/Cart/GetTotalItemInCart`);
          if (response.status == 200) {
                var result = await response.json();
                var cartCountEl = document.getElementById("cartCount");
                cartCountEl.innerHTML = result;
                }
            }
    catch (err) {
          console.log(err);
    }
}

 