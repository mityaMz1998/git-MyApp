Created application has been created that runs from the console.
For each item, it takes a command-line parameter and executes the corresponding item.
The application connects to the database using the ORM Entity Framework.
1. Creating a table with fields representing full name, date of birth, gender.
Application launch example:
MyApp 1
2. Create a record. Use the following format:
MyApp 2 FullName DateOfBirth Gender
3. Output all rows with a unique value of FullName + DateOfBirth, sorted by full name, output full name, Date of birth, gender, number of full years.
Application launch example:
MyApp 3
4. Filling in automatically 1000000 rows. The distribution of gender in them should be relatively uniform, the initial letter of the full name as well. 
Filling in automatically 100 rows in which the gender is male and the full name begins with "F".
Application launch example:
MyApp 4
5. The result of sampling from the table according to the criterion: male gender, full name begins with "F". Make a measurement of the execution time.
Application launch example:
MyApp 5
The output of the application must contain the time.
6. Perform certain manipulations on the database to speed up the query from point 5. Make sure that the execution time has decreased.
