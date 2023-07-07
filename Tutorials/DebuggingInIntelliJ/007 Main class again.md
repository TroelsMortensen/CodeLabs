# The Program
Finally, we need to update the `Main` class, to contain a bit of functionality, we can run.

You need to modify the code to look like this:

```java
public class Main {

    public static void main(String[] args) {
        showPricesWithVat();
    }

    private static void showPricesWithVat() {
        System.out.println("Product prices incl. VAT:");

        for (Product product : products) {
            double vat = Utils.calculateVat(product.getPrice(), product.getCategory());
            double priceWithVat = Math.round(product.getPrice()+ vat);
            System.out.println(product.getName() + ": " + priceWithVat + " EUR");
        }
    }

    private static final List<Product> products = new ArrayList<>(Arrays.asList(
            new Product("Batteries", Category.ELECTRONICS, 2.50),
            new Product("SD Card", Category.ELECTRONICS, 10),
            new Product("T-shirt", Category.ELECTRONICS, 15),
            new Product("Parmesan Cheese", Category.GROCERY, 7.50),
            new Product("Tomatoes", Category.GROCERY, 2))
    );
}
```

Overall, we have three parts/sections in this code.

First, lines 3-5, this is the `main` method, it just calls the `showPricesWithVat` method.\
Then, lines 7-15, this is the method which will display products and their prices, the main functionality of our tiny program.\
It loops through a list of 5 products, which is defined at the bottom of the class, lines 17-23.


### Test run
Your program should compile, and you should be able to run it.\
This should produce the following output to the console:

```
Product prices incl. VAT:
Batteries: 3.0 EUR
SD Card: 12.0 EUR
T-shirt: 18.0 EUR
Parmesan Cheese: 8.0 EUR
Tomatoes: 2.0 EUR
```