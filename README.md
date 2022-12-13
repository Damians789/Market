# Market
Program written druing classes to represent particular market situation

Write a program that will model the following market situation.

- Retailers reviewing limited product access over time that provides a free market at prices that come from:
  - the cost of producing the product
  - value (the value of money decreases over time) and effects
  - (how much he wants for the extra margin product).

  The goal is to make as much profit as possible.

- Buyers have needs, principles and money. They watch the product offers on the market. Their behavior is described by the following rules:
  - they want to buy certain products and track their prices but don't need to buy them immediately
  - they know the inflation level
  - their willingness to buy the product decreases with the increasing price of the product, regardless of whether the price increase was caused by inflation or a margin

- The Central Bank observes an increase in product prices and market turnover. Sets the current level of inflation. The bank tries to maintain constant tax revenues calculated as the product of inflation and turnover at a given inflation.

Patterns used:

Visitor pattern for updating seller product data and buyer parameters.

Observer pattern to passively observe the following changes:
- Sellers and Buyers watch the Central Bank to find out what the inflation rate is
- Buyers observe Sellers' offers and may react to them, but they do not have to.
