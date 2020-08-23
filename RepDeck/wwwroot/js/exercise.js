document.getElementById("next").disabled = true;

var connection = new signalR.HubConnectionBuilder().withUrl("./ExerciseHub").build();
connection.on("newExercise", function (name, muscleGroup, description) {
    $("li").remove();
    var ul = document.getElementById("list");
    for (i = 1; i < description.split(")").length; i++) {
        var li = document.createElement("li");
        li.appendChild(document.createTextNode(description.split(")")[i].split('.')[0]));
        li.setAttribute("class", "list-group-item");
        li.setAttribute("style", "margin:2px");
        ul.appendChild(li);

    }
    document.getElementById("name").innerHTML = name;
    document.getElementById("muscleGroup").innerHTML = muscleGroup;
    finalMessage.classList.add("show");
});
connection.on("Finished", function () {
    $("#alert").modal('show')
});

connection.start().then(function () {
    document.getElementById("next").disabled = false;
}).catch(function (err) {
    return console.error(err);
});
function send(guid, stringBind) {
    finalMessage.classList.remove("show");
    connection.invoke("NextExercise", guid, stringBind).catch(function (err) {
        return console.error(err);
    });
    event.preventDefault();
}