# Creating a schema
In the console, we will start by creating a schema.

Schemas are used similar to packages in Java, it's a way to organize your database.\
Create a database schema by entering `CREATE SCHEMA StarCompany;` in a console window and pressing the 'Execute' button (green arrow), or by Ctrl+Enter.

![img.png](img.png)

Open Schemas in the browser to the left to see the change. A StarCompany schema should appear under the Schemas section. 

![](img1.png)

Execute the command `SET SCHEMA 'starcompany';` by writing the command in the console, selecting it and pressing the execute button. This sets StarCompany as the current schema.

The effect of this is that everything we subsequently execute will be executed against this schema.

### A note about lower casing
Notice how postgresql lower cases all your names, for schemas, tables, attributes/columns. Nothing we can do about that.

### A note about upper casing
Also notice, how some _keywords_ in sql are all uppercase, like `SET SCHEMA` or `CREATE SCHEMA`.\
This is not strictly required, but just an old convention to improve readability that people generally still follow. 
Nowadays your IDE will syntax highlight your SQL, e.g. in the above screenshots SQL keywords are orange. 