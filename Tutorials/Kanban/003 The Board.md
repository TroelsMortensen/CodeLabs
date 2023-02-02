# The Kanban Board

## What is the Board
The board is the center of Kanban, everything revolves around it. The board consists of columns, containing tasks of what to do. 
Depending on the state of the task, it will be located in a specific column. As the state of the task progresses, the task moves to other columns


## A Basic Board Setup

The basic board has three columns. The names may differ, but the intention is the same:

1) Backlog / todo / requested
2) In Progress / doing
3) Done

Below is a figure displaying this setup.

![BasicKanbanSetup.png](BasicKanbanSetup.png)

All your tasks start in the Backlog column. Once you start working on something, you move that task to the In progress column.
When you are done with your task, you move it to the Done column. And then you pick the next task.

I recommend ordering the Backlog by priority, similar to Scrum. The you can (almost) always just pick the top task, when you need to start working on something new.


## Limiting Work in Progress
Kanban recommends limiting work in progres (WIP). This is to prevent having too many tasks In progress. You may get stuck on a task, and just think "I'll just start on something else, until I can complete the other". 
This way you may end up with a lot of started stuff, but not really finishing anything. And Kanban is focussed on getting things done.

The WIP is then a limit of how many tasks can be in the In progress column. Or other columns (besides backlog and done) if you have more columns. 
Some Kanban apps (Trello, Meistertask, etc) may support adding a WIP limit to columns, otherwise you must manually be vigilant about it.

When the WIP is reached, it's a sign to the team to combine their efforts on moving the tasks forward. 

What specific number to pick for WIP? It should probably match your team size, maybe 150% of that. But keep the limit small, this will encourage you to get things done.

The WIP limit lets you identify bottlenecks, i.e. why is your project stuck? If a column is full, the team needs to focus its efforts on the tasks in that column.

## Other Board Setups
Often I find the three basic columns are not enough. E.g. for SEP, the user stories moves through various phases or disciplines:

* analysis
* design
* implementation
* test
* documentation

You may even have a "define tests" before the implementation. So, consider at least having these columns. Each user story will then move through these phases.

You might also include a "next up" column, which will act a bit like a sprint. At certain times (read about meetings), you select a couple of user story tasks from the backlog, and move the to the "next up".
This gives a clearer overview of what to start on next.

The board could look like this:

![LargerBoard.png](LargerBoard.png)