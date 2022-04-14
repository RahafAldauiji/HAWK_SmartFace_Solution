let watchlistId = sessionStorage.getItem('WatchlistId');
var list
var res = fetch("https://localhost:5001/Smartface/Watchlist/getWatchlist?id=" + watchlistId, {
    method: 'GET',
    withCredentials: true,
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    }
})
    .then(result => result.json())
    .then(result => {
            list = result;
            document.getElementById("inlineFormInputGroupUsername2").value = result.id;
            document.getElementById("inlineFormInputGroupUsername3").value = result.displayName;
            document.getElementById("inlineFormInputGroupUsername4").value = result.threshold;
        }
    );

function updateWatchlist() {
    list.id = document.getElementById("inlineFormInputGroupUsername2").value;
    list.displayName = document.getElementById("inlineFormInputGroupUsername3").value;
    list.fullName = document.getElementById("inlineFormInputGroupUsername3").value;
    list.threshold = document.getElementById("inlineFormInputGroupUsername4").value;
    let watchlist = JSON.stringify(list);
    fetch("https://localhost:5001/Smartface/Watchlist/upadte?watchlist="+watchlist, {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json',
        },
        body: watchlist
    }).then(res=>window.open("Watchlists.html", "_self"));
}