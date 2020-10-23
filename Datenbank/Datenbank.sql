DROP TABLE IF EXISTS Autos;
DROP TABLE IF EXISTS Vermietung;
DROP TABLE IF EXISTS Kunde;

CREATE TABLE Autos (AutoID int NOT NULL PRIMARY KEY, Modell char(50), Marke char(50), Kraftstoffart char(50), Leistung int, Typ char(50), Sitzplaetze int, Tueren int, Tagespreis int);
CREATE TABLE Vermietung (VermietungID int NOT NULL PRIMARY KEY, Anfang date, Ende date, AutoID int, KundenID int);
CREATE TABLE Kunde (KundenID int NOT NULL PRIMARY KEY, Username char(50), Password char(50), Inhaltzusammenhang char(50));

INSERT INTO autos (AutoID, Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren, Tagespreis) VALUES
('Vierer', 'Bmw', 'Diesel', 188, 'Coupe', 4, 3, 70),
('Dreier', 'Bmw', 'Diesel', 256, 'Kombi', 5, 5, 60),
('LaFerrari', 'Ferrari', 'Benzin', 950, 'Sport', 2, 3, 200),
('Golf', 'VW', 'Diesel', 150, 'Kompakt', 5, 5, 30),
('Cherokee', 'Jeep', 'Diesel', 165, 'Gelaende', 5, 5, 40);
('RS3', 'Audi', 'Benzin', 400, 'Kompakt', 5, 5, 70);
('UP!', 'VW', 'Benzin', 80, 'Kleinwagen', 4, 3, 15);
('M4Competition', 'Bmw', 'Benzin', 510, 'Coupe', 4, 3, 150);
('Golf Gti TCR', 'VW', 'Benzin', 290, 'Kompaktwagen', 5, 3, 60);
('Passat', 'VW', 'Diesel', 150, 'Kombi', 5, 5, 40);
('M8Competition', 'Bmw', 'Benzin', 625, 'Coupe', 4, 3, 180);
('918Spyder', 'Porsche', 'Benzin', 867, 'Sport', 2, 3, 210);
('GT2RS', 'Porsche', 'Benzin', 700, 'Sport', 2, 3, 190);

INSERT INTO kunde (KundenID, Username, Password, Inhaltzusammenhang) VALUES 
('Fritzblob', 'her45lo23', 'BmwVierer'),
('Hildediegart', 'her478956', 'VwGolf'),
('Herbertsick', 'maur12356', 'FerrariLaFerrari'),
('Wombatwilli', 'brummdidumm', 'BmwDreier');


