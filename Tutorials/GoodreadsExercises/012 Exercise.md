How many books do not have an ISBN number?

<details>
<summary>Show answer</summary>

200

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT COUNT(*)
FROM book
WHERE isbn IS NULL;
```

</details>

