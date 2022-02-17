Which book has the highest average rating?

<details>
<summary>Show answer</summary>

Homo Deus: A History of Tomorrow,3.2524271844660194

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT b.title, AVG(br.rating) as rating
FROM book_read br,
     book b
WHERE br.book_id = b.id
GROUP BY b.title
ORDER BY rating 
DESC LIMIT 1;
```

</details>

