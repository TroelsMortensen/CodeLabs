How many books are written by Brandon Sanderson?

<details>
<summary>Show answer</summary>

41

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT COUNT(*)
FROM book
WHERE author_id =
      (SELECT id
       FROM author
       WHERE first_name = 'Brandon'
         AND last_name = 'Sanderson');
);
```

</details>

