class Message extends HTMLElement {
    shadowRoot;
    constructor() {
        super();
        this.shadowRoot = this.attachShadow({ mode: 'open' });
        //this.shadowRoot.innerHTML = `<p>Test</p`;

        this.createMessage();
        //this.addEventListener();
    }

    connectedCallback() {
        this.attachStyling();
    }

    createMessage() {
        const box = document.createElement('div');
        box.setAttribute('class', 'message');
        box.innerHTML = "Test";
        this.shadowRoot.appendChild(box);
    }
    
    attachStyling() {
        const link = document.createElement('link');
        link.setAttribute('rel', 'stylesheet');
        link.setAttribute('href', '~/css/message.css');
        this.shadowRoot.appendChild(link);
    }

    addEventListener() {
        this.shadowRoot.querySelector(`close`).addEventListener('click', () => {
            this.shadowRoot.removeChild('recipe-message');
        });
    }
}

customElements.define('recipe-message', Message);