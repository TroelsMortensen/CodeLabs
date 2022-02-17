Show an overview of author id and how many books they have written. Order by highest count at the top.


<details>
<summary>Show answer</summary>

![img_1.png](img_1.png)

and more..

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT author_id, COUNT(*) as count
FROM book
GROUP BY author_id
ORDER BY count DESC;
```

</details>

