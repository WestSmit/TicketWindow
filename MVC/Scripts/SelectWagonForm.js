var buttons = document.getElementsByName("selectWagon")

for (var i = 0; i < buttons.length; i++) {
    buttons[i].onclick = AddPassenger;
}

function AddPassenger() {
    var RouteId = event.currentTarget.dataset.routeid;
    var WagonNumber = event.currentTarget.dataset.number;
    var Form = document.getElementById("SelectWagonForm" + RouteId);
    Form.hidden = false;
    var PassegerList = document.getElementById("PassengerList" + RouteId);
    var NewItem = PassegerList.children.namedItem("EmptyItem").cloneNode(true);
    NewItem.setAttribute("name", "newItem");
    NewItem.hidden = false;

    NewItem.children.namedItem("passenger").textContent = "passenger 222";

    PassegerList.appendChild(NewItem);
    console.log(NewItem);
}