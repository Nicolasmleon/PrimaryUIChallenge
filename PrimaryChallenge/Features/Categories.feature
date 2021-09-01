@MercadoLibre @Categories
Feature: ExcercisesFeature
	Search products per category

Background: 
	Given I open the web page

@smoketest
Scenario Outline: Search per Category
	Given I search for products '<products>' in category '<categories>' 
	Then The results are displayed

	Examples: 
	| categories            | products  |
	| Tecnología            | SAMSUNG   |
	| Agro                  | CAMIONES  |
	| Industrias y Oficinas | OFICINAS  |
	| Hogar y Muebles       | MUEBLES   |
	| Electrodomésticos     | AIRES AC. |


@smoketest
Scenario: Search in Category
	Given I search for 'Televisores' in category 'Tecnología'
	When I filter the results by 'Capital Federal' and choose the first product
	Then The title and price from the list matches with values in the publication
