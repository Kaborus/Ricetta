const url = 'https://localhost:7090/';


context('Title', () => {
    beforeEach(() => {
        cy.visit(url)
    })

    it('Page title should contain text "Ricetta"', () => {
        cy.get('hello-world')
            .shadow()
            .find('[data-cy="page-title"]')
            .should('have.text', 'Hello World!')
    })


    it('After DOM connected tile has img element', () => {
        cy.get('steenpapierschaar-app')
            .shadow()
            .find('#card-steen')
            .shadow()
            .find('img')
            .should('be.visible')
            .should('not.have.class', 'selected')
    })

});
