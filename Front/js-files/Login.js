function login() {
    var username = document.getElementById("username").value.trim();
    var password = document.getElementById("password").value.trim();
    let data = {username: username, password: password};
    let r = fetch("https://localhost:5001/Smartface/authenticate", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(response => response.json())
        .then(res => {
                if (res !== null) {
                    var bearer = 'Bearer ' + res.token;
                    sessionStorage.setItem("userT", bearer);
                    window.open("HomePage.html", "_self");
                } else {
                    alert("Wrong username or password");
                }

            }
        )

}

function Registration() {
    window.open("registrationPage.html", "_self");
}