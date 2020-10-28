  
DROP TABLE IF EXISTS Autos;
DROP TABLE IF EXISTS Vermietung;
DROP TABLE IF EXISTS Kunde;

CREATE TABLE Autos (AutoID int NOT NULL PRIMARY KEY, Modell char(50), Marke char(50), Kraftstoffart char(50), Leistung int, Typ char(50), Sitzplaetze int, Tueren int, Tagespreis int);
CREATE TABLE Vermietung (VermietungID int NOT NULL PRIMARY KEY, Anfang date, Ende date, AutoID int, KundenID int);
CREATE TABLE Kunde (KundenID int NOT NULL PRIMARY KEY, Username char(50), Password char(50), Inhaltzusammenhang char(50));

INSERT INTO Autos (AutoID, Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren, Tagespreis) VALUES
(1, 'Vierer', 'Bmw', 'Diesel', 188, 'Coupe', 4, 3, 70),
(2, 'Dreier', 'Bmw', 'Diesel', 256, 'Kombi', 5, 5, 60),
(3, 'LaFerrari', 'Ferrari', 'Benzin', 950, 'Sport', 2, 3, 200),
(4, 'Golf', 'VW', 'Diesel', 150, 'Kompakt', 5, 5, 30),
(5, 'Cherokee', 'Jeep', 'Diesel', 165, 'Gelaende', 5, 5, 40),
(6, 'RS3', 'Audi', 'Benzin', 400, 'Kompakt', 5, 5, 70),
(7, 'UP!', 'VW', 'Benzin', 80, 'Kleinwagen', 4, 3, 15),
(8, 'M4Competition', 'Bmw', 'Benzin', 510, 'Coupe', 4, 3, 150),
(9, 'Golf Gti TCR', 'VW', 'Benzin', 290, 'Kompaktwagen', 5, 3, 60),
(10, 'Passat', 'VW', 'Diesel', 150, 'Kombi', 5, 5, 40),
(11, 'M8Competition', 'Bmw', 'Benzin', 625, 'Coupe', 4, 3, 180),
(12, '918Spyder', 'Porsche', 'Benzin', 867, 'Sport', 2, 3, 210),
(13, 'GT2RS', 'Porsche', 'Benzin', 700, 'Sport', 2, 3, 190);

INSERT INTO Kunde (KundenID, Username, Password, Inhaltzusammenhang) VALUES 
(1, 'Fritzblob', 'her45lo23', 'BmwVierer'),
(2, 'Hildediegart', 'her478956', 'VwGolf'),
(3, 'Herbertsick', 'maur12356', 'FerrariLaFerrari'),
(4, 'Wombatwilli', 'brummdidumm', 'BmwDreier');