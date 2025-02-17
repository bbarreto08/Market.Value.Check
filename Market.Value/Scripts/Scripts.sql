CREATE DATABASE Market;

CREATE TABLE Coin(
	CoinId		INT PRIMARY KEY IDENTITY,
	CoinName	VARCHAR(20) NOT NULL,
	CoinCode	VARCHAR(5) NOT NULL
);

INSERT INTO Coin
VALUES('DOLAR', 'USD');

CREATE TABLE MarketValueHistory(
	MarketValueHistoryId	INT PRIMARY KEY IDENTITY,
	CoinId					INT NOT NULL,
	Value					NUMERIC(13,4) NOT NULL,
	Date					DATE NOT NULL,
	FullDate				DateTime NOT NULL,

	FOREIGN KEY(CoinId) REFERENCES Coin(CoinId)
);

CREATE TABLE MarketValue(
	MarketValueId	INT PRIMARY KEY IDENTITY,
	CoinId			INT NOT NULL,
	LastUpdate		DATETIME NOT NULL,
	Value			NUMERIC(13,4) NOT NULL,

	FOREIGN KEY(CoinId) REFERENCES Coin(CoinId)
);

SELECT TOP 1 *
FROM MarketValueHistory m
WHERE m.CoinId = 1
ORDER BY m.Date DESC, m.FullDate DESC;

INSERT INTO MarketValueHistory
VALUES(2, 6.50, GETDATE(), GETDATE());

