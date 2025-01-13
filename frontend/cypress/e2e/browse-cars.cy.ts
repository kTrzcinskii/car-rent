describe("Browse cars", () => {
  it("it should enter browse page", () => {
    cy.visit("/");
    cy.get('[data-testid="browse-cars-button"]').click();
    cy.location("pathname").should("eq", "/browse");
  });

  it("it should filter cars based on search bar input", () => {
    cy.visit("/browse");
    cy.get('[data-testid="brand-name-search-input"]').type("skoda");
    cy.get('[data-testid="model-name-search-input"]').type("octavia");
    cy.get('[data-testid="search-cars-button"]').click();
    cy.url().should("include", "brandName=skoda");
    cy.url().should("include", "modelName=octavia");
    cy.get('[data-testid="car-card-header"]').each((el) => {
      cy.wrap(el).should("have.text", "Skoda Octavia");
    });
  });

  it("it should enter single car page after clicking car card", () => {
    cy.visit("/browse");
    cy.get('[data-testid="car-card-header"]').first().click();
    cy.location("pathname").should("match", /^\/car\/[^/]+$/);
  });

  it("it should go to the next page until last page and then back to first page", () => {
    cy.visit("/browse");

    const nextPageSelector = '[data-testid="browse-pagination-next"]';
    const prevPageSelector = '[data-testid="browse-pagination-previous"]';

    let currentPage = 0;

    function checkPage(expectedPage: number) {
      cy.url().should("include", `page=${expectedPage}`);
    }

    function goToLastPage() {
      cy.get(nextPageSelector).then((button) => {
        if (!button.attr("aria-disabled")) {
          currentPage += 1;
          cy.wrap(button).click();
          checkPage(currentPage);
          // Recursively call until the last page
          goToLastPage();
        }
      });
    }

    function goToFirstPage() {
      cy.get(prevPageSelector).then((button) => {
        if (!button.attr("aria-disabled")) {
          currentPage -= 1;
          cy.wrap(button).click();
          checkPage(currentPage);
          // Recursively call until the last page
          goToLastPage();
        }
      });
    }

    goToLastPage();
    goToFirstPage();

    // If it doesn't include page it means its 0
    cy.url().should("not.include", "page");
  });
});
