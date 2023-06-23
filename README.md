# Congestion Tax
The purpose of this project is calculating congestion tax fees for vehicles within a specific city that has its own business rules.
This project is based on DDD  and CQRS architectural patterns.
In this project, efforts have been made to adhere to SOLID principles and to follow separation of concerns  in all layers based on their respective responsibilities.


In the Domain layer, we have segregated the domains using the Aggregate Pattern. Each of them has its own Object Service for managing data and its related entities. Additionally, a separate Repository has been designed for each Aggregate.
Furthermore, all the necessary validations have been implemented in the Domain layer (in addition to the Application layer). The algorithm calculation is performed by a Domain Service called "CongestionTaxCalculatorService." Any validation error in this layer will result in throwing a CongestionTaxDomainException.











