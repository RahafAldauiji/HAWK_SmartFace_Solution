let cameraId = sessionStorage.getItem('cameraId');
let userT = sessionStorage.getItem('userT');
var camera;
var res = fetch("https://localhost:5001/Smartface/Camera/getCamera?id=" + cameraId, {
    method: 'GET',
    withCredentials: true,
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    }
})
    .then(result => result.json())
    .then(result => {
            camera = result;
            document.getElementById("inlineFormInputGroupUsername1").value = result.id;
            document.getElementById("inlineFormInputGroupUsername2").value = result.name.split('-')[0];
            document.getElementById("inlineFormInputGroupUsername3").value = result.enabled;
            document.getElementById("inlineFormInputGroupUsername4").value = result.source;
            document.getElementById("inputGroupSelect01").value = result.name.split('-')[1];
        }
    );

function updateCam() {
    camera.id = document.getElementById("inlineFormInputGroupUsername1").value ;
    camera.name = document.getElementById("inlineFormInputGroupUsername2").value.split('-')[0]+ "-" + document.getElementById("inputGroupSelect01").value;
    camera.enabled = document.getElementById("inlineFormInputGroupUsername3").value;
    camera.source = document.getElementById("inlineFormInputGroupUsername4").value;
    let cam = JSON.stringify(camera);
    fetch("https://localhost:5001/Smartface/Camera/update?camera=" + cam, {
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json',
        },
        body: cam
    }).then(res => window.open("Cameras.html", "_self"));
}