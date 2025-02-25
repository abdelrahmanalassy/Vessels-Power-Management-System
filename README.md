# Vessels Power Management System with Database Integration

This is a C# console application that manages vessel records and their power calculations, using a **SQL Server database**.

### **Services and User Interaction**
1. Add new vessels
2. Record power calculations for vessels
3. Display all vessels with efficiency calculations
4. Find a Vessel by ID
5. Search for a vessel by name or type
6. Update vessel details
7. Delete Vessel
8. Exit the program

### **Setup the Database**
```sql
CREATE DATABASE VesselDB;

USE VesselDB;

-- Create Vessels table
CREATE TABLE Vessels (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50) NOT NULL
);

-- Create PowerCalculations table
CREATE TABLE PowerCalculations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VesselId INT NOT NULL,
    EnginePower DECIMAL(10,2) NOT NULL,
    FuelConsumption DECIMAL(10,2) NOT NULL,
    Speed DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (VesselId) REFERENCES Vessels(Id)
);
```

### **Running the Application**
Start the application using `dotnet run`

### **Terminal Output**
`User Interaction`
```sql
--- Vessel Power Management System ---
1. Add a new Vessel
2. Add power calculations to a Vessel
3. Display all Vessels with average power efficiency
4. Find a Vessel by ID
5. Search for a Vessel
6. Update vessel details
7. Delete Vessel
8. Exit the program
Enter your choice:
```
`1. Add a new Vessel`
```sql
Enter your choice: 1
Enter Vessel Name: Liberty
Enter Vessel Type: Fishing
Vessel added successfully!
```
`When enter a text instead of num`
```sql
Enter your choice: 1
Enter Vessel Name: Liberty
Enter Vessel Type: Fishing
Error: A Vessel with this name already exists.
```
`2. Add power calculations to a Vessel`
```sql
Enter your choice: 2
Enter Vessel ID: 23
Enter Engine Power(KW): 190
Enter Fuel Consumption (tons/hour): 249.8
Enter speed (Knots): 304.9
Power calculations added successfully!
```
`When enter a text instead of num`
```sql
Enter your choice: 2
Enter Vessel ID: Test text input
Invalid input! Please enter a valid Vessel ID: 
```
`3. Display all Vessels with average power efficiency`
```sql
ID: 11, Name: Sea Explorer, Type: Cargo, Efficiency: 0.38 knots per ton of fuel
ID: 12, Name: Ocean King, Type: Tanker, Efficiency: 0.24 knots per ton of fuel
ID: 14, Name: Deep Blue Num two, Type: Tanker Two, Efficiency: 0.23 knots per ton of fuel
ID: 16, Name: Sea Marine, Type: Fishing 2, Efficiency: 2.31 knots per ton of fuel
ID: 20, Name: Abdelrahman, Type: Testing, Efficiency: 0.23 knots per ton of fuel
ID: 21, Name: Ashraf, Type: Tanker, Efficiency: 0.00 knots per ton of fuel
ID: 22, Name: Sea Blue, Type: Fishing, Efficiency: 0.46 knots per ton of fuel
ID: 23, Name: Liberty, Type: Fishing, Efficiency: 0.19 knots per ton of fuel
```
`4. Find a Vessel by ID`
```sql
Enter your choice: 4
Enter Vessel ID: 23
ID: 23, Name: Liberty, Type: Fishing
```
`When enter a text instead of ID num`
```sql
Enter your choice: 4
Enter Vessel ID: Test text input
Invalid input. Please enter a valid Vessel ID
```
`5. Search for a Vessel`
```sql
Enter your choice: 5
Enter Vessel Name or Type to sesrch: Liberty
ID: 23, Name: Liberty, Type: Fishing
```
`6. Update vessel details`
```sql
Enter your choice: 6
Enter Vessel ID to update: 23
Enter new Vessel Name: Sea Hawk
Enter new Vessel Type: Tanker
Vessel Updated Successfully!
```
`When enter a text instead of ID num`
```sql
Enter your choice: 6
Enter Vessel ID to update: Test text input
Invalid input. Please enter a valid Vessel ID.
```
`7. Delete Vessel`
```sql
Enter your choice: 7
Enter a vessel ID to delete: 20
Vessel deleted successfuly!
```
`8. Exit the program`
```sql
Enter your choice: 8
Exiting program...
```
