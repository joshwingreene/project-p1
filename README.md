# PizzaBox

## Project Description
PizzaBox is an MVC-based .NET application that has two major perspectives: Stores and Customers. With the Store perspective, you are able to see the store's order history and sales. For the Customer perspective, you are able to choose a store, create orders with one or more pizzas, and place your order. Additionally, you are able to edit the pizzas in your order and view your order history.

## Technologies Used

* C#
* MVC
* ASP.NET Core
* EF Core
* Azure
* Microsoft SQL Server
* Microsoft SQL Server Management Studio
* xUnit Testing

## Features

### Ready Features

* Create account and Log in
* Create an order with one or more pizzas
* View a current tally of your order
* Checkout
* Maker another order after checking out
* View User's Order History
* View a Store's Order History
* View a Store's Sales

### To Do List
* Spruce up the content area with bootstrap and css
* Add the rests of my tests for the domain models
* Be able to make another order after showing the customer's order history
* Extend the sales feature so that the total revenue for each pizza type is shown

## Getting Started

1.  Clone the repo with "git clone"
2.  CD into "PizzaBox.WebClient"
3.  Run "dotnet watch run"
4.  Using the given url where the app is listening, click on it to be pushed to your preferred browser Go to your browser and 
5.  Add '/app' to the end of the url and press enter

## Usage

1. Select either Store or Customer

### Store Path

2. Select a listed store
3. Choose between three options: Order History, Sales, and Log Out

### Customer Path

2. Create account with just your chosen username
3. Select the pizza, crust, and size
4. See Pizza Tally
5. Choose one of 3 options: Add Another Pizza, Edit Pizzas, Place Order, (if no pizzas in your order) Cancel Order 
6. After Placing an Order, choose either Make Another Order, Show Order History, Log Out

## License

* MIT
