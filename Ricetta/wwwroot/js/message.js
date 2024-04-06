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
        box.innerHTML = "Test";
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

customElements.define('recipe-message', Message);

//export default Message;