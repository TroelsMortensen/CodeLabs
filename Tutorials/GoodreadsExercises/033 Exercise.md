Show a list of each binding type and how many books are using that type.

<details>
<summary>Show answer</summary>

![img_2.png](img_2.png)

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT type, COUNT(*)
FROM binding_type bt, book b
WHERE bt.id=b.binding_id
GROUP BY type;
```

</details>

