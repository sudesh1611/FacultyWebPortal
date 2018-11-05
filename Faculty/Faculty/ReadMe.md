# **Faculty Web Portal**

## **Overview**

This Website is built in ASP.NET Core with .NET Core SDK version 2.1.4 using Razor Pages. It uses Bootstrap as frontend, Sqlite as database and Entity Framework Core for data access. Everything is dynamic including navigation bar names, department and college. Everything displayed is fetched from database.

This is a Web Portal where a faculty can do the following tasks without requiring the knowledge of HTML or CSS :

- Create a course web page containg details about course, course instructors, lecture timings, etc.
- Upload resources for a particular course, for example, lecture slides, notes, etc.
- Post assignments for students. 
- Students can enroll into a course.
- Students can upload solutions for assignments (After geting online approval from faculty).
- Faculty can add/edit details about his/her research publications.
- Faculty can add/edit details about his/her research scholars.
- Edit Professional, Personal Details and Contact Details.

## **Folders and Files**

**Data** : This folder contains DbContext classes required by Entity Framework Core to communicate with database.

**Models** : This folder contains blueprints of all the tables in the form of classes that required by Entity Framework Core DbContexts for accessing database.

**Migrations** : This folder contains migrations for database.

**assests** : This folder contains files for styling pdf files generated using this web app.

**wwwroot** : This folder contains all the static files required like javascript files, bootstrap files.

**Pages** : This folder contains Razor pages used to generate HTML pages at runtime using data from database.

**Program.cs** : This file is the starting point of application. This file loads settings from Startup.cs.

**StartUp.cs** : This file contains settings for configuring this web app.

**libwkhtmltox.dll**, **libwkhtmltox.dylib**, **libwkhtmltox.so** : These are the dlls used to generate pdf files.

## **Setup**

In addition to the general procedure for creating an ASP.NET Core Razor project, following steps are required after coping/replacing all the folders/files of this project in their respective directories:

- Install DinkToPdf NuGet Package either from NuGet Manager or by entering this command in Package Manager Console : `Install-Package DinkToPdf -Version 1.0.8`
- Add database connection string to appsettings.json file
- Add/Apply Migrations corresponding to each DbContext
    - PowerShell Command : `Add-Migration Migration_Name --context DbContext_Name`
    - Console Command : `dotnet ef migrations add Migration_Name --context DbContext_Name`
    - Replace `Migration_Name` with any name and `DbContext_Name` with a particular DbContext class name 
- Update Database after performing above steps for each DbContext
    - PowerShell Command : `Update-Database --context DbContext_Name`
    - Console Command : `dotnet ef database update --context DbContext_Name`
    - Replace `DbContext_Name` with a particular DbContext class name
- When the WebApp runs for the first time go to CreateAccount page first to create Admin Account, without Admin Account WebApp will give error. To go to CreateAccount page click on Create Admin Account Button on Error Page or type the following url to go to CreateAccount Page
    - On Localhost : `http://localhost:port_number/CreateAccount` replace port _number with the previous port number given
    - Online : `http://abc.example.com/CreateAccount` replace abc.example.com with your domain

## **Pages Folder**

This folder contains all the pages required for generating dynamic HTML pages.

### **Shared Folder**

This folder contains all the layout files used by pages depending on context and access level of a user.

### **Pages and their functionalities**

There are two files for a single page:
- First with the extension cshtml is C# HTML file for generating HTML file based on data passed from code behind file.
- Second with the extension cshtml.cs is the code behind file for accesing database and doing various other backend tasks.

#### **Pages Description**

*[Anyone/Admin/Student]* after a page name signifies who can access this page

1. About *[Anyone]* : This page displays details of faculty after fetching data from database.

