# Create `Utils` class

Inside your new project, create a class called `Utils`, it looks like this:

```java
public class Utils
{
    private final double vatGrocery = 0.07;
    private final double vatNormal = 0.19;

    public static double calculateVat(double price, Category category)
    {
        double result;

        if (category == Category.Grocery)
            result = price * vatGrocery;
        else
            result = price * vatNormal;

        return result;
    }

    public static double calculatePriceWithVat(double price, Category category)
    {
        var priceVat = price + calculateVat(price, category);
        return priceVat;
    }
}
```