var resultW = "<option value='-1' >All Members</option>";
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
                    splitHits += "<tr>";

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
                            splitHits += "<td className=\"tm-product-name\">";
                            splitHits += obj.fullName + "</td>";
                            splitHits += "<td className=\"text-center\">";
                            splitHits += obj.id + "</td>";
                            splitHits+="<td><button onclick=\"EditPage('" + obj.id + "')\" type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">\n" +
                                "<i class=\"fas fa-edit fa-lg \"></i></button></td>";
                            splitHits+="<td><button onclick=\"deleteCam('" + obj.note + "')\" type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">\n" +
                                "<i class=\"far fa-trash-alt fa-lg  \"></i></button></td>"
                            splitHits += "</tr>";

                            document.getElementById("membersList").innerHTML = splitHits;
                        });
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
                member += resultHits.note.split(',')[0] + "<br> &emsp;&nbsp;Images: <br><br>&emsp;&nbsp";
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

function EditPage(id) {
    document.getElementById('EditMember').style.display = 'block';
    var res = fetch("https://localhost:5001/Smartface/WatchlistMember/getMember?id=" + memberId, {
        method: 'GET',
        withCredentials: true,
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        }
    })
        .then(result => result.json())
        .then(result => {
                member = result;
                document.getElementById("inlineFormInputGroupUsername1").value = result.id;
                document.getElementById("inlineFormInputGroupUsername2").value = result.displayName;
                document.getElementById("inlineFormInputGroupUsername3").value = result.fullName;
                document.getElementById("inlineFormInputGroupUsername4").value = result.note.split(',')[0];
                document.getElementById("inlineFormInputGroupUsername5").value = result.note;
            }
        );
}
function updateMember() {
    member.id = document.getElementById("inlineFormInputGroupUsername1").value;
    member.displayName = document.getElementById("inlineFormInputGroupUsername2").value;
    member.fullName = document.getElementById("inlineFormInputGroupUsername3").value;
    member.note = document.getElementById("inlineFormInputGroupUsername4").value+","+document.getElementById("inlineFormInputGroupUsername5").value+","+member.note.split(',')[2];
    let watchlistMember = JSON.stringify(member);
    fetch("https://localhost:5001/Smartface/WatchlistMember/update?member=" + watchlistMember, {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json',
        },
        body: watchlistMember
    }).then(res => window.open("List.html", "_self"));
}
function deleteCam(note) {
    id=note.split(',')[2];
    alert(id)
    fetch("https://localhost:5001/Smartface/WatchlistMember/delete?id=" + id, {
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
    if (value === '-1') reset();
    else
        displayUsers("https://localhost:5001/Smartface/Watchlist/getMembers?id=" + watchlistArray[value].id)

}

function reset() {
    location.reload();
}