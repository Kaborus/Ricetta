const url = 'https://localhost:7090/Recipes/Edit/1';


context('Title', () => {
    beforeEach(() => {
        cy.visit(url)
    });

    describe('Paginacontrole Edit', () => {
        it('Toont alle vereiste elementen', () => {
            // Bezoek de pagina die je wilt testen
            cy.visit(url);

            // Controleer of de titel aanwezig is en de juiste tekst heeft
            cy.contains('h1', 'Edit').should('exist');

            // Controleer of de subtitel voor het recept aanwezig is
            cy.contains('h4', 'Recipe').should('exist');

            // Controleer of het invulveld voor de naam van het recept aanwezig is
            cy.get('input[name="Name"]').should('exist');

            // Controleer of het invulveld voor de ingrediënten aanwezig is
            cy.get('textarea[name="Ingredients"]').should('exist');

            // Controleer of het invulveld voor de bereidingsstappen aanwezig is
            cy.get('textarea[name="PreparationSteps"]').should('exist');

            // Controleer of de categorie selectie aanwezig is
            cy.get('select[name="CategoryId"]').should('exist');

            // Controleer of de "Save" knop aanwezig is
            cy.get('input[type="submit"][value="Save"]').should('exist');

            // Controleer of de "Back to List" link aanwezig is
            cy.contains('a', 'Back to List').should('exist');
        });
    });
});