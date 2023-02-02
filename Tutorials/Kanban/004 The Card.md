# The Card/Task

What is this? Kanban says little about what the task is. Often, it's just a short piece of text on a sticky note, placed on a physical board on a wall. 
Something, which must be done. A work item. Translated from Japanese, it literally means a visual (kan) card (ban) [[2]](https://kanbanize.com/kanban-resources/getting-started/what-is-kanban-card).

It is usually a good idea if you can clearly mark which member of a group to whom a specific task is assigned. Some Kanban apps supports this.

You can approach your tasks in various ways.

Generally, tasks move from left to right through the columns. Though you might find a problem when implementing, which causes the task to go back to design.\
Or maybe you'll need to create a new task to support your problem.

### User stories
Your tasks can be user stories. Then your Backlog column will initially hold all your user stories, preferably ordered by importance. Similar to the Scrum Product Backlog.

This approach, however, may pose a challenge. To complete a user story you may potentially have to do a lot of things, sub-tasks. 
And it can be difficult to keep track of these sub-tasks, if all you see on the board is the user story.

Some Kanban apps allows you to add more details to a task, e.g. a longer description, links, resources, and a todo-list.

The below is a screenshot from Trello.

![](TrelloChecklist.png)

This approach, by having user stories as the tasks, and adding sub-tasks, could be okay. I do find it lacking in at least one aspect: Each of these subtasks may need to go through analysis, design, implementation, test.
We don't have the option to mark the sub-tasks with any kind of tag in Trello. Maybe other apps allow this.\
But if each sub-task must go through the phases, which column does the user story task belong to? Analysis? Implementation?

Meistertask can generate a new "sub-kanban board" from such checklists. If you create a task, and click on it. You can add items to your checklist. 
And then convert to project. See below

![](MeisterTaskSubBoard.png)

This will create a new kanban board from the checklist. But there does not seem to be a link back to the original task with sub-tasks. So, I'm not convinced this is amazing.

Note, you might need other types of tasks than just user stories. Maybe you need to do some refactoring, or rework the UI or something. Those could be independent tasks, as they may not belong to a specific user story. 

### Sub-tasks
Alternatively, the sub-tasks mentioned above will be our real tasks. You could introduce a new column, just right of the Backlog, called sub-tasks. Whenever a user story is moved from 
the backlog, it is instead replaced with sub-tasks put into the sub-task column. All these subtask can then move across the board to finish the entire user story.\
But, if you are working on more user stories, it may not be clear which sub-tasks belong to which user story.

Some Kanban apps allow tags, labels, or colors. So you might be able to group sub-tasks of a user story together.


### Separate boards
You could create a Kanban board per user story. Though this may decrease your overview, and make it difficult to adhere to the WIP limit. 
This is essentially what Meistertask could do, as I described above.


### Your own style

You can of course experiment and see what works best for you. You could have your Backlog column, and a Sprint column, to imitate Scrum. 
You move _X_ tasks from Backlog to Sprint, and cannot take in anything else from Backlog until everything from Sprint is finished. This does not really align with the spirit of Kanban, though.