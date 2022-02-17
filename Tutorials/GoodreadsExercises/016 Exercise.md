What is the title of the book with the highest page count?

<details>
<summary>Show answer</summary>

Oathbringer (The Stormlight Archive,  #3)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT title
FROM book
WHERE page_count = (
    SELECT MAX(page_count)
    FROM book);
```

</details>

