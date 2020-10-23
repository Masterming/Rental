DROP TABLE IF EXISTS Autos;
DROP TABLE IF EXISTS Vermietung;
DROP TABLE IF EXISTS Kunde;

CREATE TABLE Autos (AutoID int NOT NULL PRIMARY KEY, Modell char(50), Marke char(50), Kraftstoffart char(50), Leistung int, Typ char(50), Sitzplaetze int, Tueren int);
CREATE TABLE Vermietung (VermietungID int NOT NULL PRIMARY KEY, Anfang date, Ende date, AutoID int, KundenID int);
CREATE TABLE Kunde (KundenID int NOT NULL PRIMARY KEY, Username char(50), Password char(50), Inhaltzusammenhang char(50));

INSERT INTO autos (Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren) VALUES ('Vierer', 'Bmw', 'Diesel', '188', 'Coupe', '4', '3');
INSERT INTO autos (Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren) VALUES ('Dreier', 'Bmw', 'Diesel', '256', 'Kombi', '5', '5');
INSERT INTO autos (Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren) VALUES ('LaFerrari', 'Ferrari', 'Benzin', '950', 'Sport', '2', '3');
INSERT INTO autos (Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren) VALUES ('Golf', 'VW', 'Diesel', '150', 'Kompakt', '5', '5');
INSERT INTO autos (Modell, Marke, Kraftstoffart, Leistung, Typ, Sitzplaetze, Tueren) VALUES ('Cherokee', 'Jeep', 'Diesel', '165', 'Gelaende', '5', '5');

INSERT INTO kunde (Username, Password, Inhaltzusammenhang) VALUES ('Fritzblob', 'her45lo23', 'BmwVierer');
INSERT INTO kunde (Username, Password, Inhaltzusammenhang) VALUES ('Hildediegart', 'her478956', 'VwGolf');
INSERT INTO kunde (Username, Password, Inhaltzusammenhang) VALUES ('Herbertsick', 'maur12356', 'FerrariLaFerrari');
INSERT INTO kunde (Username, Password, Inhaltzusammenhang) VALUES ('Wombatwilli', 'brummdidumm', 'BmwDreier');


