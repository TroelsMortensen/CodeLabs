Show how many pages each reader has read. Limit to top 10.

<details>
<summary>Show answer</summary>

| profile name     | page count |
|------------------|------------|
| ScoobyCute       | 86554      |
| Grabsware        | 84153      |
| Azlantaph        | 84052      |
| LlamadelRey      | 82854      |
| TrueTips         | 81849      |
| MakunaHatata     | 81549      |
| notmuchtoit      | 81332      |
| DosentAnyoneCare | 81312      |
| Deskinve         | 80705      |
| Booklith         | 80493      |

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT p.profile_name, SUM(b.page_count) total
FROM book_read br,
     book b,
     profile p
WHERE br.book_id = b.id
  AND br.profile_id = p.id
  AND br.status = 'read'
GROUP BY p.profile_name
ORDER BY total DESC
    LIMIT 10;
```

</details>

