Show a list of how many books reader 'radiophobia' have read each year.

<details>
<summary>Show answer</summary>

radiophobia, 2016, 13\
radiophobia, 2017, 19\
radiophobia, 2018, 23\
radiophobia, 2019, 25\
radiophobia, 2020, 21\
radiophobia, 2021, 29\
radiophobia, 2022, 1

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT p.profile_name, EXTRACT(YEAR FROM br.date_finished) as year, COUNT(*) as count
FROM book_read br,
    profile p
WHERE br.profile_id = p.id
  AND p.profile_name = 'radiophobia'
  AND br.date_finished IS NOT NULL
GROUP BY p.profile_name, year
ORDER BY year;
```

</details>

