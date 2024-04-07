const url = 'https://localhost:7090/Recipes/Create';


context('Title', () => {
    beforeEach(() => {
        cy.visit(url)
    })

    it('Page title should contain text "Create"', () => {
        cy.get('h1').should('exist').contains('Create');
    })

    describe('Paginacontrole', () => {
        it('Toont alle vereiste elementen', () => {

            cy.visit(url);

            cy.contains('h1', 'Create').should('exist');

            cy.contains('h4', 'Recipe').should('exist');

            cy.get('input[name="Name"]').should('exist');

            cy.get('textarea[name="Ingredients"]').should('exist');

            cy.get('textarea[name="PreparationSteps"]').should('exist');

            cy.get('select[name="CategoryId"]').should('exist');

            cy.get('input[type="submit"]').should('exist');

            cy.contains('a', 'Back to List').should('exist');
        });
    });

});
