SELECT 
	U.Id UserId, 
	coalesce(SUM(BA.Amount), 0) as Amount
INTO #MaleStudents
FROM Users U
	LEFT JOIN BankAccount BA ON U.Id = BA.OwnerId
GROUP BY U.Id
-- ************
SELECT U1.[Name], Fun.Amount
FROM
	Users as U1
	LEFT JOIN
		#MaleStudents as Fun ON U1.Id = Fun.UserId
WHERE 1 = 1
GROUP BY U1.[Name],
	Fun.Amount
ORDER BY Fun.Amount
-- ************
DROP TABLE #MaleStudents
