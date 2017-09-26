﻿CREATE VIEW [dbo].[AbsenceRequest_AuditHistory]
	AS SELECT AR.ID,AR.MPID,AR.FromDate,AR.ToDate,
       Case AR.ReasonID
			When 1 Then 'Government Work (Secretaries of State / Ministers of State only)'
			When 2 Then 'Constituency Engagement'
			When 3 Then 'Parliamentary Campaigning Activity'
			When 5 Then 'Personal/Other'
	   End Reason,AR.Details,AR.[Location],AR.TravelTimeInHours,
	   AR.PAWSAbsenceRequestID,
	   AR.CreatedDate,U1.ForeNames + ' '+ U1.Surname [CreatedBy],AR.LastChangedDate, U2.ForeNames + ' '+ U2.Surname [LastChangeBy],
	   ARMP.MPFullName [OppositionMPAttending]
FROM [dbo].[AbsenceRequest] AS AR
 LEFT JOIN [dbo].[AbsenceRequestOppositionMP] AS ARMP ON AR.ID=ARMP.AbsenceRequestID
 LEFT JOIN [dbo].[User] AS U1 on U1.ID=AR.CreatedBy
 LEFT JOIN [dbo].[User] AS U2 on U2.ID=AR.LastChangedBy