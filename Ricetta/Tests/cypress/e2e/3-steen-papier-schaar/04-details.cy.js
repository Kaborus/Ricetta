const url = 'https://localhost:7090/Recipes/Details/1';


context('Title', () => {
    beforeEach(() => {
        cy.visit(url)
    })

    describe('Paginacontrole Details', () => {
        it('Toont alle vereiste elementen', () => {
            cy.visit(url);

            cy.contains('h1', 'Details').should('exist');

            cy.contains('h1 a', 'Opslaan').should('exist');

            cy.contains('h4', 'Recipe').should('exist');

            cy.contains('dt', 'MemberId').should('exist');

            cy.contains('dt', 'Name').should('exist');

            cy.contains('dt', 'Category').should('exist');

            cy.contains('dt', 'Ingredients').should('exist');

            cy.contains('dt', 'PreparationSteps').should('exist');

            cy.contains('a', 'Back to List').should('exist');
        });
    });
});