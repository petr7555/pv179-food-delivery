# PV179 Food Delivery

[![Build and Test](https://github.com/petr7555/pv179-food-delivery/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/petr7555/pv179-food-delivery/actions/workflows/build_and_test.yml)
[![Build and deploy ASP.Net Core app to an Azure Web App](https://github.com/petr7555/pv179-food-delivery/actions/workflows/deploy.yml/badge.svg)](https://github.com/petr7555/pv179-food-delivery/actions/workflows/deploy.yml)

See the app [deployed here](https://pv179-food-delivery.azurewebsites.net/).

## Team

- 👨‍🎓 485122 Petr Janik (Discord `petr7555#4977`)
- 👨‍🎓 485283 Oliver Svetlik
- 👨‍🎓 484975 Luboslav Halama

## User stories

- [x] As a new customer I can create an account. As a registered customer I can log in to my account.
- [x] As a customer I can edit my personal information (delivery address, billing address).
- [x] As a customer I can can choose a food category.
- [x] As a customer I can search by name of restaurant.
- [x] As a customer I can sort by food price, delivery price or restaurant rating.
- [x] As a customer I can view menu of restaurants (food/drinks).
- [x] As a customer I can create order and choose payment method (card, cash, coupon).
- [x] As a customer I can view my order history.
- [x] As a customer I can rate a restaurant.
- [x] As an admin I can create content managers and ban customers.
- [x] As a content manager I can add/delete/edit restaurants, food and drinks.

## How to run

- `dotnet watch --project FoodDelivery.FE` to run web application with hot reload
- To use Stripe checkout when running the application locally,
  you must run `stripe listen --forward-to https://localhost:7127/webhook/StripeWebHook`.

## Manual Testing

### Users

| Role            | Email                | Password |
|-----------------|----------------------|----------|
| Admin           | admin@example.com    | pass     |
| Content manager | cm@example.com       | pass     |
| Customer        | customer@example.com | pass     |

### Stripe

Use `4242 4242 4242 4242` as a card number for Stripe checkout.

### Coupons

| Code   | Discount        |
|--------|-----------------|
| ABC123 | 200 CZK / 8 EUR |
| DEF456 | 100 CZK / 4 EUR |
