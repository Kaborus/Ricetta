$(document).ready(function () {
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
});