# Part 1
Question 1
The Contract Monthly Claim System is developed with a straightforward and open interface that lecturers, program coordinators, and academic managers may easily navigate. The colour scheme will be mostly straightforward, with white backgrounds and light grey panels. Blue buttons will be used for important duties such as filing or approving claims. This keeps the design clean and professional while being simple to implement in a.NET MVC project. The font will be a standard option  because these are typical system fonts that are easy to read. Navigation will be straightforward, with a top bar including connections to each role. Each page will have the same colors, buttons, and font style, making it easier to use and less likely to cause confusion.

Each role’s view:
•	Lecturers can file monthly claims, upload supporting documentation, and check the progress of their claims. The interface is straightforward, with forms for inputting hours worked, uploading documents, and filing claims in a single click.

•	Coordinators can examine claims filed by lecturers, approve or reject them, and provide comments as needed. The interface displays all pending claims in a list manner, complete with approval action buttons.

•	Academic Manager: Managers approve claims after the coordinator has evaluated them. They can also read summaries or reports for all claims. The design features a basic dashboard that displays claims awaiting approval, approved claims, and any comments provided by coordinators

The main tables in our database are Lecturer,Claims,Documents,ProgCoordinator and the Manager. The Lecturer table contains information such as ID, name, email, and hourly rate. The Claim table is linked to the Lecturer and includes details such as hours worked, total amount, claim status, and date submitted. SupportingDocument stores file paths for uploaded documents that have links with specific claims. The ProgrammeCoordinator and AcademicManager tables contain information about users who examine and approve claims. The relationships are basic and easy to understand, making the database useful at this point of the project.

Assumptions:
•	Lecturers submit  their hours and valid documents.
•	Programme Coordinators and Academic Managers review claims and either Approve or Reject
•	All users have the necessary computer literacy and internet access to use the system.
Constraints
•	The system will  be developed using  MVC.
•	Part 1 is a prototype only, so it does not include backend functionality yet.
•	The project must be completed within the semester timeframe, limiting the scope of features.
# Question 2
 <img width="837" height="741" alt="image" src="https://github.com/user-attachments/assets/ae0f63ec-7c94-4580-83a3-ef2744e3741b" />
# Question 3
<img width="584" height="687" alt="image" src="https://github.com/user-attachments/assets/c2313aa4-3fc4-4340-b54e-dd6d15d78ab8" />
<img width="548" height="432" alt="image" src="https://github.com/user-attachments/assets/98a27e13-851f-4a39-9d2b-4bf24ceefd82" />

# Part 3
# YT link
yt: https://youtu.be/m2FyEIpodOM
# Powerpoint
[PROG6212 presentation.pptx](https://github.com/user-attachments/files/23682845/PROG6212.presentation.pptx)

# References
/*
Microsoft. 2025. Working with a database in ASP.NET Core MVC (Version 2.0) [Source code].
Available at: <https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-10.0&tabs=visual-studio>
[Accessed 19 November 2025].

Hòa Nguyễn Coder (SkipperHoa). 2024. Login and Register using ASP.NET MVC 5 (Version 2.0) [Source code].
Available at: <https://dev.to/skipperhoa/login-and-register-using-asp-net-mvc-5-3i0g>
[Accessed 19 November 2025].

 Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core (Version 2.0) [Source code].
 Available at: <https://bencull.com/blog/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core>
 [Accessed 19 November 2025].

Ravitej Herwatta. 2024. A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC (Version 2.0)[Source code].
Available at: <https://medium.com/@ravitejherwatta/a-step-by-step-process-to-set-up-a-database-connection-in-asp-net-core-mvc-a03ac8b7cc04>
[Accessed 19 November 2025].

Bootstrap. 2023. Bootstrap 5 Documentation.[online]
Available at:
<https: //getbootstrap.com/docs/5.3/getting-started/introduction />
[Accessed 16 September 2025].

*/

