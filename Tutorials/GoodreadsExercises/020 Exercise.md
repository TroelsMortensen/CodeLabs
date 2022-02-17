How many books have the author Ray Porter co-authored?

<details>
<summary>Show answer</summary>

3

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT COUNT(*)
FROM co_authors
WHERE author_id =
      (SELECT id
       FROM author
       WHERE first_name = 'Ray'
         AND last_name = 'Porter');

```

</details>

