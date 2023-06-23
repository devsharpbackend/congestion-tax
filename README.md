# Congestion Tax
The purpose of this project is calculating congestion tax fees for vehicles within a specific city that has its own business rules.
This project is based on DDD  and CQRS architectural patterns.
In this project, efforts have been made to adhere to SOLID principles and to follow separation of concerns  in all layers based on their respective responsibilities.


In the Domain layer, we have segregated the domains using the Aggregate Pattern. Each of them has its own Object Service for managing data and its related entities. Additionally, a separate Repository has been designed for each Aggregate.
Furthermore, all the necessary validations have been implemented in the Domain layer (in addition to the Application layer). The algorithm calculation is performed by a Domain Service called "CongestionTaxCalculatorService." Any validation error in this layer will result in throwing a CongestionTaxDomainException.

In the Infrastructure layer, all database management operations and Repositories have been implemented.

All concerns such as error handling, logging management, transaction management, and validation management are implemented using CQRS Behaviors. It's worth mentioning that logs are managed by the Serilog tool and stored in a table called "LogEvents" in the database.

Since all business rules have been implemented independently in the Domain layer, a project named "CongestionTax.Domain.UnitTests" has been created to test this layer. It tests all the main functionalities in Aggregates and Domain Services.

 In order to maintain clean code, numerous Building Blocks have been created, which have resulted in code cleanliness.

Execution:

To execute, you need to have SQL Server installed and set the password for the "sa" user as "12312". Then run the project, and the migrations will be performed automatically.





