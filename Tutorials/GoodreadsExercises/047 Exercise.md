Which reader has read the most books

<details>
<summary>Show answer</summary>

spuffyffet, 390

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT p.profile_name, COUNT(*) count
FROM book_read br,
    profile p
WHERE br.profile_id = p.id
GROUP BY p.profile_name
ORDER BY count DESC
    LIMIT 1;
```

</details>

