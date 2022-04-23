var split = "";
var list;
fetch("https://localhost:5001/Smartface/Watchlist/getAllWatchlist", {
    method: 'GET',
    withCredentials: true,
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    }
}).then(resul => resul.json())
    .then(result => {
        result.items.map(obj => {
            split += "<tr> ";
            split += "<td className=\"tm-product-name\">";
            split += obj.displayName + "</td>";
            split += "<td className=\"text-center\">";
            split += obj.threshold + "</td>";
            split += "<td> " +
                "<button  onclick=\"EditPage('" + obj.id + "')\" type=\"button\" class=\" btn btn-sh btn-sm \"  style=\"float: right\">" +
                "<i class=\"fas fa-edit fa-lg \" ></i></button>" +
                "<button type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">" +
                "<i class=\"far fa-trash-alt fa-lg \" onclick=\"deleteWatchlist('" + obj.id + "')\"></i></button>" +
                "</td>";
            split += "</tr> ";
        })

        document.getElementById("WatchList").innerHTML = split;
    });

function AddPage() {
    document.getElementById('AddWatchlist').style.display = 'block';
}
function addWatchlist() {
    let displayName = document.getElementById("inlineFormInputGroupUsername1").value;
    let fullName = document.getElementById("inlineFormInputGroupUsername6").value;
    let threshold = document.getElementById("inlineFormInputGroupUsername2").value;
    let data = {displayName: displayName, fullName: fullName, threshold: parseInt(threshold)};
    fetch("https://localhost:5001/Smartface/Watchlist/create", {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(res => window.open("Watchlists.html", "_self"));
}
function EditPage(id) {
    document.getElementById('EditWatchlist').style.display = 'block';
    var res = fetch("https://localhost:5001/Smartface/Watchlist/getWatchlist?id=" + id, {
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
                document.getElementById("inlineFormInputGroupUsername3").value = result.id;
                document.getElementById("inlineFormInputGroupUsername4").value = result.displayName;
                document.getElementById("inlineFormInputGroupUsername7").value = result.fullName;
                document.getElementById("inlineFormInputGroupUsername5").value = result.threshold;
            }
        );

}

function updateWatchlist() {
    list.id = document.getElementById("inlineFormInputGroupUsername3").value;
    list.displayName = document.getElementById("inlineFormInputGroupUsername4").value;
    list.fullName = document.getElementById("inlineFormInputGroupUsername7").value;
    list.threshold = document.getElementById("inlineFormInputGroupUsername5").value;
    let watchlist = JSON.stringify(list);
    fetch("https://localhost:5001/Smartface/Watchlist/upadte?watchlist=" + watchlist, {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json',
        },
        body: watchlist
    }).then(res => window.open("Watchlists.html", "_self"));
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