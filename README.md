# RedBadgeProject

## Travel Logistics Organizer : A planning tool

### By: Jordan

Various industries need a way to keep track of companies, employees, and vehicles, for different kinds of trips.
This application should assist in the organization of these various pieces by allowing users to add, update, and coordinate from an easy to access web front end.

---
Several steps need to be completed in order to run this application yourself.
1. Clone the code
2. Have appropriately credentialed access to a running server
3. Create or access secrets.json on the server csproj file and enter the following code: (replace the asterisks with your relevant connection string)
```
{
    "ConnectionStrings:DefaultConnection" : "*******"
}
```
4. Run the command below to migrate the code first database to the above server:
```
dotnet ef database update -p .\RedBadgeProject\Server\ -s .\RedBadgeProject\Server\
```
5. Some initial data should be seeded into the database as well from the command above.

6. Run the application with the following command:
```
dotnet run --project .\RedBadgeProject\Server\
```
---
This application will primarily be accessed via a blazor front end (you may also interact with api endpoints) but due to the database structure and foreign key dependencies, certain functions should be processed before others.
(Be sure to capture your initial connection url in your running terminal {or launchsSettings.json under properties in the Server folder}.)

1. Register to create an account (you will be propmpted to if any nav bar items are clicked).

2. Locations are simpliest with no have no foreign keys and should be processed first.

3. Companies require Locations and should be processed next.

4. Vehicles require Companies and should be processed next.

5. Staff require Companies and Locations, so ensure they are setup previously.

6. Trips require Locations and Vehicles (staff and Trips may be processed in either order).

7. Within either Staff or Trips, Staff may be assigned to Trips.
   A list of Staff's trips may be accessed from the details page of a specific staff memmber.
   A list of Trip's staff members may be accessed from the details page for a specific trip.

Keep in mind when attempting to delete objects, if there are any dependencies, the object will not delete before having itself removed from the other object.
---
Vist this link to see the database layout and structure: [TravelLogDb - dbdiagram.io](https://dbdiagram.io/d/TravelLogDb-654d14307d8bbd6465de3644)
