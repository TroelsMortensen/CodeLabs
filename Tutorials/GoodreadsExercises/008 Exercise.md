The previous exercise would show 15 books without a page count first. We don't care about those now.

Create a list of book titles and their page count, order by the book with the highest page count first, but remove books without a page count.

<details>
<summary>Show answer</summary>

N/A

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT title, page_count
FROM book
WHERE page_count IS NOT NULL
ORDER BY page_count DESC;
```

</details>

