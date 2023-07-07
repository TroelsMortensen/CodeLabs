# Create `Utils` class

Inside your new project, create a class called `Utils`, it looks like this:

```java
public class Utils
{
    private static final double vatGrocery = 0.07;
    private static final double vatNormal = 0.19;

    public static double calculateVat(double price, Category category)
    {
        double result;

        if (category == Category.GROCERY)
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

It is not currently important what the code does, we will step through it and investigate, when we start the debugging.

The class depends on a `Category`, and your code does not compile, so we will create that next.