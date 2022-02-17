What's the binding type of 'Fly by Night'?

<details>
<summary>Show answer</summary>

Hardcover

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT type
FROM binding_type
WHERE id = (SELECT binding_id
            FROM book
            WHERE title = 'Fly by Night');
```

</details>

