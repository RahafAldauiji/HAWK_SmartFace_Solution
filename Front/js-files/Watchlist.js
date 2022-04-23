fetch("https://localhost:44313/Smartface/Watchlist/getAllWatchlist", {
    method: 'GET',
    withCredentials: true,
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    }
}).then(resul => resul.json())
    .then(result => {

        var split = "";
        result.items.map(obj => {
            split += "<tr> ";
            split += "<td className=\"tm-product-name\">";
            split += obj.displayName + "</td> <br>";
            split += "<td className=\"text-center\">";
            split += obj.threshold + "</td>";
            split += "<td> " +
                "<button  onclick=\"EditPage('" + obj.id + "')\" type=\"button\" class=\" btn btn-sh btn-sm \"  style=\"float: right\">" +
                "<i class=\"fas fa-edit fa-lg \" ></i></button>" +
                "<button type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">" +
                "<i class=\"far fa-trash-alt fa-lg \" onclick=\"deleteWatchlist('" + obj.id + "')\"></i></button>" +
                "</td>"; 
        })
        
        document.getElementById("WatchList").innerHTML = split;
    });

function AddPage() {
   // window.open("addWatchlist.html", "_self");
    document.getElementById('AddWatchlist').style.display='block';
}

function EditPage(id) {
    sessionStorage.setItem('WatchlistId', id);
    //window.open("editWatchlist.html", "_self");
    document.getElementById('EditWatchlist').style.display='block';
}

function deleteWatchlist(id) {
    //alert(id)
    fetch("https://localhost:44313/Smartface/Watchlist/delete?id=" + id, {
        method: 'DELETE',
        withCredentials: true,
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        }
    }).then(o => location.reload())

}