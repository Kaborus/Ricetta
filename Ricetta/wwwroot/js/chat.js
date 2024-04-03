
const LobbyModule = (function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    //Disable the send button until connection is established.
    document.getElementById("sendButton").disabled = true;

    connection.on("ReceiveMessage", (user, message) => {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you 
        // should be aware of possible script injection concerns.
        li.textContent = `${user}: ${message}`;
    });

    connection.start().then(() => {
        connection.invoke("JoinGroup", window.LobbyId).catch((err) => {
            console.error(err.toString());
        })
    }).then(() => {
        document.getElementById("sendButton").disabled = false;
    }).catch((err) => {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", (event) => {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendMessage", window.LobbyId, user, message).catch((err) => {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    /*window.addEventListener("beforeunload", () => {
        connection.invoke("LeaveGroup", window.LobbyId).catch((err) => {
            console.error(err.toString());
        });*/
    
        return {};
//})();
})

const module = new LobbyModule();