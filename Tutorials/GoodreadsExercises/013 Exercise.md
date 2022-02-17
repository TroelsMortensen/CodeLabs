How many authors have a middle name?

<details>
<summary>Show answer</summary>

47

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT COUNT(*)
FROM author
WHERE middle_name IS NOT NULL;
```

</details>

