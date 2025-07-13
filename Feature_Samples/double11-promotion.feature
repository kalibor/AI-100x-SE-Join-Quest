@order_pricing
Feature: E-commerce Order Double11 Promotions
  As a shopper
  I want the system to calculate my order total with Double11 promotions
  So that I can understand how much to pay and what items I will receive
  
  
  Scenario: Get 20% off for every 10 identical items purchased
    Given Double11 promotion is enabled
    When a customer places an order with 
      | productName | quantity | unitPrice |
      | T-shirt     | 10        | 500      |
    Then the order summary should be:
      | totalAmount | discount | finalAmount |
      | 5000        | 5000*0.2 | 4000 |
   
  Scenario: Buy twelve pairs of socks at $100 per pair
    Given Double11 promotion is enabled
    When a customer places an order with 
      | productName | quantity | unitPrice |
      | socks       | 12        | 100      |
    Then the order summary should be:
      | totalAmount | discount   | finalAmount       |
      | 1200        | 10*100*0.2 | 1200 - 10*100*0.2 |
   
  Scenario: Buy twenty seven pairs of socks at $100 per pair
    Given Double11 promotion is enabled
    When a customer places an order with
      | productName | quantity | unitPrice |
      | socks       | 27        | 100      |
    Then the order summary should be:
      | totalAmount | discount   | finalAmount                    |
      | 2700        | 10*100*0.2 * 2 | 2700 - 10*100*0.2*2 = 2300 |
  
  Scenario: Buy 10 different products (A, B, C, D, E, F, G, H, I, J)
     Given Double11 promotion is enabled and their prices are as follows
      | productName | unitPrice |
      | A           | 100       |
      | B           | 100       |
      | C           | 100       |
      | D           | 100       |
      | E           | 100       |
      | F           | 100       |
      | G           | 100       |
      | H           | 100       |
      | I           | 100       |
      | J           | 100       |
     When a customer places an order with
      | productName | quantity | 
      | A           | 1        |
      | B           | 1        |
      | C           | 1        |
      | D           | 1        |
      | E           | 1        |
      | F           | 1        |
      | G           | 1        |
      | H           | 1        |
      | I           | 1        |
      | J           | 1        |
     Then the order summary should be:
      | totalAmount | discount   | finalAmount |
      | 1000        | 0          | 1000|

