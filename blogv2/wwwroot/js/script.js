window.onscroll = function () { myFunction() };

var header = document.getElementById("myHeader");
var kayit = document.getElementById("kayitol");
var sticky = header.offsetTop;

function myFunction() {
    if (window.pageYOffset > sticky) {
        header.classList.add("fixed");
        header.style = "background-color: #fff;";
        kayit.style = "background-color: #1ba306;";
    } else {
        header.style = "background-color: #ffc017;";
        kayit.style = "background-color: #000;";
        header.classList.remove("fixed");
    }
}