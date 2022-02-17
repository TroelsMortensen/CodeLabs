What's the poorest rated book?

<details>
<summary>Show answer</summary>

"Storm Front (The Dresden Files,  #1)",2.7272727272727273

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT b.title, AVG(br.rating) as rating
FROM book b,
     book_read br
WHERE b.id = br.book_id
GROUP BY b.title
ORDER BY rating ASC
    LIMIT 1;
```

</details>

