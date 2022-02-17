Show all the genres of the book 'Hand of Mars (Starship's Mage,  #2)'.

<details>
<summary>Show answer</summary>

science-fiction\
fantasy\
space\
space-opera\
magic\
war\
military-fiction\
military-science-fiction\
fiction\
science-fiction-fantasy\
adventure


</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT genre
FROM genre g, book_genre bg, book b
WHERE g.id = bg.genre_id AND bg.book_id=b.id
  AND b.title='Hand of Mars (Starship''s Mage,  #2)';
```

</details>

