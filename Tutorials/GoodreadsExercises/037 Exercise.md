What is the title of the book which is read by most readers.

<details>
<summary>Show answer</summary>

Title: Deception Point\
Id: 976\
Numbers of read: 253


</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT title, book_id, COUNT(*) as count
FROM book_read, book
WHERE book_read.book_id=book.id
  AND book_read.status='read'
GROUP BY title, book_id
ORDER BY count DESC
    LIMIT 1;
```

</details>

