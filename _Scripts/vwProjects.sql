SELECT dbo.Project.ID, dbo.Project.ProjectNumber, dbo.Project.ProjectTitle, dbo.Project.Description, dbo.Project.ProjectDocument, dbo.Project.EstimatedDeadLine
      , dbo.Project.ProjectStatusID, dbo.ProjectStatus.StatusDescription
	  , dbo.Project.PriorityID, dbo.Priority.PriorityDescription
	  , dbo.Project.RequestedDeptID, dbo.Department.DepartmentName
	  , dbo.Project.LegalObligationId, dbo.LegalObligation.LegalObligationDescription
FROM  dbo.Project INNER JOIN dbo.ProjectStatus ON dbo.Project.ProjectStatusID = dbo.ProjectStatus.ID 
INNER JOIN dbo.Priority ON dbo.Project.PriorityID = dbo.Priority.ID 
INNER JOIN dbo.Department ON dbo.Project.RequestedDeptId = dbo.Department.ID
INNER JOIN dbo.LegalObligation ON dbo.Project.LegalObligationId = dbo.LegalObligation.ID


