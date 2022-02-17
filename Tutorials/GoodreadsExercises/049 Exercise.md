What's the lowest number of days to read 'Oathbringer (The Stormlight Archive,  #3)', and who did that?

Subtracting one `DATE` from another will give the difference in days.

<details>
<summary>Show answer</summary>

tachophobia,4
angelic,4
MinyKissez,4
Pleauxin,4

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT p.profile_name, MIN(date_finished - date_started) as quickest
FROM book_read br,
     profile p
WHERE br.profile_id = p.id
  AND br.status = 'read'
  AND br.book_id = (
    SELECT id
    FROM book
    WHERE title = 'Oathbringer (The Stormlight Archive,  #3)'
)
GROUP BY p.profile_name
ORDER BY quickest ASC;
```

</details>

