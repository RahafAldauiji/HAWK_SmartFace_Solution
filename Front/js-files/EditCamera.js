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
            document.getElementById("inlineFormInputGroupUsername3").value = result.id;
            document.getElementById("inlineFormInputGroupUsername4").value = result.name.split('-')[0];
            document.getElementById("inlineFormInputGroupUsername5").value = result.enabled;
            document.getElementById("inlineFormInputGroupUsername6").value = result.source;
            document.getElementById("inputGroupSelect01").value = result.name.split('-')[1];
        }
    );

function updateCam() {
    camera.id = document.getElementById("inlineFormInputGroupUsername3").value ;
    camera.name = document.getElementById("inlineFormInputGroupUsername4").value.split('-')[0]+ "-" + document.getElementById("inputGroupSelect02").value;
    camera.enabled = document.getElementById("inlineFormInputGroupUsername5").value;
    camera.source = document.getElementById("inlineFormInputGroupUsername6").value;
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