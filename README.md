# Resto Bakso Satelit

## Spec API

### Products

| Endpoint Path   | HTTP Method | Description                                       | Available |
| --------------- | ----------- | ------------------------------------------------- | --------- |
| `/products`     | GET         | Get all products                                  |           |
| `/products/:id` | GET         | Get one product by id                             |           |
| `/laptops`      | POST        | Add new product                                   |           |
| `/laptops/:id`  | DELETE      | Delete one laptop by id                           |           |
| `/laptops/:id`  | PUT         | Update laptop by id. Create data if doesn't exist |           |

### Cart

| Endpoint          | HTTP     | Description            | Available |
| ----------------- | -------- | ---------------------- | --------- |
| `/cart`           | `GET`    | Get cart one users     |           |
| `/cart/items`     | `POST`   | Post cart item         |           |
| `/cart/items/:id` | `DELETE` | Delete cart item by Id |           |
| `/cart/items/:id` | `PUT`    | Update Cart item by Id |           |
