fetch("https://localhost:5001/Smartface/Camera/getAllCameras", {
    method: 'GET',
    withCredentials: true,
    headers: {
        'Authorization': sessionStorage.getItem('userT'),
        'Content-Type': 'application/json'
    }
})
    .then(results => results.json()
        .then(result => {
                var split = "";
               
                result.map(obj => {
                    split += "<tr> ";
                    split += "<td className=\"tm-product-name\">";
                    split += obj.name.split('-')[0] + "</td>";
                    split += "<td className=\"text-center\">";
                    split += obj.enabled + "</td>";
                    split += "<td >";
                    split += obj.name.split('-')[1] ==="1" ? "In": "Out" + "</td>";
                    split += "<td> " +
                        "<button  onclick=\"EditPage('" + obj.id + "')\" type=\"button\" class=\" btn btn-sh btn-sm \"  style=\"float: right\">" +
                        "<i class=\"fas fa-edit fa-lg \" ></i></button>" +
                        "<button type=\"button\" class=\" btn btn-sh btn-sm \" style=\"float: right\">" +
                        "<i class=\"far fa-trash-alt fa-lg \" onclick=\"deleteCam('" + obj.id + "')\"></i></button>" +
                        "</td>";
                    //splitHits += "</tr>";

                });

                document.getElementById("cameraList").innerHTML = split;

            }
        ));

function AddPage() {
  //  window.open("addCamera.html", "_self");
    document.getElementById('AddCamera').style.display='block';
}
function addCam(){
    let name = document.getElementById("inlineFormInputGroupUsername1").value+"-"+document.getElementById("inputGroupSelect01").value;
    let source=document.getElementById("inlineFormInputGroupUsername2").value;
    let data = {source: source, name: name};
    fetch("https://localhost:5001/Smartface/Camera/create",{
        method: 'POST',
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(res=>window.open("Cameras.html","_self"));
}

function EditPage(id) {
    document.getElementById('EditCamera').style.display='block';
    var camera;
    var res = fetch("https://localhost:5001/Smartface/Camera/getCamera?id=" + id, {
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


}
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
function deleteCam(id) {
    //alert(id)
    fetch("https://localhost:5001/Smartface/Camera/delete?id=" + id, {
        method: 'DELETE',
        withCredentials: true,
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        }
    }).then(o => location.reload())

}