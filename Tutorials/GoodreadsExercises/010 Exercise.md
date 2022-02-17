Who published 'Tricked (The Iron Druid Chronicles,  #4)' 

<details>
<summary>Show answer</summary>

Random House Publishing Group,169

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT publisher_name, id
FROM publisher
WHERE id = (SELECT publisher_id
            FROM book
            WHERE title = 'Tricked (The Iron Druid Chronicles,  #4)');
```

</details>

