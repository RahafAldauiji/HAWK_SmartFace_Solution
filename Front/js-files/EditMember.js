let memberId = sessionStorage.getItem('memberId');
var member;
var res = fetch("https://localhost:44313/Smartface/WatchlistMember/getMember?id=" + memberId, {
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
            document.getElementById("inlineFormInputGroupUsername4").value = result.note;
        }
    );

function updateMember() {
    member.id = document.getElementById("inlineFormInputGroupUsername1").value;
    member.displayName = document.getElementById("inlineFormInputGroupUsername2").value;
    member.fullName = document.getElementById("inlineFormInputGroupUsername3").value;
    member.note = document.getElementById("inlineFormInputGroupUsername4").value;
    let watchlistMember = JSON.stringify(member);
    fetch("https://localhost:44313/Smartface/WatchlistMember/update?member=" + watchlistMember, {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json',
        },
        body: watchlistMember
    }).then(res => window.open("List.html", "_self"));
}