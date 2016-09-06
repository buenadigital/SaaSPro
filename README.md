## Synopsis

SaaSPro is a .NET SaaS starter kit built for the cloud that contains everything you need to jumpstart your next SaaS project.

## Motivation

The purpose of the project was to leverage our experience building eReferralPro (ereferrrralpro.com). eReferralPro is a SaaS site designed for the healthcare industry. We pulled out all the good bits and put them into a solution where SaaS applications can be brought online faster. SaaSPro contains all the plumbing and infrastructure needed to get you going so you can focus on the business logic of your product. I hope SaaSPro will continue to evolve and grow with help from the community. We put a lot of love and effort into this project and we
hope it carries on.

## Projects

There are three web projects in the solution. App, Web, and Management.  The App is the multi-tenant application that is the 'app'. The Web project is the site that shows users what the product is and allows them to signup for the service. Management is the website that manages the application
in terms of adding new users, reports, error logs and many other features.

## Installation

1. Create new application in IIS
2. Add new binding with host name "saaspro.local" and point it the directory \src\SaaSPro.Web.Main
3. Create new application in IIS
4. Add new binding with host name "admin.saaspro.local" and point it the directory \src\SaaSPro.Web.Management
5. Create new application in IIS
6. Add new binding with host name "client1.saaspro.local" and point it the directory \src\SaaSPro.Web.App
7. Create new application in IIS
8. Add new binding with host name "client2.saaspro.local" and point it the directory \src\SaaSPro.Web.App
9. Create new application in IIS
10. Add new binding with host name "client3.saaspro.local" and point it the directory \src\SaaSPro.Web.App
11. In hosts file add the following lines:
   127.0.0.1 saaspro.local
   127.0.0.1 admin.saaspro.local
   127.0.0.1 client1.saaspro.local
   127.0.0.1 client2.saaspro.local
   127.0.0.1 client3.saaspro.local
12. Create a new database named 'SaaSPro'
13. Create new login "SaaSPro" in sql server
14. Run the sql script names database.sql located in the Databases folder
15. Change app settings in the following locations
    Tests
    -----
    SaaSPro.Services.Tests (log4net.config) (app.config)
    SaaSpro.Web.App.Tests (log4net.config) (app.config)
    SaaSPro.Web.Main.Tests (log4net.config) (app.config)
    SaaSPro.Web.Management.Tests (log4net.config) (app.config)

    Web Sites
    ---------
    SaaSPro.Web.App (connectionstrings.config) (log4net.confg)
    SaaSPro.Web.Main (connectionstrings.config) (log4net.confg)
    SaaSPro.Web.Management (connectionstrings.config) (log4net.confg)

16. Start website (it should open "saaspro.local")
17. Create a new account customer from saaspro.local with a host name of 'client1'.
18. Once created, login to the site with the following URL: client1.saaspro.local
19. Login with your credentials and once you get to the second login page, answer the question with the same answer as the question. Example: question: a, answer: a. Etc.
20. The user security question logic can be modified in the Services project, planservice.cs

## Demo

To see SaaSPro running on Azure, visit https://www.saaspro.net/demo

## License

MIT
