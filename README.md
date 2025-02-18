# Vessels Power Management System with Database Integration

This is a C# console application that manages vessel records and their power calculations, using a **SQL Server database**.

### **Services and User Interaction**
1. Add new vessels
2. Record power calculations for vessels
3. Display all vessels with efficiency calculations
4. Search for a vessel by ID
5. Update vessel details
6. Delete Vessel
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
`Add a new Vessel`
```sql
Enter your choice: 1
Enter Vessel Name: 
Sea Marine 4
Enter Vessel Type: 
Passenger 4
Vessel added successfully!
```
`Add power calculations to a Vessel`
```sql
Enter your choice: 2
Enter Vessel ID: 17
Enter Engine Power (KW): 2324
Enter Fuel Consumption (tons/hour): 2324
Enter Speed (Knots): 543
Power calculations added successfuly!
```
`Display all Vessels with average power efficiency`
```sql
Enter your choice: 3
ID: 11, Name: Sea Explorer, Type: Cargo, Efficiency: 29.26 Knots per ton of fuel
ID: 12, Name: Ocean King, Type: Tanker, Efficiency: 6.86 Knots per ton of fuel
ID: 14, Name: Deep Blue, Type: Fishing, Efficiency: 1.04 Knots per ton of fuel
ID: 16, Name: Sea Marine, Type: Fishing 2, Efficiency: 10.05 Knots per ton of fuel
ID: 17, Name: Sea Marine 4, Type: Passenger 4, Efficiency: 0.23 Knots per ton of fuel
```
`Find a Vessel by ID`
```sql
Enter your choice: 4
Enter Vessel ID: 
17
ID: 17, Name: Sea Marine 4, Type: Passenger 4
```
`Search for a Vessel`
```sql
Enter your choice: 5
Enter Vessel Name or Type to sesrch: Deep Blue
ID: 14, Name: Deep Blue, Type: Fishing
```
`Update vessel details`
```sql
Enter your choice: 6
Enter Vessel ID to update: 17
Enter new Vessel Name: Marine 5
Enter new Vessel Type: Type 5
Vessel updated successfully!
```
`Delete Vessel`
```sql
Enter your choice: 7
Enter a vessel ID to delete: 17
Vessel deleted successfuly!
```
`Exit the program`
```sql
Enter your choice: 8
Exiting program...
```
