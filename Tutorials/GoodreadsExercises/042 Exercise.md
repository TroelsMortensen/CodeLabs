How many books have reader 'radiophobia' read in 2018?

You can extract a year from a DATE with `EXTRACT(YEAR FROM book_read.date_finished)`.

<details>
<summary>Show answer</summary>

radiophobia, 23

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT p.profile_name, COUNT(*)
FROM book_read br,
     profile p
WHERE br.profile_id = p.id
  AND EXTRACT(YEAR FROM br.date_finished) = 2018
  AND br.status='read'
  AND p.profile_name = 'radiophobia'
GROUP BY p.profile_name;
```

</details>

