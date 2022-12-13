# PV179 Food Delivery
[![Build and Test](https://github.com/petr7555/pv179-food-delivery/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/petr7555/pv179-food-delivery/actions/workflows/build_and_test.yml)
[![Build and deploy ASP.Net Core app to an Azure Web App](https://github.com/petr7555/pv179-food-delivery/actions/workflows/deploy.yml/badge.svg)](https://github.com/petr7555/pv179-food-delivery/actions/workflows/deploy.yml)

See the app [deployed here](https://pv179-food-delivery.azurewebsites.net/).

## Team
- 👨‍🎓 485122 Petr Janik (Discord `petr7555#4977`)
- 👨‍🎓 485283 Oliver Svetlik
- 👨‍🎓 484975 Luboslav Halama


## User stories
- As a new customer I can create an account. As a registered customer I can log in to my account.
- As a customer I can edit my personal information (delivery address, billing address).
- As a customer I can can choose a food category.
- As a customer I can search by name of restaurant.
- As a customer I can filter by food price, delivery price, delivery time or restaurant rating.
- As a customer I can view menu of restaurants (food/drinks).
- As a customer I can create order and choose payment method (card, cash, coupon).
- As a customer I can view my order history.
- As a customer I can rate a restaurant.
- As an admin I can create content managers and ban customers.
- As a content manager I can add/delete/edit restaurants, food and drinks.

## How to run
- `dotnet watch --project FoodDelivery.FE` to run web application with hot reload
- `stripe listen --forward-to https://localhost:7127/webhook/StripeWebHook` for the checkout to work
