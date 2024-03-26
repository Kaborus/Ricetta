/*$(document).ready(function () {
    let ingredientIndex = 1;
    console.log("test");

    $('.add-ingredient').click(function () {
        let newIngredientRow = `
            <div class="ingredient-row">
                <input type="text" class="form-control" name="Ingredients[${ingredientIndex}].Name" placeholder="Ingredient name" />
                <button type="button" class="btn btn-danger remove-ingredient">Remove</button>
            </div>
        `;
        $('#ingredientContainer').append(newIngredientRow);
        ingredientIndex++;
    });

    $('#ingredientContainer').on('click', '.remove-ingredient', function () {
        $(this).closest('.ingredient-row').remove();
    });
});*/

class SearchBar extends HTMLElement {

    shadowRoot;
    constructor() {
        super();
        this.shadowRoot = this.attachShadow({ mode: 'open' });
        this.shadowRoot.innerHTML = `<input type="text"/>
        <p>type</p>`;
        this.addEventListener();
    }

    addEventListener() {
        this.shadowRoot.querySelector(`input`).addEventListener('keyup', (event) => {
            this.shadowRoot.querySelector(`p`).innerHTML = event.target.value;
        });
    }
}

customElements.define('search-bar', SearchBar);