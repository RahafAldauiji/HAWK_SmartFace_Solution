function addCam(){
    let name = document.getElementById("inlineFormInputGroupUsername1").value;
    let source=document.getElementById("inlineFormInputGroupUsername2").value;
    let data = {source: source, name: name};
    fetch("https://localhost:5001/HAWK/Camera/create",{
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(res=>window.open("Cameras.html","_self"));
}