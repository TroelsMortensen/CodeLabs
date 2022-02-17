How many readers have read the book 'Gullstruck Island'?

<details>
<summary>Show answer</summary>

189

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT COUNT(*)
FROM book_read
WHERE status = 'read'
  AND book_id = (
    SELECT id
    FROM book
    WHERE title = 'Gullstruck Island'
);
```

</details>

