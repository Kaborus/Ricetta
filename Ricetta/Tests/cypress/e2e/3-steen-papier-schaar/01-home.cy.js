const url = 'https://localhost:7090/';

context('Window', () => {
  beforeEach(() => {
    cy.visit(url)
  })

/*  it('cy.window() - get the global window object', () => {
    cy.window().should('have.property', 'top')
  })*/

    describe('Paginacontrole', () => {
        it('Bevat een h1-element', () => {
            // Bezoek de pagina die je wilt testen
            cy.visit('https://localhost:7090/');

            // Controleer of het h1-element aanwezig is
            cy.get('h1').should('exist').contains('Welcome');
        });
    });

 /* it('cy.document() - get the document object', () => {
    // https://on.cypress.io/document
    cy.document().should('have.property', 'charset').and('eq', 'UTF-8')
  })

  it('cy.title() - get the title', () => {
    // https://on.cypress.io/title
    cy.title().should('include', 'Ricetta');
  })*/
})


