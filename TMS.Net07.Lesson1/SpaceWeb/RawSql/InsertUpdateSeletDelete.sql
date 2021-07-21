SELECT * FROM Users

INSERT INTO Users ([Name])
VALUES ('Ivan')

DELETE FROM Users
WHERE Id IN (
	SELECT Id
	FROM Users
	WHERE Id NOT IN (
		SELECT min(Id) MinId
		FROM Users
		GROUP BY [Name])
)

UPDATE Users
SET [Password] = '123'
WHERE Id in (1,2)
