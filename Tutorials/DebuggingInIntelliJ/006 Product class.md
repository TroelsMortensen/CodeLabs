# Product Class

Now we need the `Product` class, so we can calculate some prices, and taxes, and stuff.

Create a new class called `Product`, in the same package as the rest.

The code looks like this:

```java
public class Product
{
    private String name;
    private Category category;
    private double price;

    public Product(String name, Category category, double price)
    {
        this.name = name;
        this.category = category;
        this.price = price;
    }

    public String getName() {
        return name;
    }

    public Category getCategory() {
        return category;
    }

    public double getPrice() {
        return price;
    }
}
```