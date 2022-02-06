# Exercise 3

What are the addresses of each store?

<details>
<summary>Show answer</summary>

![img_3.png](img_3.png)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT *
FROM address
WHERE address_id IN (
    SELECT address_id
    FROM store
    );
```

</details>