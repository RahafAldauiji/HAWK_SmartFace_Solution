fetch("https://localhost:5001/Smartface/Watchlist/getAllWatchlist", {
    method: 'GET',
    withCredentials: true,
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    }
}).then(resul => resul.json())
    .then(result => {

        var split = "";
        for (let i = 0; i < result.items.length; i++) {
            split += "<tr> ";
            split += "<td className=\"tm-product-name\">";
            split += result.items[i].displayName + "</td> <br>";
            split += "<td className=\"text-center\">";
            split += result.items[i].threshold + "</td>";
            split += "<td> " +
                "<button  onclick=\"EditPage('" + result.items[i].id + "')\" type=\"button\" class=\" btn btn-sh btn-sm \"  style=\"float: right\">" +
                "<i class=\"fas fa-edit fa-lg \" ></i></button>" +
                "<button type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">" +
                "<i class=\"far fa-trash-alt fa-lg \" onclick=\"deleteWatchlist('" + result.items[i].id + "')\"></i></button>" +
                "</td>";
        }
        document.getElementById("WatchList").innerHTML = split;
    });

function AddPage() {
    window.open("addWatchlist.html", "_self");
}

function EditPage(id) {
    sessionStorage.setItem('WatchlistId', id);
    window.open("editWatchlist.html", "_self");
}

function deleteWatchlist(id) {
    //alert(id)
    fetch("https://localhost:5001/Smartface/Watchlist/delete?id=" + id, {
        method: 'DELETE',
        withCredentials: true,
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        }
    }).then(o => location.reload())

}