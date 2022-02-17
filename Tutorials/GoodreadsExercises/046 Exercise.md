Is there any book, which hasn't been read?

<details>
<summary>Show answer</summary>

Apparently no.

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT book_id
FROM book_read
WHERE book_read.status = 'read'
  AND book_read.book_id NOT IN
      (SELECT id
       FROM book);
```

</details>

