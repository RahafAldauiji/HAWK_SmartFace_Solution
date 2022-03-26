fetch("https://localhost:5001/HAWK/Camera/getAllCameras", {
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
                    //splitHits += "<tr>";
                    split += "<td className=\"tm-product-name\">";
                    split += obj.name + "</td> <br>";
                    split += "<td className=\"text-center\">";
                    split += obj.enabled + "</td>";
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
    window.open("addCamera.html", "_self");
}

function EditPage(id) {
    sessionStorage.setItem('cameraId', id);
    window.open("editCamera.html", "_self");
}

function deleteCam(id) {
    //alert(id)
    fetch("https://localhost:5001/HAWK/Camera/delete?id=" + id, {
        method: 'DELETE',
        withCredentials: true,
        headers: {
            'Authorization': sessionStorage.getItem('userT'),
            'Content-Type': 'application/json'
        }
    }).then(o => location.reload())

}