let cameraId=sessionStorage.getItem('cameraId');
let userT=sessionStorage.getItem('userT');
var camera;
var res = fetch("https://localhost:5001/HAWK/Camera/getCamera?id="+cameraId,{
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
            document.getElementById("inlineFormInputGroupUsername1").value=result.id;
            document.getElementById("inlineFormInputGroupUsername2").value=result.name;
            document.getElementById("inlineFormInputGroupUsername3").value=result.enabled;
            document.getElementById("inlineFormInputGroupUsername4").value=result.source;

        }
    );
function updateCam(){
    camera.id=document.getElementById("inlineFormInputGroupUsername1").value;
    camera.name=document.getElementById("inlineFormInputGroupUsername2").value;
    camera.enabled=document.getElementById("inlineFormInputGroupUsername3").value;
    camera.source=document.getElementById("inlineFormInputGroupUsername4").value;
    let cam =  JSON.stringify(camera);
    fetch("https://localhost:5001/HAWK/Camera/update?camera="+cam,{
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json',
        },
        body: cam
    }).then(res=>window.open("Cameras.html", "_self"));
}