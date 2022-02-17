Show a top 10 of highest rated books.

You can use `LIMIT x` to take only the x first rows of a result.

<details>
<summary>Show answer</summary>

Homo Deus: A History of Tomorrow, 3.2524271844660194\
"Traitor's Blade (Greatcoats,  #1)", 3.2355769230769231\
Perfect State, 3.2114285714285714\
"Scourged (The Iron Druid Chronicles,  #9)", 3.2053571428571429\
"Foundryside (The Founders Trilogy,  #1)", 3.2041884816753927\
"Shadow and Bone (The Shadow and Bone Trilogy,  #1)", 3.1941747572815534\
In the Shadow of Lightning (Glass Immortals #1), 3.1933701657458564\
Artemis, 3.1930693069306931\
"The Last War (The Last War,  #1)", 3.191588785046729\
"Summer Knight (The Dresden Files,  #4)", 3.1897435897435897

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT book.title, AVG(rating) as rating
FROM book_read,
     book
WHERE book_read.book_id = book.id
GROUP BY book_id, book.title
ORDER BY rating DESC
LIMIT 10;
```

</details>

