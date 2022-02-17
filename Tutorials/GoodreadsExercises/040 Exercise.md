Show a list of publisher names and how many books they each have published

<details>
<summary>Show answer</summary>

Orbit,29\
Tor Books,29\
Del Rey,22\
Faolan's Pen Publishing Inc.,21\
Roc,20\
Gollancz,13\
47North,13\
Broad Reach Publishing,7\
HarperCollinsChildren'sBooks,6\
...

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT publisher.publisher_name, COUNT(*) as count
FROM publisher, book
WHERE publisher.id=book.publisher_id
GROUP BY publisher.publisher_name
ORDER BY count DESC;
```

</details>

