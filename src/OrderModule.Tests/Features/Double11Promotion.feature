@order_pricing
Feature: E-commerce Order Double11 Promotions
  As a shopper
  I want the system to calculate my order total with Double11 promotions
  So that I can understand how much to pay and what items I will receive
  
  Scenario: Walking skeleton test for Double11
    Given a simple Double11 test scenario
    When I run the Double11 test
    Then the Double11 test should pass

  Scenario: Get 20% off for every 10 identical items purchased
    Given Double11 promotion is enabled
    When a customer places an order with:
      | productName | quantity | unitPrice |
      | T-shirt     | 10        | 500      |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 5000           | 1000     | 4000        |

  Scenario: Buy twelve pairs of socks at $100 per pair
    Given Double11 promotion is enabled
    When a customer places an order with:
      | productName | quantity | unitPrice |
      | socks       | 12        | 100      |
    Then the order summary should be:
      | originalAmount | discount   | totalAmount |
      | 1200           | 200        | 1000        |

  Scenario: Buy twenty seven pairs of socks at $100 per pair
    Given Double11 promotion is enabled
    When a customer places an order with:
      | productName | quantity | unitPrice |
      | socks       | 27        | 100      |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 2700           | 400      | 2300        |

  Scenario: Buy 10 different products (A, B, C, D, E, F, G, H, I, J)
    Given Double11 promotion is enabled
    When a customer places an order with:
      | productName | quantity | unitPrice |
      | A           | 1        | 100       |
      | B           | 1        | 100       |
      | C           | 1        | 100       |
      | D           | 1        | 100       |
      | E           | 1        | 100       |
      | F           | 1        | 100       |
      | G           | 1        | 100       |
      | H           | 1        | 100       |
      | I           | 1        | 100       |
      | J           | 1        | 100       |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 1000           | 0        | 1000        |
