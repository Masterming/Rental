mysql -u root

show databases;
create database Vermietung;
use Vermietung;

create table Marke (MarkenID int AUTO_INCREMENT NOT NULL PRIMARY KEY, Hersteller int);
create table Wagenart (WagenID int AUTO_INCREMENT NOT NULL PRIMARY KEY, Klasse char(50));
create table Kraftstoff (StoffID int AUTO_INCREMENT NOT NULL PRIMARY KEY, Stoff char(50));
create table Modelle (ModellID int AUTO_INCREMENT NOT NULL PRIMARY KEY, Modell char(50));

show tables;

insert into Wagenart (Klasse) VALUES ("Sport"), ("SUV"), ("Gelände"), ("Coupe"), ("Cabrio");
insert into Marke (Hersteller) VALUES ("BMW"), ("Mercedes"), ("Porsche"), ("Jeep"), ("VW"), ("Ferrari"), ("Audi");
insert into Kraftstoff (Stoff) VALUES ("Diesel"), ("Benzin"), ("Elektro");
insert into Modelle (Modell) VALUES ("LaFerrari"), ("Golf"), ("A3Limo"), ("RS3"), ("M8"), ("G-Klasse"), ("918Spyder"), ("Cherokee"), ("Passat"), ("UP!"), ("M4Cabrio"), ("X6");

