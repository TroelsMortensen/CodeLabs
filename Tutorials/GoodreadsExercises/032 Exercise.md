What type of binding does

```
Dead Iron (Age of Steam,  #1)
```

have?

<details>
<summary>Show answer</summary>

Paperback

</details>

<br/>

<details>
<summary>Show SQL</summary>

```sql
SELECT type
from binding_type,
     book
WHERE book.binding_id = binding_type.id
  AND book.title = 'Dead Iron (Age of Steam,  #1)';
```

</details>

