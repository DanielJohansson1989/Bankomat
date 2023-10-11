# Bankomat

This is a project that I created as an assignment from my school. It is a console application written without using any additonal classes.

The application is meant to simulate an ATM or Internet bank. There are five premade user profiles with different amounts of accounts and balances. Each profile is represented by two lists, a list of the type string containing the names for the accounts and a list of the decimal type that contains the balance of each account. To access a profile the user needs to enter the correct username och pin code and by doing so the method Menu() is called using the corresponding to lists and the pin code as arguments. 

The method Menu() contains six functionalities:

- print accounts in the console window

- transfer money between two accounts

- withdraw money 

- deposit money

- open new account

- logg out

Each of them represent a separate method that uses the lists and pin code as arguments except logg out which only assignes the value false to a boolean to exit the menu method.
