Is there any book, which hasn't been read?

<details>
<summary>Show answer</summary>

Books with the following IDs has not been read:\
29384742\
12111823\
18243345\
21032488\
15998999\
15999003\
10626950\
8074907\
13485378

So, yes.

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT id
FROM book
WHERE id NOT IN
      (SELECT book_id
       FROM book_read
       WHERE status='read');
```

</details>

