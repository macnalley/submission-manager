# Submission Manager
A web app for managing document submissions to a magazine, press, publisher, grant application, etc. It was created to meet the project requirements for the Code Louisville Software Development 2 course.

This program can take document submissions from submitters, and allow them to check the status of their submissions. Administrators can then review submissions, and then accept or reject them.

This program targets .NET 6. The .NET 6 rutime can be obtained from https://dotnet.microsoft.com/en-us/download/dotnet/6.0. The program should be run from the SubmissionManager.WebApp project directory.

## Functionality
### Submitting
From the home page, the index of the Home controller, public users have the ability to send a submission from the "Submit" link. A submission title, author name, email, file upload, and cover letter are required. The file upload accepts documents of .txt, .rtf, .doc, and .docx types. Once sent, the submitter is returned to the home page along with a message displaying the ID number of their submission. 

Submitters who wish to know the status of their submission can click the "Check Status" link. Entering their email and the ID they were assigned, they'll be given a message about the status of their submission.

### Management
Submission readers can access the list of submissions from the [url]/admin controller. By default, the page shows all new submissions. A filter can be used to show all submissions, or only advanced, rejected, or accepted submissions. Clicking the author name redirects to a page showing the details of a submission. Clicking the submission title downloads the submitted document. Submissions can also be quickly advanced or rejected from this page.

Advancing a submission is a feature for organizational purposes, allowing a submission to be moved forward to a future round of evaluations before acceptance.

## Code Louisville Requirements
### Feature List
#### Implement a regular expression (regex) to ensure a field either a phone number or an email address is always stored and displayed in the same format.
Regex is used to validate emails for submissions. The regex is added as a validation data attribute in the Submission.cs file in the SubmissionManager.Data entities. 

In addition, regex is used to parse .rtf files for their word count, as it is used to ignore metadata text in the file format. In the Document.cs file in the SubmissionManager.Data entities, there is a WordCount() method. If the method is called on a .rtf file, the regex is used to match and replace any metadata or formatting text with empty strings, leaving only the content text, so that an accurate word count can be ascertained.

#### Use a LINQ query to retrieve information from a data structure (such as a list or array) or file.
In the HomeController.cs, LINQ queries are used to retrieve a submission when a user queries their submission status. The CheckStatus() method calls a GetByIdAndEmailAsync() method in the Context.cs, which uses a LINQ query to return an entry from the database that matches the provided email and ID.

#### Analyze text and display information about it.
Every uploaded file is analyzed to retrieve its word count via the WordCounty() method in Document.cs in the SubmissionManager.Data entities. .txt and .rtf files are converted into a string, which then uses a regex pattern to count the number of words. .doc and .docx files use Microsoft's DocumentFormat.OpenXml package, which is used for manipulating MS Office files, to retrieve the word count from the file's metadata.

#### Create an additional class which inherits one or more properties from its parent
The SubmissionContext class in context.cs in SubmissionManager.Data inherits from the DbContext class. The HomeController and AdminController classes in SubmissionManager.WebApp both inherit from the Controller class.