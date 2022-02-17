Show how many pages each reader has read. Limit to top 10.

<details>
<summary>Show answer</summary>

| profile name       | page count |
|--------------------|------------|
| spuffyffet         | 157087     |
| zemmiphobia        | 156852     |
| timberheadsweeping | 155924     |
| MakunaHatata       | 155712     |
| ledoriginally      | 155430     |
| Azlantaph          | 155357     |
| Grabsware          | 154824     |
| LlamadelRey        | 154227     |
| musophobia         | 153972     |
| Norware            | 153552     |

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
GROUP BY p.profile_name
ORDER BY total DESC
    LIMIT 10;
```

</details>

