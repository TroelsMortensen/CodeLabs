Who are the names of the author of the book which contains 'The Summer Dragon' in its title? 

<details>
<summary>Show answer</summary>

Todd,Lockwood,"The Summer Dragon (The Evertide,  #1)"

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT first_name, last_name, title
FROM book, author
WHERE book.author_id = author.id
  AND title LIKE '%The Summer Dragon%';
```

</details>

