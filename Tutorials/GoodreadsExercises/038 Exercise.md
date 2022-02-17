For the top-ten largest books (page count wise) show their title and binding type.

<details>
<summary>Show answer</summary>

| title                                                                         | page count | binding               |
|-------------------------------------------------------------------------------|------------|-----------------------|
| "Oathbringer (The Stormlight Archive, #3)"                                    | 1248       | Hardcover             |
| "Rhythm of War (The Stormlight Archive, #4)"                                  | 1230       | Hardcover             |
| The Stand                                                                     | 1152       | Hardcover             |
| "A Dance with Dragons (A Song of Ice and Fire, #5)"                           | 1125       | Kindle Edition        |
| "Words of Radiance (The Stormlight Archive, #2)"                              | 1087       | Hardcover             |
| "The Way of Kings (The Stormlight Archive, #1)"                               | 1007       | Hardcover             |
| "The Wise Man's Fear (The Kingkiller Chronicle, #2)"                          | 994        | Hardcover             |
| "A Clash of Kings (A Song of Ice and Fire, #2)"                               | 969        | Paperback             |
| "The Damned Trilogy: A Call to Arms, The False Mirror, and The Spoils of War" | 958        | Kindle Edition        |
| Swan Song                                                                     | 956        | Mass Market Paperback |

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT title, page_count, type
FROM book,
     binding_type
WHERE book.binding_id = binding_type.id
  AND page_count IS NOT NULL
ORDER BY page_count DESC
LIMIT 10;
```

</details>

