//import Message from '.~/js/message.js';

class Message extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
        //this.shadowRoot.innerHTML = `<p>Test</p`;

        this.createMessage();
        this.attachStyling();
        this.addEventListener();
    }

    createMessage() {
        const box = document.createElement('div');
        box.setAttribute('class', 'message');

        const closebtn = document.createElement('button');
        closebtn.textContent = "X";
        box.innerHTML = `Test`;
        this.shadowRoot.appendChild(box);
        this.shadowRoot.appendChild(closebtn);
    }

    attachStyling() {
        const link = document.createElement('link');
        link.setAttribute('rel', 'stylesheet');
        link.setAttribute('href', '/css/message.css');
        this.shadowRoot.appendChild(link);
    }

    addEventListener() {
        this.shadowRoot.querySelector(`button`).addEventListener('click', () => {
            alert("Test geslaagd")
            this.shadowRoot.removeChild('recipe-message');
        });
    }
}



//export default Message;
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


const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ChatCreated" , ()=> {
    alert("Iemand heeft een chatroom met jou aangemaakt");
}) 

connection.on("RecipeCreated", (recipeName, categoryName) => {
    // Toon de melding aan de gebruiker
    alert(`Nieuw recept aangemaakt: ${recipeName}, wat de categorie ${categoryName} heeft`);
    customElements.define('recipe-message', Message);

    //customElements.define('recipe - message', Message);
});

connection.start().catch(err => console.error(err.toString()));
