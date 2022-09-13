# Complete Todo Web API Endpoint
We move on to the Web API endpoint.

Give it a try yourself first, with this method.

It should be a PATCH, returns `Task<ActionResult>` and receives a TodoUpdateDto.

<details>
<summary>hint</summary>

```csharp
[HttpPatch]
public async Task<ActionResult> UpdateAsync([FromBody] TodoUpdateDto dto)
{
    try
    {
        await todoLogic.UpdateAsync(dto);
        return Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e.Message);
    }
}
```

</details>


Now test that you can update a Todo. Verify the changes by call the GET endpoint or checking the data.json file in the WebAPI project.

Generally a PATCH request will contain only the changes. You should be able to remove some parts of the json sent, and it should still work.

For example, if you just want to complete Todo with ID 1, you would send:

```json
{
  "id": 1,
  "isCompleted": true
}
```

The other properties of the `TodoUpdateDto` object will just be defaulted to `null`, and therefore no changes are made to those properties of the existing Todo.