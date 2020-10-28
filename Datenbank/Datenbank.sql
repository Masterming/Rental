  
DROP TABLE IF EXISTS Autos;
DROP TABLE IF EXISTS Vermietung;
DROP TABLE IF EXISTS Kunde;

CREATE TABLE Autos (AutoID int NOT NULL PRIMARY KEY, Modell char(50), Marke char(50), Kraftstoffart char(50), Leistung int, Typ char(50), Sitzplaetze int, Tueren int, Tagespreis int);
CREATE TABLE Vermietung (VermietungID int NOT NULL PRIMARY KEY, Anfang date, Ende date, AutoID int, KundenID int);
CREATE TABLE Kunde (KundenID int NOT NULL PRIMARY KEY, Username char(50), Password char(50), Inhaltzusammenhang char(50));

INSERT INTO Autos (AutoID, Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren, Tagespreis) VALUES
(0, 'Vierer', 'Bmw', 'Diesel', 188, 'Coupe', 4, 3, 70),
(1, 'Dreier', 'Bmw', 'Diesel', 256, 'Kombi', 5, 5, 60),
(2, 'LaFerrari', 'Ferrari', 'Benzin', 950, 'Sport', 2, 3, 200),
(3, 'Golf', 'VW', 'Diesel', 150, 'Kompakt', 5, 5, 30),
(4, 'Cherokee', 'Jeep', 'Diesel', 165, 'Gelaende', 5, 5, 40),
(5, 'RS3', 'Audi', 'Benzin', 400, 'Kompakt', 5, 5, 70),
(6, 'UP!', 'VW', 'Benzin', 80, 'Kleinwagen', 4, 3, 15),
(7, 'M4Competition', 'Bmw', 'Benzin', 510, 'Coupe', 4, 3, 150),
(8, 'Golf Gti TCR', 'VW', 'Benzin', 290, 'Kompaktwagen', 5, 3, 60),
(9, 'Passat', 'VW', 'Diesel', 150, 'Kombi', 5, 5, 40),
(10, 'M8Competition', 'Bmw', 'Benzin', 625, 'Coupe', 4, 3, 180),
(11, '918Spyder', 'Porsche', 'Benzin', 867, 'Sport', 2, 3, 210);

INSERT INTO Kunde (KundenID, Username, Password, Inhaltzusammenhang) VALUES 
(0, 'Fritzblob', 'her45lo23', 'BmwVierer'),
(1, 'Hildediegart', 'her478956', 'VwGolf'),
(2, 'Herbertsick', 'maur12356', 'FerrariLaFerrari'),
(3, 'Wombatwilli', 'brummdidumm', 'BmwDreier');