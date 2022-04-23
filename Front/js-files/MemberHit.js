function hitsSript() {
    let res;
    var jresult;
    var splitHits = "";
    var image;
    console.log("In")
    res = fetch("https://localhost:44313/Smartface/Match", {
        method: 'GET',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },
    })
        .then(resultHits => resultHits.json())
        .then(resultHits => {
                if (resultHits != null) {
                    jresult = JSON.stringify(resultHits);
                    console.log(jresult)
                    fetch("https://localhost:44313/Smartface/WatchlistMember/getMemberFace?id=" + resultHits.watchlistMemberId, {
                        method: 'GET',
                        headers: {
                            'Authorization': sessionStorage.getItem('userT'),
                            'Content-Type': 'application/json'
                        },
                    })
                        .then(resultHits1 => resultHits1.json())
                        .then(resultHits2 => {
                            jresult = JSON.stringify(resultHits2);
                            console.log(jresult);
                            image = jresult;
                            splitHits += "<tr><td><i class=\"fas\">";
                            splitHits += "<img src=\'data:image/(png|jpg|jpeg);base64," +
                                image.replaceAll("\"", " ") + "'width='90px'height='90px'><br>";
                            splitHits += resultHits.watchlistMemberFullName + "<br>";
                            splitHits += "Match Score: " + resultHits.score + "</i></td></tr>";
                            document.getElementById("tableMembers").innerHTML = splitHits;
                        });

                }
            }
        );
}

setInterval(hitsSript, 500);