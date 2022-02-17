Show a list of both author and co-authors for the book with title 'Dark One'.

<details>
<summary>Show answer</summary>

| first_name | last_name |
|------------|-----------|
| Brandon    | Sanderson |
| Collin     | Kelly     |
| Jackson    | Lanzing   |
| Kurt       | Russell   |
| Nathan     | Gooden    |

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
(SELECT first_name, last_name
 FROM author,
      book
 WHERE author.id = book.author_id
   AND book.title = 'Dark One')
UNION
(SELECT first_name, last_name
 FROM author,
      book,
      co_authors
 WHERE book.id = co_authors.book_id
   AND author.id = co_authors.author_id
   AND book.title = 'Dark One');
```

</details>

