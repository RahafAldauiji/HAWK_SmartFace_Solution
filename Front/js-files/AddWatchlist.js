﻿function addWatchlist() {
    let displayName = document.getElementById("inlineFormInputGroupUsername1").value;
    let fullName = document.getElementById("inlineFormInputGroupUsername6").value;
    let threshold = document.getElementById("inlineFormInputGroupUsername2").value;
    let data = {displayName: displayName, fullName: fullName, threshold: parseInt(threshold)};
    fetch("https://localhost:44313/Smartface/Watchlist/create", {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(res => window.open("Watchlists.html", "_self"));
}