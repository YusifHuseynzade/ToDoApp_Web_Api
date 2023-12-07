# ToDoApp_Web_Api

This project is written using .NET Core 6 and follows the N-Tier architecture.

## Features

- **User Management**
  - User registration (Register)
  - User login (Login) - Using JWT token and refresh token
  - View users
  - Delete users
  - Change user information

- **Authorization**
  - Allowing users to perform authorized operations after login
  - Two main roles based on permissions: Project Manager and Worker

- **Sprint Operations**
  - Add, edit, and delete sprints

- **Task Operations**
  - Add, edit, and delete tasks

- **Roles**
  - There are two main roles for users:
    - Project Manager
    - Worker

- **Project Manager Permissions:**
  - Control all processes of sprints and tasks
  - Add, delete workers to tasks or change the position of these workers from one task to another
  - Change the status of a task
  - Monitor all sprints and tasks associated with them
  - Update a task to change the sprint it belongs to
  - Write comments in the Todo List during the Code Review phase

- **Worker Permissions:**
  - View all tasks
  - Only view their own tasks
  - Change the status of a task
  - Write comments in the Todo List during the Code Review phase only for the assigned task

- Automatically change the status of tasks to "failed" when their expiration date has passed (Using a Cron job)

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
