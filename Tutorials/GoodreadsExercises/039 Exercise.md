Show a count of how many books there are in each genre

<details>
<summary>Show answer</summary>

fiction,448\
fantasy,411\
audiobook,298\
science-fiction-fantasy,262\
science-fiction,228\
magic,227\
adventure,216\
adult,173\
high-fantasy,147\
epic-fantasy,139\
urban-fantasy,124\
young-adult,114\
mystery,107\
paranormal,102
...

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT genre, COUNT(title) count
FROM genre, book, book_genre
WHERE genre.id = book_genre.genre_id AND book_genre.book_id=book.id
GROUP BY genre
ORDER BY count DESC;
```

</details>

