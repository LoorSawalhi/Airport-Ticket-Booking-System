import csv
from faker import Faker
import random
from datetime import datetime
from enum import Enum

class ClassType(Enum):
    ECONOMY = 1
    BUSINESS = 2
    FIRST_CLASS = 3

class Status(Enum):
    BOOKED = 1
    CANCELLED = 2
    COMPLETED = 3
    

# Initialize a Faker generator
fake = Faker()

# Define a list to hold our fake flight and booking data
flights = []
bookings = []
passengers = []
classes = []
flight_classes = []
airports = []

# Generate fake data
num_flights = 100
num_passengers = 500
num_bookings = 300
num_airports = 100

#Generate Airports 
for i in range(num_airports):
    airport = {
        "Id": i + 1,
        "Name": fake.city(),
        "Country": fake.country(),
    }
    airports.append(airport)

# Generating flight data
for i in range(num_flights):
    departure_date = fake.date_between(start_date="+1d", end_date="+1y")
    flight = {
        "Id": i + 1,
        "DepartureDate": departure_date.strftime("%Y-%m-%d"),
        "DepartureAirport": random.choice(airports)["Id"],
        "ArrivalAirport": random.choice(airports)["Id"],
    }
    flights.append(flight)
    
# Generating classes data
for i in range(1,4):
    fclass = {
        "Id": i,
        "Name": ClassType(i),
        "MaxPrice": random.randint(60, 100)*(i + 1),
        "MinPrice": random.randint(10, 50)*(i + 1),
        "MaxSeat": random.randint(10, 50)*i
    }
    classes.append(fclass)
    
# Generating flight classes data
for i in range(num_flights):
    for j in range(1,4):
        fclass = {
            "FlightId": flights[i]['Id'],
            "ClassId": j,
            "Price": random.randint(classes[j-1]["MinPrice"], classes[j-1]["MaxPrice"])
#             "Price": random.randint(classes[j-1]["MinPrice"], classes[j-1]["MaxPrice"])
        }
        flight_classes.append(fclass)

# Generate fake passengers
for i in range(num_passengers):
    passenger = {
        "ID": i + 1,
        "Name": fake.name(),
    }
    passengers.append(passenger)
    
    
# Generating booking data
for i in range(num_bookings):
    flight = random.choice(flights)
    booking = {
        "Id": i + 1,
        "FlightId": random.choice(flights)["Id"],
        "ClassId": random.choice(classes)["Id"],
        "PassengerId": random.choice(passengers)["ID"]
#         "Status": random.choice(list(Status)).name
    }
    bookings.append(booking)

# Save to CSV
def save_to_csv(data, fieldnames, filename):
    with open(filename, 'w', newline='') as csvfile:
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows(data)

# Field names and file names
flight_fields = [
    "Id","DepartureDate", "DepartureAirport", "ArrivalAirport"
]
booking_fields = ["Id",  "FlightId", "ClassId", "PassengerId"]  # Changed "ClassType" to "ClassId"
class_flight_fields = ["FlightId", "ClassId", "Price"]
airport_fields = ["Id", "Name", "Country"]
class_fields = ["Id","Name","MaxPrice","MinPrice","MaxSeat"]


# Writing to csv files
save_to_csv(flights, flight_fields, 'flights.csv')
save_to_csv(passengers, ["ID", "Name"], "passengers.csv")
save_to_csv(bookings,booking_fields, "bookings.csv")
save_to_csv(airports, airport_fields, "airport.csv")
save_to_csv(flight_classes, class_flight_fields, "flight_classes.csv")
save_to_csv(classes, class_fields, "classes.csv")



