How many books has the reader with the profile name 'Venom_Fate' read?

<details>
<summary>Show answer</summary>

179

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT COUNT(*)
FROM book_read
WHERE status = 'read'
  AND profile_id = (
    SELECT id
    FROM profile
    WHERE profile_name = 'Venom_Fate'
);
```

</details>

