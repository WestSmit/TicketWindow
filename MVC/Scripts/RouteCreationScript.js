document.getElementById("btnAddStation").onclick = AddStation;
document.getElementById("btnAddCarriage").onclick = AddCarriage;



function AddStation() {
    var inputName = document.getElementById("SelectedStationId");
    var inputTime = document.getElementById("StationTimeId");

    document.getElementById("validLabel").hidden = true;

    if (inputTime.value == "") {
        document.getElementById("validLabel").hidden = false;
        document.getElementById("validLabel").textContent = "You must set time"
        return;
    }

    document.getElementById("EmptyStationBlockid").hidden = true; //hide info block

    var List = document.getElementById("StationListId");
    List.hidden = false;                                     //show block of list station

    var ListItem = document.getElementById("ListItemid");
    var CloneItem = ListItem.cloneNode(true);
    CloneItem.setAttribute("id", "newItem")
    CloneItem.hidden = false;
    List.appendChild(CloneItem);
    
    var NameElements = document.getElementsByName("name");
    var TimesElements = document.getElementsByName("time");

    for (var i = 1; i < NameElements.length; i++) {
        if (NameElements[i].textContent == "") {
            NameElements[i].insertAdjacentText('afterbegin', inputName.value);  
            TimesElements[i].insertAdjacentText('afterbegin', inputTime.value)
        }
    }
    document.getElementById("StationsId").value = "";
    document.getElementById("StationsTimeId").value = "";
    for (var i = 1; i < NameElements.length; i++) {
        document.getElementById("StationsId").value += NameElements[i].textContent + ",";
        document.getElementById("StationsTimeId").value += TimesElements[i].textContent + ",";
    }     
    inputName.value = "";
    inputTime.value = "";
}
function RemoveStation() {
    event.currentTarget.parentElement.parentElement.remove();

    var NameElements = document.getElementsByName("name");
    var TimesElements = document.getElementsByName("time");

    document.getElementById("StationsId").value = "";
    document.getElementById("StationsTimeId").value = "";

    for (var i = 1; i < NameElements.length; i++) {
        document.getElementById("StationsId").value += NameElements[i].textContent + ",";
        document.getElementById("StationsTimeId").value += TimesElements[i].textContent + ",";
    }
}

function AddCarriage() {
    var emptyItem = document.getElementById("emptyItem");
    document.getElementById("EmptyCarriageBlockid").hidden = true;

    var CloneItem = emptyItem.cloneNode(true);
    CloneItem.hidden = false;
    CloneItem.setAttribute("id", "");

    CloneItem.onclick = function (event)
    {
        event.currentTarget.remove();

        var NameElements = document.getElementsByName("CariagesItem");

        document.getElementById("CarriagesId").value = "";
        for (var i = 1; i < NameElements.length; i++) {
            document.getElementById("CarriagesId").value += NameElements[i].textContent + ",";
        }
    };
   

    CloneItem.insertAdjacentText('beforeend', document.getElementById("CarriageTypeId").value)
    emptyItem.parentElement.appendChild(CloneItem);
 
    var NameElements = document.getElementsByName("CariagesItem");

    document.getElementById("CarriagesId").value = "";
    for (var i = 1; i < NameElements.length; i++) {
        document.getElementById("CarriagesId").value += NameElements[i].textContent + ",";
    }
}

