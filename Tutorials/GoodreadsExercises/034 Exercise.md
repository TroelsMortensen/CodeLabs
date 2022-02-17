For each profile, show how many books they have read.

<details>
<summary>Show answer</summary>

![](img_3.png)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT profile_name, COUNT(*) count
FROM profile, book_read
WHERE book_read.status='read'
  AND profile.id=book_read.profile_id
GROUP BY profile_name
ORDER BY count DESC;
```

</details>

