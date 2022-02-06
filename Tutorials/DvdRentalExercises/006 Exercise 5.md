# Exercise 5

What are the categories of the film 'ARABIA DOGMA'?

<details>
<summary>Show answer</summary>

![img_5.png](img_5.png)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT name FROM category, film_category
WHERE category.category_id = film_category.category_id
AND film_category.film_id = (
    SELECT film_id
    FROM film
    WHERE title = 'ARABIA DOGMA'
    );
```

</details>