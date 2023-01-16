# PV179 Food Delivery

[![Build and Test](https://github.com/petr7555/pv179-food-delivery/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/petr7555/pv179-food-delivery/actions/workflows/build_and_test.yml)
[![Deploy to Kubernetes](https://github.com/petr7555/pv179-food-delivery/actions/workflows/deploy_to_kubernetes.yml/badge.svg)](https://github.com/petr7555/pv179-food-delivery/actions/workflows/deploy_to_kubernetes.yml)

See the app [deployed here](https://food-delivery.dyn.cloud.e-infra.cz/).

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

### Using Docker

- `docker build -t food-delivery .` to build the image
- `docker run -p 8000:8000 food-delivery` to run the image
- visit [http://localhost:8000](http://localhost:8000) to see the app

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
