document.addEventListener('DOMContentLoaded', function () {

    var loginLink = document.getElementById('loginLink');
    var girisForm = document.getElementById('girisForm');
    var RegContainer = document.getElementById('RegisterContain');

    var loginContainer = document.getElementById('LoginContain');
    var overlay = document.getElementById('overlay');
    girisForm.addEventListener('click', function (event) {

        event.preventDefault();


        overlay.style.display = "block";
        overlay.style.opacity = "1";
        RegContainer.style.display = "none";
        RegContainer.style.opacity = "0";

        loginContainer.style.display = "block";
        loginContainer.style.opacity = "0";
        fadeIn(loginContainer, 300);
    });

    loginLink.addEventListener('click', function (event) {

        event.preventDefault();


        overlay.style.display = "block";
        overlay.style.opacity = "1";


        loginContainer.style.display = "block";
        loginContainer.style.opacity = "0"; 
        fadeIn(loginContainer, 300); 
    });
});


function fadeIn(element, duration) {
    var start = performance.now();

    function animate(time) {
        var progress = (time - start) / duration;
        if (progress > 1) {
            progress = 1;
        }

        element.style.opacity = progress;

        if (progress < 1) {
            requestAnimationFrame(animate);
        }
    }

    requestAnimationFrame(animate);
}



document.addEventListener('DOMContentLoaded', function () {

    var loginLink = document.getElementById('kayitol');

    var loginContainer = document.getElementById('RegisterContain');
    var overlay = document.getElementById('overlay');


    loginLink.addEventListener('click', function (event) {

        event.preventDefault();


        overlay.style.display = "block";
        overlay.style.opacity = "1";


        loginContainer.style.display = "block";
        loginContainer.style.opacity = "0";
        fadeIn(loginContainer, 300);
    });
});




function fadeIn(element, duration) {
    var start = performance.now();

    function animate(time) {
        var progress = (time - start) / duration;
        if (progress > 1) {
            progress = 1;
        }

        element.style.opacity = progress;

        if (progress < 1) {
            requestAnimationFrame(animate);
        }
    }

    requestAnimationFrame(animate);
}

document.addEventListener("DOMContentLoaded", function () {
    var registerContainer = document.getElementById("RegisterContain");
    var closeRegisterBtn = document.getElementById("closeRegisterBtn");
    var overlay = document.getElementById('overlay');

    closeRegisterBtn.addEventListener("click", function () {
        registerContainer.style = "display: none";
        overlay.style.display = "none";
        overlay.style.opacity = "0";
    });


 
});


document.addEventListener("DOMContentLoaded", function () {
    var registerContainer = document.getElementById("LoginContain");
    var closeRegisterBtn = document.getElementById("closeloginBtn");
    var overlay = document.getElementById('overlay');

    closeRegisterBtn.addEventListener("click", function () {
        registerContainer.style = "display: none";
        overlay.style.display = "none";
        overlay.style.opacity = "0";
    });



});

       