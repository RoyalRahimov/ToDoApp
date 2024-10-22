Basic ToDo API server app

P.S. Instructions given with the focus on Mac system!

1. Download API server app from Github: open the terminal and navigate to the desired directory (for example: "cd Downloads").
2. Run command: "git clone https://github.com/RoyalRahimov/ToDoApp.git"
3. Navigate to the app folder: "cd ../ToDoApp"
4. First, let's navigate to test folder and run implemented test cases (run: "cd ToDoTasksApi.Tests")
5. Run "dotnet clean" beforehand to clean any of of the cache data.
6. Run "dotnet test".

You should see the successful outputs (see screenshot)
<img width="566" alt="testcases" src="https://github.com/user-attachments/assets/7ebfc964-205f-4cf2-9189-24f2e11e9901">

Now, let's run our localhost:

1. Navigate back to main folder (cd ..)
2. Run "dotnet clean"
3. Run "dotnet build"
4. Finally, run "dotnet run"

You can test in Postman by the following way:

POST:

<img width="1041" alt="Screenshot 2024-10-22 at 11 03 18" src="https://github.com/user-attachments/assets/ba55afc3-1f9b-4af1-871f-d66fe87f5072">

GET:

<img width="967" alt="Screenshot 2024-10-22 at 11 03 53" src="https://github.com/user-attachments/assets/eb08dabd-476c-4f58-b883-ec4b7cf9bdb8">

PUT:

<img width="1127" alt="Screenshot 2024-10-22 at 11 04 45" src="https://github.com/user-attachments/assets/fcfbd2e5-9848-4c09-844c-d54e6761260a">

DELETE:

<img width="978" alt="Screenshot 2024-10-22 at 11 05 11" src="https://github.com/user-attachments/assets/85bcbeb5-b95a-4166-b218-c827d8ff12e3">
