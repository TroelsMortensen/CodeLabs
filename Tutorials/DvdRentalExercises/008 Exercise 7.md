# Exercise 7

What is the name of the staff who rented a copy of 'HUNCHBACK IMPOSSIBLE' to customer KURT EMMONS?

<details>
<summary>Show answer</summary>

![img_7.png](img_7.png)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT customer_id, rental.staff_id, first_name, last_name
FROM rental, staff
WHERE rental.staff_id=staff.staff_id
AND inventory_id IN (
    SELECT inventory_id
    FROM inventory
    WHERE film_id = (
        SELECT film_id
        FROM film
        WHERE title = 'HUNCHBACK IMPOSSIBLE'
        )
    );
```

</details>