# What is a migration
Your domain model probably evolves over time, and your database should be kept in sync.

A migration is an update to the database, so whenever you change your domain model, you should create a migration. We will do this continuously throughout this tutorial.

Each migration is like a checkpoint in time, describing changes to the database.