# Resto Bakso Satelit

## Spec API

### Products

| Endpoint Path            | HTTP Method | Description                                                  | Available |
| ------------------------ | ----------- | ------------------------------------------------------------ | --------- |
| `/products`              | GET         | Get all products                                             | ✅        |
| `/products/:productCode` | GET         | Get one product by productCode                               | ✅        |
| `/products`              | POST        | Add new product                                              | ✅        |
| `/products/:productCode` | DELETE      | Delete one product by productCode                            | ✅        |
| `/products/:productCode` | PUT         | Update products by productCode. Create data if doesn't exist | ✅        |
