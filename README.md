# CodeLabs

This project is my own personal version of google codelabs. I got annoyed by their tooling and workflow, so I made my own.

The result is hosted as a github page here: https://troelsmortensen.github.io/CodeLabs/index.html

There is a BasePage.html, which contains the skeleton for how each codelab tutorial looks like.  
I then have the source markdown files for each tutorial in the Tutorials folder. Each step in a tutorial is a separate .md file.  
The CodeLabsGenerator/Generator file generates the final Page.html for a tutorial, based on the md files. 

A new tutorial is created by:
1) Making a new sub-folder in Tutorials.
2) Making .md files in above made folder, one for each step. Naming convention is to start with a number, the steps will be generated in the same order as the .md files are ordered. The actual name of the file is now used as the name in the steps overview on the left.
3) In the CodeLabsGenerator/Program class, you modify the path in the main method, to point to your new tutorials folder, you run the program. A Page.html is generated in that tutorials folder. This is the final tutorial page.
4) update index.html with a link to the new tutorial.

Tutorials folder structure must be strictly as above: A folder inside Tutorials, this is because path to css and js files are relative. creating sub-folders for resources (images, etc) should be okay.

Prismjs is used for styling the code.

Maybe someday it will be updated to Blazor-WASM.

## MD Syntax
Through the generator class, I have made support for a few extra things. That can be found [here](https://troelsmortensen.github.io/CodeLabs/Tutorials/CodelabsDoc/Page.html)


