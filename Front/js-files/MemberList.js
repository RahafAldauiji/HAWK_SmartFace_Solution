var resultW = "<option selected >All Members</option>";
var watchlistArray = [];
var fetchUrl = "https://localhost:5001/Smartface/WatchlistMember/GetAllWatchlistMembers";
displayUsers(fetchUrl)

fetch("https://localhost:5001/Smartface/Watchlist/getAllWatchlist", {
    method: 'GET',
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    },
}).then(resultHits => resultHits.json()).then(resultHits => {
        let counter = 0;
        resultHits.items.map(obj => {
            resultW += " <option value=\"" + counter + "\">" + obj.displayName + "</option>"
            watchlistArray.push(obj)
            counter++;
        });
        document.getElementById("inputGroupSelect01").innerHTML = resultW;

    }
)

function displayUsers(url) {
    let res;
    var jresult;
    var splitHits = "";
    res = fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },
    })
        .then(resultHits => resultHits.json())
        .then(resultHits => {
                jresult = JSON.stringify(resultHits);
                resultHits.map(obj => {
                    splitHits += "<tr onclick=\"memberData('" + obj.id + "')\">";
                    //
                    fetch("https://localhost:5001/Smartface/WatchlistMember/getMemberFace?id=" + obj.id, {
                        method: 'GET',
                        headers: {
                            'Authorization': sessionStorage.getItem('userT'),
                            'Content-Type': 'application/json'
                        },
                    })
                        .then(resultHits1 => resultHits1.json())
                        .then(resultHits1 => {
                            splitHits += "<td>";
                            splitHits += "<img src=\'data:image/(png|jpg|jpeg);base64," +
                                resultHits1.toString() + "'width='60px'height='60px' onclick= \"memberData('" + obj.id + "')\">";
                            splitHits += "</td>";
                            splitHits += " <th scope=\"row\"> <input  id=\"" + obj.id + "\" name='member' type=\"hidden\" onclick=\"memberData('" + obj.id + "')\">" +
                                "</th>";
                            splitHits += "<td className=\"tm-product-name\">";
                            splitHits += obj.fullName + "</td> <br>";
                            splitHits += "<td className=\"text-center\">";
                            splitHits += obj.id + "</td>";

                            splitHits += "</tr>";
                            // splitHits += "<td> " +
                            //     // "<button  onclick=\"EditPage('" + obj.id + "')\" type=\"button\" class=\" btn btn-sh btn-sm \"  style=\"float: right\">" +
                            //     // "<i class=\"fas fa-edit fa-lg \" ></i></button>" +
                            //     // "<button type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">" +
                            //     // "<i class=\"far fa-trash-alt fa-lg \" onclick=\"deleteCam('" + obj.id + "')\"></i></button>" +
                            //     "</td>";

                            document.getElementById("membersList").innerHTML = splitHits;
                        });
                    //  window.alert(obj.fullName);

                });

            }
        );
}

function memberData(id) {
    
    document.getElementById("membersInfo").style.display = "block";
    var member = "";
    var jresult = "";
    fetch("https://localhost:5001/Smartface/WatchlistMember/getMember?id=" + id, {
        method: 'GET',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },

    })
        .then(resultHits => resultHits.json())
        .then(resultHits => {
                jresult = JSON.stringify(resultHits);
                member += "<tr><td><i class=\"fas fa-user-circle \"> Full Name: ";
                member += resultHits.fullName + "<br> &emsp;&nbsp;Display Name: ";
                member += resultHits.displayName + "<br> &emsp;&nbsp;Note: ";
                member += resultHits.note + "<br> &emsp;&nbsp;Images: <br><br>&emsp;&nbsp";
            sessionStorage.setItem('memberId', resultHits.note.split(',')[2]);
                var counter = 0;
                fetch("https://localhost:5001/Smartface/WatchlistMember/getFaces?id=" + id, {
                    method: 'GET',
                    headers: {
                        'Authorization': sessionStorage.getItem('userT'),
                        'Content-Type': 'application/json'
                    },
                })
                    .then(resultHits1 => resultHits1.json())
                    .then(resultHits1 => {
                        jresult = JSON.stringify(resultHits1);
                        resultHits1.map(obj => {
                            member += "<img src=\'data:image/(png|jpg|jpeg);base64," +
                                obj.toString() + "'width='90px'height='90px'>";
                            counter++;
                            if (counter === 3) {
                                member += "<br>&emsp;&nbsp;";
                                counter = 0;
                            }
                        });
                        member += "</i></td></tr>";
                        document.getElementById("memberBody").innerHTML = member;

                    });
            }
        );

}

function EditPage() {
    window.open("editMember.html", "_self");
}

function deleteCam() {
    let memberId = sessionStorage.getItem('memberId');
    fetch("https://localhost:5001/Smartface/WatchlistMember/delete?id=" + memberId, {
        method: 'DELETE',
        withCredentials: true,
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        }
    }).then(o => location.reload());

}

function filter() {
    var e = document.getElementById("inputGroupSelect01");
    var value = e.value;
    displayUsers("https://localhost:5001/Smartface/Watchlist/getMembers?id=" + watchlistArray[value].id)
   
}
function reset(){
   location.reload();
}