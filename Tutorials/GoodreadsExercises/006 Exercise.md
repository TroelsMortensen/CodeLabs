Are there two authors with the same first name?

<details>
<summary>Show answer</summary>

John,8\
Stephen,6\
Michael,6\
Douglas,5\
Peter,5\
Richard,5\
Robert,5\
James,4\
David,4\
Brian,4

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT first_name, COUNT(first_name) count
FROM author
GROUP BY first_name
ORDER BY count DESC;
```

</details>