2. AddNewAssignment *[Admin]* : This page is used to create a new assignment for a course.
3. AddNewCourse *[Admin]* : This page is used to create a new course.
4. AddNewNotice *[Admin]* : This page is used to create a new Notice.
5. AddNewPhdScholar *[Admin]* : This page is used to add new phd scholar to existing ones.
6. AddNewPublication *[Admin]* : This page is used to add new research publication to existing ones.
7. AddNewResource *[Admin]* : This page is used to add a new resource for a particular course.
8. AdminDashboard *[Admin]* : This page contains all the links to perform all the tasks like creating course, viewing assignment submissions, editing profile, etc.
9. AllCourses *[Anyone]* : This page displays all the available courses and their titles in the form of a table.
10. AddNotices *[Anyone]* : This page displays all the available notices in the form of a table.
11. AllPublications *[nyone]* : This page displays all the publications after fetching data from database.
12. Approve *[Admin]* : This page contains only backend and invoked by ViewRegistrationAdmin page to approve a particular student to enroll in a particular course.
13. Assignments *[Anyone]* : This page displays all the assignments of a particular course.
14. ChangePassword *[Admin/Student]* : This page is used to change password of admin account or student accound depending on authentication.
15. Contact *[Anyone]* : This page displays contact details of the faculty after fetching data from database.
16. Course *[Anyone]* : This is a particular course's home page displaying outlines of that course.
17. CourseRegistration *[Student]* : This page is used by student to request for enrollment in a particular course.
18. CourseResources *[Anyone]* : This is a particular course's resource page, where all resources of that course are displayed.
19. **CreateAccount : This is the page used to create one and only admin-cum-faculty account. This can only be called at once. At the time of initial setup of this webApp, this should be the first page that should be called. See Setup section above for more details about this page. Without setting up the account, WebApp will show only Error Page**
20. CreateAccountStudent *[Anyone]* : This page is used to create a student account.
21. Decline *[Admin]* : This page contains only backend and invoked by ViewRegistrationAdmin page to decline a particular student from enrolling in a particular course.
22. DeleteAssignment *[Admin] : This page is used to delete a particular assignment and its submissions.
23. DeleteCourse *[Admin]* : This page is used to delete a particular course and everything related to it.
24. DeleteNotice *[Admin]* : This page is used to delete a particular notice.
25. DeletePhdScholar *[Admin]* : This page is used to delete a particular phd student from the list.
26. DeletePublication *[Admin]* : This page is used to delete a particular publication from the list.
27. DeleteResource *[Admin]* : This page is used to delete a particular resource from a course.
28. EditAssignmentDeadline *[Admin]* : This is used to edit assignment submission deadline of a particular assignment.
29. EditAssignmentsPage *[Admin]* : This shows all the assignments of a particular course. Admin can edit/delete or view submissions of an assignment from here.
30. EditCourse *[Admin]* : Admin can edit information about a particular course from here.
31. EditCourseResourcesPage *[Admin]* : This shows all the resources of course. Admin can add/delete a resource from here.
32. EditCoursesPage *[Admin]* : This shows all the courses present. Admin can add/edit/delete courses from here.
33. EditNoticesPage *[Admin]* : This shows all the notices present. Admin can add/delete notices from here.
34. EditPhdScholar *[Admin]* : This is used to edit a particular phd student details.
35. EditPhdScholarsPage *[Admin]* : This shows all the phd scholars. Admin can add/edit/delete scholars from here.
36. EditProfile *[Admin]* : This page is used to edit all the details of the faculty.
37. EditProfileStudent *[Student]* : This page is used to edit the current year of a student.
38. EditPublication *[Admin]* : This page is used to edit details of a particular publication.
39. EditPublicationsPage *[Admin]* : This page displays all the publications. Admin can add/edit/delete publications from here.
40. Enroll *[Student]* : This is backend page only and is invoked by CourseRegistration page to request for enrollment in a particular course.
41. Error *[Anyone]* : This page is invoked when an error occurs.
42. Exams *[Anyone]* : This page displays examination information related to a particular course.
43. ExportAssignmentSubmissions *[Admin]* : This is backend only page and is used to generate submissions information of a particular assignment in the form of a PDF.
44. ExportRegistrationList *[Admin]* : This is backend only page and is used to generate student registrations information of a particular course in the form of a PDF.
45. Index *[Anyone]* : This is the HomePage of this WebApp.
46. LogOut *[Admin/Student]* : This is backend only page and is used to log out a student/admin.
47. Login *[Student/Admin/Anyone]* : This is used to login into WebApp using either admin credentials or student credentials.
48. NotFound *[Anyone]* : This page is displayed when 404 error occurs i.e. when requested page is not present.
49. ResearchScholars *[Anyone]* : This displays all the PhD Scholars that are working or had worked under the faculty.
50. StudentDashboard *[Student]* : This displays all the links required by student to perform tasks.
51. SubmissionSuccessful *[Student]* : This page is displayed whwn a student successfully submits an assignment's solution.
52. SubmitAssignment *[Student]* : This is backend only page and is invoked by UploadAssignment Page. It saves solution submitted by student from student's file system into server's file system.
53. UploadAssignment *[Student]* : This page provides interface to choose solution of assignment from local file system and passes it to SubmitAssignment Page.
54. UploadAssignmentAttachment *[Admin]* : This is backend only page and is invoked by UploadAssignmentAttachmentPage Page. It saves a particular assignment's attachment submitted by faculty from faculty's file system into server's file system.
55. UploadAssignmentAttachmentPage *[Admin]* : This page provides interface to choose an attachment of assignment from local file system and passes it to UploadAssignmentAttachment Page.
56. UploadPic *[Admin]* : This is backend only page. It is used to save faculty's pic into server's file system.
57. UploadResourceAttachment *[Admin]* : This is backend only page and is invoked by UploadResourceAttachmentPage. It is used to save a particular course's resource's attachment into server's file system.
55. UploadResourceAttachmentPage *[Admin]* : This page provides interface to choose an attachment of a particular course's resource from local file system and passes it to UploadResourceAttachment Page.
56. ViewAssignment *[Anyone]* : This page displays a particular assignment and details related to it, that are assignment description, submission link, attachment and submission deadline.
57. ViewAssignmentAdmin *[Admin]* : This page displays a particular assignment details along with links to view submission also.
58. ViewAssignmentSubmissions *[Admin]* : This page displays all the submissions details of a particular assignment.
59. ViewNotice *[Anyone]* This page displays a particular detailed notice.
60. ViewRegistrationAdmin *[Admin]* : This page displays all the requested/accepted/declined students requests for a particular course. Admin can approve/decline a student's enrollment request from here.
61. ViewRegistrationPage *[Admin]* : This page displays all courses from which admin can choose a particular course and see its enrollment requests.

Contact at [sudesh1611@gmail.com](mailto:sudesh1611@gmail.com)