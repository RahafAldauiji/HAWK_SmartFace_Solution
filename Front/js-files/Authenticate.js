
    var username ="Admin"
    var password = "Admin"
    let data = {username: username, password: password};
    let r = fetch("https://localhost:44313/Smartface/authenticate", {
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
                } 

            }
        )


