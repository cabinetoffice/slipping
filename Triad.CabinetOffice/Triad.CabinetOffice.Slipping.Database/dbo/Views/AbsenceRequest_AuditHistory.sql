CREATE VIEW [dbo].[AbsenceRequest_AuditHistory] AS 
SELECT AR.ID,AR.MPID,AR.FromDate,AR.ToDate,
       Case AR.ReasonID
			When 1 Then 'Government Work (Secretaries of State / Ministers of State only)'
			When 2 Then 'Constituency Engagement'
			When 3 Then 'Parliamentary Campaigning Activity'
			When 5 Then 'Personal/Other'
	   End Reason,AR.Details,AR.[Location],AR.TravelTimeInHours,
	   AR.CreatedDate,U1.ForeNames + ' '+ U1.Surname [CreatedBy],AR.LastChangedDate, U2.ForeNames + ' '+ U2.Surname [LastChangeBy],
	   STRING_AGG(ARMP.MPFullName, ' , ')  AS  [OppositionMPAttending],
	   AR.PAWSAbsenceRequestID
FROM [dbo].[AbsenceRequest] AR
 LEFT JOIN [dbo].[AbsenceRequestOppositionMP] ARMP ON AR.ID=ARMP.AbsenceRequestID
 LEFT JOIN [dbo].[User] U1 on U1.ID=AR.CreatedBy
 LEFT JOIN [dbo].[User] U2 on U2.ID=AR.LastChangedBy
WHERE AR.PAWSAbsenceRequestID IS NOT NULL
Group by AR.ID,AR.MPID,AR.FromDate,AR.ToDate,
       Case AR.ReasonID
			When 1 Then 'Government Work (Secretaries of State / Ministers of State only)'
			When 2 Then 'Constituency Engagement'
			When 3 Then 'Parliamentary Campaigning Activity'
			When 5 Then 'Personal/Other'
	   End,
	   AR.Details,AR.[Location],AR.TravelTimeInHours,
	   AR.CreatedDate,U1.ForeNames + ' '+ U1.Surname,AR.LastChangedDate, U2.ForeNames + ' '+ U2.Surname,
	   AR.PAWSAbsenceRequestID
