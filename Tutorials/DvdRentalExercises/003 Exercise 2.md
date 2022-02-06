# Exercise 2

Create a list of films (title and description) longer than 120 minutes.

<details>
<summary>Show answer</summary>

![img_3.png](img_2.png)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT title, description
FROM film
WHERE lenght > 120;
```

</details>